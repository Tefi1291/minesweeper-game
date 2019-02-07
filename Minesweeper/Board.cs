using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IBoard
    {
        Square[,] Squares { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        int FlagsLeft { get; set; }
        int MinesNumber { get; set; }
        SortedSet<Tuple<int, int>> MinesPositions { get; set; }

        bool IsValidPosition(IPosition pos);
        void PutFlag(IPosition pos);
        void TakeOffFlag(IPosition pos);

        void ShowSquare(IPosition pos);

        bool IsUncoverMine(IPosition pos);
    }

    public class Board : IBoard
    {
        public Square[,] Squares { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int FlagsLeft { get; set; }
        public int MinesNumber { get; set; }
        public SortedSet<Tuple<int, int>> MinesPositions { get; set; }

        public Board(int h, int w, int mines)
        {
            Height = h;
            Width = w;
            
            MinesPositions = new SortedSet<Tuple<int, int>>();
            MinesNumber = mines;
            FlagsLeft = MinesNumber;

            Squares = BuildBoard(Height, Width, MinesNumber);

            //set mines
            var totalMines = MinesNumber;
            while (0 < totalMines)
            {
                SetMineOnBoard();
                totalMines--;
            }

            //check seted mines == total mines
            int totalSets = 0;
            foreach (var s in Squares)
            {
                totalSets += s.IsMine() ? 1 : 0;
            }
            Debug.Assert(MinesNumber == totalSets);

            //set values around mine squares

            foreach (var m in MinesPositions)
            {
                var x = m.Item1;
                var y = m.Item2;
                //upper
                //AddMineReference(new Position(,))
                AddMineReference(x - 1, y - 1);
                AddMineReference(x - 1, y);
                AddMineReference(x - 1, y + 1);
                //same Row
                AddMineReference(x , y - 1);
                AddMineReference(x , y + 1);
                //buttom
                AddMineReference(x + 1, y - 1);
                AddMineReference(x + 1, y);
                AddMineReference(x + 1, y + 1);
            }
        }


        private Square[,] BuildBoard(int h, int w, int mines)
        {
            var board = new Square[h, w] ;
            
            for (var i = 0; i < h; i++)
            {
                for (var j = 0; j < w; j++)
                {

                    board[i, j] = new Square()
                    {
                        Value = 0,
                        Status = SquareStatus.UnShowed
                    };

                }
            }
            //board.Initialize();
            
            return board;
        }


        private void SetMineOnBoard()
        {
            var wasSet = false;
            //check
            while (!wasSet)
            {
                var randomH = new Random().Next(0, Height - 1);
                var randomW = new Random().Next(0, Width - 1);

                var s = Squares[randomH, randomW];
                if (!s.IsMine())
                {
                    s.SetMine();
                    var position = Tuple.Create<int, int>(randomH, randomW);
                    MinesPositions.Add(position);
                    wasSet = true;
                }
                
            }

            
        }

        private void AddMineReference(IPosition pos)
        {

            if (IsValidPosition(pos) && !Squares[pos.Row, pos.Column].IsMine())
            {
                Squares[pos.Row, pos.Column].Value += 1;
            }
        }

        [Obsolete]
        private void AddMineReference(int x, int y)
        {

            if ((0 <= x && x <= Height - 1) && (0 <= y && y <= Width - 1) && !Squares[x, y].IsMine())
            {
                Squares[x, y].Value += 1;
            }
        }

        public bool IsValidPosition(IPosition pos)
        {
            return (0 <= pos.Row && pos.Row < (Squares.GetLength(0))
                && 0 <= pos.Column && pos.Column < (Squares.GetLength(1)));
        }

        public void PutFlag(IPosition pos)
        {
            try
            {
                var square = Squares[pos.Row, pos.Column];
                if (square.Status != SquareStatus.Flag)
                {
                    square.Status = SquareStatus.Flag;
                    FlagsLeft--;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void TakeOffFlag(IPosition pos)
        {
            try
            {
                var square = Squares[pos.Row, pos.Column];
                if (square.Status == SquareStatus.Flag)
                {
                    square.Status = SquareStatus.UnShowed;
                    FlagsLeft++;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ShowSquare(IPosition pos)
        {
            try
            {
                var square = Squares[pos.Row, pos.Column];
                square.Status = SquareStatus.Showed;
                if (square.Value == 0)
                {
                    var list = new List<IPosition>() { pos };

                    while (list.Count > 0)
                    {
                        var newN = new List<IPosition>();
                        foreach (var l in list)
                        {
                            var nl = ShowNeighbords(l);
                            nl.RemoveAll(e => e == null);
                            newN.AddRange(nl);
                        }

                        list = new List<IPosition>(newN) ;

                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        delegate IPosition ToShowedPosition(IPosition pos);
        private List<IPosition> ShowNeighbords(IPosition pos)
        {
            List<IPosition> result = new List<IPosition>();

            var position = pos.GetPosition();

            var x = position.Item1;
            var y = position.Item2;

            ToShowedPosition f = (IPosition p) => 
            {
                Square s = null;
                if (IsValidPosition(p))
                {
                    var square = Squares[p.Row, p.Column];
                    if (!square.IsMine() && square.Status != SquareStatus.Showed)
                    {
                        square.Status = SquareStatus.Showed;
                        s = square;
                    }
                }
                return (s?.Value == 0) ? p : null;
            };

            List<int> xList = new List<int>() { x-1, x, x+1 };
            List<int> yList = new List<int>() { y - 1, y, y + 1 };
            
            result.Add(f(new Position(x - 1, y - 1)));
            result.Add(f(new Position(x - 1, y)));
            result.Add(f(new Position(x - 1, y + 1)));

            result.Add(f(new Position(x, y - 1)));
            result.Add(f(new Position(x , y + 1)));

            result.Add(f(new Position(x+1, y - 1)));
            result.Add(f(new Position(x + 1, y )));
            result.Add(f(new Position(x + 1, y + 1)));

            return result;
        }


        public bool IsUncoverMine(IPosition pos)
        {
            var UncoverMine = false;
            try
            {
                var s = Squares[pos.Row, pos.Column];
                UncoverMine = (s.Status == SquareStatus.Showed && s.IsMine());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return UncoverMine;
        }
    }


}
