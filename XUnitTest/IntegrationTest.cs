using FluentAssertions;
using Moq;
using ReactiveMarbles.PropertyChanged;
using System;
using System.ComponentModel;
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
