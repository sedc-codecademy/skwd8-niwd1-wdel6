using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci
{
    public class Fibonacci
    {
        private Dictionary<int, long> results = new()
        {
            { 0, 1 },
            { 1, 1 }
        };
        public int Count { get; private set; } = 0;


        public long Calculate(int index)
        {
            Count += 1;
            if (results.ContainsKey(index))
            {
                return results[index];
            }

            var result = Calculate(index - 1) + Calculate(index - 2);
            results[index] = result;
            return result;
        }


        // SLOOOOOWW
        //public int Calculate(int index)
        //{
        //    Count += 1;
        //    // Console.WriteLine($"Index is {index}");
        //    // pure from here onward
        //    if (index is 0 or 1)
        //    {
        //        return 1;
        //    }
        //    return Calculate(index - 1) + Calculate(index - 2);
        //}
    }
}
