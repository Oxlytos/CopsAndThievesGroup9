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

        //Greet another person
        public virtual void Greet(string otherPersonName, int x, int y)
        {
            string randomGreet = RandomGreet();
            Console.WriteLine($"{this.FirstName} {this.SurName} greets {otherPersonName} at position {x} {y} with a : \"{randomGreet}\"");
        }

        string RandomGreet()
        {
            var rand = new Random();
            string[] greets = {
                    "Hey there!",
                    "Yo!",
                    "Good morning!",
                    "Good evening!",
                    "Howdy!",
                    "What’s up?",
                    "Nice to see you!",
                    "Long time no see!",
                    "Greetings, traveler.",
                    "Welcome back!",
                    "Sup dude!",
                    "Hey!",
                    "Hi!",
                    "Hello there!",
                    "Hey buddy!",
                    "Good to have you here!",
                    "How’s it going?",
                    "What brings you here?",
                    "Yo yo yo!",
                    "How’ve you been?",
                    "Ayo!",
                    "Back again, huh?",
                    "Look who it is!",
                    "Hey champ!",
                    "Hey stranger!",
                    "Sup bro!",
                    "Yo, what’s good?",
                    "Hey hey!",
                    "Top of the morning!",
                    "Hey legend!",
                    "You again?",
                    "Welcome, human.",
                    "Ah, it’s you!",
                    "What’s cookin’?",
                    "Glad you dropped by!",
                    "Good to see you again!",
                    "Yo, ready for this?",
                    "Hey boss!",
                    "Hail!",
                    "Salutations!",
                    "How do you do?",
                    "Hey sunshine!",
                    "Yo, my friend!",
                    "Greetings, hero!",
                    "Hey you!",
                    "Hey there, friend!",
                    "What’s new?",
                    "Nice to meet you!",
                    "Good day!",
                    "Oh, hi!",
                    "Hey player!"
                };


            //Index in relation to total length of the array
            int greetIndex = rand.Next(greets.Length);
            string greet = greets[greetIndex];

            return greet;



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
            //Check with Chat if there's a logic problem with emoji and respawn
            if (PosX <= 0)
            {
                PosX = (maxX * 2) - 8;
            }
            
            else if (PosX >= (maxX * 2))
            {
                PosX = 8;
            }
              

            // Vertical wrapping
            if (PosY < 0)
            {
                PosY = maxY - 2;
            }

            else if (PosY >= maxY)
            {
                PosY = 2;
            }
                

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

        //Standard police greets citizen
        public override void Greet(string otherPersonName, int x, int y)
        {
            Console.WriteLine($"Officer {this.FirstName} {this.SurName} greets a citizen of the city");
        }

        //Greet other police
        public void GreetPolice(string otherPersonName, int x, int y)
        {
            Console.WriteLine($"Officer {base.FirstName} {base.SurName} says: What a horrible situation we have with these crimninals in this city, {otherPersonName} huh? At pos {x}, {y}");
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
            Sprite = "🧍";
        }

        //Citizens greet either another citizen or a cop
        void Greet() { }
    }

}









