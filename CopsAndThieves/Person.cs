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

        public List<string> Inventory = new List<string>();
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
                    "Hey!",
                    "Hi!",
                    "Hello!",
                    "Yo!",
                    "Yoo!",
                    "Sup?",
                    "Heya!",
                    "Howdy!",
                    "Greetings!",
                    "What’s up?",
                    "Hiya!",
                    "Yo yo!",
                    "Ello!",
                    "Hey there!",
                    "Wassup!",
                    "Salutations!",
                    "Hola!",
                    "Ahoy!",
                    "Yo mate!",
                    "Good day!"
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
        public List<string> ConfiscatedItems = new List<string>();

        public Police(string fName, string sName)
        {

            FirstName = fName;
            SurName = sName;
            Sprite = "👮";
        }

        //Standard police greets citizen
        public override string Greet(Person citizen, int x, int y)
        {
            
            string randoGreet = RandomGreet();
            string greet = $"{this.Sprite} Officer {this.FirstName} greets {citizen.Sprite} {citizen.FirstName}: \"{randoGreet}\"";
            return greet;
            

        }
        public string PoliceGreet(Police otherOfficer, int x, int y)
        {
            string randoGreet = RandomGreet(otherOfficer);
            string greet = $"{this.Sprite} Officer {this.FirstName} greets officer {otherOfficer.Sprite} {otherOfficer.FirstName}: \"{randoGreet}\"";
            return greet;

        }


        //Arrest a criminal
        public string Arrest(Theif arrestTarget)
        {
            if (arrestTarget.Inventory.Count > 0 && !arrestTarget.inPrison) 
            {
                arrestTarget.inPrison = true;
                arrestTarget.HoursInPrison = 10*arrestTarget.Inventory.Count;
                return $"{this.Sprite}💬 I'm putting you away! {arrestTarget.Sprite} {arrestTarget.FirstName}";
            }
            else
            {
                return null;
            }
           
        }

        public string Confiscate(Theif arrestedTheif)
        {
            if(arrestedTheif.Inventory.Count > 0)
            {
                int amountOfItems = arrestedTheif.Inventory.Count();

                int time = 10 * arrestedTheif.Inventory.Count();
                for (int i = 0; i < arrestedTheif.Inventory.Count; i++)
                {
                    ConfiscatedItems.Add(arrestedTheif.Inventory[i]);
                }


                //  Console.WriteLine("Counted items in total?: " + amountOfItems);
                arrestedTheif.Inventory.Clear();
                return $"{this.Sprite} {this.FirstName} {ConfiscatedItems.Count()} has confscated {amountOfItems} items from {arrestedTheif.Sprite} {arrestedTheif.FirstName}";
            }
            else
            {
                return null;
            }

           



        }
        static string RandomGreet()
        {
            string[] greets = {
                "Evening, citizen!",
                "Stay out of trouble!",
                "All quiet tonight?",
                "Keep it lawful!",
                "Patrol says hi!",
                "Stay safe out there!",
                "Watch yourself!",
                "Greetings, citizen!",
                "Everything under control?",
                "Stay sharp!"
            };

            //Index in relation to total length of the array
            int greetIndex = rand.Next(greets.Length);
            string greet = greets[greetIndex];

            return greet;
        }
        static string RandomGreet(Police otherOfficer)
        {
            string[] greets = {
                "Hey, partner!",
                "All good?",
                "Patrol check-in!",
                "Stay sharp!",
                "Copy that!",
                "Looking busy?",
                "Got your six!",
                "On it!",
                "Move out!",
                "Watch your back!"
            };

            //Index in relation to total length of the array
            int greetIndex = rand.Next(greets.Length);
            string greet = greets[greetIndex];

            return greet;
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
        public string Steal(Citizen cit)
        {

            int RandomIndex = rand.Next(0, cit.Inventory.Count);
            string stolenItem;
            stolenItem = cit.Inventory[RandomIndex];
           // this.Inventory = new List<string>();

            this.Inventory.Add(stolenItem);
            cit.Inventory.RemoveAt(RandomIndex);
            return $"{this.Sprite} {this.FirstName} has stolen {stolenItem} from {cit.FirstName}";


            //Sno Från den andras lista
            //Sno(cit.Inventory[RandomNumber]);

        }

        public void Move(int minX, int maxX, int minY, int maxY)
        {

            if (inPrison)
            {

              //  Console.WriteLine("I should be in prison...");
                PrisonMove(minX,maxX,minY,maxY);
            }
            else
            {
                base.Move(mapX, mapY);
            }

        }

        void PrisonMove(int minX, int maxX, int minY, int maxY)
        {
            //Console.WriteLine("What a shitty prison");
            //Before we change position, store old position
            //Check if crossing limits

            OldPosX = PosX;
            OldPosY = PosY;

            int x = rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Move
            //PosX++;
            PosY++;

            // Wrap horizontally
            if (PosX < minX) PosX = maxX;
            else if (PosX > maxX) PosX = minX;

            // Wrap vertically
            if (PosY < minY) PosY = maxY;
            else if (PosY > maxY) PosY = minY;
        }
    }

    public class Citizen : Person
    {
        public Citizen(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
            Inventory.Add("Potato");
        }

        //Citizens greet either another citizen or a cop
        void Greet() { }
    }
}
   

    









