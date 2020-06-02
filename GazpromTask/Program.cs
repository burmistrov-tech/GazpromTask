using System;
using System.Collections.Generic;

namespace GazpromTask
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] input = new[,] {
                { 0, 2, 3, 9, 7 },
                { 6, 2, 4, 5, 8 },
                { 8, 9, 1, 2, 8 },
                { 6, 5, 4, 3, 0 },
                { 9, 7, 6, 2, 1 }
            };
            Console.WriteLine("Before: \n");
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write($"[{input[i, j]}]");
                }
                Console.WriteLine();
            }

            int[,] output = MultiplySimilarNumbers(input);
            Console.WriteLine("After: \n");

            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    Console.Write($"[{output[i, j]}]");
                }
                Console.WriteLine();
            }
        }
        static int[,] MultiplySimilarNumbers(int[,] input)
        {
            var numbers = new Number[input.GetLength(0), input.GetLength(1)];

            for (int i = 0; i < input.GetLength(1); i++)
            {
                var history = new List<int>();
                ArrayRow<int> row = new ArrayRow<int>(input, i);
                for (int j = 0; j < row.Length; j++)
                {
                    if (history.Contains(j))
                        continue;

                    int[] indexes = row.IndexesOf(row[j], j);
                    Number number = new Number(row[j], indexes.Length);

                    foreach (int index in indexes)
                        numbers[i, index] = number;

                    history.AddRange(indexes);
                }
            }

            for (int i = 0; i < input.GetLength(0); i++)
            {
                var history = new List<int>();
                ArrayColumn<int> column = new ArrayColumn<int>(input, i);
                for (int j = 0; j < column.Length; j++)
                {
                    if (history.Contains(j))
                        continue;

                    int[] indexes = column.IndexesOf(column[j], j);

                    foreach (int index in indexes)
                        if (indexes.Length > 1)
                            numbers[index, i].Multiplier += indexes.Length - 1;
                }
            }

            int[,] output = new int[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
                for (int j = 0; j < input.GetLength(1); j++)
                    output[i, j] = numbers[i, j].Multiplication;

            return output;
        }
    }
}
