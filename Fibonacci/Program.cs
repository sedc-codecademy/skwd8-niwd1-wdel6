using System;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var fibs = new Fibonacci();
            Console.WriteLine(fibs.Calculate(60));
            Console.WriteLine(fibs.Count);
        }

    }
}
