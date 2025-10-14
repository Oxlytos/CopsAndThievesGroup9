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

            for(int i = 0; i<10; i++)
            {
               // bool writeHash = true;
               //Roof
                Console.Write(".");

              
                //Console.WriteLine();
            }
            for (int j = 0; j < 10; j++)
            {
                Console.WriteLine(".");

                //  writeHash = !writeHash;
            }
            for (int i = 0; i < 10; i++)
            {
                // bool writeHash = true;
                //Roof
                Console.Write(".");


                //Console.WriteLine();
            }


        }

    }
}
