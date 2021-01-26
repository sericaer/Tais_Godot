using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xunit;
using Tais;
using Tais.Run;
using FluentAssertions;

namespace XUnitTest.RunnerTest
{
    public class ChaotingTest
    {
        [Fact]
        void Test1()
        {
            TEST1 t1 = new TEST1();
            t1.elem = TEST1.ELEM.DEF1;

            int value = -1;
            t1.OBSProperty(x => x.elem).Subscribe(x => value = x.a);
            value.Should().Be(1);

            t1.elem = TEST1.ELEM.DEF2;
            value.Should().Be(3);
        }
    }

    public class TEST1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ELEM elem { get; set; }

        public class ELEM
        {
            public static ELEM DEF1 = new ELEM() { a = 1, b = 2 };
            public static ELEM DEF2 = new ELEM() { a = 3, b = 4 };

            public int a;
            public int b;
        }
    }
}
