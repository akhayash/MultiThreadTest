using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadTest.Service
{
    internal static class Calculator
    {
        private static readonly SemaphoreSlim Semaphore = new(1, 1);

        public static async Task CalcArrayAsync(int taskIndex, Result result, int[] testArray)
        {
            foreach (var t in testArray)
            {
                await Semaphore.WaitAsync();
                {
                    result.Value += (ulong) t;
                    // System.Console.WriteLine($"{taskIndex} -> { Result.Value}");
                    // await Task.Delay(100);
                }
                Semaphore.Release();
            }
        }
    }
}