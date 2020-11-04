using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
		async static Task Main()
		{
			for (int i = 0; i < 10; i += 1)
			{
				Console.WriteLine(i);
				await Task.Delay(1000);
			}
		}
	}
}
