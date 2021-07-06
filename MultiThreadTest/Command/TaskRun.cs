using MultiThreadTest.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreadTest.Command
{
    class TaskRun : TestBase
    {
        internal TaskRun()
        {
            Name = "TaskRun";
        }

        internal override async Task Execute()
        {
            Sw.Start();
            IEnumerable<Task> tasks = Enumerable.Range(1, TaskNum).Select(idx =>
            {
                return Task.Run(async () =>
                     {
                         await Calculator.CalcArrayAsync(idx, Result, TestArray);
                     }
                );
            });

            await Task.WhenAll(tasks);
            Sw.Stop();
        }
    }
}
