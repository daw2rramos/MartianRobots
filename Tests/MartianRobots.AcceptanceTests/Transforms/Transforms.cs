using TechTalk.SpecFlow;

namespace MartianRobots.AcceptanceTests.Transformations
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation]
        public int CoordinatesTransform(string position)
        {
            return int.Parse(position);
        }
    }
}
