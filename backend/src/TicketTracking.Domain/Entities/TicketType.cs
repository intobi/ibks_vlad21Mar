namespace TicketTracking.Domain.Entities
{
    public class TicketType : IReferenceData
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}