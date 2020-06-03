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
                // Ещё тестовые данные
                // { 0, 2, 3, 9, 7, 123 },
                //{ 123, 2, 4, 5, 8, 256 },
                //{ 123, 9, 1, 2, 8, 123 },
                //{ 256, 5, 4, 3, 0, 256 },
                //{ 9, 7, 6, 2, 1, 123 }
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

            // Получение результата
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
        // Вкратце, алгоритм работает следующим образом:
        //    1. Проходит по каждой строчке (горизонтально)
        //    2. Заносит все значения в буфер "numbers"
        //    3. Проходит по каждому столбцу (вертикально)
        //    4. Корректирует значения в буфере
        //    5. Получает из буфера произведения чисел на количество их повторений
        //    6. Возращает результат
        static int[,] MultiplySimilarNumbers(int[,] input)
        {
            // Буфер
            var numbers = new Number[input.GetLength(0), input.GetLength(1)];
            // Перебор всех строк
            for (int i = 0; i < input.GetLength(0); i++)
            {
                // История найденных индексов позволяет
                // пропускать индесы, которые уже учавствовали в поиске
                var history = new List<int>();
                // Обёртка над строкой упрощает взаимодействие
                ArrayRow<int> row = new ArrayRow<int>(input, i);
                for (int j = 0; j < row.Length; j++)
                {
                    if (history.Contains(j))
                        continue;
                    // Нахождение индексов столбца, равный текущему элементу
                    int[] indexes = row.IndexesOf(row[j], j);
                    Number number = new Number(row[j], indexes.Length);
                    // Заполнение буфера
                    foreach (int index in indexes)
                        numbers[i, index] = number;
                    // Заполнение истории индексов строки
                    history.AddRange(indexes);
                }
            }
            // Перебор всех столбцов, как и у строк
            for (int i = 0; i < input.GetLength(1); i++)
            {
                var history = new List<int>();
                // Обёртка над столбцом упрощает взаимодействие
                ArrayColumn<int> column = new ArrayColumn<int>(input, i);
                for (int j = 0; j < column.Length; j++)
                {
                    if (history.Contains(j))
                        continue;

                    int[] indexes = column.IndexesOf(column[j], j);

                    foreach (int index in indexes)
                        if (indexes.Length > 1)                       
                            numbers[index, i].Multiplier += indexes.Length - 1;

                    history.AddRange(indexes);
                }
            }
            // Получение результата из буфера
            int[,] output = new int[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
                for (int j = 0; j < input.GetLength(1); j++)
                    output[i, j] = numbers[i, j].Multiplication;

            return output;
        }
    }
}
