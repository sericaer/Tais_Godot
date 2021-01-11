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

        [Fact]
        public void TestIntegrationDate()
        {
            var integration1 = runner.integrations.Should().ContainSingle(runner.date, runner.taishou);
            integration1.IsBindWith(x => x.value, y => y.DaysInc);

            var integration3 = runner.integrations.Should().ContainSingle(runner.date, runner.economy);
            integration3.IsBindWith(x => x.value, y => y.DaysInc);
        }

        [Fact]
        public void TestIntegrationAdjust()
        {
            var integration1 = runner.integrations.Should().ContainSingle(runner.adjusts[ADJUST_TYPE.POP_TAX], runner.departs.SelectMany(d => d.pops));
            integration1.IsBindWith(x => x.percent, y => y.UpdateTaxPercent);

            var integration2 = runner.integrations.Should().ContainSingle(runner.adjusts[ADJUST_TYPE.CHAOTING_TAX], runner.chaoting);
            integration2.IsBindWith(x => x.percent, y => y.UpdateReportTaxPercent);
        }

        [Fact]
        public void TestIntegrationDepart()
        {
            var integration1 = runner.integrations.Should().ContainSingle(runner.departs as IEnumerable<Depart>, runner.economy);
            integration1.IsBindWith(x => x.incomeDetail, y => y.UpdateIncome);
        }

        [Fact]
        public void TestIntegrationChaoting()
        {
            var integration5 = runner.integrations.Should().ContainSingle(runner.chaoting, runner.economy);
            integration5.IsBindWith(x => x.outputDetail, y => y.UpdateOutput);
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

    class TEST : AdjustPopTaxDef
    {
        public override int init_percent => 10;
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
            runner.chaoting = new Mock<IChaoting>().Object;
            runner.eventMgr = new Mock<IEventManager>().Object;

            runner.adjusts = new Dictionary<ADJUST_TYPE, IAdjust>()
            {
                { ADJUST_TYPE.POP_TAX, new Adjust() { type = ADJUST_TYPE.POP_TAX, percent = 10} },
                { ADJUST_TYPE.CHAOTING_TAX, new Adjust() { type = ADJUST_TYPE.CHAOTING_TAX, percent = 100} }
            };

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
