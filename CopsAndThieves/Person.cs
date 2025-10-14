using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class Person
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public List<string> Inventory { get; set; }
        public string Sprite { get; set; }

        void Move()
        {

        }
    }

    class Police : Person
    {
        public Police(string fName, string sName)
        {

            FirstName = fName;
            SurName = sName;
            Sprite = "P";
        }


        //Arrest a criminal
        void Arrest()
        {

        }
    }

    class Theif : Person
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




    public Theif(string fName, string sName) 
    {
          FirstName = fName;
          SurName = sName;
          Sprite = "T";
    }

        //Steal from a citizen
    void Steal() 
    {
        
    }
    

    class Citizen : Person
    {
        public Citizen(string fName, string sName) 
        {
            FirstName = fName;
            SurName = sName;
            Sprite = "C";
        }

        
        void Greet() { }
    }
}



