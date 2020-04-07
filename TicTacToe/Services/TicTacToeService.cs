using System;
using System.Text;

namespace TicTacToe.Services
{
    public enum BoardWinner { x, o, cat, none };

    public class TicTacToeService
    {
        public String[,] Board { get; set; }

        public TicTacToeService()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        { 
            Board = new String[3, 3] 
            { 
                { "1", "2", "3" }, 
                { "4", "5", "6"  }, 
                { "7", "8", "9"  } 
            };
        }

        public string GetBoardString()
        {
            var sb = new StringBuilder();

            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    sb.Append(Board[x, y]);
                    if (y < Board.GetLength(1) - 1)
                        sb.Append("|");
                    else
                        sb.AppendLine();
                }

                if (x < Board.GetLength(0) - 1) sb.AppendLine("-----");
            }

            return sb.ToString();
        }

        internal bool UpdateCell(int cell, BoardWinner playerTurn)
        {
            int? x = null;
            int? y = null;

            switch (cell)
            {
                case 1:
                    x = 0; y = 0;
                    break;
                case 2:
                    x = 0; y = 1;
                    break;
                case 3:
                    x = 0; y = 2;
                    break;
                case 4:
                    x = 1; y = 0;
                    break;
                case 5:
                    x = 1; y = 1;
                    break;
                case 6:
                    x = 1; y = 2;
                    break;
                case 7:
                    x = 2; y = 0;
                    break;
                case 8:
                    x = 2; y = 1;
                    break;
                case 9:
                    x = 2; y = 2;
                    break;
                default:
                    break;
            }

            if (x != null && y != null)
            {
                if (Board[(int)x, (int)y] == "X" || Board[(int)x, (int)y] == "O") return false;
                Board[(int)x, (int)y] = playerTurn == BoardWinner.x ? "X" : "O";
                return true;
            }

            return false;
        }

        public bool GameIsOver()
        {
            return GetGameWinner() != BoardWinner.none;
        }

        public BoardWinner GetGameWinner()
        {
            // check h 
            for (int x = 0; x < 3; x++)
            {
                if (Board[x, 0] == Board[x, 1] && Board[x, 1] == Board[x, 2])
                {
                    return Board[x, 1] == "X" ? BoardWinner.x : BoardWinner.o;
                }
            }

            // check v
            for (int y = 0; y < 3; y++)
            {
                if (Board[0, y] == Board[1, y] && Board[1, y] == Board[2, y])
                {
                    return Board[0, y] == "X" ? BoardWinner.x : BoardWinner.o;
                }
            }

            // check d 
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2])
            {
                return Board[0, 0] == "X" ? BoardWinner.x : BoardWinner.o;
            }
            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0])
            {
                return Board[0, 2] == "X" ? BoardWinner.x : BoardWinner.o;
            }

            // check cat 
            for (int x = 0; x < Board.GetLength(0); x++)
            {
                for (int y = 0; y < Board.GetLength(1); y++)
                {
                    if (Board[x, y] != "X" && Board[x, y] != "O") return BoardWinner.none;
                }
            }

            return BoardWinner.cat;
        }
    }
}
