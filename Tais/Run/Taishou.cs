using System;
using Tais.API;

namespace Tais.Run
{
    interface ITaishou
    {
        string party { get; }
        string name { get; }
        decimal age { get; }

        void DaysInc(decimal days);
    }

    class Taishou : ITaishou
    {
        public string party { get; }
        public string name { get; }
        public decimal age { get; set; }

        public Taishou(string name, int age, Type party)
        {
            this.name = name;
            this.age = age;
            this.party = party.Name;
        }

        public void DaysInc(decimal days)
        {

        }
    }
}