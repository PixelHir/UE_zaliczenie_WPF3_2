using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UEWPF3_2
{
    class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Person(int id, string fn, string ln, int ag)
        {
            PersonId = id;
            FirstName = fn;
            LastName = ln;
            Age = ag;
        }
        public override string ToString()
        {
            return $"{PersonId} {FirstName} {LastName} {Age}";
        }
    }
}
