using Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RunMinesweeper
{
    public class UserInputParser
    {

        public UserInputParser()
        {
            PrintInitialMsg();
        }

        public Level ParserLevel()
        {
            Console.WriteLine("CHOOSE A GAME LEVEL:");
            Console.WriteLine("1. EASY " +
                "2. NORMAL " +
                "3. EXPERT");
            var levelString = Console.ReadLine();
            int result;
            Level level = Level.Easy;
            // if user put a right level, parser it.
            //if not we create a easy level default
            
            return Int32.TryParse(levelString, out result) ? (Level) result : level;
        }

        public Queue<string> ParserCommand(string userInput)
        {
            var result = new Queue<string>();

            var input = userInput.Trim();
            if (input != "q" && input != "Q")
            {
                var splitted = userInput.Split(' ');
                foreach (var s in splitted)
                {
                    result.Enqueue(s);
                }
            }
            return result;
        }

        private static void PrintInitialMsg()
        {
            

            Console.WriteLine("WELCOME TO MINESWEEPER");
           
        }
    }
}
