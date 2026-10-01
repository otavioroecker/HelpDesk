using HelpDesk.Model;


namespace HelpDesk.DTO.GetAllTickets
{
    public record GetAllTicketsResponse(string Title, string Status)
    {
        public static implicit operator GetAllTicketsResponse(Ticket ticket)
            => new(ticket.Title, ticket.Status);
    };
}