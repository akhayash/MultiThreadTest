using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MultiThreadTest.Service;

namespace MultiThreadTest.Command
{
    internal class Tpl : TestBase
    {
        internal Tpl()
        {
            Name = "TPL";
        }

        internal override async Task Execute()
        {
            Sw.Start();

            /* TPL */

            var cancellationSource = new CancellationTokenSource(); // give cancel if required in the code.

            var calculationBlock = new ActionBlock<ulong>(_ =>
            {
                // Console.WriteLine(n);
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
                CancellationToken = cancellationSource.Token
            });

            var transformBlock = new TransformBlock<int, ulong>(async index =>
            {
                await Calculator.CalcArrayAsync(index, Result, TestArray);
                // Console.WriteLine($"Transform {index} => {Result.Value}");
                return Result.Value;
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
                EnsureOrdered = false,
                BoundedCapacity = DataflowBlockOptions.Unbounded,
                CancellationToken = cancellationSource.Token
            });

            transformBlock.LinkTo(calculationBlock);

            foreach (var idx in Enumerable.Range(1, TaskNum))
                await transformBlock.SendAsync(idx, cancellationSource.Token);

            transformBlock.Complete();
            //Completion should includes in Try-Catch block when exception is required to be caught.
            transformBlock.Completion.Wait(cancellationSource.Token);
            // Console.WriteLine($"Result -> {Result.Value}");

            Sw.Stop();
        }
    }
}