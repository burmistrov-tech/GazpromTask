using System;
using System.Collections.Generic;
using System.Text;

namespace GazpromTask
{
    public struct Number
    {
        public int Value { get; } // значение
        public int Multiplier { get; set; } // степень
        public int Multiplication { get { return (int)(Value * Math.Pow(2, Multiplier)/2); } }
        public Number(int value, int power)
        {
            this.Value = value;
            this.Multiplier = power;
        }
    }
}
