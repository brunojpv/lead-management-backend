using LeadManagement.Api.Controllers;
using LeadManagement.Application.Commands;
using LeadManagement.Domain.Entities;
using LeadManagement.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LeadManagement.Tests.Api
{
    public class LeadsControllerIntegrationTests
    {
        [Fact]
        public async Task Create_Then_Accept_Then_Decline_Lead()
        {
            // Arrange
            var repoMock = new Mock<ILeadRepository>();
            var mediatorMock = new Mock<IMediator>();
            var mapperMock = new Mock<AutoMapper.IMapper>();

            var lead = new Lead("Bruno", "Vieira", "Centro", "Obra", "Desc", 1500, "1199999", "a@b.com");

            mediatorMock.Setup(m => m.Send(It.IsAny<CreateLeadCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(lead);
            mediatorMock.Setup(m => m.Send(It.IsAny<AcceptLeadCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(() =>
                        {
                            lead.Accept();
                            return lead;
                        });
            mediatorMock.Setup(m => m.Send(It.IsAny<DeclineLeadCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(() =>
                        {
                            lead.Decline();
                            return lead;
                        });

            mapperMock.Setup(m => m.Map<It.IsAnyType>(It.IsAny<object>()))
                      .Returns((object source) => source);

            var controller = new LeadsController(repoMock.Object, mediatorMock.Object, mapperMock.Object);

            // ACT - Create
            var createResult = await controller.Create(new CreateLeadCommand
            {
                FirstName = "Bruno",
                LastName = "Vieira",
                Suburb = "Centro",
                Category = "Obra",
                Description = "Desc",
                Price = 1500m,
                PhoneNumber = "1199999",
                Email = "a@b.com"
            });
            var created = Assert.IsType<OkObjectResult>(createResult).Value as Lead;

            // ACT - Accept
            var acceptResult = await controller.Accept(created!.Id);
            var accepted = Assert.IsType<OkObjectResult>(acceptResult).Value as Lead;
            Assert.Equal("Accepted", accepted!.Status);
            Assert.Equal(1350m, accepted.Price); // 10% desconto

            // ACT - Decline (simulação posterior — status muda)
            var declineResult = await controller.Decline(created.Id);
            var declined = Assert.IsType<OkObjectResult>(declineResult).Value as Lead;
            Assert.Equal("Declined", declined!.Status);
        }
    }
}
