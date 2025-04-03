namespace LeadManagement.Application.DTOs
{
    public class LeadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Suburb { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
