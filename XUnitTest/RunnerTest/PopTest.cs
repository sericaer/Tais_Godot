using FluentAssertions;
using Moq;
using Newtonsoft.Json;
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
        internal static PopDef def;

        [Fact]
        public void InitTest()
        {
            var pop = Pop.Gen(def);

            pop.num.Should().Be(def.num);
            pop.name.Should().Be(def.GetType().FullName);
            pop.isTax.Should().BeTrue();
        }

        [Fact]
        public void TestNumChanged()
        {
            var pop = Pop.Gen(def);
            pop.UpdateTaxPercent(100);

            decimal baseValue = -1;
            pop.OBSProperty(x => x.tax.baseValue).Subscribe(x => baseValue = x);
            pop.num += 1000;

            baseValue.Should().Be(pop.tax.baseValue);

        }

        [Fact]
        public void TestTaxPercentChanged()
        {
            var pop = Pop.Gen(def);
            pop.UpdateTaxPercent(100);

            decimal baseValue = -1;
            pop.OBSProperty(x => x.tax.baseValue).Subscribe(x => baseValue = x);
            pop.UpdateTaxPercent(90);

            baseValue.Should().Be(pop.tax.baseValue);
        }

        [Fact]
        public void TestSerialize()
        {
            var pop = Pop.Gen(def);

            var json = JsonConvert.SerializeObject(pop, 
                Formatting.Indented, 
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var popDe = JsonConvert.DeserializeObject<Pop>(json, 
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            popDe.num.Should().Be(pop.num);
            popDe.name.Should().Be(pop.name);
            popDe.isTax.Should().Be(pop.isTax);
        }
    }

    public class PopTestFixture : IDisposable
    {
        public PopTestFixture()
        {
            var mock = new Mock<PopDef>();
            mock.Setup(l => l.num).Returns(1000);
            mock.Setup(l => l.is_tax).Returns(true);

            PopTest.def = mock.Object;
        }

        public void Dispose()
        {
            
        }
    }
}
