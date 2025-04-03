using LeadManagement.Application.Commands;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Interfaces;
using MediatR;

namespace LeadManagement.Application.Handlers
{
    public class CreateLeadHandler : IRequestHandler<CreateLeadCommand, Lead>
    {
        private readonly ILeadRepository _repository;

        public CreateLeadHandler(ILeadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Lead> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = new Lead(
                request.FirstName,
                request.LastName,
                request.Suburb,
                request.Category,
                request.Description,
                request.Price,
                request.PhoneNumber,
                request.Email
            );

            await _repository.AddAsync(lead);
            await _repository.SaveChangesAsync();

            return lead;
        }
    }
}
