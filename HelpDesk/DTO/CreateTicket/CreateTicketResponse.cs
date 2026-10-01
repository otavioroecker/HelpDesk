using HelpDesk.Model;

namespace HelpDesk.DTO.CreateTicket
{
    public record CreateTicketResponse(int Id, string Title, string Description, string Status)
    {
        public static implicit operator CreateTicketResponse(Ticket ticket)
            => new(ticket.Id, ticket.Title, ticket.Description, ticket.Status);
    }
}
