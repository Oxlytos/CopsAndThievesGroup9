using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    internal class CitySimulation
    {


        int[,] citySize = { { 100 }, { 100 } }; 

       public static void Run()
        {
            Console.WriteLine("City");

            for(int i = 0; i<100; i++)
            {
                bool writeHash = true;
               // Console.WriteLine("#");

                for(int j = 0; j < 100; j++)
                {
                    if (writeHash)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write("$");
                    }

                    writeHash = !writeHash;
                }
                Console.WriteLine();
            }
        
        
        }

    }
}
