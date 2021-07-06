using MultiThreadTest.Command;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiThreadTest
{
    class Program
    {
        static async Task Main()
        {

            List<TestBase> processes = new List<TestBase>();

            TestBase taskRun = new TaskRun();
            TestBase parallelForeach = new ParallelForeach();
            TestBase plinq = new PLinq();
            TestBase tpl = new Tpl();
            processes.Add(taskRun);
            processes.Add(parallelForeach);
            processes.Add(plinq);
            processes.Add(tpl);

            foreach (var process in processes)
            {
                await process.Execute();
                process.OutputResult();
            }

        }
    }
}
