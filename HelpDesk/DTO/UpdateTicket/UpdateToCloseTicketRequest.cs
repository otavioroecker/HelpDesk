using HelpDesk.Model;

namespace HelpDesk.DTO.UpdateTicket
{
    public record UpdateToCloseTicketRequest(string Resolution)
    {
        public static implicit operator UpdateToCloseTicketRequest(Ticket ticket)
            => new(ticket.Resolution);
    }
}
