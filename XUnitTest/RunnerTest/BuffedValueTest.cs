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
    public class BuffedValueTest
    {
        [Fact]
        public void BaseValueChangeTest()
        {
            IBuffedValue buffedValue = new BuffedValue();

            decimal value = -1;
            buffedValue.OBSProperty(x => x.value).Subscribe(x=> value = x);

            buffedValue.baseValue = 100;

            value.Should().Be(buffedValue.baseValue);
        }

        [Fact]
        public void BufferAddTest()
        {

        }

        [Fact]
        public void BufferRemoveTest()
        {

        }
    }
}
