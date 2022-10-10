using FluentAssertions;
using MartianRobots.AcceptanceTests.Contexts;
using MartianRobots.Domain.MapsAggregate;
using TechTalk.SpecFlow;

namespace MartianRobots.AcceptanceTests.Steps
{
    [Binding]
    public class DeployRobotSteps
    {
        private readonly DeployRobotContext context;

        public DeployRobotSteps(DeployRobotContext context)
        {
            this.context = context;
        }

        [Given(@"the control technician requests to deploy the robot on the coordinates (.*) and (.*)")]
        public async Task GivenTheControlTechnicianRequestsToDeployTheRobotOnTheCoordinatesAnd(int xPos, int yPos)
        {
            var response = await this.context.RequestPlaceOn(xPos, yPos);

            response.IsSuccessStatusCode.Should().BeTrue();
        }                

        [When(@"the robot is placed on the map")]
        public void WhenTheRobotIsPlacedOnTheMap()
        {
            var robot = this.context.GetRobot();

            robot.Coordinates.Should().NotBeNull();
        }

        [Then(@"the robot initial coordinates are (.*) and (.*)")]
        public void ThenTheRobotInitialCoordinatesAreAnd(int xPos, int yPos)
        {
            var robot = this.context.GetRobot();

            robot.Coordinates.Should().Be(Coordinates.Create(xPos, yPos));
        }        
    }
}
