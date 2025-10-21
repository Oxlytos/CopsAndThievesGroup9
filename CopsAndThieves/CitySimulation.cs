using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;


namespace CopsAndThieves
{
    internal class CitySimulation
    {
        public static int width = 12;
        public static int height = 12;

        static int prisonHeight = 6;
        public static int prisonWidth = 6;


        static int maxAmountOfCivllians = 15;


        static List<Person> allPeopple = new List<Person>();

        static List<Police> thePolice = new List<Police>();
        static List<Citizen> citizens = new List<Citizen>();
        static List<Theif> theives = new List<Theif>();

        static NewsFeed feed = new NewsFeed(height + 5, Console.WindowWidth);

        static DateTime inGameDate = DateTime.Now;

        public static void Run()
        {
            //Create some people
            CreatePeople();
            inGameDate.AddHours(1);

            //No visible cursor
            Console.CursorVisible = false;

            //Draw city with some dimensions
            DrawCity();
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, height+2);
            DrawPrison();
          
            //Constantly draw people as they do "logic"/stuff
            while (true) 
            {
                DrawInhabitants();
                //Pause each turn
                Thread.Sleep(200);
            
            }
        }

        static void CreatePeople()
        {
            for (int i = 0; i <= maxAmountOfCivllians; i++)
            {
                //Create citizen, add to list
                citizens.Add(GeneratePerson.GenerateRandomCititzen());

                //Creates a random spawn point
                citizens.ElementAt(i).SpawnRandomPosition(width, height);

                //Add citizen(i) till main list
                allPeopple.Add(citizens.ElementAt(i));
            }

            //Every 3 people spawns a cop at the start
            for (int i = 0; i <= 5; i++)
            {
                thePolice.Add(GeneratePerson.GenerateRandomPolice());
                thePolice.ElementAt(i).SpawnRandomPosition(width, height);

                allPeopple.Add(thePolice.ElementAt(i));


            }

            //Every 4 people create a theif
            for (int i = 0; i <= 5; i++)
            {
                theives.Add(GeneratePerson.GenerateRandomTheif());
                theives.ElementAt(i).SpawnRandomPosition(width, height);

                allPeopple.Add(theives.ElementAt(i));
            }

        }

        static void DrawCity()
        {
          //  Console.Clear();
            string wall = "🧱";
            string empty = "⬜";

            // Top
            for (int x = 0; x <= width + 1; x++)
            {
                Console.Write(wall);
            }
            Console.WriteLine();

            // Middle part with walls and empty spaces
            for (int y = 0; y < height; y++)
            {
                Console.Write(wall);
                for (int x = 0; x < width; x++)
                {
                        Console.Write(empty);
                }

                Console.WriteLine(wall);
            }

            // Bottom
            for (int x = 0; x <= width + 1; x++) 
            {
                Console.Write(wall); 
            }
            Console.WriteLine("\nESTONIA\n");

        }

        static void DrawPrison()
        {
          //  Console.Clear();
            string wall = "🧱";
            string empty = "⬜";

            //Get Top LEft X Value
            int top = Console.CursorTop +1;
            for (int x = 0; x <= prisonHeight + 1; x++)
            {
                Console.Write(wall);
            }
            Console.WriteLine();

            // Middle part with walls and empty spaces
            for (int y = 0; y < prisonWidth; y++)
            {
                Console.Write(wall);
                for (int x = 0; x < prisonHeight; x++)
                {
                    Console.Write(empty);
                }

                Console.WriteLine(wall);
            }

            // Bottom
            for (int x = 0; x <= prisonWidth + 1; x++)
            {
                Console.Write(wall);
            }
            //Get bottom right X Value
            Console.WriteLine();

        }
        static void DrawInhabitants()
        {
            //Draw the people in the city ON the city graphic

            //Remove all sprites
            //Write empty thing
            foreach (var pop in allPeopple)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                // Erase old position
                Console.SetCursorPosition(pop.PosX * 2, pop.PosY + 1);
                //Write emoji
                Console.Write("⬜");

                // Move after erase
                pop.Move(width, height);

                // Draw at new position
                Console.SetCursorPosition(pop.PosX * 2, pop.PosY + 1);
                Console.Write(pop.Sprite);
            }

            List<(int, int)> collisions = DetectCollisions();


            DrawCollisions(collisions);

            /////Greet/Collision part
            //////////////////////////////////////////////////////////
            ///
           

