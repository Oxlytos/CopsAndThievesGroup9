using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;


namespace CopsAndThieves
{
    internal class CitySimulation
    {
        public static int width = 60;
        public static int height = 12;

        static int prisonHeight = 10;
        public static int prisonWidth = 10;


        static int civilCount = 15;


        static List<Person> allPeopple = new List<Person>();

        static List<Police> thePolice = new List<Police>();
        static List<Citizen> citizens = new List<Citizen>();
        static List<Theif> theives = new List<Theif>();

        //Height is the cities combined + 5 for spacing
        static NewsFeed feed = new NewsFeed(height + prisonHeight + 5);

        static DateTime inGameDate = DateTime.Now;


        public static void Run()
        {
            //Create some people
            CreatePeople();
            inGameDate.AddHours(1);
            feed.SimulationDate = inGameDate;

            //No visible cursor
            Console.CursorVisible = false;

            //Draw city with some dimensions
            DrawCity();
            Thread.Sleep(1500);
            Console.SetCursorPosition(0, height + 2);
            DrawPrison();

            //Constantly draw people as they do "logic"/stuff
            while (true)
            {
                if ((Console.WindowWidth < 70 || Console.WindowHeight < 20))
                {
                    feed.AddImportant(inGameDate.ToString("dd-HH") + "CONSOLE WINDOW MAY BE TO SMALL FOR SIMULATION TO RUN SMOOTHLY! PROCCEED AT YOUR OWN RISK!");
                    Thread.Sleep(1000);
                }
                try
                {
                    //Methods broken up for claritys sake
                    //Move and handle interactions

                    UpdateMovement();

                    //Draw people after they've moved
                    DrawInhabitants();

                    //Interactions on the same space
                    HandleInteractions();

                    HandlePrisoners();

                    //News
                    feed.DrawMessageBoard();

                    //Progress time
                    inGameDate = inGameDate.AddHours(1);
                    feed.SimulationDate = inGameDate;
                }

                catch (ArgumentOutOfRangeException)
                {
                    //Windows might be too small to render stuff
                }
                //Pause each turn
                Thread.Sleep(1000);

            }
        }

        static void CreatePeople()
        {
            for (int i = 0; i < civilCount; i++)
            {
                //Create citizen, add to list
                citizens.Add(GeneratePerson.GenerateRandomCititzen());


                //Creates a random spawn point
                citizens.ElementAt(i).SpawnRandomPosition(width, height);

                //Add citizen(i) till main list
                allPeopple.Add(citizens.ElementAt(i));
            }
            feed.CitizensAmount = citizens.Count;

            //Every 3 people spawns a cop at the start

            for (int i = 0; i < civilCount/3; i++)
            {
                thePolice.Add(GeneratePerson.GenerateRandomPolice());
                thePolice.ElementAt(i).SpawnRandomPosition(width, height);

                allPeopple.Add(thePolice.ElementAt(i));


            }
            feed.PoliceAmount = thePolice.Count;
            //Every 4 people create a theif
            for (int i = 0; i < civilCount/4; i++)
            {
                theives.Add(GeneratePerson.GenerateRandomTheif());
                theives.ElementAt(i).SpawnRandomPosition(width, height);

                allPeopple.Add(theives.ElementAt(i));
            }
            feed.ThiefAmount = theives.Count;
        }

        static void DrawCity()
        {
            Place city = new City(width, height);
            city.Draw();
            Console.WriteLine();

        }

