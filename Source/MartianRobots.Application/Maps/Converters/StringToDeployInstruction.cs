using System.Text.RegularExpressions;
using MartianRobots.Application.Maps.DTOs;
using MartianRobots.SharedKernel;

namespace MartianRobots.Application.Maps.Converters
{
    public class StringToDeployInstruction
    {
        public static DeployInstructionDto Convert(string deployInstruction)
        {
            Guards.ThrowIfNullOrEmpty(deployInstruction);

            // Check patter
            // N* N* X
            const string pattern = @"^(\d*)\s(\d*)\s(\w)$";
            // Should be 3 positions
            var match = Regex.Match(deployInstruction, pattern, RegexOptions.IgnoreCase);

            if (!match.Success || match.Groups.Count != 4)
                throw new ArgumentException($"The value '{deployInstruction}' is not valid.");

            var coords = new CoordinatesDto(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
            var orientation = KeyToOrientation.Convert(match.Groups[3].Value);

            return new DeployInstructionDto(coords, orientation);
        }
    }
}
