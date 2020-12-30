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

            adjust.name.Should().Be(def.GetType().BaseType.FullName);
            adjust.currLevel.Should().Be(def.init_level);
            adjust.rates.Should().BeEquivalentTo(def.level_rates);
            adjust.currRate.Should().Be(def.level_rates[def.init_level]);
        }

        [Fact]
        void TestLevelChanged()
        {
            var adjust = new Adjust(def);

            decimal rate = 0M;
            adjust.OBSProperty(x => x.currRate).Subscribe(x=> rate = x);

            adjust.currLevel = 3;
            rate.Should().Be(adjust.rates[adjust.currLevel]);
        }

        [Fact]
        void TestSerialize()
        {
            var adjust = new Adjust(def);

            adjust.currLevel = 2;

            var json = JsonConvert.SerializeObject(adjust,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var adjustDe= JsonConvert.DeserializeObject<Adjust>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            adjustDe.name.Should().Be(adjust.name);
            adjustDe.currLevel.Should().Be(adjust.currLevel);
            adjustDe.rates.Should().BeEquivalentTo(adjust.rates);

            decimal rate = 0M;
            adjustDe.OBSProperty(x => x.currRate).Subscribe(x => rate = x);

            adjustDe.currLevel = 6;

            rate.Should().Be(adjustDe.rates[adjustDe.currLevel]);

        }
    }

    public class AdjustTestFixture : IDisposable
    {
        public AdjustTestFixture()
        {
            var mock = new Mock<AdjustDef>();
            mock.Setup(l => l.init_level).Returns(1);
            mock.Setup(l => l.level_rates).Returns(new decimal[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            AdjustTest.def = mock.Object;
        }

        public void Dispose()
        {
        }
    }
}
