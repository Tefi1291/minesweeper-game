using Minesweeper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RunMinesweeper
{
    public interface IExecute
    {
        IList<ICommand> CommandList { get; set; }
        IGame Game { get; }

        int RunCommand(ICommand command, int roundId);
    }

    class Execute : IExecute
    {
        public IList<ICommand> CommandList { get; set; }

        public IGame Game { get; internal set; }

        public Execute(IGame newRound)
        {
            CommandList = new List<ICommand>();
            Game = newRound;
        }

        public int RunCommand(ICommand command, int roundId)
        {

            CommandList.Add(command);

            return Game.RunCommand(command, roundId) ? 0 : -1;
        }
    }
}
