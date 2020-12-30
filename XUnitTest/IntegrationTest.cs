using FluentAssertions;
using Moq;
using ReactiveMarbles.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Tais;
using Tais.API;
using Tais.Mod;
using Tais.Run;
using Xunit;

namespace XUnitTest
{
    public class IntegrationTest : IClassFixture<IntegrationTestsFixture>
    {
        internal static Tais.Run.Runner runner;
        internal static Adjust taxAdjust;

        [Fact]
        public void TestIntegrationGroup()
        {
            var integration1 = runner.integrations.Should().ContainSingle(runner.date, runner.taishou);
            integration1.IsBindWith(x => x.value, y => y.DaysInc);

            var integration2 = runner.integrations.Should().ContainSingle(taxAdjust, runner.departs.SelectMany(d=>d.pops));
            integration2.IsBindWith(x => x.currRate, y => y.UpdateTaxRate);

            var integration3 = runner.integrations.Should().ContainSingle(runner.date, runner.economy);
            integration3.IsBindWith(x => x.value, y => y.DaysInc);

            var integration4 = runner.integrations.Should().ContainSingle(runner.departs as IEnumerable<Depart>, runner.economy);
            integration4.IsBindWith(x => x.incomeDetail, y => y.UpdateIncome);
        }

        [Fact]
        public void TestIntegrationImp()
        {
            ClassSource source = new ClassSource();
            ClassDest dest = new ClassDest();

            source.data = 100;

            var integration = new IntegrationImp<ClassSource, ClassDest>(source, dest);
            integration.With(x=>x.data, y=>y.Function);

            dest.data.Should().Be(100);

            source.data = 200;
            dest.data.Should().Be(200);
        }

    }

    class TEST : AdjustTaxDef
    {
        public override decimal[] level_rates => throw new NotImplementedException();

        public override int init_level => throw new NotImplementedException();
    }

    public class IntegrationTestsFixture : IDisposable
    {
        public IntegrationTestsFixture()
        {

            var r1 = typeof(TEST).GetInterfaces().Any(x=>x.IsAssignableFrom(typeof(AdjustDef)));
            var r2 = typeof(TEST).GetInterfaces().Any(x=>x.IsInstanceOfType(typeof(AdjustDef)));

            var runner = new Tais.Run.Runner();

            runner.date = new Mock<IDate>().Object;
            runner.taishou = new Mock<ITaishou>().Object;
            runner.economy = new Mock<IEconomy>().Object;

            IntegrationTest.taxAdjust = new Adjust() { name = typeof(AdjustTaxDef).FullName, rates = new Decimal[] { 1, 2, 3, 4 }, currLevel = 1 };

            runner.adjusts = new List<Adjust>() { IntegrationTest.taxAdjust };
            runner.IntegrateData();

            IntegrationTest.runner = runner;
        }

        public void Dispose()
        {

        }
    }

    public class ClassSource : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public int data { get; set; }
    }

    public class ClassDest
    {
        public int data;

        public void Function(int data)
        {
            this.data = data;
        }
    }
}
