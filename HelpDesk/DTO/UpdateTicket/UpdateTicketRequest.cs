using HelpDesk.Model;

namespace HelpDesk.DTO.UpdateTicket
{
    public record UpdateTicketRequest(string Title, string Description)
    {
        public static implicit operator UpdateTicketRequest(Ticket ticket)
            => new(ticket.Title, ticket.Description);

        public static implicit operator Ticket(UpdateTicketRequest ticket)
            => new()
            {
                Title = ticket.Title,
                Description = ticket.Description
            };
    }
}
