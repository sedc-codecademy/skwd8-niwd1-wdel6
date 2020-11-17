using System;


namespace SedcServer
{
    class Calculator
    {
        public CalculationResult Calculate(string operation, double first, double second)
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
                Arguments = new double[]{ first, second },
                Result = Math.Round(result, 15, MidpointRounding.AwayFromZero)
            };
        }
    }

    struct CalculationResult
    {
        public string Operation { get; set; }
        public double[] Arguments { get; set; }
        public double Result { get; set; }
    }
}
