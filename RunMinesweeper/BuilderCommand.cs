using Minesweeper;
using System;
using System.Collections.Generic;
using System.Text;


namespace RunMinesweeper
{

    public interface IBuilderCommand
    {
        ICommand ParserCommand(string userCmd, string[] args = null);
    }

    class BuilderCommand : IBuilderCommand
    {

        public ICommand ParserCommand(string userCmd, string[]args = null)
        {
            ICommand result = new CommandBase();
            //parser position
            if (args?.Length == 2)
            {

                
                int r, c;
                if (Int32.TryParse(args[0], out r) && Int32.TryParse(args[1], out c))
                {
                    var p = new Position(r, c);
                    result.SquarePosition = p;
                }
            }

            //parser command
            switch (userCmd)
            {
                case ("f"):
                    result.Type = CommandType.PutFlag;
                    break;
                case ("s"):
                    result.Type = CommandType.ShowSquare;
                    break;

                case ("tf"):
                    result.Type = CommandType.TakeOffFlag;
                    break;
                default:
                    result = null;
                    break;

            }
            
            return result;
        }
        
        
    }
}
