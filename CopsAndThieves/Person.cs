using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<string> Inventory { get; set; }
        public static string Sprite { get; set; }
    }

    class Police : Person
    {
        //Sprite = "P";
        void Arrest() { }
    }

    class Theif : Person
    {
        void Steal() { }
    }

    class Citizen : Person
    {
        void Greet() { }
    }

}
