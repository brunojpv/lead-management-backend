using LeadManagement.Application.Commands;
using LeadManagement.Application.Handlers;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Enums;
using LeadManagement.Domain.Interfaces;
using Moq;

namespace LeadManagement.Tests.Application
{
    public class AcceptLeadHandlerTests
    {
        [Fact]
        public async Task Handle_ValidLead_AppliesDiscountAndReturnsLead()
        {
            // Arrange
            var lead = new Lead(
                firstName: "Bruno",
                lastName: "Vieira",
                suburb: "Centro",
                category: "Obra",
                description: "Descrição",
                price: 1000m,
                phone: "11999999999",
                email: "bruno@email.com"
            );

            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(lead.Id)).ReturnsAsync(lead);
            repoMock.Setup(r => r.UpdateAsync(It.IsAny<Lead>()));
            repoMock.Setup(r => r.SaveChangesAsync());

            var handler = new AcceptLeadHandler(repoMock.Object);
            var command = new AcceptLeadCommand(lead.Id);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.Equal(LeadStatus.Accepted.ToString(), result.Status);
            Assert.Equal(900m, result.Price); // 10% de desconto
        }

        [Fact]
        public async Task Handle_InvalidLead_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repoMock = new Mock<ILeadRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Lead?)null);

            var handler = new AcceptLeadHandler(repoMock.Object);
            var command = new AcceptLeadCommand(Guid.NewGuid());

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                handler.Handle(command, default));
        }
    }
}
