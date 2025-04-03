using LeadManagement.Domain.Entities;
using MediatR;

namespace LeadManagement.Application.Commands
{
    public record AcceptLeadCommand(Guid LeadId) : IRequest<Lead>;
}
