using HelpDesk.Model;

namespace HelpDesk.DTO.CreateTicket
{
    public record CreateTicketRequest(string Title, string Description)
    {
        public static implicit operator CreateTicketRequest(Ticket ticket)
            => new(ticket.Title, ticket.Description);

        public static implicit operator Ticket(CreateTicketRequest ticket)
            => new()
            {
                Title = ticket.Title,
                Description = ticket.Description
            };
    };
}
