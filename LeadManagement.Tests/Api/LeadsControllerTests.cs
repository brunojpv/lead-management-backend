using AutoMapper;
using LeadManagement.Api.Controllers;
using LeadManagement.Application.Commands;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Enums;
using LeadManagement.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeadManagement.Tests.Api
{
    public class LeadsControllerTests
    {
        [Fact]
        public async Task Accept_Returns_Ok_With_Lead()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var repoMock = new Mock<ILeadRepository>();
            var mapperMock = new Mock<IMapper>(); // 👈 Novo mock

            var leadId = Guid.NewGuid();

            var expectedLead = new Lead(
                firstName: "Bruno",
                lastName: "Vieira",
                suburb: "Centro",
                category: "Obra",
                description: "Descrição",
                price: 550,
                phone: "11999999999",
                email: "teste@mail.com"
            );

            expectedLead.Accept();

            mediatorMock
                .Setup(m => m.Send(It.Is<AcceptLeadCommand>(cmd => cmd.LeadId == leadId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedLead);

            // Mock do AutoMapper (se a action retorna um DTO, mapeamos aqui)
            mapperMock
                .Setup(m => m.Map<It.IsAnyType>(It.IsAny<object>()))
                .Returns((object source) => source); // retorna o próprio Lead se não estiver usando DTO

            var controller = new LeadsController(repoMock.Object, mediatorMock.Object, mapperMock.Object);

            // Act
            var result = await controller.Accept(leadId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedLead = Assert.IsType<Lead>(okResult.Value);
            Assert.Equal("Bruno", returnedLead.FirstName);
            Assert.Equal(LeadStatus.Accepted.ToString(), returnedLead.Status);
        }
    }
}
