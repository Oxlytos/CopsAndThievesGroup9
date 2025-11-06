using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class Person
    {
        //Base person stuff
        public string FirstName { get; set; }
        public string SurName { get; set; }

        public List<Item> Inventory = new List<Item>();
        public string Sprite { get; set; } = "⬜";

        protected static Random rand = new Random();

        protected static int mapX; 
        internal static int mapY;

        //Position variable
        public int PosX, PosY;

        //Old position for clearing
        public int OldPosX, OldPosY;

        //Where we spawn
        public void SpawnRandomPosition(int mapX, int mapY)
        {
            //3 spawns a person a bit to the right of the left most wall and below top most wall
            int x = rand.Next(3, mapX);
            int y = rand.Next(3, mapY);

            //These are the coordinates that we'll write them on later
            PosX = x;
            PosY = y;
        }

        //Greet another person
        public virtual string Greet(Person other, int x, int y)
        {
            //Get a lil greeting
            string randomGreet = RandomGreet();
            string theGreet = $"{this.Sprite}💬 {this.FirstName} {this.SurName} greets {other.Sprite} {other.FirstName} at x:{x} y:{y}: \"{randomGreet}\"";
            return theGreet;
        }

        string RandomGreet()
        {
            //Real basic greetings
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

        

       
        //Base class move that everyone besides an inprisoned theif can do
        public virtual void Move(int maxX, int maxY)
        {
            //Before we change position, store old position
            OldPosX = PosX;
            OldPosY = PosY;

            //Random value to move with
            int x = rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Change delta aka position
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

    //The police are people in this simulation
    public class Police : Person
    {
        //They have the unique property of a confiscated list of items
        public List<Item> ConfiscatedItems = new List<Item>();

        //All police share the same sprite
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

        //Police to police
        public string PoliceGreet(Police otherOfficer, int x, int y)
        {
            string randoGreet = RandomGreet(otherOfficer);
            string greet = $"{this.Sprite} Officer {this.FirstName} greets officer {otherOfficer.Sprite} {otherOfficer.FirstName}: \"{randoGreet}\"";
            return greet;

        }

        //Arrest a criminal
        public string Arrest(Theif arrestTarget)
        {
            //Make sure we're not imprisoning someone who haven't commited a crime yet, that'd be illegal
            if (arrestTarget.Inventory.Count > 0 && !arrestTarget.inPrison) 
            {
                //Send em' to prison
                arrestTarget.inPrison = true;
                arrestTarget.HoursInPrison = 10*arrestTarget.Inventory.Count;
                return $"{this.Sprite}💬 I'm putting you away! {arrestTarget.Sprite} {arrestTarget.FirstName}";
            }
            else
            {
                //Don't
                return null;
            }
           
        }

        public string Confiscate(Theif arrestedTheif)
        {
            //Confiscate if there's stuff
            if(arrestedTheif.Inventory.Count > 0)
            {
                //Get a total as an int for convencience
                int amountOfItems = arrestedTheif.Inventory.Count();

                //10 hours * how much was stolen
                int time = 10 * amountOfItems;

                //Add the vhieves inventory to the police's, the confiscated list could grow endless?
                //In cops and theives 2, add a depo to deposit confiscated items
                ConfiscatedItems.AddRange(arrestedTheif.Inventory);

                // Clear the inventory
                arrestedTheif.Inventory.Clear();
                return $"{this.Sprite} {this.FirstName} has confscated {amountOfItems} items from {arrestedTheif.Sprite} {arrestedTheif.FirstName}";
            }
            else
            {
                return null;
            }
        }
        //Police to civilian
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
        //Police to police
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

    //Theives are people
    public class Theif : Person
    {
        //The unique property to be jailed
        public bool inPrison = false;

        //Prison sentence
        public int HoursInPrison {  get; set; }

        //Returning the release date for later
        DateTime RelaseDate { get; set; }

        public Theif(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
            Sprite = "🦹";
        }
        //Call this after being arrested and getting put in prison
        public DateTime SetReleaseDate(DateTime orgDate)
        {
            RelaseDate = orgDate.AddHours(HoursInPrison);
            return RelaseDate;
        }
        //Check release date
        public DateTime GetReleaseDate()
        {
            return RelaseDate;
        }
        //Steal from a citizen
        //We check in the simulation if they've got any items
        public string Steal(Citizen cit)
        {
            int RandomIndex = rand.Next(0, cit.Inventory.Count);
            Item stolenItem;
            stolenItem = cit.Inventory[RandomIndex];
           // this.Inventory = new List<string>();

            this.Inventory.Add(stolenItem);
            cit.Inventory.RemoveAt(RandomIndex);
            return $"{this.Sprite} {this.FirstName} has stolen {stolenItem.Name} from {cit.FirstName}";
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

        //The big difference is just that the prison has other coordinates and that we inlucde min values for clarities sake
        void PrisonMove(int minX, int maxX, int minY, int maxY)
        {
            //Before we change position, store old position
            OldPosX = PosX;
            OldPosY = PosY;

            //Give some random
            int x = rand.Next(-1, 2);
            int y = rand.Next(-1, 2);

            //Move like the standard Move
            PosX+=x;
            PosY+=y;

            // Horizontal reach around
            if (PosX < minX) PosX = maxX;
            else if (PosX > maxX) PosX = minX;

            //Same but vertical
            if (PosY < minY) PosY = maxY;
            else if (PosY > maxY) PosY = minY;
        }
    }

    //Citizens use the base class greets when talking to other Citizens
    public class Citizen : Person
    {
        public Citizen(string fName, string sName)
        {
            FirstName = fName;
            SurName = sName;
        }

        //Citizens greet either another citizen or a cop
        public string Greet(Police officer, int x, int y)
        {
            string randoGreet = RandomGreet(officer);
            string greet = $"{this.Sprite} {this.FirstName} greets officer {officer.Sprite} {officer.FirstName}: \"{randoGreet}\"";
            return greet;

        }
        //Citizen to police
        static string RandomGreet(Police otherOfficer)
        {
            string[] greets = {
               "Pig!",
               "Sup coppo",
               "ACAB!",
               "Officer...",
               "Order and justice!"
            };

            //Index in relation to total length of the array
            int greetIndex = rand.Next(greets.Length);
            string greet = greets[greetIndex];

            return greet;
        }
    }
}
   

    









