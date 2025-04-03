using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LeadManagement.Infrastructure.Data
{
    public class LeadDbContextFactory : IDesignTimeDbContextFactory<LeadDbContext>
    {
        public LeadDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeadDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=LeadDB;User Id=sa;Password=Bjpv@1982;TrustServerCertificate=True;");

            return new LeadDbContext(optionsBuilder.Options);
        }
    }
}
