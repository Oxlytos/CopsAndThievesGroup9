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
        public static int width = 30;
        public static int height = 10;

        public static void Run()
        {
            Console.WriteLine("City");

            List<Police> thePolice = new List<Police>();

            for (int i = 0; i < 7; i++) 
            {
                thePolice.Add(GeneratePerson.GenerateRandomPolice());
            
            }
            foreach (Police policeObj in thePolice) 
            {
                Console.WriteLine($"{policeObj.FirstName} {policeObj.SurName}has arrived at the scene \n");
                policeObj.SpawnRandomPosition();
            
            }

            // Skapa en agent
            Person police = GeneratePerson.GenerateRandomPolice();


            //Basic initial random position

            //Set a random position at the start
            police.SpawnRandomPosition();


            // Rita staden med agenten
            DrawCity(police);
        }

        static void DrawCity(Person agent)
        {
            string wall = "🧱";
            string empty = "⬜";

            

            //Decide a place to stand
            while (true)
            {
                if(agent.PosY < 0 || agent.PosY <= height-1)
                {
                    agent.Move();
                }
               
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
                        if (x == agent.PosX && y == agent.PosY)
                            Console.Write(agent.Sprite);
                        else
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

                Console.WriteLine($"Police {agent.FirstName} has;");
                for(int p = 0; p<agent.Inventory.Count; p++)
                {
                    Console.WriteLine(agent.Inventory.ElementAt(p));
                }
               
                Console.Write("\n");
                Console.ReadLine();
                Console.Clear();
                }
        }
    }
}
