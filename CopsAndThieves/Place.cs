using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    //Just handle drawing places => prison and citys
    public class Place
    {
        public int Width { get; set; }
        public int Height { get; set; }

        //Emojis there
        public string Wall { get; set; } = string.Empty;

        public string EmptyTile { get; set; } = string.Empty;


        public Place(int _width, int _height) 
        {
            Width = _width; 
            Height = _height;
        
        }

        //Draw a square shape
        public void Draw()
        {
            // Top
            for (int x = 0; x <= Width + 1; x++)
            {
                Console.Write(Wall);
            }
            Console.WriteLine();

            // Middle part with walls and empty spaces
            for (int y = 0; y < Height; y++)
            {
                Console.Write(Wall);
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(EmptyTile);
                }

                Console.WriteLine(Wall);
            }

            // Bottom
            for (int x = 0; x <= Width + 1; x++)
            {
                Console.Write(Wall);
            }
        }
       
    }

    public class City : Place
    {
        //City walls
        string _cityWall = "🧱";
        string _emptyTile = "⬜";
        public City(int width, int height) : base(width,height)
        {
            Width = width;
            Height = height;
            Wall = _cityWall;
            EmptyTile = _emptyTile;
        }

        
    }

    public class Prison : Place
    {
        //Jailhouse for every wall? More noticeable
        string _prisonWall = "🏤";
        string _emptyTile = "⬜";

        public Prison(int width, int height) : base (width,height) 
        {
            Width =width;
            Height =height;
            Wall = _prisonWall;
            EmptyTile = _emptyTile;
        }
    }


}
