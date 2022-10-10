# .Net Code Challenge - Martian Robots

Code challenge implementation of the requirements defined in the [problem](#The-Problem) section below.

# Objectives
- Complete the challenge using C# language and a .NET program.

  For this challenge, an LTS version of .NET will be used as the target for the executable interface, specifically .NET 6.

- Leave margin for improvement and expansion with future implementations.
- The app should properly work with at least the example input..
- The app should be hosted in a GitHub public repository.
	- It should have a README.md describing the product and explaining how to run it.

# How to run

## Requirements
To compile this source code you will need to have [.NET 6.0 SDK](https://dotnet.microsoft.com/download) installed to take advantage of the latest C# 10.0 features.

## Execution
To run the program you can use the following commands:

Commands must be run independently, so open a single cmd for every command.
The client, which is represented by a console app requires that the API is running, so execute the command in this order.

```
$ dotnet run --project .\Source\MartianRobots.API\MartianRobots.API.csproj -c Release
$ dotnet run --project .\Apps\MartianRobots.Console\MartianRobots.Console.csproj -c Release
```

# Tests

This code contains some tests.
Test libraries used:
- [xUnit](https://xunit.net/)
- [Moq](https://github.com/moq/moq4)
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)
- [Specflow](https://specflow.org/)
- [FluentAssertions](https://fluentassertions.com/)

To run the test you can use the following command:

```
$ dotnet test MartianRobots.sln -c Release
```

# The Problem

The surface of Mars can be modeled by a rectangular grid around which robots are able to move according to instructions provided by Earth. You are to write a program that determines each sequence of robot positions and reports the final position of the robot.

A robot position consists of a grid coordinate (a pair of integers: x-coordinate followed by y-coordinate) and an orientation (N, S, E, W for north, south, east, and west). A robot instruction is a string of the letters "L", "R", and "F" which represent, respectively, the instructions:

*   Left: the robot turns left 90 degrees and remains on the current grid point.
*   Right: the robot turns right 90 degrees and remains on the current grid point.
*   Forward: the robot moves forward one grid point in the direction of the current orientation and maintains the same orientation.

The direction North corresponds to the direction from grid point (x, y) to grid point (x, y+1).

There is also a possibility that additional command types may be required in the future and provision should be made for this.

Since the grid is rectangular and bounded (...yes Mars is a strange planet), a robot that moves "off" an edge of the grid is lost forever. However, lost robots leave a robot "scent" that prohibits future robots from dropping off the world at the same grid point. The scent is left at the last grid position the robot occupied before disappearing over the edge. An instruction to move "off" the world from a grid point from which a robot has been previously lost is simply ignored by the current robot.

## The Input

The first line of input is the upper-right coordinates of the rectangular world, the lower-left coordinates are assumed to be 0, 0.

The remaining input consists of a sequence of robot positions and instructions (two lines per robot). A position consists of two integers specifying the initial coordinates of the robot and an orientation (N, S, E, W), all separated by whitespace on one line. A robot instruction is a string of the letters "L", "R", and "F" on one line.

Each robot is processed sequentially, i.e., finishes executing the robot instructions before the next robot begins execution.

The maximum value for any coordinate is 50.

All instruction strings will be less than 100 characters in length.

## The Output

For each robot position/instruction in the input, the output should indicate the final grid position and orientation of the robot. If a robot falls off the edge of the grid the word "LOST" should be printed after the position and orientation.

### Sample Input

```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFRFLFL
```

### Sample Output

```
1 1 E
3 3 N LOST
4 2 N
```