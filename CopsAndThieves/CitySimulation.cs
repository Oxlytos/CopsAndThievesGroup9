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

        static List<Police> thePolice = new List<Police>();
        static List<Citizen> citizens = new List<Citizen>();

        public static void Run()
        {
            Console.WriteLine("City");

            Console.CursorVisible = false;


            for(int i= 0; i<= maxAmountOfCivllians; i++)
            {
                citizens.Add(GeneratePerson.GenerateRandomCititzen());
            }

            for (int i = 0; i <= maxAmountOfCivllians/3; i++) 
            {
                thePolice.Add(GeneratePerson.GenerateRandomPolice());

            
            }
            foreach(Citizen ciz in citizens)
            {
                ciz.SpawnRandomPosition(width, height);
            }
            foreach (Police policeObj in thePolice) 
            {
                Console.WriteLine($"{policeObj.FirstName} {policeObj.SurName} has arrived at the scene \n");
                policeObj.SpawnRandomPosition(width, height);
            
            }

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

                /*   Console.WriteLine($"Police {agent.FirstName} has;");
                   for(int p = 0; p<agent.Inventory.Count; p++)
                   {
                       Console.WriteLine(agent.Inventory.ElementAt(p));
                   }*/
               
                Console.Write("\n");
                DrawInhabitants();


                Console.ReadLine();
                Console.Clear();
                }
        }
        static void DrawInhabitants()
        {
            //Draw the people in the city ON the city graphic
            Console.WriteLine($"Amount of cops in the city: {thePolice.Count}");
            Console.WriteLine($"Map limit is {width} by {height}");

            foreach(Citizen ciz in citizens)
            {
                ciz.Move(width, height);
            }

            foreach(Police pig in thePolice)
            {

                pig.Move(width, height);
            }

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


                }
            }


        }
    }

    
}
