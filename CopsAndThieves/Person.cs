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

        public void SpawnRandomPosition(int mapX, int mapY)
        {
            Random rand = new Random();
            int x = rand.Next(0, mapX);
            int y = rand.Next(0, mapY);

            PosX = x;
            PosY = y;


        }

        public void Move(int maxX, int maxY)
        {

            Random rand = new Random();

            int x =rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Console.WriteLine($"Trying to move with x: {x} and y: {y}");
            //Limits class that checks if said person can walk there, don't cross city limits



            // Horizontal wrapping
            //If 20 is the width in bricks
            //double it?
            if (PosX < 0)
                PosX = (maxX*2);
            else if (PosX >= (maxX*2))
                PosX = 0;

            // Vertical wrapping
            if (PosY < 0)
                PosY = maxY - 1;
            else if (PosY >= maxY)
                PosY = 0;

            //If pos is 5 => 5 - 1
            PosX += x;
            PosY += y;
            //PosX++;
            //PosX--;
            //PosY--;
            //PosY++;
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









