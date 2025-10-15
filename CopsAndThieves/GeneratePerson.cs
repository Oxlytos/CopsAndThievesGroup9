using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class GeneratePerson
    {

        public static void TestRun()
        {
            //People in the city
            List<Person> people = new List<Person>();

            //The Police force of said city
            List<Police> thePolice = new List<Police>();


            //12 people for starters
            for (int i = 0; i < 12; i++) 
            {
              people.Add(GenerateRandomPerson());
            
            }

            //For every 3 people generate this many police (if 12 people => 4 police)
            for(int i = 0;i<people.Count/3;i++)
            {
                thePolice.Add(GenerateRandomPolice());
            }

            //Lets print the people in the console 
            foreach (Person person in people) 
            {
                Console.WriteLine($"{person.FirstName} {person.SurName} \n");
            
            }

            //The police with their items
            foreach (Police police in thePolice)
            {
                //Display some ultra basic inventory insights
                Console.WriteLine($"Police {police.FirstName} {police.SurName} - Inventory:");

                //Loop through their inventory
                for (int k = 0; k<police.Inventory.Count; k++)
                {
                    //If we're at the last item, don't include a ", "
                    if (k == police.Inventory.Count-1)
                    {
                        Console.Write(police.Inventory.ElementAt(k));
                    }
                    //All other items continue with ", " until the last one
                    else
                    {
                        Console.Write(police.Inventory.ElementAt(k) + ", ");
                    }
                    //New row
                    Console.Write("\n");
                }
                Console.Write("\n");
            }
        }

        //Names from ChatGPT can't be arsed to ínput 60 different names
        static string[] firstNames =
         {
            // Svenska (20)
            "Erik", "Johan", "Anna", "Maria", "Lars", "Karin", "Anders", "Eva", "Joel",
            "Per", "Sara", "Karl", "Emma", "Mats", "Lisa", "Henrik", "Julia",
            "Fredrik", "Sofia", "Daniel",

            // Amerikanska (20)
            "Michael", "Jessica", "John", "Emily", "David", "Ashley", "James", "Hannah",
            "Robert", "Olivia", "William", "Ava", "Matthew", "Chloe", "Christopher", "Grace",
            "Andrew", "Megan", "Joshua", "Abigail",

            // Mellanöstern (20)
            "Ahmed", "Fatima", "Ali", "Layla", "Omar", "Aisha", "Yusuf", "Zara",
            "Hassan", "Noor", "Ibrahim", "Mariam", "Samir", "Leila", "Khalid", "Rania",
            "Tariq", "Salma", "Nadia", "Amir"
        };

        //Surnames from ChatGPT in the same manner
        static string[] surNames =
        {
            // Svenska (20)
            "Stormsten", "Eriksson", "Johansson", "Andersson", "Karlsson", "Nilsson",
            "Larsson", "Olsson", "Persson", "Svensson", "Gustafsson", "Pettersson",
            "Lindberg", "Lundqvist", "Bergström", "Lindgren", "Sandberg", "Hansson",
            "Berg", "Holm",

            // Amerikanska (20)
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Miller", "Davis", "Wilson",
            "Taylor", "Anderson", "Thomas", "Moore", "Jackson", "White", "Harris",
            "Martin", "Thompson", "Garcia", "Martinez", "Clark",

            // Mellanöstern (20)
            "Al-Farsi", "Haddad", "Rahman", "Khatib", "Darwish", "Najjar", "Saidi", "Mansour",
            "Aziz", "Saleh", "Nasser", "Abboud", "Tariq", "Hakim", "Farouk", "Zayed",
            "Shadid", "Hosseini", "Bakir", "Al-Mansouri"
        };


        //Generate basic person with random name
        public static Person GenerateRandomPerson()
        {
            //Creating something to return
            Person person = new Person();

            //Random instance thing to use
            Random random = new Random();


            //First name is this names(withIndex)
            string fname =  GetFirstName(random);
            string sName = GetSurName(random);

            person.FirstName = fname;
            person.SurName = sName;

            
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
            string fname = "Konstapel " + GetFirstName(random);
            string sName = GetSurName(random);

            //Create a base police with name
            Police pol = new Police(fname, sName);

            //In their inventory becomes a new empty list
            pol.Inventory = new List<string>();


            //Until there's 3 items in their inventory
            while (pol.Inventory.Count < 3) 
            {
                //Dummy item class for testing
                string exampleItem = new DummyItem().PossibleName;

                //If we don't already have this item
                if (!pol.Inventory.Contains(exampleItem))
                {
                    //Add that item
                    pol.Inventory.Add(exampleItem);
                }
            
            }

            return pol;
        }

        //Return a string with a name
        static string GetFirstName(Random rando)
        {
            //Index in relation to total length of the array
            int fNameIndex = rando.Next(firstNames.Length);
            string firstName = firstNames[fNameIndex];  

            return firstName;
        }

        //Surname from array of surnames
        static string GetSurName(Random rando) 
        {
            //Index in relation to total length of the array
            int sNameIndex = rando.Next(surNames.Length);
            string surName = surNames[sNameIndex];

            return surName;
        }


        //In the future => Change the list of person class to include <Item> in the list instread of <string>
        //Don't wanna start changing around in the Person class as of today
         class DummyItem
        {
            //In a real use case scenario, decide what object it is first (Like some phone), then a fitting name (From a list of phone names => "Samsung s23" , then som value "Random or accurate"
            public string PossibleName { get; set; }

            //ChatGPT random 60 strings of everyday and other items, there's a sniper rifle in there
            static string[] itemNames =
             {
                "Phone", "Wallet", "Wireless Earbuds", "Gun", "Bubblegum", "Cigarettes",
                "Chocolate Bar", "Leather Gloves", "Sniper Rifle", "Gym Card", "Debit Card",
                "Notebook", "Pen", "Flashlight", "Pocket Knife", "Lighter", "Water Bottle",
                "Sunglasses", "USB Drive", "Keys", "Smartwatch", "Baseball Cap", "Scarf",
                "Energy Drink", "Snack Bar", "Headphones", "Hand Sanitizer", "Map", "Compass",
                "Backpack", "Lip Balm", "Bandages", "Book", "Passport", "Camera", "Battery Pack",
                "Matchbox", "Small First Aid Kit", "Coffee Cup", "Tissues", "Handgun", "Magazine",
                "Car Keys", "Toolbox", "ID Badge", "Lockpick Set", "Binoculars", "Notebook Paper",
                "Bluetooth Speaker", "Multitool", "Rope", "Spray Paint", "Credit Card", "Phone Charger",
                "Chewing Tobacco", "Lotion", "Perfume Bottle", "Notepad", "Playing Cards", "Flash Drive"
            };

            //DummyItem is a class that gets assigned a random string
            public DummyItem() 
            {
                Random random = new Random();
                int randomNameIndex = random.Next(itemNames.Length);
                string randoName = itemNames[randomNameIndex];

                PossibleName = randoName;

            }
        }
    }
}