            //In all of the plice
            //Check if a theif has the same position
            foreach(Police cop in thePolice)
            {
                foreach(Theif theif in theives)
                {
                    if(cop.PosX == theif.PosX && cop.PosY == theif.PosY)
                    {
                        feed.AddMsg($"{cop.Sprite} {cop.Arrest()}");
                        cop.Arrest(theif);
                        theif.SetReleaseDate(inGameDate);
                       // Console.WriteLine($"Theif free on {theif.RelaseDate}");
                    }
                }
            }


            foreach (Citizen ciz in citizens)
            {
                //Nested loop to check other citizens with X citizen we have
                foreach (Citizen hitPerson in citizens)
                {
                    //If we're not the same person i both lists
                    if (ciz != hitPerson)
                    {
                        //If we've collided
                        if (ciz.PosX == hitPerson.PosX && ciz.PosY == hitPerson.PosY)
                        {

                            //Do the greet
                            string msg = ciz.Greet(hitPerson.FirstName, ciz.PosX, ciz.PosY);
                            //tjuv.Sno(annanPErson)
                            //feed.AddMsg(msg);
                            //Thread.Sleep(1000);
                          
                          //  Console.Beep();
                          //Pause
                        }
                    }
                    else
                    {
                        //Nothing
                    }
                }
            }
            //Police greetings
            foreach(Police polA in thePolice)
            {
                //Check police A collides with other police
                foreach (Police polB in thePolice) 
                {
                    if(polB != polA)
                    {
                        if(polB.PosX == polA.PosX && polB.PosY == polA.PosY)
                        {
                           // polA.GreetPolice(polB.SurName, polB.PosX, polB.PosY);
                          //  Console.Beep();
                        }
                    }
                
                }
            }

            foreach (Theif prisonedTheif in theives)
            {
                if (prisonedTheif.inPrison == true)
                {
                    DateTime checkIfFreeDate = prisonedTheif.GetReleaseDate();
                    if (checkIfFreeDate < inGameDate)
                    {
                        prisonedTheif.inPrison = false;
                        feed.AddMsg($"{prisonedTheif.Sprite} {prisonedTheif.FirstName} is free!");
                       /// Console.WriteLine($"{prisonedTheif.FirstName} is free");
                        break;

                    }


                    Console.SetCursorPosition(prisonedTheif.PosX * 2, prisonedTheif.PosY + 1);
                    //Write emoji
                    Console.Write("⬜");

                    // Width of prison, heihght, 
                    prisonedTheif.Move(14, 15, height +2);

                    // Draw at new position
                    Console.SetCursorPosition(prisonedTheif.PosX * 2, prisonedTheif.PosY + 1);
                    Console.Write(prisonedTheif.Sprite);
                }
            
            }

            foreach(Theif thivevingTheif in theives) 
            {
                foreach(Citizen robTarget in citizens)
                {
                    //Check if position is the same

                    //Steal something
                }
            
            }
            //Console.WriteLine(inGameDate);
            inGameDate = inGameDate.AddHours(1);



        }

        //Handle Collision
        static List<(int,int)> DetectCollisions()
        {
            //2d list
            var listOfCollisionPositions = new List<(int, int)>();

            //Foreach or For loop
            //Check if subject A or I has hit another
            //If a for loop, count from 0-max (0 is person number 0 could be Erik Eriksson)
            //As it's nested make sure to check that its different number => Erik Eriksson shouldn't collide with himself
            //Basic check with thingA != thingB => then add
            for(int subjectA = 0; subjectA < allPeopple.Count; subjectA++)
            {
                //Subject B or J
                for(int subjectB = 0; subjectB < allPeopple.Count; subjectB++)
                {
                    if (subjectA != subjectB)
                    {
                        //If they have the exact same X and Y coordinates
                        if (allPeopple[subjectA].PosX == allPeopple[subjectB].PosX &&
                            allPeopple[subjectA].PosY == allPeopple[subjectB].PosY)
                        {
                            //They've collided
                            listOfCollisionPositions.Add((allPeopple[subjectA].PosX, allPeopple[subjectA].PosY));
                        }
                    }
                    
                }
            }


            return listOfCollisionPositions.Distinct().ToList();
        }
        static void DrawCollisions(List<(int x, int y)> listOfCollisionPositions)
        {
            //Var becuase let God handle the type
            foreach (var collisionPoint in listOfCollisionPositions) 
            {
                //The old colors
                var oldBg = Console.BackgroundColor;
                var oldFg = Console.ForegroundColor;

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;

                //Handle the damn emoji size
                Console.SetCursorPosition(collisionPoint.x * 2, collisionPoint.y + 1);
                Console.Write("💥");

                // Reset colors to initial type
                Console.BackgroundColor = oldBg;
                Console.ForegroundColor = oldFg;
            }
        }
    }

   

    
}
