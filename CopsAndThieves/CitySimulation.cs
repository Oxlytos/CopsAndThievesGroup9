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
        static int width = 30;
        static int height = 10;

        public static void Run()
        {
            Console.WriteLine("City");

            // Skapa en agent
            Person police = new Police("Erik", "Eriksson");
            int policeX = 5;
            int policeY = 3;

            // Rita staden med agenten
            DrawCity(police, policeX, policeY);
        }

        static void DrawCity(Person agent, int agentX, int agentY)
        {
            string wall = "🧱";
            string empty = "⬜";

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
                    if (x == agentX && y == agentY)
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
        }
    }
}
