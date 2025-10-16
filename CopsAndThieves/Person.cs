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
        public string Sprite { get; set; } = "⬜";


        //Position variable
        public int PosX, PosY;

        public void SpawnRandomPosition()
        {
            Random rand = new Random();
            int x = rand.Next(0, 20);
            int y = rand.Next(0, 10);

            PosX = x;
            PosY = y;


        }

        public void Move()
        {
            if(this.PosY > CitySimulation.height-2)
            {
                PosY = -1;
            }
            //Move method
            PosY++;
        }
    }

    public class Police : Person
    {
        public Police(string fName, string sName)
        {

            FirstName = fName;
            SurName = sName;
            Sprite = "👮";
        }


        //Arrest a criminal
        void Arrest()
        {

        }
    }

    public class Theif : Person
    {

        public Theif(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
            Sprite = "🕵️";
        }

        //Steal from a citizen
        void Steal()
        {

        }
    }

    public class Citizen : Person
    {
        public Citizen(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
            Sprite = "🧍";
        }

        //Citizens greet either another citizen or a cop
        void Greet() { }
    }

}









