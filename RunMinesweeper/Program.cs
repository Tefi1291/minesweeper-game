using Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RunMinesweeper
{
    class Program
    {
        private static IGame currentGame;
        private static int currentRoundId; 

        static void Main(string[] args)
        {
            
            var userInput = new UserInputParser();
            var level = userInput.ParserLevel();
            //armar tablero & nueva partida
            MakeNewGame(level);
            
            var builder = new BuilderCommand();
            var execute = new Execute(currentGame);

            while (true)
            {
                Console.Clear();

                var boardToUser = currentGame.ShowBoard(currentRoundId);
                PrintBoard(boardToUser);

                if (!currentGame.IsWinning(currentRoundId))
                {
                    Console.WriteLine("Partida perdida");
                    Console.ReadLine();
                    break;
                }

                PrintCommandsList();

                // is valid command ?
                // quit, not or yes
                var input = Console.ReadLine();
                var cmdList = userInput.ParserCommand(input);
                //if 0 -> quit
                if (cmdList.Count() == 0)
                    break;
                
                // parsear el input del usuario 

                var cmd = builder.ParserCommand(cmdList.Dequeue(), cmdList.ToArray());
                if (cmd != null)
                {
                    // enviar el comando a ejecutar
                    var result = execute.RunCommand(cmd, currentRoundId);

                }
                else
                {
                    Console.WriteLine("Invalid command. Try again.");
                }
                

            }

        }

        private static void MakeNewGame(Level level)
        {
            var game = new Game();
            var roundId = game.StartRound(level);
            currentRoundId = roundId;
            currentGame = game;
            
        }

        private static void PrintBoard(string[,] board)
        {
            var h = board.GetLength(0);
            var w = board.GetLength(1);
            var pColum = "  - ";
            foreach (var n in Enumerable.Range(0, w))
            {
                pColum += n.ToString();
            }
            Console.WriteLine(pColum);
            //var separator = Enumerable.Repeat('-', w);
            //var pMsg = "";
            //foreach (var s in separator)
            //{
            //    pMsg += s;
            //}
            //Console.WriteLine(pMsg);

            for (var i = 0; i < h; i++)
            {
                var rowMsg = string.Format("{0} - ", (i).ToString());
                for (var j = 0; j < w; j++)
                {
                    rowMsg+= board[i, j];
                }
                Console.WriteLine(rowMsg);

            }

        }

        private static void PrintCommandsList()
        {


        }
    }
}
