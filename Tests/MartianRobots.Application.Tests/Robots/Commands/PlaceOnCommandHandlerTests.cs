using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using MartianRobots.Application.Robots.Commands;
using MartianRobots.Application.Tests.DataSources;
using Xunit;

namespace MartianRobots.Application.Tests.Robots.Commands
{
    public class PlaceOnCommandHandlerTests
    {       
        [Theory, RobotNotRetrievedDataSource]
        public async Task Handle_GivenUnableToRetrieveTheRobotById_ReturnsErrorMessage(IFixture fixture, PlaceOnCommand request)
        {
            // Arrange
            var sut = fixture.Create<PlaceOnCommandHandler>();

            // Act
            var result = await sut.Handle(request, CancellationToken.None);

            // Assert
            result.Error.Should().Be($"Robot with id: {request.RobotId} not found");
        }
    }
}
