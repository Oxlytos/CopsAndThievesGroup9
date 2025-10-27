using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class NewsFeed
    {
        public DateTime SimulationDate {  get; set; }
        public int CitizensAmount { get; set; } = 0;
        public int PoliceAmount { get; set; } = 0;
        public int ThiefAmount { get; set; } = 0;
        public int ObjectsStolen { get; set; } = 0;
        public int ObjectsConfiscated { get; set; } = 0;
        public int CitizensRobbed { get; set; } = 0;
        public int ThievesInPrison { get; set; } = 0;


        //Dimensions
        private int startY;
        //Handles amount we render and showcase
        private int maxMessages;
        //Just messages
        private List<string> greets = new List<string>();
        //All actions that may be important
        private List<string> actions = new List<string>();

        //Constructor for newsFeed
        public NewsFeed(int startPosY, int maxMsg = 10)
        {
            startY = startPosY;
            this.maxMessages = maxMsg;
        }

        //When creating a new message
        public void AddGreet(string msg)
        {
            //Add it to the list
            greets.Add(msg);

            //If more than X messages
            if (greets.Count > maxMessages) 
            {
                //Removes oldest one
                greets.RemoveAt(0);
            }

            //Draw the board with all the messages
            //DrawMessageBoard();
        }
        public void AddImportant(string msg)
        {
            actions.Add(msg);

            //If more than X messages
            if (actions.Count > maxMessages)
            {
                //Removes oldest one
                actions.RemoveAt(0);
            }
        }

        public void DrawMessageBoard() 
        {
            int consoleWidth = Console.WindowWidth;
            int halfOfTheScreen = consoleWidth / 2;
            int y = startY;

            for (int i = 0; i < greets.Count + 3; i++) 
            {
                Console.SetCursorPosition(0, y + i);
                Console.Write(new string(' ', consoleWidth));
            }
            Console.SetCursorPosition(0, y);
            Console.Write(UpdateCityStatistics().PadRight(consoleWidth * 2));


            string actionFeed = "ACTIONS FEED!";
            string greetFeed = "GREETS FEED!";

            Console.SetCursorPosition(halfOfTheScreen/4, y+2);
            Console.Write(actionFeed);
            Console.SetCursorPosition((halfOfTheScreen*2)/2, y+2);
            Console.Write(greetFeed);

            int maxRows = Math.Max(actions.Count, greets.Count);
            for (int f = 0; f < maxRows; f++) {

                if (f < actions.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(0, y + 3 + f);
                    Console.Write(actions[f]);
                }
                if (f < greets.Count)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(halfOfTheScreen, y + 3 + f);
                    Console.Write(greets[f].PadRight(halfOfTheScreen));
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

        }
        string UpdateCityStatistics()
        {

            return $" {SimulationDate}🧍{CitizensAmount} 👮{PoliceAmount} 🦹{ThiefAmount} | " +
              $"Robbed: {CitizensRobbed}, Stolen: {ObjectsStolen}, Confiscated: {ObjectsConfiscated} | " +
              $"In Prison: {ThievesInPrison}/{ThiefAmount} On the loose: {ThiefAmount-ThievesInPrison}";
        }
    }
   
}
