using LeadManagement.Application.Commands;
using LeadManagement.Application.Handlers;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Enums;
using LeadManagement.Domain.Interfaces;
using Moq;

namespace LeadManagement.Tests.Application
{
    public class DeclineLeadHandlerTests
    {
        [Fact]
        public async Task Handle_ValidLead_SetsStatusToDeclined()
        {
            // Arrange
            var lead = new Lead("Bruno", "Vieira", "Centro", "Obra", "Desc", 1000m, "1199999", "a@b.com");
            var repoMock = new Mock<ILeadRepository>();

            repoMock.Setup(r => r.GetByIdAsync(lead.Id)).ReturnsAsync(lead);
            repoMock.Setup(r => r.UpdateAsync(It.IsAny<Lead>()));
            repoMock.Setup(r => r.SaveChangesAsync());

            var handler = new DeclineLeadHandler(repoMock.Object);
            var command = new DeclineLeadCommand(lead.Id);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal(LeadStatus.Declined.ToString(), result.Status);
        }
    }
}
