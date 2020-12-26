using Newtonsoft.Json;
using System;

namespace Tais.Run
{
    interface ITaishou
    {
        string party { get; }
        string name { get; }
        decimal age { get; }

        void DaysInc(decimal days);
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Taishou : ITaishou
    {
        [JsonProperty]
        public string party { get; set; }

        [JsonProperty]
        public string name { get; set; }

        [JsonProperty]
        public decimal age { get; set; }

        public Taishou(string name, int age, Type party)
        {
            this.name = name;
            this.age = age;
            this.party = party.FullName;
        }

        public void DaysInc(decimal days)
        {

        }

        [JsonConstructor]
        private Taishou()
        {

        }
    }
}