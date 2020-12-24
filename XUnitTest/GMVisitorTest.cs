using FluentAssertions;
using System;
using Tais;
using Tais.Mod;
using Tais.Visitor;
using Xunit;

namespace XUnitTest
{
    public class GMVisitorTest : IClassFixture<VisitorTestsFixture>
    {
        public static Root root;

        [Fact]
        void TestSetValue()
        {
            var visitor = HelperClass<Root>.Property(x => x.b.l.value);
            visitor.SetValue(99);

            root.b.l.value.Should().Be(99);
        }

        [Fact]
        void TestGetValue()
        {
            root.b.l.value = 100;

            var visitor = HelperClass<Root>.Property(x => x.b.l.value);

            visitor.GetValue().Should().Be(100);
        }
    }

    public class VisitorTestsFixture : IDisposable
    {
        public VisitorTestsFixture()
        {
            GMVisitorTest.root = new Root() { b = new Branch() { l = new Leaf()  } };

            VisitorData.dict.Add(typeof(Root), GMVisitorTest.root);
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }

    public class Root
    {
        public Branch b { get; set; }
    }

    public class Branch
    {
        public Leaf l { get; set; }
    }

    public class Leaf
    {
        public int value { get; set; }
    }
}
