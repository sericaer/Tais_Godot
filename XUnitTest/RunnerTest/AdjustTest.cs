using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tais;
using Tais.API;
using Xunit;
using Tais.Run;
using FluentAssertions;
using Newtonsoft.Json;

namespace XUnitTest.RunnerTest
{
    public class AdjustTest : IClassFixture<AdjustTestFixture>
    {
        public static AdjustDef def;

        [Fact]
        void TestInit()
        {
            var adjust = new Adjust(def);

            adjust.type.Should().Be(def.type);
            adjust.percent.Should().Be(def.init_percent);
        }

        [Fact]
        void TestSerialize()
        {
            var adjust = new Adjust(def);

            adjust.percent = 2;

            var json = JsonConvert.SerializeObject(adjust,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var adjustDe = JsonConvert.DeserializeObject<Adjust>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            adjustDe.type.Should().Be(adjust.type);
            adjustDe.percent.Should().Be(adjust.percent);

        }
    }

    public class AdjustTestFixture : IDisposable
    {
        public AdjustTestFixture()
        {
            var mock = new Mock<AdjustPopTaxDef>();
            mock.Setup(l => l.init_percent).Returns(100);

            AdjustTest.def = mock.Object;
        }

        public void Dispose()
        {
        }
    }
}
