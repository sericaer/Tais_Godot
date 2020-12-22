using System;
using Tais.API;

namespace Tais.Run
{
    class Taishou
    {
        public string party;
        public string name;
        public int age;

        public Taishou(string name, int age, Type party)
        {
            this.name = name;
            this.age = age;
            this.party = party.Name;
        }
    }
}