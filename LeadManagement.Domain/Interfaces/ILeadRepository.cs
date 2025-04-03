using LeadManagement.Domain.Common;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Enums;

namespace LeadManagement.Domain.Interfaces
{
    public interface ILeadRepository
    {
        Task<PagedResult<Lead>> GetFilteredAsync(string? search, string? status, int pageNumber, int pageSize);
        Task<List<Lead>> GetAllAsync();
        Task<Lead?> GetByIdAsync(Guid id);
        Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status);
        Task AddAsync(Lead lead);
        Task UpdateAsync(Lead lead);
        Task SaveChangesAsync();
    }
}
