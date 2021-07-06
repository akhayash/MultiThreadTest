using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MultiThreadTest.Service;

namespace MultiThreadTest.Command
{
    internal abstract class TestBase
    {
        internal readonly Result Result = new();
        internal readonly int[] TestArray = Enumerable.Range(1, 10000).ToArray();
        internal string Name;
        internal Stopwatch Sw;
        internal int TaskNum = 32;

        protected TestBase()
        {
            Sw = new Stopwatch();
        }

        internal abstract Task Execute();

        internal void OutputResult()
        {
            Console.WriteLine($"{Name,20}: Result:{Result.Value}  ETA :{Sw.ElapsedMilliseconds} milliseconds");
        }
    }
}