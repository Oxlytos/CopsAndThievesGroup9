using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class CitySimulation
    {
        static int width = 30;
        static int height = 10;

        int[,] citySize = { { 100 }, { 100 } };

        public static void Run()
        {
            Console.WriteLine("City");

            string symbolForRoof = "=";
            string symbolForPillar = "X";
            //Draw along the X axis
                Console.ForegroundColor = ConsoleColor.Green;
            for(int z = 0; z <= width+1; z++)
            {
                Console.Write($"{symbolForRoof}");
            }
               

                //Change line after loop is done

                Console.WriteLine();
                //Y Axis part that uses WriteLine
                //Handles the height of a grid, left and right
                for (int innerWall = 0; innerWall < height; innerWall++)
                {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{symbolForPillar}");
                for (int space = 0; space < width; space++) 
                {
                    Console.Write(".");
                }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{symbolForPillar}");
                }

                //Bottom part of the wholass graphic
                //Like the roof, outside its loop in this case
            for (int col = 0; col <= width+1; col++)
            {
                
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{symbolForRoof}");

            }
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