        static void DrawPrison()
        {
            Place prison = new Prison(prisonWidth, prisonHeight);
            prison.Draw();
        }
        //Only moves peoples value => x+-1 y+-1
        static void UpdateMovement()
        {
            foreach (var pop in allPeopple)
            {
                //If said person is a theif
                if (pop is Theif thief)
                {
                    //And they're in prison
                    if (thief.inPrison)
                    {
                        //Continue
                        //Continue as in skip the rest of the code for this pop, do the rest of the foreach people
                        feed.AddGreet($"{thief.Sprite}{thief.FirstName} sulks in the prison");
                        continue;
                    }
                }
                // Erase old position
                Console.SetCursorPosition(pop.PosX * 2, pop.PosY + 1);
                //Write emoji
                Console.Write("⬜");

                // Move after erase
                pop.Move(width, height);
            }

        }
        static Dictionary<(int, int), List<Person>> HandleCollision()
        {
            //Dictionary to keep track of people interacting with eachother
            //List is for the people sharing a space/on the same position, COULD be an infinte amount of people on the same space
            //We can read it as (0,1) : [Citizen]
            //(5,1) : [Police, Citizen)
            var hitMapping = new Dictionary<(int, int), List<Person>>(); //Why List<Person>? Base class of everything, and SHOULD remeber the original subclass always

            //People list
            foreach (var person in allPeopple)
            {
                //A X and Y position value
                var pos = (person.PosX, person.PosY);

                //If we dont already have people with coordinates here
                //Keeps track of them
                if (!hitMapping.ContainsKey(pos))
                {
                    //This position NOW keeps track of people on this position
                    hitMapping[pos] = new List<Person>(); //Open up a new list for people to be recorded here

                }
                //This point already has p people here, add this person to it
                hitMapping[pos].Add(person);
            }
            //Use this mapping elswhere
            return hitMapping;


        }


