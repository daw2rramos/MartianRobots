using MartianRobots.API.Models;
using MartianRobots.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = CreateHostBuilder(args).Build();

var client = host.Services.GetRequiredService<Client>();

Console.WriteLine(">_ Running example input...");

Console.WriteLine();
Console.WriteLine(">_ Waiting for the mapping of the Jezero Crater...");

var tryGetMap = await client.GetMap("Jezero Crater");

if(tryGetMap.Failure)
{
    Console.WriteLine($">_ Unable to map the surface of the Jezero Crater => {tryGetMap.Error}");
    Console.WriteLine(">_ Press any key to quit.");
    Console.ReadKey(true);
    return;
}

var map = tryGetMap.Value;

Console.WriteLine();
Console.WriteLine(">_ Discovering Mars! Rectangle grid mapped to Jezero Crater surface.");
Console.WriteLine($">_ Mars surface name: {map.Name}");
Console.WriteLine($">_ Mars size: {map.MarsWidth}x{map.MarsHeight}");

Console.WriteLine();
Console.WriteLine(">_ Trying to communicate with Perseverance...");

var tryGetRobot = await client.GetRobot("Perseverance");

if(tryGetRobot.Failure)
{
    Console.WriteLine($">_ Unable to establish communication with the robot => {tryGetRobot.Error}");
    Console.WriteLine(">_ Press any key to quit.");
    Console.ReadKey(true);
    return;
}

var robot = tryGetRobot.Value;

Console.WriteLine();
Console.WriteLine(">_ Communication established! Perseverance is ready to be operated.");

var instructionPairs = new Dictionary<string, string> {
    { "1 1 E", "RFRFRFRF" },
    { "3 2 N", "FRRFLLFFRRFLL" },
    { "0 3 W", "LLFFFRFLFL" },
};

Console.WriteLine(">_ Processing instructions:");

foreach (var pair in instructionPairs)
{        
    Console.WriteLine($">_ Robot initial position => {pair.Key}");
    
    var deployInstruction = new PlaceRobotModel(map.Id, pair.Key);    

    await client.PlaceRobot(robot.Id, deployInstruction);

    Console.WriteLine($">_ Robot instructions => {pair.Value}");

    var instruction = new MoveRobotModel(map.Id, pair.Value);

    await client.MoveRobot(robot.Id, instruction);    

    var tryGetRobotReport = await client.GetRobotReport(robot.Id);

    if(tryGetRobotReport.Failure)
    {
        Console.WriteLine(">_ Unable to generate the report of the mission");
    }
    else
    {
        var robotReport = tryGetRobotReport.Value;

        Console.Write(">_ Mission results => ");
        Console.Write($"{robotReport.XPos} ");
        Console.Write($"{robotReport.YPos} ");
        Console.Write($"{robotReport.Orientation}");

        var robotMode = robotReport.RobotMode.ToString().ToUpper();
        if (robotMode == "LOST")
        {
            Console.Write($" {robotMode}");
        }

        Console.WriteLine();
    }        
}

Console.WriteLine(">_ Press any key to quit.");
Console.ReadKey(true);

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            (_, services) => services
                .AddHttpClient<Client>("MartianRobots", api => 
                {
                    api.BaseAddress = new Uri("https://localhost:7257"); 
                }));
}
