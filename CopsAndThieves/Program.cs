using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CopsAndThieves
{
   internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\"Use the program in fullscreen for smoothest possible experience\"");
            CitySimulation.Run();
        }


    }

}