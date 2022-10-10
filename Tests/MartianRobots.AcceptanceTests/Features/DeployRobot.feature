Feature: Deploy Robot

As a control technician
I want deploy a robot in the Mars surface
So I can send instructions to operate it


Scenario Outline: Robot is deployed on the map in the requested place

	Given the control technician requests to deploy the robot on the coordinates <xPos> and <yPos>

	When the robot is placed on the map

	Then the robot initial coordinates are <xPos> and <yPos>

	Examples:

	| xPos | yPos |
	| 2    | 3    |
	| 1    | 4    |
