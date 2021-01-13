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
using System.Threading.Tasks;

namespace XUnitTest.RunnerTest
{
    public class EventTest : IClassFixture<DepartTestFixture>
    {

        public static EventDef def;

        [Fact]
        public void InitTest()
        {
        }

        [Fact]
        public void EventTrigger()
        {
            EventManager eventManager = new EventManager();

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            List<IEvent> events = new List<IEvent>()
            {
                Mock.Of<IEvent>(e=>e.title_format == "EVENT_TITLE_1" && e.trigger ==  Mock.Of<ConditionDef>(c=>c.isTrue() == true)),
                Mock.Of<IEvent>(e=>e.title_format == "EVENT_TITLE_2" && e.trigger ==  Mock.Of<ConditionDef>(c=>c.isTrue() == true))
            };

            eventManager.events = events;

            IEvent currEvent = null;
            eventManager.OBSProperty(x => x.currEvent).Subscribe(e=>
            {
                if(e == null)
                {
                    return;
                }

                currEvent = e;
                currEvent.FinishNotify = async () =>
                {
                    await tcs.Task;
                };
            });

            eventManager.DaysIncAsync((0, 0, 0));

            currEvent.title_format.Should().Be("EVENT_TITLE_1");

            tcs.SetResult(true);

            currEvent.title_format.Should().Be("EVENT_TITLE_2");
        }
    }
}
