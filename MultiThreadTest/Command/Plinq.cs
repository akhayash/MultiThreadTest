using System.Linq;
using System.Threading.Tasks;
using MultiThreadTest.Service;

namespace MultiThreadTest.Command
{
    internal class PLinq : TestBase
    {
        internal PLinq()
        {
            Name = "PLinq";
        }

        internal override async Task Execute()
        {
            Sw.Start();

            /* P-LINQ */
            Enumerable.Range(1, TaskNum).AsParallel()
                .ForAll(idx => Calculator.CalcArrayAsync(idx, Result, TestArray).Wait());
            await Task.Delay(1);

            Sw.Stop();
        }
    }
}