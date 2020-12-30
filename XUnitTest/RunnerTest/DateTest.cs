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
using System.ComponentModel;

namespace XUnitTest.RunnerTest
{
    public class DateTest
    {
        [Fact]
        void TestDateInc()
        {
            IDate date = new Date();

            (int y, int m, int d) dateValue = (-1, -1, -1);
            date.OBSProperty(x => x.value).Subscribe(x => dateValue = x);

            for(int i=0; i<1000; i++)
            {
                dateValue.y.Should().Be(1+i / 12 / 30);
                dateValue.m.Should().Be(1+(i / 30) % 12);
                dateValue.d.Should().Be(1+i % 30);

                date.Inc();
            }
        }

        [Fact]
        void TestSerialize()
        {
            IDate date = new Date();

            var json = JsonConvert.SerializeObject(date,
                Formatting.Indented,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            var dateDe = JsonConvert.DeserializeObject<IDate>(json,
                new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects });

            date.value.Should().Be((1, 1, 1));
        }
    }
}
