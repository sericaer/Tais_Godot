using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{

    interface IEventManager : INotifyPropertyChanged
    {
        IEvent currEvent { get; set; }

        void DaysIncAsync((int y, int m, int d) obj);
    }

    interface IEvent
    {
        (int? y, int? m, int? d)? date { get; }

        ConditionDef trigger { get; }

        string title_format { get; }
        string[] title_objs { get; }

        string desc_format { get; }
        string[] desc_objs { get; }

        IOption[] options { get; }

        Func<Task> FinishNotify{ get; set; }

        bool isTrigger((int y, int m, int d) date);
    }

    interface IOption
    {
        string desc_format { get; }
        string[] desc_objs { get; }

        IOperation[] operations { get; }
    }

    class Option : IOption
    {
        public Option()
        {

        }

        public Option(OptionDef def)
        {
            desc_format = def.desc.format;
            desc_objs = def.desc.objs.Select(x => x.ToString()).ToArray();

            operations = def.operations;
        }

        public string desc_format { get; set; }

        public string[] desc_objs { get; set; }

        public IOperation[] operations { get; set; }
    }

    interface ICondition
    {
        bool isTrue();
    }

    class Event : IEvent
    {
        public (int? y, int? m, int? d)? date { get; set; }

        public ConditionDef trigger { get; set; }

        public string title_format => _title.format;
        public string[] title_objs => _title.objs.Select(x => x.ToString()).ToArray();

        public string desc_format => _desc.format;
        public string[] desc_objs => _desc.objs.Select(x => x.ToString()).ToArray();

        public IOption[] options => _options;

        public Func<Task> FinishNotify { get; set; }

        
        private IDesc _title;
        private IDesc _desc;
        private Option[] _options;

        public static Event Gen(EventDef def)
        {
            var inst = new Event();

            inst.trigger = def.trigger;
            inst._title = def.title;
            inst._desc = def.desc;
            inst._options = def.options.Select(x => new Option(x)).ToArray();

            if (def.date != null)
            {
                inst.date = (def.date.year, def.date.month, def.date.day);
            }
            

            return inst;
        }


        public bool isTrigger((int y, int m, int d) date)
        {
            if(!isVaildDate(date))
            {
                return false;
            }

            if(trigger == null)
            {
                return true;
            }

            return trigger.isTrue();
        }

        private bool isVaildDate((int y, int m, int d) _date)
        {
            if(date == null)
            {
                return false;
            }

            if(date.Value.y != null && _date.y != date.Value.y)
            {
                return false;
            }
            if (date.Value.m != null && _date.m != date.Value.m)
            {
                return false;
            }
            if (date.Value.d != null && _date.d != date.Value.d)
            {
                return false;
            }

            return true;
        }
    }

    class EndEvent : IEvent
    {
        public (int? y, int? m, int? d)? date => null;

        public ConditionDef trigger => null;

        public string title_format => "GAME_OVER_TITLE";

        public string[] title_objs => null;

        public string desc_format => "GAME_OVER_DESC";

        public string[] desc_objs => null;

        public IOption[] options => new IOption[]
        {
            new Option()
            {
                desc_format = "GAME_OVER_OPTION_DESC"
            }
        };


        public Func<Task> FinishNotify { get; set; }

        public bool isTrigger((int y, int m, int d) date)
        {
            return false;
        }
    }


    class EventManager : IEventManager
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public IEvent currEvent { get; set; }

        internal List<IEvent> events = new List<IEvent>();

        internal static IEventManager Gen(IEnumerable<EventDef> defs)
        {
            var inst = new EventManager();

            foreach(var def in defs)
            {
                inst.events.Add(Event.Gen(def));
            }

            return inst;
        }

        public async void DaysIncAsync((int y, int m, int d) date)
        {
            foreach (var eventobj in events)
            {
                if (!eventobj.isTrigger(date))
                {
                    continue;
                }

                currEvent = eventobj;

                LOG.INFO("FinishNotify");
                await currEvent.FinishNotify();
                LOG.INFO("FinishNotifyed");

                currEvent = null;
            }

        }
    }


}