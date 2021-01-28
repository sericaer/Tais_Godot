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
        public void EventManagerDaysIncTest()
        {
            EventManager eventManager = new EventManager();

            TaskCompletionSource<bool> tcs = null;

            List<IEvent> events = new List<IEvent>()
            {
                Mock.Of<IEvent>(e=>e.title_format == "EVENT_TITLE_1" && e.isTrigger(Tuple.Create(0,0,0).ToValueTuple()) == true),
                Mock.Of<IEvent>(e=>e.title_format == "EVENT_TITLE_2" && e.isTrigger(Tuple.Create(0,0,0).ToValueTuple()) == true
                                                                     && e.isTrigger(Tuple.Create(1,1,1).ToValueTuple()) == true)
            };

            eventManager.events = events;

            IEvent currEvent = null;
            eventManager.OBSProperty(x => x.currEvent).Subscribe(e =>
            {
                if (e == null)
                {
                    return;
                }

                currEvent = e;
                currEvent.FinishNotify = async () =>
                {
                    tcs = new TaskCompletionSource<bool>();
                    await tcs.Task;
                };
            });

            eventManager.DaysIncAsync((0, 0, 0));

            currEvent.title_format.Should().Be("EVENT_TITLE_1");
            currEvent = null;

            tcs.SetResult(true);

            currEvent.title_format.Should().Be("EVENT_TITLE_2");
            currEvent = null;

            tcs.SetResult(true);

            eventManager.DaysIncAsync((1, 1, 1));

            currEvent.title_format.Should().Be("EVENT_TITLE_2");
            currEvent = null;

            tcs.SetResult(true);

            eventManager.DaysIncAsync((2, 2, 2));

            currEvent.Should().BeNull();
        }

        [Fact]
        public void EventTriggerTest()
        {
            var eventObj = new Event();

            eventObj.date = null;
            eventObj.isTrigger((0,0,0)).Should().BeFalse();

            eventObj.date = (1,1,1);
            eventObj.trigger = Mock.Of<ConditionDef>(l => l.isTrue() == true);

            eventObj.isTrigger((1, 1, 1)).Should().BeTrue();

            eventObj.date = (1, 1, 1);
            eventObj.trigger = Mock.Of<ConditionDef>(l => l.isTrue() == false);

            eventObj.isTrigger((1, 1, 1)).Should().BeFalse();

            eventObj.date = (1, 1, 1);
            eventObj.trigger = Mock.Of<ConditionDef>(l => l.isTrue() == true);

            eventObj.isTrigger((0, 0, 0)).Should().BeFalse();

            eventObj.date = (null, null, null);

            eventObj.isTrigger((0, 0, 0)).Should().BeTrue();

        }
    }
}
