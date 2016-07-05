using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lotto_simulator
{
    class Program
    {
        static void Main(string[] args)
        {

            // menu options function  called before starting game
            MenuOptions();
            // Console.ReadLine();
        }

        /// <summary>
        /// playing in manual mode
        /// </summary>
        static void ManualGame(int rows)
        {

            int index = 0;
            int[,] matrix = new int[rows, 7];
            int[] column = new int[7];
            Random r = new Random();
            int[] winnerRow = RandomArrayPopulation(6, r,0);
            Console.Clear();
            while (index != rows)
            {
                Console.WriteLine("Now you go to fill  "+(index+1)+"  row....");
                column = ManualArrayPopulation(6);
                int e = CompareRows(winnerRow, column);
                CopyToTable(matrix, column, index, e);
                index++;
            }
            
            ViewResults(matrix, winnerRow);

        }

        /// <summary>
        /// playing automation mode 
        /// </summary>
        static void AutomaticGame(int rows)
        {

            int index = 0;
            int[,] matrix = new int[rows, 7];
            int[] column = new int[7];
            Random r = new Random();
            int[] winnerRow = RandomArrayPopulation(6, r,0);
            Console.Clear();
            while (index != rows)
            {
                column = RandomArrayPopulation(6, r,index+1);
                int e = CompareRows(winnerRow, column);
                CopyToTable(matrix, column, index, e);
                index++;
            }

            ViewResults(matrix, winnerRow);
            
        }

        /// <summary>
        /// comparing winner-row and current-row detected equals
        /// </summary>
        /// <param name="winnerRow"></param>
        /// <param name="column"></param>
        private static int CompareRows(int[] winnerRow, int[] column)
        {
            int equvivalent = 0;
            for (int i = 0; i < winnerRow.Length; i++)
            {
                for (int j = i; j < column.Length; j++)
                {
                    if (winnerRow[i] == column[j])
                    {
                        equvivalent++;
                        break;
                    }
                }
            }
            return equvivalent;
        }

        /// <summary>
        /// helper function for user input and screen  preparing
        /// </summary>
        /// <param name="title"></param>

        static int BeforeInput(string title)
        {
            Console.Clear();
            Console.Title = title;
            Console.WriteLine("Click desirable amount columns , and press enter...");
            int rows = PromptForInt16();
            return rows;
        }

        /// <summary>
        /// function for  random population 
        /// </summary>
        /// <param name="SIZE"></param>
        static int[] RandomArrayPopulation(int SIZE, Random r, int ind)
        {

            int[] array = new int[SIZE];
            int beforeAdding = 0;


            for (int i = 0; i <= array.Length - 1; i++)
            {

                beforeAdding = r.Next(1, 45);

                for (int k = i; k >= 0; k--)
                {

                    if (array[k] == beforeAdding)
                    {
                        i--;
                        break;
                    }
                    else if (k == 0)
                    {
                        array[i] = beforeAdding;
                    }
                }
            }
            Console.WriteLine(ind + " row was generated  by system....");
            Console.WriteLine(String.Join(" ", array));
            Console.WriteLine();
            return array;
        }


        /// <summary>
        /// function for manual lotto-input from user
        /// </summary>
        /// <param name="SIZE"></param>
        static int[] ManualArrayPopulation(int SIZE)
        {

            int[] array = new int[SIZE];
            int beforeAdding = 0;


            for (int i = 0; i <= array.Length - 1; i++)
            {
                beforeAdding = PromptForInt16();
                for (int k = i; k >= 0; k--)
                {

                    if (array[k] == beforeAdding)
                    {
                        Console.WriteLine("This number already exists , please try again...");
                        Console.WriteLine("Current row " + String.Join(" ", array));
                        i--;
                        break;
                    }
                    else if (k == 0)
                    {
                        array[i] = beforeAdding;
                        Console.WriteLine("Current row " + String.Join(" ", array));
                  

                    }
                }
            }


            return array;
        }

        /// <summary>
        /// copying all lotto-array to matrix
        /// </summary>
        /// <param name="p"></param>
        /// <returns>2d array</returns>
        private static int[,] CopyToTable(int[,] matrix, int[] column, int index, int e)
        {
            for (int i = index; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j != 6)
                    {
                        matrix[i, j] = column[j];
                    }
                    else
                    {
                        matrix[i, j] = e;

                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        private static void PrintTable(int[,] matrix)
        {
            int sixShots = 0, fiveShots = 0, fourShots = 0; 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == 6)
                    {
                        Console.Write("|{0}|-The number shots column", matrix[i, j]);

                        if(matrix[i,j]== 6 )
                            sixShots++;
                        
                        else if(matrix[i,j]== 5 )
                            fiveShots++;
                        
                        else if (matrix[i, j] == 4) 
                            fourShots++;
                        

                    }
                    else
                        Console.Write("{0,4} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("Number rows with six shots " + sixShots);
            Console.WriteLine("Number rows with five shots " + fiveShots);
            Console.WriteLine("Number rows with four shots " + fourShots);
            Console.WriteLine("Press any key to continue....\n" + "Thanks You!!!!");
            Console.ReadKey();
            MenuOptions();
        }

        private static void ViewResults(int[,] matrix, int[] winner) {

            Console.WriteLine("Press any key  and see results.....");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Is winner row: "+String.Join(" ",winner));
            Console.WriteLine("You  results....");
            Console.WriteLine();
            PrintTable(matrix);
        }

        static int PromptForInt16()
        {
            while (true)
            {
                Int16 result;
                if (Int16.TryParse(Console.ReadLine(), out result) && (result >= 1 && result <= 45))
                {
                    return result;
                }
                Console.WriteLine("Sorry, invalid value entered. Try again....");
            }
        }

        /// <summary>
        /// main menu function 
        /// </summary>
        static void MenuOptions()
        {
            int choose = 0;
            // Creating items for simple menu bar 
            Console.Clear();
            Console.Title = "Welcome to lotto simulator!!!";
            Console.WriteLine(Console.Title);
            Console.WriteLine();
            Console.WriteLine("Menu Options :");
            Console.WriteLine();
            Console.WriteLine("    1. Manual mode.");
            Console.WriteLine("    2. Automation mode.");
            Console.WriteLine("    3. Exit.");
            Console.WriteLine();
            Console.WriteLine("Choose appropriate , and press enter......");
            choose = PromptForInt16();

            while (choose != 1 && choose != 3 && choose != 2)
            {
                Console.WriteLine("You can choose 1 2 3 menu items....");
                choose = PromptForInt16();
            }

            switch (choose)
            {

                // choosing right case and calling appropriate function

                case 1:
                    {
                        int rows = BeforeInput("Manual game mode...");
                        ManualGame(rows);
                        break;
                    }
                case 2:
                    {
                        int rows = BeforeInput("Automatic game mode...");
                        AutomaticGame(rows);
                        break;
                    }
                case 3:
                    {

                        return;
                    }
            }
        }
    }
}
