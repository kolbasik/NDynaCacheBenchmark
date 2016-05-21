using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace NDynaCacheBenchmarkApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var config = ManualConfig
                .Create(DefaultConfig.Instance)
                .With(Job.Default.WithLaunchCount(3).WithTargetCount(100));

            BenchmarkRunner.Run<DynaCacheBenchmark>(config);

            Console.WriteLine(@"Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}