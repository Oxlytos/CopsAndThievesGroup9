using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class NewsFeed
    {
        //Just all the stats that we're gonna print and user later for the users convenience
        public DateTime SimulationDate {  get; set; }
        public int CitizensAmount { get; set; } = 0;
        public int PoliceAmount { get; set; } = 0;
        public int ThiefAmount { get; set; } = 0;
        public int ObjectsStolen { get; set; } = 0;
        public int ObjectsConfiscated { get; set; } = 0;
        public int CitizensRobbed { get; set; } = 0;
        public int ThievesInPrison { get; set; } = 0;

        //Height is the city+prison heights + 5
        private int startY;
        //Handles amount we render and showcase
        private int maxMessages;
        //Message offset
        private int messageOffsetY = 3;


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

        }
        
        //For all the actions
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

        //Draw the message board
        public void DrawMessageBoard() 
        {

            //Get the current width (can scale a bit)
            int consoleWidth = Console.WindowWidth;
            //Middle of the screen is width/2
            int halfOfTheScreen = consoleWidth / 2;

            //At the bottom of the screen
            int y = startY;
            

            //Clears old messages in a block area
            for (int i = 0; i < maxMessages; i++) 
            {
                Console.SetCursorPosition(0, y + i);
                Console.Write(new string(' ', consoleWidth));
            }

            //Print statistics at the top, first move to the cleared area
            Console.SetCursorPosition(0, y);
            //Then print
            Console.Write(UpdateCityStatistics());

            //Not really needed, a bit more convenient
            string actionFeed = "ACTIONS FEED!";
            string greetFeed = "GREETS FEED!";

            //Move to the left of the screen, and then print ACTION feed
            Console.SetCursorPosition(0, y+2);
            Console.Write(actionFeed);

            //To the middle of the screen, write GREET
            Console.SetCursorPosition(halfOfTheScreen, y+2);
            Console.Write(greetFeed);

            //Loop through all the messages in boths lists
            for (int f = 0; f < maxMessages; f++) {

                if (f < actions.Count)
                {
                    //Yellow text to signal that these may be more important
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //Move cursor to the left, with start Pos from bottom of the jail, with an offset, and with the message count
                    //Aka move with a scroll based on the Message[f], where newer are at the bottom
                    Console.SetCursorPosition(0, y + messageOffsetY + f);
                    Console.Write(actions[f]);
                }
                if (f < greets.Count)
                {
                    //Greets are more common, white color for them
                    Console.ForegroundColor = ConsoleColor.White;
                    //Move to the middle of the screen and start printing your messages
                    Console.SetCursorPosition(halfOfTheScreen,y+ messageOffsetY + f);
                    //Padright with spacing of half the screen (for more PadRight: https://learn.microsoft.com/en-us/dotnet/api/system.string.padright?view=net-9.0)
                    Console.Write(greets[f].PadRight(halfOfTheScreen));
                }
            }
            Console.ForegroundColor = ConsoleColor.White;

        }
        //Updates the stats, was not fun to try and handle this
        string UpdateCityStatistics()
        {

            return $" {SimulationDate}🧍{CitizensAmount} 👮{PoliceAmount} 🦹{ThiefAmount} | " +
              $"🥷 Robbed: {CitizensRobbed}, 💰 Stolen: {ObjectsStolen}, 🫴 Confiscated: {ObjectsConfiscated} | " +
              $"🏤 In Prison: {ThievesInPrison}/{ThiefAmount} 🦹 On the loose: {ThiefAmount-ThievesInPrison}";
        }
    }
   
}
