using FluentAssertions;
using Moq;
using System;
using Tais;
using Tais.API;
using Tais.Mod;
using Tais.Run;
using Tais.Visitor;
using Xunit;

namespace XUnitTest.RunnerTest
{
    public class PopTest : IClassFixture<PopTestFixture>
    {
        internal static IPop def;

        [Fact]
        public void InitTest()
        {
            var pop = new Pop(def);

            pop.num.Should().Be(def.num);
            pop.name.Should().Be(def.GetType().FullName);
            pop.isTax.Should().BeTrue();
        }
    }

    public class PopTestFixture : IDisposable
    {
        public PopTestFixture()
        {
            var mock = new Mock<IPop>();
            mock.Setup(l => l.num).Returns(1000);
            mock.Setup(l => l.is_tax).Returns(true);

            PopTest.def = mock.Object;
        }

        public void Dispose()
        {
            
        }
    }
}
