using System;
using System.Text.RegularExpressions;
using TicTacToe.Services;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var tttService = new TicTacToeService();

            do
            {
                tttService.InitializeBoard();
                var playerTurn = BoardWinner.x;
                var message = String.Empty;

                Console.Clear();
                Console.WriteLine("Would you like to play a game? How about a nice game of global thermal nuclear tic tac toe?");

                while (!tttService.GameIsOver())
                {
                    if (!String.IsNullOrEmpty(message)) Console.WriteLine(message);
                    Console.WriteLine();
                    Console.WriteLine(tttService.GetBoardString());
                    Console.Write(String.Format("Player {0} what cell would you like to occupy? ", playerTurn == BoardWinner.x ? "X" : "O"));

                    var line = Console.ReadLine();
                    //validate the entry 
                    var digitRegex = new Regex(@"^\d$");
                    if (digitRegex.IsMatch(line))
                    {
                        //process the entry
                        if (tttService.UpdateCell(Convert.ToInt32(line), playerTurn))
                        { 
                            //update the player turn
                            playerTurn = playerTurn == BoardWinner.x ? BoardWinner.o : BoardWinner.x;
                            message = String.Empty;
                        }
                        else
                        {
                            message = "The selected cell is occupied.";
                        }
                    }
                    else
                    {
                        message = "Invalid selection. Please choose a cell 1 through 9.";
                    }

                    Console.Clear();
                }

                var winner = tttService.GetGameWinner();

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(tttService.GetBoardString());

                switch (winner)
                {
                    case BoardWinner.x:
                        Console.Write("X ");
                        break;
                    case BoardWinner.o:
                        Console.Write("O ");
                        break;
                    case BoardWinner.cat:
                    case BoardWinner.none:
                        Console.Write("Cat ");
                        break;
                    default:
                        break;
                }

                Console.Write("wins! Play again? (y/n) ");
            } while (Console.ReadLine() == "y");
        }
    }
}
