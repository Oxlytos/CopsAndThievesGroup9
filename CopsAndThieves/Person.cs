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

        protected static Random rand = new Random();

        protected static int mapX; 
        internal static int mapY;

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
        public virtual string Greet(Person other, int x, int y)
        {
            string randomGreet = RandomGreet();
            string theGreet = $"{this.Sprite}💬 {this.FirstName} {this.SurName} greets {other.Sprite} {other.FirstName} at x:{x} y:{y}: \"{randomGreet}\"";
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

       
        public virtual void Move(int maxX, int maxY)
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
                PosX = maxX - 1;
            }

            else if (PosX >= maxX+1)
            {

                //Left
                PosX = 1;
            }

            // Vertical wrapping
            if (PosY < 1)
            {
                //Bottom
                PosY = maxY;
            }

            else if (PosY >= maxY+1)
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
        public override string Greet(Person otherPersonName, int x, int y)
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
        public string Arrest(Theif arrestTarget)
        {
            arrestTarget.inPrison = true;
            arrestTarget.HoursInPrison = 5;
            return $"{this.Sprite}💬 I'm putting you away! {arrestTarget.Sprite} {arrestTarget.FirstName}";
        }

      
    }

    public class Theif : Person
    {
        public bool inPrison = false;

        public int HoursInPrison {  get; set; }

        public DateTime RelaseDate { get; set; }

        public Theif(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
            Sprite = "🦹";
        }

        public DateTime SetReleaseDate(DateTime orgDate)
        {
            RelaseDate = orgDate.AddHours(HoursInPrison);
            return RelaseDate;
        }

        public DateTime GetReleaseDate()
        {
            return RelaseDate;
        }
        //Steal from a citizen
        public void Steal(Citizen cit)
        {
            //Sno Från den andras lista
            //Sno(cit.Inventory[RandomNumber]);
        }

        public void Move(int prisonMaxX, int prisonMaxY, int prisonPosY)
        {

            if (inPrison)
            {

              //  Console.WriteLine("I should be in prison...");
                PrisonMove(prisonMaxX, prisonMaxY, prisonPosY);
            }
            else
            {
                base.Move(mapX, mapY);
            }

        }

        void PrisonMove(int maxX, int maxY, int startYPos)
        {
           //Console.WriteLine("What a shitty prison");
            //Before we change position, store old position
            this.PosX = rand.Next(2,6);
            this.PosY = maxY;
            OldPosX = PosX;
            OldPosY = PosY;

            int x = rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Change delta
            PosX += x;
            PosY += y;


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
            if (PosY < maxY)
            {
                //Bottom
                PosY = maxY+2;
            }

            else if (PosY >= maxY*2)
            {
                //Top
                PosY = 1;
            }

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
   

    









