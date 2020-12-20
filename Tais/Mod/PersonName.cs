using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace Tais.Mod
{
    class PersonName
    {
        private readonly string[] family;
        private readonly string[] given;

        public PersonName(string[] family, string[] given)
        {
            this.family = family;
            this.given = given;
        }

        public string GetRandomFamily()
        {
            return family.OrderBy(_ => Guid.NewGuid().ToString()).First();
        }

        public string GetRandomGiven()
        {
            return given.OrderBy(_ => Guid.NewGuid().ToString()).First();
        }

        internal static PersonName Load(string dir)
        {
            var familyNamePath = $"{dir}/person_family.txt";
            var givenNamePath = $"{dir}/person_given.txt";

            string[] family = { };
            if (File.Exists(familyNamePath))
            {
                family = File.ReadAllLines(familyNamePath);
            }

            string[] given = { };
            if (File.Exists(givenNamePath))
            {
                given = File.ReadAllLines(givenNamePath);
            }

            if (family.Count() == 0 && given.Count() == 0)
            {
                return null;
            }

            return new PersonName(family, given);
        }

        public (string family, string given) GetRandomFull()
        {
            return (GetRandomFamily(), GetRandomGiven());
        }
    }
}