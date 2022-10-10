using AutoFixture;
using FluentAssertions;
using MartianRobots.Domain.MapsAggregate;
using MartianRobots.Domain.RobotsAggregate;
using Xunit;

namespace MartianRobots.Domain.Tests.Robots
{
    public class RobotTests
    {
        private readonly IFixture fixture = new Fixture();

        [Fact]
        public void PlaceOn_GivenInitialCoordinates_RobotIsPlacedOnMap()
        {
            // Arrange
            var sut = this.fixture.Create<Robot>();

            var xPos = this.fixture.Create<int>();
            var yPos = this.fixture.Create<int>();

            // Act
            sut.PlaceOn(xPos, yPos, Orientation.N);

            // Assert
            sut.Coordinates?.XPos.Should().Be(xPos);
            sut.Coordinates?.YPos.Should().Be(yPos);
        }

        [Fact]
        public void PlaceOn_GivenRobotIsPlacedOnMap_RobotModeIsSetToInOperation()
        {
            // Arrange
            var sut = this.fixture.Create<Robot>();

            var xPos = this.fixture.Create<int>();
            var yPos = this.fixture.Create<int>();

            // Act
            sut.PlaceOn(xPos, yPos, Orientation.N);

            // Assert
            sut.RobotMode.Should().Be(RobotMode.InOperation);
        }
    }
}