        static void HandleInteractions()
        {
            var positionMapping = HandleCollision();
            //Pos acts as a key with Int Int to check in positionMapping
            //People is like the entry as a List<People> type in positionMapping
            foreach (var (pos, peopleHere) in positionMapping)
            {
                //If there's less than 2 on a tile, don't handle any interactions, there's no one to interact with
                if (peopleHere.Count < 2) continue; //If there ARE 2 people on a tile, do all this below
                {
                    //More than 2 people on a space creates a emoji for highlight
                    Console.SetCursorPosition(pos.Item1 * 2, pos.Item2 + 1);

                    Console.Write("💥");

                    //All them cops
                    var cops = peopleHere.OfType<Police>().ToList();

                    //All them theives
                    var thievesos = peopleHere.OfType<Theif>().ToList();

                    //All them ordinaries
                    var citizens = peopleHere.OfType<Citizen>().ToList();

                    //Cops try and arrest a theif
                    foreach (var coppo in cops)
                    {
                        //This thief on this spot has to be eligle to be arrested => not in prison
                        var possibleTheifToArrest = thievesos.FirstOrDefault(t => !t.inPrison && t.Inventory.Count() > 0);

                        //If a theif on our coordinate exists and they're not in prison
                        if (possibleTheifToArrest != null)
                        {
                            //Message and arrest them
                            if (possibleTheifToArrest.Inventory.Count() > 0)
                            {
                                feed.ObjectsConfiscated += possibleTheifToArrest.Inventory.Count();
                                feed.ObjectsStolen -= possibleTheifToArrest.Inventory.Count();

                                feed.AddImportant(inGameDate.ToString("dd-HH") + coppo.Arrest(possibleTheifToArrest));

                                feed.AddImportant(inGameDate.ToString("dd-HH") + coppo.Confiscate(possibleTheifToArrest));

                                feed.ThievesInPrison++;
                                //Prison time
                                possibleTheifToArrest.SetReleaseDate(inGameDate);

                                //Makes sure that a cop can't re-arrest them every frame
                                possibleTheifToArrest.inPrison = true;
                            }


                        }
                    }
                    //Theif robs
                    foreach (var theifo in thievesos)
                    {
                        //If somoones got stuff
                        var robTarget = citizens.FirstOrDefault(c => c.Inventory.Count != 0);

                        //If they exist with items at all
                        if (robTarget != null)
                        {
                            //Print
                            feed.AddImportant(inGameDate.ToString("dd-HH") + theifo.Steal(robTarget));

                            //Stats
                            feed.CitizensRobbed++;
                            feed.ObjectsStolen++;
                        }
                    }

                    //Iterate though all citizens on every spot
                    for (int i = 0; i < citizens.Count; i++)
                    {
                        //Every other citizen on every spot (i+1 makes sure its not the same person, when i = 5 then j = 6 in the total index count)
                        //TLDR dont greet yourslef count from yourself +1
                        for (int j = i + 1; j < citizens.Count; j++)
                        {
                            //Message and greet
                            feed.AddGreet(inGameDate.ToString("dd-HH") + citizens[i].Greet(citizens[j], citizens[i].PosX, citizens[i].PosY));
                        }
                    }

                    //Cop greet
                    for (int i = 0; i < cops.Count; i++)
                    {
                        //+ 1 don't greet yourself
                        for (int j = i + 1; j < cops.Count; j++)
                        {
                            feed.AddGreet(inGameDate.ToString("dd-HH") + cops[i].PoliceGreet(cops[j], cops[i].PosX, cops[i].PosY));
                        }
                        //All citizens on this spot, start on the same index as the cop
                        for (int j = i; j < citizens.Count; j++)
                        {
                            feed.AddGreet(inGameDate.ToString("dd-HH") + cops[i].Greet(citizens[j], cops[i].PosX, cops[i].PosY));
                        }
                    }


                }
            }
        }
        //Draw the prisoners in the prison
        static void HandlePrisoners()
        {
            //Prisoned theives are theives with the inPrison is true
            foreach (Theif prisonedTheif in theives.Where(thi => thi.inPrison))
            {
                //If they should get realeased
                if (prisonedTheif.GetReleaseDate() < inGameDate)
                {
                    feed.ThievesInPrison--;
                    //release em
                    prisonedTheif.inPrison = false;
                    feed.AddImportant($"{inGameDate.ToString("dd-HH")}{prisonedTheif.Sprite} {prisonedTheif.FirstName} is free!");
                    //Continue the rest of the method for the rest of the prisoners
                    continue;
                    /// Console.WriteLine($"{prisonedTheif.FirstName} is free");

                }
                //If x theif is not to be released
                else
                {
                    feed.ThievesInPrison = theives.Where(t => t.inPrison).Count();
                    //Draw em
                    Console.SetCursorPosition(prisonedTheif.PosX * 2, prisonedTheif.PosY + 1);
                    //Write emoji
                    Console.Write("⬜");

                    //Left wall
                    int prisonStartX = 1;

                    //Top with offset for wall
                    int prisonStartY = height + 2;

                    //Right wall
                    int prisonEndX = prisonStartX + prisonWidth - 1;

                    //Bottom wall
                    int prisonEndY = prisonStartY + prisonHeight - 1;



                    prisonedTheif.Move(prisonStartX, prisonEndX, prisonStartY, prisonEndY);

                    // Draw at new position
                    Console.SetCursorPosition(prisonedTheif.PosX * 2, prisonedTheif.PosY + 1);
                    Console.Write(prisonedTheif.Sprite);
                }



            }
        }

        static void DrawInhabitants()
        {
            //Draw the people in the city ON the city graphic

            foreach (var pop in allPeopple)
            {
                //New pos
                Console.SetCursorPosition(pop.PosX * 2, pop.PosY + 1);

                //Write
                Console.Write(pop.Sprite);
            }
        }

        /*
        //Handle Collision
        static List<(int, int)> DetectCollisions()
        {
            //2d list
            var listOfCollisionPositions = new List<(int, int)>();

            //Foreach or For loop
            //Check if subject A or I has hit another
            //If a for loop, count from 0-max (0 is person number 0 could be Erik Eriksson)
            //As it's nested make sure to check that its different number => Erik Eriksson shouldn't collide with himself
            //Basic check with thingA != thingB => then add
            for (int subjectA = 0; subjectA < allPeopple.Count; subjectA++)
            {
                //Subject B or J
                for (int subjectB = 0; subjectB < allPeopple.Count; subjectB++)
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
        }*/
    }




}

