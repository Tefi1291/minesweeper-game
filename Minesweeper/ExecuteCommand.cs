using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IExecuteCommand
    {
        void Run(IRound round, ICommand command);
    }

    class ExecuteCommand : IExecuteCommand
    {
        public void Run(IRound round, ICommand command)
        {
            if (command?.SquarePosition != null && round != null)
            {
                round.RunActionOnBoard(command);
            }
            
        }
    }
}
