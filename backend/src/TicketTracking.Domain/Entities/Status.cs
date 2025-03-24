namespace TicketTracking.Domain.Entities
{
    public class Status : IReferenceData
    {
        public int Id { get; set; }
        public string? Title { get; set; }
    }
}