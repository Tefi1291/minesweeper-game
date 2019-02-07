using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface ICommand
    {

        IPosition SquarePosition { get; set; }
        CommandType Type { get; set; }
    }

    public class CommandBase : ICommand
    {
        public IPosition SquarePosition { get; set; }
        public CommandType Type { get; set; }
    }

    public enum CommandType
    {
        ShowSquare = 0, PutFlag = 1, TakeOffFlag = 2
    }

    public class Position : IPosition
    {
        
        public int Row { get; private set; }
        public int Column { get; private set; }

       
        public Position(int x, int y)
        {
            Row = x;
            Column = y;
        }

        public Tuple<int, int> GetPosition()
        {
            return Tuple.Create(Row, Column);
        }
    }

    public interface IPosition
    {
        int Row { get;  }
        int Column { get; }

        Tuple<int, int> GetPosition();
    }
}
