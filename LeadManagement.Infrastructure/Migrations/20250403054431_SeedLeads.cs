using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeadManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedLeads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "PhoneNumber", "Suburb", "Category", "Description", "Price", "Status", "CreatedAt" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Bruno", "Vieira", "bruno@mail.com", "11999999999", "São Paulo", "Obra", "Reforma completa", 1500m, "Invited", DateTime.UtcNow },
                    { Guid.NewGuid(), "Ana", "Silva", "ana@mail.com", "21988887777", "Rio", "Elétrica", "Instalação de rede", 800m, "Accepted", DateTime.UtcNow },
                    { Guid.NewGuid(), "Carlos", "Lima", "carlos@mail.com", "31977776666", "BH", "Drywall", "Montagem de parede", 1200m, "Declined", DateTime.UtcNow },
                    { Guid.NewGuid(), "Juliana", "Souza", "ju@mail.com", "41966665555", "Curitiba", "Pintura", "Pintura interna", 700m, "Invited", DateTime.UtcNow }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove todos com base no e-mail, por exemplo
            migrationBuilder.DeleteData(
                table: "Leads",
                keyColumn: "Email",
                keyValues: new object[]
                {
                    "bruno@mail.com", "ana@mail.com", "carlos@mail.com", "ju@mail.com"
                });
        }
    }
}
