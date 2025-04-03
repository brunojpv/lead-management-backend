using LeadManagement.Application.Commands;
using LeadManagement.Application.Handlers;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Interfaces;
using Moq;

namespace LeadManagement.Tests.Application
{
    public class CreateLeadHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_CreatesLeadAndReturns()
        {
            // Arrange
            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Lead>()));
            repoMock.Setup(r => r.SaveChangesAsync());

            var handler = new CreateLeadHandler(repoMock.Object);

            var command = new CreateLeadCommand
            {
                FirstName = "Bruno",
                LastName = "Vieira",
                Suburb = "Centro",
                Category = "Obra",
                Description = "Desc",
                Price = 1500m,
                PhoneNumber = "1199999",
                Email = "a@b.com"
            };

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal("Bruno", result.FirstName);
            Assert.Equal("Invited", result.Status);
        }
    }
}
