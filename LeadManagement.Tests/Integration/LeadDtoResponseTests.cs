using FluentAssertions;
using LeadManagement.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace LeadManagement.Tests.Integration
{
    public class LeadDtoResponseTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LeadDtoResponseTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Then_GetById_Should_Return_LeadDto()
        {
            // Arrange
            var createCommand = new
            {
                firstName = "Bruno",
                lastName = "Vieira",
                suburb = "Centro",
                category = "Obra",
                description = "Construção nova",
                price = 800,
                phone = "11999999999",
                email = "bruno@mail.com"
            };

            var responseCreate = await _client.PostAsJsonAsync("/api/leads", createCommand);
            responseCreate.EnsureSuccessStatusCode();

            var leadDto = await responseCreate.Content.ReadFromJsonAsync<LeadDto>();
            leadDto.Should().NotBeNull();
            leadDto!.Id.Should().NotBeEmpty();
            leadDto.FirstName.Should().Be("Bruno");
            leadDto.Status.Should().Be("Invited");

            // Act
            var responseGet = await _client.GetAsync($"/api/leads/{leadDto.Id}");
            responseGet.EnsureSuccessStatusCode();

            var resultDto = await responseGet.Content.ReadFromJsonAsync<LeadDto>();

            // Assert
            resultDto.Should().NotBeNull();
            resultDto!.Id.Should().Be(leadDto.Id);
            resultDto.Email.Should().Be("bruno@mail.com");
        }
    }
}
