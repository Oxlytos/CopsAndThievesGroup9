using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopsAndThieves
{
    public class NewsFeed
    {
        //Dimensions
        private int startY;
        //Handles amount we render and showcase
        private int maxMessages;
        //All messages
        private List<string> messages = new List<string>();

        //Constructor for newsFeed
        public NewsFeed(int startPosY, int maxMsg = 15)
        {
            startY = startPosY;
            this.maxMessages = maxMsg;
        }

        //When creating a new message
        public void AddMsg(string msg)
        {
            //Add it to the list
            messages.Add(msg);

            //If more than X messages
            if (messages.Count > maxMessages) 
            {
                //Removes oldest one
                messages.RemoveAt(0);
            }

            //Draw the board with all the messages
            //DrawMessageBoard();
        }

        public void DrawMessageBoard() 
        {
            int consoleWidth = Console.WindowWidth;
            int y = startY;

            for (int i = 0; i < messages.Count + 2; i++) 
            {
                Console.SetCursorPosition(0, y + i);
                Console.Write(new string(' ', consoleWidth));
            }

            Console.SetCursorPosition(0, y);
            string titleFeed = "NEWS FEED!";

            int feedCenterPos = Math.Max((consoleWidth-titleFeed.Length) / 2,0);
            Console.SetCursorPosition(feedCenterPos, y);
            Console.Write(titleFeed);

            for(int i=0; i < messages.Count; i++)
            {
                Console.SetCursorPosition(0, y + 1+ i);
                Console.Write(messages[i].PadRight(consoleWidth));
            }
          
        }
    }
}
