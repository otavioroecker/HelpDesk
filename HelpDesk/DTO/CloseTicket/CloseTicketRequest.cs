using HelpDesk.Model;

namespace HelpDesk.DTO.UpdateTicket
{
    public record CloseTicketRequest(string Resolution)
    {
        public static implicit operator CloseTicketRequest(Ticket ticket)
            => new(ticket.Resolution);
    }
}
