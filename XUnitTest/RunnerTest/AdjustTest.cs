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
            adjust.level.Should().Be(def.init_level);
            adjust.percent.Should().Be(adjust.level * 10);
        }

        [Fact]
        void TestSerialize()
        {
            var adjust = new Adjust(def);

            adjust.level = 2;
            adjust.min_level = 1;

            var json = JsonConvert.SerializeObject(adjust,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var adjustDe = JsonConvert.DeserializeObject<Adjust>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            adjustDe.type.Should().Be(adjust.type);
            adjustDe.level.Should().Be(adjust.level);
            adjustDe.min_level.Should().Be(adjust.min_level);
            adjustDe.percent.Should().Be(adjust.percent);

        }

        [Fact]
        void TestLevelChanged()
        {
            var adjust = new Adjust(def);

            decimal percent = adjust.percent;
            adjust.OBSProperty(x => x.percent).Subscribe(p => percent = p);

            for(int i=1; i<=10; i++)
            {
                adjust.level = i;
                percent.Should().Be(i*10);
            }
            
        }

        [Fact]
        void TestMinChanged()
        {
            var adjust = new Adjust(def);

            int level = adjust.level;
            adjust.OBSProperty(x => x.level).Subscribe(p => level = p);

            adjust.min_level = level + 1;

            level.Should().Be(adjust.min_level);

            int old_level = level;

            adjust.min_level -= 1;

            level.Should().Be(old_level);
        }
    }

    public class AdjustTestFixture : IDisposable
    {
        public AdjustTestFixture()
        {
            var mock = new Mock<AdjustPopTaxDef>();
            mock.Setup(l => l.init_level).Returns(100);

            AdjustTest.def = mock.Object;
        }

        public void Dispose()
        {
        }
    }
}
