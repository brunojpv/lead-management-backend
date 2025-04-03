using LeadManagement.Application.Commands;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Interfaces;
using MediatR;

namespace LeadManagement.Application.Handlers
{
    public class DeclineLeadHandler : IRequestHandler<DeclineLeadCommand, Lead>
    {
        private readonly ILeadRepository _repository;

        public DeclineLeadHandler(ILeadRepository repository)
        {
            _repository = repository;
        }

        public async Task<Lead> Handle(DeclineLeadCommand request, CancellationToken cancellationToken)
        {
            var lead = await _repository.GetByIdAsync(request.LeadId);

            if (lead is null)
                throw new KeyNotFoundException($"Lead com ID {request.LeadId} não encontrado.");

            lead.Decline();

            await _repository.UpdateAsync(lead);
            await _repository.SaveChangesAsync();

            return lead;
        }
    }
}
