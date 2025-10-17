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
        private int height;
        private int width;

        //All messages
        private List<string> messages;

        //Handles amount we render and showcase
        private int maxMessages;

        //Constructor for newsFeed
        public NewsFeed(int startPosY, int widthX, int maxMsg = 5)
        {
            height = startPosY;
            width = widthX;
            this.maxMessages = maxMsg;
            messages = new List<string>();
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
            DrawMessageBoard();
        }

        public void DrawMessageBoard() 
        {
            //Loop through all messages
            // Draw title

            //Adjusting for window height
            int adjustedHeight = maxMessages + 1;

            //Height is the smallest at height + 2, depening on the buffer height
            height = Math.Min(height + 2, Console.BufferHeight - adjustedHeight);

            Console.SetCursorPosition(0, height);
            Console.Write(new string(' ', width));  
            Console.SetCursorPosition(0, height);

            Console.Write("🎺🎺🎺🎺NEWS FEED🎺🎺🎺🎺");

            for (int i = 0; i < messages.Count; i++) 
            {
                //Clear and spacing
                Console.SetCursorPosition(0, height + 1 + i);
                Console.Write(new string(' ', width));
                
                //Prepare row, then write
                Console.SetCursorPosition(0, height +1 + i);
                Console.Write(messages[i]);

            }
            Thread.Sleep(500);
        
        }
    }
}
