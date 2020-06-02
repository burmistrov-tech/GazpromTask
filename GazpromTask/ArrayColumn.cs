using System;
using System.Collections.Generic;
using System.Text;

namespace GazpromTask
{
    public struct ArrayColumn<T>
    {
        private readonly T[,] array;
        private readonly int column;
        public ArrayColumn(T[,] array, int column)
        {
            this.array = array;
            this.column = column;
        }
        public int Length
        {
            get { return array.GetLength(0); }
        }
        public T this[int row]
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
