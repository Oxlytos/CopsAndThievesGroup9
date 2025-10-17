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

        static Random rand = new Random();

        //Position variable
        public int PosX, PosY;

        //Old position for clearing
        public int OldPosX, OldPosY;

        public void SpawnRandomPosition(int mapX, int mapY)
        {
            int x = rand.Next(3, mapX);
            int y = rand.Next(3, mapY);

            PosX = x;
            PosY = y;
        }

        //Greet another person
        public virtual string Greet(string otherPersonName, int x, int y)
        {
            string randomGreet = RandomGreet();
            string theGreet = $"{this.Sprite}💬 {this.FirstName} {this.SurName} greets {otherPersonName} at x:{x} y:{y}: \"{randomGreet}\"";
            return theGreet;
        }

        string RandomGreet()
        {
            string[] greets = {
                "*Spits at their shoes*",
                "Walking out in that? 👀",
                "Stop pretending 😈",
                "Daring outfit today 😬",
                "Back already? 😤",
                "Your aura stinks. 🤢",
                "Blink weird. 🤨",
                "Hi. Don’t care. 😐",
                "Chaos returns 😏",
                "Your face spoils things 😳",
                "Stop thinking out loud 😒",
                "Look at what the cat dragged in 😑",
                "Act normal 😒",
                "*Aura farms*",
                "Npc detected 👀",
                "No rizz 🤯",
                "Stop moving. Or don’t. 👀",
                "Improvising… tragic. 😏",
                "Human glitch detected. 😈",
                "Stop acting normal. 😒",
                "You exist. Terrible. 😬",
                "Opinions! Unwanted. 🤨",
                "Smile… alarming. 😳"
            };

            //Index in relation to total length of the array
            int greetIndex = rand.Next(greets.Length);
            string greet = greets[greetIndex];

            return greet;



        }

       
        public void Move(int maxX, int maxY)
        {
            //Before we change position, store old position
            OldPosX = PosX;
            OldPosY = PosY;

            //Random value to move with
            int x = rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Change delta
            PosX += x;
            PosY += y;
            //Console.WriteLine($"Trying to move with x: {x} and y: {y}");
            //Limits class that checks if said person can walk there, don't cross city limits


            //Check if crossing limits
            if (PosX < 1)
            {
                //To the right side
                PosX = maxX - 2;
            }

            else if (PosX >= maxX)
            {

                //Left
                PosX = 1;
            }

            // Vertical wrapping
            if (PosY < 1)
            {
                //Bottom
                PosY = maxY - 2;
            }

            else if (PosY >= maxY)
            {
                //Top
                PosY = 1;
            }


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

        //Standard police greets citizen
        public override string Greet(string otherPersonName, int x, int y)
        {
            string greet = $"\nOfficer {this.FirstName} {this.SurName} greets a {otherPersonName}, a citizen of the city";
            return greet;

        }

        //Greet other police
        public void GreetPolice(string otherPersonName, int x, int y)
        {
            // Console.WriteLine($"\nOfficer {base.SurName} says: Donuts? {otherPersonName}? At pos {x}, {y}");
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
            Sprite = "🦹";
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
        }

        //Citizens greet either another citizen or a cop
        void Greet() { }
    }
}
   

    









