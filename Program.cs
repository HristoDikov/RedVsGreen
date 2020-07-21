using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;

namespace RedVsGreen
{
    class Program
    {
        static void Main(string[] args)
        {
            //Reading Grid Size
            int[] gridSize = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            //Initializing the matrix
            int[,] grid = new int[gridSize[0], gridSize[1]];

            //Setting values
            for (int i = 0; i < gridSize[0]; i++)
            {
                int[] rowValues = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();

                for (int j = 0; j < gridSize[1]; j++)
                {
                    grid[i, j] = rowValues[j];
                }
            }

            //Reading the coordinates and the generation N
            int[] coordinatesAndGenN = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            //Calculating
            int timesTargetedCellIsGreen = CalculateGenerations(grid, coordinatesAndGenN);

            Console.WriteLine(timesTargetedCellIsGreen);
        }

        private static int CalculateGenerations(int[,] grid, int[] coordinatesAndGenN)
        {
            int coordinateX = coordinatesAndGenN[0];
            int coordinateY = coordinatesAndGenN[1];
            int generationN = coordinatesAndGenN[2];
            int timesTargetedCellIsGreen = 0;
            int reds = 0;
            int greens = 0;

            //Initializing a copy matrix of the original grid, so it can be used to apply the 4 rules at the same time in the original grid
            int[,] gridCopy = new int[grid.GetLength(0), grid.GetLength(1)];

            //Setting gridCopy values as the original one
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    gridCopy[i, j] = grid[i, j];
                }
            }

            //Loop for the generations
            for (int k = 0; k < generationN; k++)
            {
                //loop for the rows
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    //loop for the cols
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        //Checking if the current cell has a neighbour over it
                        if (i - 1 >= 0)
                        {
                            //if the cell exists we check if it is red or green
                            if (grid[i - 1, j] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i - 1, j] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its left corner
                        if (i - 1 >= 0 && j - 1 >= 0)
                        {
                            if (grid[i - 1, j - 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i - 1, j - 1] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its right corner
                        if (i - 1 >= 0 && j + 1 < grid.GetLength(1))
                        {
                            if (grid[i - 1, j + 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i - 1, j + 1] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its left
                        if (j - 1 >= 0)
                        {
                            if (grid[i, j - 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i, j - 1] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its down left corner
                        if (i + 1 < grid.GetLength(0) && j - 1 >= 0)
                        {
                            if (grid[i + 1, j - 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i + 1, j - 1] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour beneath it
                        if (i + 1 < grid.GetLength(0))
                        {
                            if (grid[i + 1, j] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i + 1, j] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its right
                        if (j + 1 < grid.GetLength(1))
                        {
                            if (grid[i, j + 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i, j + 1] == 1)
                            {
                                greens++;
                            }
                        }
                        //Checking if the current cell has a neighbour on its down right corner
                        if (i + 1 < grid.GetLength(0) && j + 1 < grid.GetLength(1))
                        {
                            if (grid[i + 1, j + 1] == 0)
                            {
                                reds++;
                            }
                            else if (grid[i + 1, j + 1] == 1)
                            {
                                greens++;
                            }
                        }

                        //Checking if the current cell is red
                        if (grid[i, j] == 0)
                        {
                            //checking if its going to turn green
                            if (greens == 3 || greens == 6)
                            {
                                gridCopy[i, j] = 1;
                            }
                        }
                        //Checking if the current cell is green
                        else if (grid[i, j] == 1)
                        {
                            //Checking if its goint to turn red
                            if (greens == 0 || greens == 1 || greens == 4 || greens == 5 || greens == 7 || greens == 8)
                            {
                                gridCopy[i, j] = 0;
                            }
                        }

                        //Reseting reds and grens
                        reds = 0;
                        greens = 0;
                    }

                    
                }

                //Applying the 4 rules to the original grid at the same time
                for (int e = 0; e < grid.GetLength(0); e++)
                {
                    for (int f = 0; f < grid.GetLength(1); f++)
                    {
                        grid[e, f] = gridCopy[e, f];
                    }
                }

                //Checking if at this moment the targeted cell is green
                if (grid[coordinateX, coordinateY] == 1)
                {
                    timesTargetedCellIsGreen++;
                }
            }

            return timesTargetedCellIsGreen;
        }
    }
}
