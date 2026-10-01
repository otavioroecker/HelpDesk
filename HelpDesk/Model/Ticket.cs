using HelpDesk.DTO.CreateTicket;
using System.Diagnostics.CodeAnalysis;

namespace HelpDesk.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string Status { get; set; } = "Aberto";
        public string? Resolution { get; set; }
    }
}
