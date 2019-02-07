using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IRound
    {
        int RoundId { get; set; }
        Level Level { get; set; }
        Status RoundStatus { get; set; }
        IBoard Board { get; }

        void InitRound(Level level, int id);
        void RunActionOnBoard(ICommand action);
    }
    public class Round : IRound
    {
        public int RoundId { get ; set ; }
        public Level Level { get ; set ; }
        public Status RoundStatus { get ; set ; }

        public IBoard Board { get; internal set; }


        public Round()
        {

        }

        public void InitRound(Level level, int id)
        {
            Level = level;
            RoundId = id;
            RoundStatus = Status.Initial;
            var size = ConstData.BoardSizes[level];
            var mines = ConstData.MinesNumber[level];
            Board = new Board(size.Height, size.Width, mines);

        }

        public void RunActionOnBoard(ICommand action)
        {
            var type = action.Type;
            var position = action.SquarePosition;
            switch (type)
            {
                case (CommandType.PutFlag):
                    Board.PutFlag(position);
                    break;


                case (CommandType.ShowSquare):
                    Board.ShowSquare(position);

                    if (Board.IsUncoverMine(position))
                    {
                        //lost game

                        RoundStatus = Status.GameOver;
                    }

                    break;
                case (CommandType.TakeOffFlag):
                    Board.TakeOffFlag(position);

                    break;
            }
        }
    }
}
