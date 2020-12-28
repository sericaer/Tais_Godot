using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using Tais.Init;
using Tais.Mod;
using Tais.Run;
using Tais.Visitor;

namespace Tais
{
    class GMRoot
    {
        public static Runner runner
        {
            get
            {
                return _runner;
            }
            set
            {
                _runner = value;
                VisitorData.dict[typeof(Runner)] = value;

                if(!_runner.isInitialized)
                {
                    _runner.date = new Date();
                    _runner.taishou = new Taishou(initer.name, initer.age, initer.party);
                    _runner.departs.AddRange(modder.departs.Select(x=>new Depart(x)));
                    _runner.adjusts.AddRange(modder.adjusts.Select(x=>new Adjust(x)));

                    _runner.IntegrateData();
                }
            }
        }

        public static Modder modder
        {
            get
            {
                return _modder;
            }
            set
            {
                _modder = value;
            }
        }

        public static Initer initer
        {
            get
            {
                return _initer;
            }
            set
            {
                _initer = value;
                VisitorData.dict[typeof(Initer)] = value;
            }
        }

        public static Action<object[]> logger
        {
            set
            {
                LOG.logger = value;
            }
        }

        private static Runner _runner;
        private static Modder _modder;
        private static Initer _initer;
    }
}
