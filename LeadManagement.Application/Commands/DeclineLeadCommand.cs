using LeadManagement.Domain.Entities;
using MediatR;

namespace LeadManagement.Application.Commands
{
    public record DeclineLeadCommand(Guid LeadId) : IRequest<Lead>;
}
