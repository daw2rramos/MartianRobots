using System.Text;
using System.Text.Json;
using MartianRobots.API.Models;
using MartianRobots.SharedKernel;

namespace MartianRobots.Console
{
    public class Client
    {
        private readonly HttpClient httpClient;

        public Client(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Result<MapModel>> GetMap(string mapName)
        {           
            var endpoint = $"/maps/{mapName}";

            var result = await this.httpClient.GetAsync(endpoint);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var map = JsonSerializer.Deserialize<MapModel>(content);

                if (map is null)
                {
                    return Result.Fail<MapModel>($"Unable to serialize the response from the server.");

                }
                return Result.Ok(map);
            }

            return Result.Fail<MapModel>($"Unable to retrieve the map due to: {result.ReasonPhrase}");
        }        

        public async Task<Result<RobotModel>> GetRobot(string robotName)
        {
            var endpoint = $"/robots/{robotName}";

            var result = await this.httpClient.GetAsync(endpoint);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var robot = JsonSerializer.Deserialize<RobotModel>(content);

                if (robot is null)
                {
                    return Result.Fail<RobotModel>($"Unable to serialize the response from the server.");
                }

                return Result.Ok(robot);
            }

            return Result.Fail<RobotModel>($"Unable to retrieve the robot due to: {result.ReasonPhrase}");            
        }

        public async Task<Result<RobotReportModel>> GetRobotReport(Guid id)
        {
            var endpoint = $"/robots/{id}/report";

            var result = await this.httpClient.GetAsync(endpoint);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var robotReport = JsonSerializer.Deserialize<RobotReportModel>(content);

                if(robotReport is null)
                {
                    return Result.Fail<RobotReportModel>($"Unable to serialize the response from the server.");
                }

                return Result.Ok(robotReport);
            }

            return Result.Fail<RobotReportModel>($"Unable to retrieve the robot report due to: {result.ReasonPhrase}");            
        }

        public async Task<Result> PlaceRobot(Guid robotId, PlaceRobotModel deployInstruction)
        {
            var enpoint = $"/robots/{robotId}/place";

            var payload = new StringContent(JsonSerializer.Serialize(deployInstruction), Encoding.UTF8, "application/json");

            var result = await this.httpClient.PostAsync(enpoint, payload);

            if (result.IsSuccessStatusCode)
            {
                return Result.Ok();
            }

            return Result.Fail(result.ReasonPhrase);
        }

        public async Task<Result> MoveRobot(Guid robotId, MoveRobotModel instrution)
        {
            var enpoint = $"/robots/{robotId}/move";

            var payload = new StringContent(JsonSerializer.Serialize(instrution), Encoding.UTF8, "application/json");

            var result = await this.httpClient.PostAsync(enpoint, payload);

            if (result.IsSuccessStatusCode)
            {
                return Result.Ok();
            }

            return Result.Fail(result.ReasonPhrase);
        }
    }
}
