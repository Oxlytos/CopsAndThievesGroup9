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
            Console.WriteLine("Ser du detta har jag commitat från en annan dator och pushat några skillnader");
            CitySimulation.Run();
            GeneratePerson.TestRun();
        }


    }

}