using System;
using System.Threading;

namespace LWTech.NicoleBinette.Assignment6
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("That's life.");
            bool quit = false;
            int width = 60;
            int height = 40;
            int numMilliseconds = 50;
            int numberOfGenerations = 0;
            int[,] board = new int[width, height];
            Random rng = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int random = rng.Next(100);
                    if (random < 20)
                    {
                        board[x, y] = 1;
                    }
                    else
                    {
                        board[x, y] = 0;
                    }
                }
            }

            while (!quit)
            {
                numberOfGenerations++;
                Console.WriteLine("Number of generations: {0}", numberOfGenerations);
                string s = "";
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (board[x, y] == 0)
                        {
                            s += " ";
                        }
                        else
                        {
                            s += "*";
                        }
                    }
                    s += "\n";
                }
                Console.WriteLine(s);
                int[,] nextGen = new int[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        nextGen[x, y] = board[x, y];
                    }
                }

                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int numberOfNeighbors = board[x - 1, y - 1] + board[x, y - 1] + board[x + 1, y - 1]
                                                        + board[x - 1, y] + board[x + 1, y]
                            + board[x - 1, y + 1] + board[x, y + 1] + board[x + 1, y + 1];
                        if (numberOfNeighbors > 3 || numberOfNeighbors < 2)
                        {
                            nextGen[x, y] = 0;
                        }
                        if (numberOfNeighbors == 3)
                        {
                            nextGen[x, y] = 1;
                        }
                    }
                    //Console.WriteLine(nextGen);
                    //Thread.Sleep(numMilliseconds);
                }

                board = nextGen;

                Thread.Sleep(numMilliseconds);


                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                        case ConsoleKey.R:
                            board = new int[width, height];
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    int random = rng.Next(100);
                                    if (random < 20)
                                    {
                                        board[x, y] = 1;
                                    }
                                    else
                                    {
                                        board[x, y] = 0;
                                    }
                                }
                            }
                            numberOfGenerations = 0;
                            break;
                        case ConsoleKey.F:
                            board = new int[width, height];
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    board[x, y] = 1;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
