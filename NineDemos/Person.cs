using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineDemos
{
    public record Person
    {

        public Dictionary<Dictionary<string, string>, Func<int, int, bool>> dictionary = new();
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public Person()
        {

        }
        
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
