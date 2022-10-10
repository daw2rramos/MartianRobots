using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using MartianRobots.Application.Robots.Repositories;
using MartianRobots.Domain.RobotsAggregate;

namespace MartianRobots.Application.Tests.DataSources
{
    public class RobotNotRetrievedDataSource : AutoDataAttribute
    {
        public RobotNotRetrievedDataSource() : base(GetDefaultFixture)
        {
        }

        public static IFixture GetDefaultFixture()
        {           
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Setup required Stubs
            var robotRepositoryStub = fixture.Freeze<Mock<IRobotRepository>>();
            robotRepositoryStub
                .Setup(x => x.GetAsync(It.IsAny<Guid>(), CancellationToken.None))
                .ReturnsAsync((Robot) null);

            return fixture;
        }
    }
}
