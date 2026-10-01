using HelpDesk.Model;

namespace HelpDesk.DTO.GetTicketById
{
    public record GetTicketByIdResponse(string Title, string Description, string Status, string? Resolution)
    {
        public static implicit operator GetTicketByIdResponse(Ticket ticket)
            => new(ticket.Title, ticket.Description, ticket.Status, ticket.Resolution);
    };

}