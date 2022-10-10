using System.Text;
using System.Text.Json;
using MartianRobots.AcceptanceTests.Support;
using MartianRobots.API.Models;
using MartianRobots.Domain.RobotsAggregate;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.AcceptanceTests.Contexts
{
    public class DeployRobotContext
    {
        private const string JsonMediaType = "application/json";
        private const string RobotEndpoint = "robots";

        private readonly RobotWebApplicationFactory factory;

        public DeployRobotContext(RobotWebApplicationFactory factory)
        {
            this.factory = factory;
        }

        public async Task<HttpResponseMessage> RequestPlaceOn(int xPos, int yPos)
        {
            var endpoint = $"{RobotEndpoint}/{this.factory.DefaultRobotId}/place";

            var model = new PlaceRobotModel(this.factory.DefaultMapId, $"{xPos} {yPos} E");

            var jsonModel = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, JsonMediaType);

            return await this.factory.HttpClient.PostAsync(endpoint, jsonModel);
        }

        public Robot GetRobot()
        {
            using var context = factory.DbContextFactory.CreateDbContext();

            return context.Robots!.Include(robot => robot.Coordinates).FirstOrDefault(robot => robot.Id == this.factory.DefaultRobotId);
        }
    }
}
