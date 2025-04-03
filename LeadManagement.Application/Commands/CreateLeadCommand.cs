using LeadManagement.Domain.Entities;
using MediatR;

namespace LeadManagement.Application.Commands
{
    public class CreateLeadCommand : IRequest<Lead>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Suburb { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string PhoneNumber { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
