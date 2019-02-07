using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Square
    {
        // mine : -1
        // 0 ... 8 
        //property only get
        public int Value { get; internal set; }
        public SquareStatus Status;

        public Square()
        {
            Value = 0;
            Status = SquareStatus.UnShowed;

        }

        public void SetMine()
        {
            Value = -1;
        }

        public void RemoveMine()
        {
            Value = 0;
        }

        public void SetValue(int value)
        {
            Value = value;
        }

        public bool IsMine()
        {
            return Value == -1;
        }
    }
    public enum SquareStatus
    {
        UnShowed, Showed, Flag
    }
}
