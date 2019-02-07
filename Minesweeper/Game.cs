using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public interface IGame
    {
        List<IRound> Rounds { get; set; }
        

        int StartRound(Level level);

        Status GetStatusRound(int roundId);

        bool IsWinning(int roundId);

        bool RunCommand(ICommand cmd, int roundId);

        /// <summary>
        /// Get current board to the user
        /// </summary>
        /// <returns> array of strings, each position (x, y) 
        /// show the current value for this square
        /// (M = mine, F= flag, H=Hidden, x -> (0...8) = value of showed position)
        /// </returns>
        string[,] ShowBoard(int roundId);
    }

    public class Game : IGame
    {

        public List<IRound> Rounds { get; set; }
        private int _nextRoundId;
        private readonly IExecuteCommand _execute;

        public Game()
        {
            Rounds = new List<IRound>();
            _execute = new ExecuteCommand();
            _nextRoundId = 0;
        }


        /// <summary>
        /// inicializa una nueva partida en un nivel especifico.
        /// crea el tablero y empieza el timer
        /// </summary>
        /// <param name="level"></param>
        public int StartRound(Level level)
        {
            var id = _nextRoundId ++; 
            var round = new Round();
            round.InitRound(level, id);
            Rounds.Add(round);
            
            return id;
        }
        
        /// <summary>
        /// Consulta el estado de la partida
        /// </summary>
        /// <returns></returns>
        public Status GetStatusRound(int roundId)
        {
            throw new NotImplementedException();
        }

        public bool IsWinning(int roundId)
        {
            return Rounds.FirstOrDefault(x => x.RoundId == roundId)?.RoundStatus != Status.GameOver;
        }

        /// <summary>
        /// ejecuta un comando en la partida
        /// </summary>
        /// <returns>-1 si se corrio con error, 0 caso contrario</returns>
        public bool RunCommand(ICommand cmd, int roundId)
        {
            var result = false;
            var round = Rounds.FirstOrDefault(x => x.RoundId == roundId);
            try
            {
                _execute.Run(round, cmd);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }


            return result;
        }

        public string[,] ShowBoard(int roundId)
        {
            var currentBoard = Rounds.FirstOrDefault(x => x.RoundId == roundId)?.Board;
            var result = new string[0,0];
            if (currentBoard != null) {
                LogBoard(currentBoard);
                result = new string[currentBoard.Height, currentBoard.Width];
                for (var h = 0; h < currentBoard.Height; h++)
                {
                    for (var w = 0; w < currentBoard.Width; w++)
                    {
                        var cSquare = currentBoard.Squares[h, w];
                        var value = "";
                        switch (cSquare.Status)
                        {
                            case (SquareStatus.UnShowed):
                                value = "H";
                                break;
                            case (SquareStatus.Flag):
                                value = "F";
                                break;
                            case (SquareStatus.Showed):
                                value = cSquare.Value.ToString();
                                break;
                            
                        }
                        result[h, w] = value;
                    }
                }
            }

            return result;
            
        }

        private void LogBoard(IBoard board)
        {
            for (var h = 0; h < board.Height; h++)
            {
                for (var w = 0; w < board.Width; w++)
                {
                    var cSquare = board.Squares[h, w];
                    Console.Write((cSquare.Value== -1) ? "x" : cSquare.Value.ToString());
                }
                Console.WriteLine(" ");
            }
        }

    }

    public enum Level { Easy = 1, Intermediate, Difficult }
    public enum Status { Initial, Started, Finished, GameOver }
}
