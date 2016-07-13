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

        }

        /// <summary>
        /// playing in manual mode
        /// </summary>
        /// <param name="rows">number rows</param>
        static void ManualGame(int rows)
        {

            int index = 0;
            int[,] matrix = new int[rows, 7];   // results table  
            int[] column = new int[7];          // game array
            Random r = new Random();            // instanse random-object

            int[] winnerRow = RandomArrayPopulation(6, r, 0);   // generating winer array 

            Console.Clear();

            while (index != rows)
            {
                Console.WriteLine("Now you go to fill  " + (index + 1) + "  row....");
                column = ManualArrayPopulation(6);                      //  filling new row 
                int e = CompareRows(winnerRow, column);                 //  comparing current row with winner-array and return number shots
                CopyToTable(matrix, column, index, e);                  //  write results to table 
                index++;                                                //  go to next row
            }

            ViewResults(matrix, winnerRow);                             //  view game-results 

        }

        /// <summary>
        /// playing automation mode 
        /// </summary>
        /// <param name="rows">sending rows numbers</param>
        static void AutomaticGame(int rows)
        {

            int index = 0;
            int[,] matrix = new int[rows, 7];       // results table 
            int[] column = new int[7];              // game array
            Random r = new Random();
            int[] winnerRow = RandomArrayPopulation(6, r, 0);     // winner array
            Console.Clear();
            while (index != rows)
            {
                column = RandomArrayPopulation(6, r, index + 1);    // generete row
                int e = CompareRows(winnerRow, column);          // compare to winner
                CopyToTable(matrix, column, index, e);          //  copy to table
                index++;                                        // next row
            }

            ViewResults(matrix, winnerRow);                   // print result

        }

        /// <summary>
        /// comparing winner-row and current-row  couinting equals elements
        /// </summary>
        /// <param name="winnerRow">array with winner shot</param>
        /// <param name="column"> array with current row</param>
        private static int CompareRows(int[] winnerRow, int[] column)
        {
            int equvivalent = 0;
            for (int i = 0; i < winnerRow.Length; i++)
            {
                for (int j = 0; j < column.Length; j++)
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
        /// <param name="title"> title string</param>

        static int BeforeInput(string title)
        {
            Console.Clear();
            Console.Title = title;
            Console.WriteLine("Click desirable amount rows , and press enter...");
            int rows = PromptForInt16();
            return rows;
        }

        /// <summary>
        /// function for  random population 
        /// </summary>
        /// <param name="SIZE">size of current row</param>
        /// <param name="ind">current-row index</param>
        /// <param name="r">instance of random object</param>
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
        /// <param name="SIZE"> size of row </param>
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
        /// <param name="matrix">matrix with copied rows</param>
        /// <param name="column">current row</param>
        /// <param name="index">index of row</param>
        /// <param name="e">number equivalent shots in row <param>
        /// <returns>2d array</returns>
        private static int[,] CopyToTable(int[,] matrix, int[] column, int index, int e)
        {
            for (int i = index; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j != 6)
                    {
                        matrix[i, j] = column[j];      // copy array to result table
                    }
                    else
                    {
                        matrix[i, j] = e;              // cause j==6  write   number shots to last table element in row

                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// print table
        /// </summary>
        /// <param name="matrix">matrix with game result</param>
        private static void PrintTable(int[,] matrix)
        {
            int[] shots = new int[7];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == 6)      // in cause j==6  anyway get number shots 
                    {
                        Console.Write("|{0}|-The number shots in row", matrix[i, j]);

                        if (matrix[i, j] == 6)
                            shots[6]++;         // count rows  with 6 shots

                        else if (matrix[i, j] == 5)
                            shots[5]++;            // count rows  with 5 shots

                        else if (matrix[i, j] == 4)
                            shots[4]++;                // count rows  with 4 shots

                        if (matrix[i, j] == 3)
                            shots[3]++;         // count rows  with 3 shots

                        else if (matrix[i, j] == 2)
                            shots[2]++;            // count rows  with 2 shots

                        else if (matrix[i, j] == 1)
                            shots[1]++;                // count rows  with 1 shots

                        else if (matrix[i, j] == 0)
                            shots[0]++;                // count rows  with 0 shots
                    }
                    else
                        Console.Write("{0,4} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Number rows with six shots  {0}", shots[6]);
            Console.WriteLine("Number rows with five shots {0}", shots[5]);
            Console.WriteLine("Number rows with four shots {0}", shots[4]);
            Console.WriteLine("Number rows with free shots {0}", shots[3]);
            Console.WriteLine("Number rows with two shots  {0}", shots[2]);
            Console.WriteLine("Number rows with one shots  {0}", shots[1]);
            Console.WriteLine("Number rows with zero shots {0}", shots[0]);
            Console.WriteLine();
            Console.WriteLine("Press any key to continue....\n" + "Thanks You!!!!");
            Console.WriteLine();
            Console.ReadKey();
            MenuOptions();     // re-calling menu  
        }
        /// <summary>
        /// End result view function, called after automation/manual game 
        /// </summary>
        /// <param name="matrix">game result</param>
        /// <param name="winner">winner row</param>
        private static void ViewResults(int[,] matrix, int[] winner)
        {

            Console.WriteLine("Press any key  and see results.....");
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Is winner row: " + String.Join(" ", winner)); // printing winner-row
            Console.WriteLine("You  results....");
            Console.WriteLine();
            PrintTable(matrix);     // printing table with user results
        }
        /// <summary>
        /// input validation function
        /// </summary>
        /// <returns></returns>
        static int PromptForInt16()
        {
            while (true)
            {
                Int16 result;
                if (Int16.TryParse(Console.ReadLine(), out result) && (result >= 1 && result <= 45))  //get input and check if it number from range(1,45)
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

            choose = PromptForInt16();   // call validation function

            while (choose != 1 && choose != 3 && choose != 2)           //  additional checking
            {                                                           //  for menu items choose
                Console.WriteLine("You can choose 1 2 3 menu items....");
                choose = PromptForInt16();
            }

            switch (choose)
            {

                // choosing right case and calling appropriate function

                case 1:
                    {
                        int rows = BeforeInput("Manual game mode...");         // calling preparing function 
                        ManualGame(rows);                                      // choose manual-game
                        break;
                    }
                case 2:
                    {
                        int rows = BeforeInput("Automatic game mode...");       // calling preparing function 
                        AutomaticGame(rows);                                    // choose automatic-game
                        break;
                    }
                case 3:
                    {

                        return;      // end programm
                    }
            }
        }
    }
}
