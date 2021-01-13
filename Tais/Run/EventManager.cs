﻿using System;
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
        IEvent currEvent { get; }

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

        bool isVaildDate((int y, int m, int d) date);
    }

    interface IOption
    {
        string desc_format { get; }
        string[] desc_objs { get; }
    }

    class Option : IOption
    {
        private OptionDef def;

        public Option(OptionDef def)
        {
            this.def = def;
        }

        public string desc_format => def.desc.format;

        public string[] desc_objs => def.desc.objs.Select(x => x.ToString()).ToArray();
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

        internal static Event Gen(EventDef def)
        {
            var inst = new Event();

            inst.trigger = def.trigger;
            inst._title = def.title;
            inst._desc = def.desc;
            inst._options = def.options.Select(x => new Option(x)).ToArray();
            inst.date = def.date;

            return inst;
        }

        public bool isVaildDate((int y, int m, int d) _date)
        {
            if(date == null)
            {
                return true;
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
                if (!eventobj.isVaildDate(date))
                {
                    continue;
                }

                if (!eventobj.trigger.isTrue())
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