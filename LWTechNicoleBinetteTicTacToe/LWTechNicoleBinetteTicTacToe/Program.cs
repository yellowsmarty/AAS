using System;
using System.Security.Cryptography.X509Certificates;

namespace LWTechNicoleBinetteTicTacToe
{
    class Program
    {

        static Random rng = new Random();

        static void Main(string[] args)
        {

            bool gameOver = false;

            do
            {

                string[,] board = BuildBoard();
                Play(board);
                DisplayBoard(board);
                int w = Winner(board);
                if (w == 0)
                {
                    Console.WriteLine("It's a tie!");
                }

                if (w == 1)
                {
                    Console.WriteLine("Player 1 wins!");
                }

                if (w == -1)
                {
                    Console.WriteLine("Player 2 wins!");
                }

                bool yesOrNo = false;
                while (!yesOrNo)
                {
                    Console.WriteLine("Play again (yes/no)?");
                    string playAgain = Console.ReadLine();
                    playAgain.ToLower();
                    playAgain.Trim();
                    playAgain.Replace("\\s", "");
                    if (playAgain == "yes")
                    {
                        gameOver = false;
                        yesOrNo = true;
                    }
                    if (playAgain == "no")
                    {
                        gameOver = true;
                        yesOrNo = true;
                    }
                }
            }

            while (gameOver == false);

        }

        public static string[,] BuildBoard()
        {
            string[,] newBoard = new string[3, 3];
            return newBoard;
        }

        public static void DisplayBoard(string[,] b) 
        {
            string[,] board = b;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("|_");
                    Console.Write(board[i,j]);
                }
                Console.Write("|");
                Console.WriteLine("");
            }
        }

        public static void Play(string[,] b)
        {

            String p1 = "X";
            String p2 = "O";

            string[,] board = b;

            for (int i = 0; i < 3; i++)
            {
                i = P1Turn();

                for (int j = 0; j < 3; j++ )
                {
                    j = P1Turn();

                    if (board[i,j] != "X" || board[i, j] != "O")
                    {
                        board[i, j] = p1;
                        i = P2Turn();
                        j = P2Turn();

                        if (board[i, j] != "X" || board[i, j] != "O")
                        {
                            board[i, j] = p2;
                        }

                        else
                        {
                            break;
                        }
                    }

                    else 
                    {
                        break;
                    }
                    
                }

            }

        }

        public static int P1Turn()
        {
            int placeOnBoard = rng.Next(3);
            return placeOnBoard;
        }

        public static int P2Turn()
        {
            int placeOnBoard = rng.Next(3);
            return placeOnBoard;
        }

        public static int Winner(string[,] b)
        {
            int countforP1 = 0;
            int countforP2 = 0;
            int winner = 0;

            // Check rows
            for (int i = 0; i < 3; i++)
            {
                countforP1 = 0;
                countforP2 = 0;

                for (int j = 0; j < 3; j++)
                {


                    if (b[i, j] == "X")
                    {
                        countforP1++;
                    }

                    if (b[i, j] == "O")
                    {
                        countforP2++;
                    }

                    if (countforP1 == 3)
                    {
                        winner = 1;
                        return winner;
                    }

                    if (countforP2 == 3)
                    {
                        winner = -1;
                        return winner;
                    }

                    if (countforP1 == 3 && countforP2 == 3)
                    {
                        winner = 0;
                        return winner;
                    }
                }
            }

            // Check rows
            for (int i = 0; i < 3; i++)
            {
                countforP1 = 0;
                countforP2 = 0;

                for (int j = 0; j < 3; j++)
                {


                    if (b[j, i] == "X")
                    {
                        countforP1++;
                    }

                    if (b[j, i] == "O")
                    {
                        countforP2++;
                    }

                    if (countforP1 == 3)
                    {
                        winner = 1;
                        return winner;
                    }

                    if (countforP2 == 3)
                    {
                        winner = -1;
                        return winner;
                    }

                    if (countforP1 == 3 && countforP2 == 3)
                    {
                        winner = 0;
                        return winner;
                    }
                }
            }

            // Check diagonals (needs work)
            for (int i = 0; i < 3; i++)
            {
                countforP1 = 0;
                countforP2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (i == j && b[i, j] == "X")
                    {
                        countforP1++;
                    }

                    if (i == j && b[i, j] == "O")
                    {
                        countforP2++;
                    }

                    if (countforP1 == 3)
                    {
                        winner = 1;
                        return winner;
                    }
                    if (countforP2 == 3)
                    {
                        winner = -1;
                        return winner;
                    }

                    if (countforP1 == 3 && countforP2 == 3)
                    {
                        winner = 0;
                        return winner;
                    }
                }
            }

            return winner;
        }
    }
}
