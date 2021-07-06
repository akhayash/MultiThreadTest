using System.Linq;
using System.Threading.Tasks;
using MultiThreadTest.Service;

namespace MultiThreadTest.Command
{
    internal class ParallelForeach : TestBase
    {
        internal ParallelForeach()
        {
            Name = "Parallel For Each";
        }

        internal override async Task Execute()
        {
            Sw.Start();
            var parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = TaskNum
            };

            Parallel.ForEach(Enumerable.Range(1, TaskNum), parallelOptions,
                idx => { Calculator.CalcArrayAsync(idx, Result, TestArray).Wait(); });

            Sw.Stop();

            await Task.Delay(1);
        }
    }
}