using System;
using System.Collections.Generic;
using System.Text;

namespace SedcServer
{
    class Calculator
    {
        public CalculationResult Calculate(string operation, int first, int second)
        {
            var result = operation switch
            {
                "add" => first + second,
                "sub" => first - second,
                "mul" => first * second,
                "div" => first / second,
            };
            return new CalculationResult
            {
                Operation = operation,
                Arguments = new int[]{ first, second },
                Result = result
            };
        }
    }

    struct CalculationResult
    {
        public string Operation { get; set; }
        public int[] Arguments { get; set; }
        public int Result { get; set; }
    }
}
