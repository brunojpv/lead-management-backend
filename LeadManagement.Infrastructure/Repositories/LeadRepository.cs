using LeadManagement.Domain.Common;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Enums;
using LeadManagement.Domain.Interfaces;
using LeadManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadManagement.Infrastructure.Repositories
{
    public class LeadRepository : ILeadRepository
    {
        private readonly LeadDbContext _context;

        public LeadRepository(LeadDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lead>> GetAllAsync()
        {
            return await _context.Leads
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<PagedResult<Lead>> GetFilteredAsync(string? search, string? status, int pageNumber, int pageSize)
        {
            var query = _context.Leads.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(l => l.FirstName.Contains(search) || l.LastName.Contains(search));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(l => l.Status == status);

            var totalItems = await query.CountAsync();

            var data = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Lead>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                Data = data
            };
        }

        public async Task AddAsync(Lead lead) => await _context.Leads.AddAsync(lead);

        public async Task<Lead?> GetByIdAsync(Guid id) =>
            await _context.Leads.FindAsync(id);

        public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status) =>
            await _context.Leads
                .Where(l => l.Status == status.ToString())
                .ToListAsync();


        public async Task UpdateAsync(Lead lead)
        {
            _context.Leads.Update(lead);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
