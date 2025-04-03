using LeadManagement.Domain.Enums;

namespace LeadManagement.Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Suburb { get; private set; } = string.Empty;
        public string Category { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Status { get; private set; } = LeadStatus.Invited.ToString();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Construtor usado pelo EF
        private Lead() { }

        // Construtor usado pelo domínio
        public Lead(string firstName, string lastName, string suburb, string category, string description, decimal price, string phone, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Suburb = suburb;
            Category = category;
            Description = description;
            Price = price;
            PhoneNumber = phone;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            Status = LeadStatus.Invited.ToString();
        }

        public void Accept()
        {
            if (Price > 500)
                Price *= 0.9m;

            Status = LeadStatus.Accepted.ToString();
        }

        public void Decline()
        {
            Status = LeadStatus.Declined.ToString();
        }
    }
}
