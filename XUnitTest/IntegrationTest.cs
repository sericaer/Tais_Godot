using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tais;
using Tais.Mod;
using Tais.Run;
using Xunit;

namespace XUnitTest
{
    public class IntegrationTest : IClassFixture<IntegrationTestsFixture>
    {
        internal static Tais.Run.Runner runner;

        [Fact]
        public void TestIntegrationGroup()
        {
            var integration = runner.integrations.Should().ContainSingle(runner.date, runner.taishou);

            integration.IsBindWith(x => x.total_days, y => y.DaysInc);
        }

    }


    public class IntegrationTestsFixture : IDisposable
    {
        public IntegrationTestsFixture()
        {
            var runner = new Tais.Run.Runner();

            runner.date = new Mock<IDate>().Object;
            runner.taishou = new Mock<ITaishou>().Object;
            runner.IntegrateData();

            IntegrationTest.runner = runner;
        }

        public void Dispose()
        {

        }
    }
}
