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
        public static int width = 20;
        public static int height = 20;

        static int maxAmountOfCivllians = 15;


        static List<Person> allPeopple = new List<Person>();

        static List<Police> thePolice = new List<Police>();
        static List<Citizen> citizens = new List<Citizen>();
        static List<Theif> theives = new List<Theif>();

        public static void Run()
        {
            Console.WriteLine("City");

            Console.CursorVisible = false;


            for(int i= 0; i<= maxAmountOfCivllians; i++)
            {
                //Create citizen, add to list
                citizens.Add(GeneratePerson.GenerateRandomCititzen());

                //Creates a random spawn point
                citizens.ElementAt(i).SpawnRandomPosition(width, height);


                //Add citizen(i) till main list
                allPeopple.Append(citizens.ElementAt(i));
            }


            for (int i = 0; i <= maxAmountOfCivllians/3; i++) 
            {
                thePolice.Add(GeneratePerson.GenerateRandomPolice());
                thePolice.ElementAt(i).SpawnRandomPosition(width,height);

                allPeopple.Append(citizens.ElementAt(i));

            
            }

            for(int i= 0; i<= maxAmountOfCivllians/4; i++)
            {
                theives.Add(GeneratePerson.GenerateRandomTheif());
                theives.ElementAt(i).SpawnRandomPosition(width, height);

                allPeopple.Append(theives.ElementAt(i));
            }


            List<Person> persons = new List<Person>();
            persons.Add(GeneratePerson.GenerateRandomPerson());
            persons.Add(GeneratePerson.GenerateRandomPolice());

            // Skapa en agent
            //Person police = GeneratePerson.GenerateRandomPolice();
            Theif harald = new Theif("Harald", "Tjuven");


            //Basic initial random position

            //Set a random position at the start
           // police.SpawnRandomPosition();


            // Rita staden med agenten
            DrawCity(harald);
        }

        static void DrawCity(Person agent)
        {
            Console.Clear();
            string wall = "🧱";
            string empty = "⬜";

            

            //Decide a place to stand
            while (true)
            {
                agent.Move(width, height);
               
                // Tak
                Console.ForegroundColor = ConsoleColor.Green;
                for (int x = 0; x <= width + 1; x++) Console.Write(wall);
                Console.WriteLine();

                // Rutnät
                for (int y = 0; y < height; y++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(wall);
                    Console.ForegroundColor = ConsoleColor.White;

                    for (int x = 0; x < width; x++)
                    {
                        //if (x == agent.PosX && y == agent.PosY)
                          //  Console.Write(agent.Sprite);
                        //else
                            Console.Write(empty);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(wall);
                }

                // Botten
                for (int x = 0; x <= width + 1; x++) Console.Write(wall);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Police position is: {agent.PosY}");

              
               
                Console.Write("\n");
                DrawInhabitants();

                Thread.Sleep(1000);
                Console.Clear();
                }
        }
        static void DrawInhabitants()
        {
            //Draw the people in the city ON the city graphic
            Console.WriteLine($"Amount of cops in the city: {thePolice.Count}");
            Console.WriteLine($"Map limit is {width} by {height}");



            //Move part
            foreach(Citizen ciz in citizens)
            {
                ciz.Move(width, height);
            }

            foreach(Police pig in thePolice)
            {

                pig.Move(width, height);
            }
            foreach(Theif thif in theives)
            {
                thif.Move(width, height);
            }
            //////////////////////////////////////////////////////////////////////////
            ///

            ///Draw part
            ////////////////////////////////////////////////////////////////////////

            for(int y = 0; y <= height; y++)
            {
                //Brick Emoji is 2 spaces wide
                //Take into account when printing and managing the sprites/objects movements
                //Aka 10 bricks = 20 spaces => 20 bricks = 40 spaces
                //Handle width with a multiplier of 2
                for(int x = 0; x <= width*2; x++)
                {
                    foreach (Police pol in thePolice) 
                    {
                       
                        if (pol.PosX == x && pol.PosY == y)
                        {
                            Console.SetCursorPosition(x, y);
                            
                            
                            Console.Write(pol.Sprite);
                        }
                     //   Console.Write($"Police at {pol.PosX}{pol.PosY}");
                    }

                    foreach (Citizen ciziten in citizens)
                    {


                        if (ciziten.PosX == x && ciziten.PosY == y)
                        {
                            Console.SetCursorPosition(x, y);


                            Console.Write(ciziten.Sprite);
                        }
                        //   Console.Write($"Police at {pol.PosX}{pol.PosY}");
                    }

                    foreach(Theif theif in theives)
                    {
                        if(theif.PosX == x && theif.PosY == y)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write(theif.Sprite);
                        }
                    }


                }

            }
            /////Greet/Collision part
            //////////////////////////////////////////////////////////

            Console.SetCursorPosition(0, height+5);
            Console.WriteLine("🎺🎺🎺🎺🎺🎺🎺🎺🎺🎺NEWSFEED🎺🎺🎺🎺🎺🎺🎺🎺🎺🎺🎺");
           

            foreach(Citizen ciz in citizens)
            {
                foreach (Citizen hitPerson in citizens)
                {
                    if (ciz != hitPerson)
                    {
                        if (ciz.PosX == hitPerson.PosX && ciz.PosY == hitPerson.PosY)
                        {
                            ciz.Greet(hitPerson.FirstName, ciz.PosX, ciz.PosY);
                          //  Console.Beep();
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        //Nothing
                    }
                }
            }
            foreach(Police polA in thePolice)
            {
                foreach (Police polB in thePolice) 
                {
                    if(polB != polA)
                    {
                        if(polB.PosX == polA.PosX && polB.PosY == polA.PosY)
                        {
                            polA.GreetPolice(polB.SurName, polB.PosX, polB.PosY);
                          //  Console.Beep();
                            Thread.Sleep(2000);
                        }
                    }
                
                }
            }
            



        }
    }

    
}
