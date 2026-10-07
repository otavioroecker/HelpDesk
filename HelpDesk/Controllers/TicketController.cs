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

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateTicket(int id, UpdateToCloseTicketRequest request)
        {

            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound();


            if (string.IsNullOrWhiteSpace(request.Resolution))
            {
                return BadRequest("Precisa ser adicionado uma resolução para o chamado ser encerrado");
            }
            else
            {
                ticket.Resolution = request.Resolution;
                ticket.Status = "Fechado";
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpDelete("{id}")] // verificar se nao tem uma maneira de só utilizar true ou false para detectar o ticket, pq como ta ele mostra todo o ticket.
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

// Notes
// ctrl + m | solta | ctrl + o | isso encolhe o codigo