using System;
using System.Collections.Generic;
using System.Text;

namespace GazpromTask
{
    public struct ArrayRow<T>
    {
        private readonly T[,] array;
        private readonly int row;
        public ArrayRow(T[,] array, int row)
        {
            this.array = array;
            this.row = row;
        }
        public int Length
        {
            get { return array.GetLength(1); }
        }
        public T this[int column]
        {
            get { return array[row, column]; }
            set { array[row, column] = value; }
        }
        public int[] IndexesOf(T item, int startIndex)
        {
            var indexes = new List<int>();
            for (int i = startIndex; i < Length; i++)
                if (this[i].Equals(item))
                    indexes.Add(i);

            return indexes.ToArray();
        }
    }
}
