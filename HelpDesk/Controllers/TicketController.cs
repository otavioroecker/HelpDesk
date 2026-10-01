using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Data;
using HelpDesk.Model;
using HelpDesk.DTO.CreateTicket;
using HelpDesk.DTO.GetAllTickets;
using HelpDesk.DTO.GetTicketById;
using HelpDesk.DTO.UpdateTicket;

namespace HelpDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAllTicketsResponse>>> GetAllTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();

            var responseTickets = new List<GetAllTicketsResponse>();

            foreach (var ticket in tickets)
            {
                responseTickets.Add((GetAllTicketsResponse)ticket);
            }

            return Ok(responseTickets);
        }

        [HttpGet("AllInfos")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTicketsAdm()
        {
            var tickets = await _context.Tickets.ToListAsync();

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTicketByIdResponse>> GetTicketById(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                return Ok((GetTicketByIdResponse)ticket);
            }
        }

        [HttpPost] 
        public async Task<ActionResult<Ticket>> CreateTicket(CreateTicketRequest ticket)
        {
            var newTicket = _context.Tickets.Add(ticket).Entity;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, (CreateTicketResponse)newTicket);
        }

        [HttpPut("{id}")] // tem que testar ainda
        public async Task<IActionResult> UpdateTicket(int id, UpdateTicketRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var ticket = await _context.Tickets.AsNoTracking().Where(t => t.Id == id).FirstOrDefaultAsync(cancellationToken);
                if (ticket is null)
                {
                    return NotFound();
                }

                Ticket newTicket = request;
                newTicket.Id = id;
                newTicket.Status = ticket.Status;
                newTicket.Resolution = ticket.Resolution;

                _context.Tickets.Entry(newTicket).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}