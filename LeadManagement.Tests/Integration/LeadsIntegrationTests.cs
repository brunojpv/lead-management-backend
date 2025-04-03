using FluentAssertions;
using LeadManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace LeadManagement.Tests.Integration
{
    public class LeadsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public LeadsIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<Infrastructure.Data.LeadDbContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    services.AddDbContext<Infrastructure.Data.LeadDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            });

            _client = _factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "fake-jwt-token");
        }

        [Fact]
        public async Task Full_Lead_Creation_Acceptance_And_Decline_Flow()
        {
            var requestBody = new
            {
                firstName = "Bruno",
                lastName = "Vieira",
                suburb = "Centro",
                category = "Obra",
                description = "Reforma completa",
                price = 700,
                phone = "11999999999",
                email = "bruno@mail.com"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Criar
            var responseCreate = await _client.PostAsync("/api/leads", content);
            responseCreate.EnsureSuccessStatusCode();
            var createdLead = await responseCreate.Content.ReadFromJsonAsync<Lead>();

            // Aceitar
            var responseAccept = await _client.PostAsync($"/api/leads/accept/{createdLead!.Id}", null);
            responseAccept.EnsureSuccessStatusCode();
            var acceptedLead = await responseAccept.Content.ReadFromJsonAsync<Lead>();
            acceptedLead!.Status.Should().Be(Domain.Enums.LeadStatus.Accepted.ToString());
            acceptedLead.Price.Should().Be(630); // 10% de desconto

            // Recusar
            var responseDecline = await _client.PostAsync($"/api/leads/decline/{createdLead.Id}", null);
            responseDecline.EnsureSuccessStatusCode();
            var declinedLead = await responseDecline.Content.ReadFromJsonAsync<Lead>();
            declinedLead!.Status.Should().Be(Domain.Enums.LeadStatus.Declined.ToString());
        }
    }
}
