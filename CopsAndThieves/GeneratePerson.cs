using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class GeneratePerson
    {
        static Random random = new Random();
        //Names from ChatGPT can't be arsed to ínput 60 different names
        static string[] firstNames =
         {
            // Swed (20)
            "Erik", "Johan", "Anna", "Maria", "Lars", "Karin", "Anders", "Eva", "Joel",
            "Per", "Sara", "Karl", "Emma", "Mats", "Lisa", "Henrik", "Julia",
            "Fredrik", "Sofia", "Daniel",

            // Amer (20)
            "Michael", "Jessica", "John", "Emily", "David", "Ashley", "James", "Hannah",
            "Robert", "Olivia", "William", "Ava", "Matthew", "Chloe", "Christopher", "Grace",
            "Andrew", "Megan", "Joshua", "Abigail",

            // Mid east (20)
            "Ahmed", "Fatima", "Ali", "Layla", "Omar", "Aisha", "Yusuf", "Zara",
            "Hassan", "Noor", "Ibrahim", "Mariam", "Samir", "Leila", "Khalid", "Rania",
            "Tariq", "Salma", "Nadia", "Amir"
        };

        //Surnames from ChatGPT in the same manner
        static string[] surNames =
        {
            // Swedi (20)
            "Stormsten", "Eriksson", "Johansson", "Andersson", "Karlsson", "Nilsson",
            "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson",
            "Lindberg", "Lundqvist", "Bergström", "Lindgren", "Sandberg", "Hansson",
            "Berg", "Holm",

            // American (20)
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Wilson",
            "Taylor", "Anderson", "Thomas", "Moore", "Jackson", "White", "Harris",
            "Martin", "Thompson", "Garcia", "Martinez", "Clark",

            // Mid east (20)
            "Al-Farsi", "Haddad", "Rahman", "Khatib", "Darwish", "Najjar", "Saidi", "Mansour",
            "Aziz", "Saleh", "Nasser", "Abboud", "Tariq", "Hakim", "Farouk", "Zayed",
            "Shadid", "Hosseini", "Bakir", "Al-Mansouri"
        };

        static string RandomSprite()
        {
            string[] sprites = { // Person standing (neutral)
            "🧍",
            "🧍🏻", "🧍🏼", "🧍🏽", "🧍🏾", "🧍🏿",

            // Woman standing
            "🧍‍♀️",
            "🧍🏻‍♀️", "🧍🏼‍♀️", "🧍🏽‍♀️", "🧍🏾‍♀️", "🧍🏿‍♀️",

            // Man standing
            "🧍‍♂️",
            "🧍🏻‍♂️", "🧍🏼‍♂️", "🧍🏽‍♂️", "🧍🏾‍♂️", "🧍🏿‍♂️"};

            int randInd = random.Next(0, sprites.Length);
            string randomSprite = sprites[randInd];
            return randomSprite;

        }


        //Generate basic person with random name
        public static Person GenerateRandomPerson()
        {
            //Creating something to return
            Person person = new Person();
            //First name is this names(withIndex)
            string fname =  GetFirstName();
            string sName = GetSurName();
            string sprite = RandomSprite();

            person.FirstName = fname;
            person.SurName = sName;
            person.Sprite = sprite; 
            
            return person;
        }

        public static Citizen GenerateRandomCititzen()
        {
           
            //First name is this names(withIndex)
            string fname = GetFirstName();
            string sName = GetSurName();
            string sprite = RandomSprite();

            //Creating something to return
            Citizen person = new Citizen(fname,sName);

            //Create a base police with name

            //In their inventory becomes a new empty list
            person.Inventory = new List<Item>();
            //pol.SpawnRandomPosition();

            //Until there's 3 items in their inventory
            while (person.Inventory.Count < 4)
            {
                //Get a lil civilian item
                Item civItem = new CivilianItem("",person);

                //If we don't already have this item
                if (!person.Inventory.Contains(civItem))
                {
                    //Add that item
                    person.Inventory.Add(civItem);
                }

            }

            //Assign names to this person so we can spawn them later
            person.Sprite = sprite;


            return person;
        }
        public static Theif GenerateRandomTheif()
        {
            //First name is this names(withIndex)
            string fname = GetFirstName();
            string sName = GetSurName();

            //Creating something to return
            Theif person = new Theif(fname, sName);

            return person;
        }

        //Generating a police is similar but a bit different
        public static Police GenerateRandomPolice()
        {
            //Random instance
            Random random = new Random();

            //Random amount of items decider/generator
            int randomAmount = random.Next(1,10);

            //Their literal name rn is Konstapel Eric for ex, a title field could be used here instead
            string fname = GetFirstName();
            string sName = GetSurName();

            //Create a base police with name
            Police pol = new Police(fname, sName);

            //In their inventory becomes a new empty list
            pol.Inventory = new List<Item>();

        
            //pol.SpawnRandomPosition();
            //Until there's 3 items in their inventory
            while (pol.Inventory.Count < 4) 
            {
                //Dummy item class for testing
                PoliceItem polItem = new PoliceItem("", pol);

                //If we don't already have this item
                if (!pol.Inventory.Contains(polItem))
                {
                    //Add that item
                    pol.Inventory.Add(polItem);
                }
            
            }

            return pol;
        }

        //Return a string with a name
        static string GetFirstName()
        {
            //Index in relation to total length of the array
            int fNameIndex = random.Next(firstNames.Length);
            string firstName = firstNames[fNameIndex];  

            return firstName;
        }

        //Surname from array of surnames
        static string GetSurName() 
        {
            //Index in relation to total length of the array
            int sNameIndex = random.Next(surNames.Length);
            string surName = surNames[sNameIndex];

            return surName;
        }

    }
}
