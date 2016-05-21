using System;
using BenchmarkDotNet.Attributes;
using DynaCache;

namespace NDynaCacheBenchmarkApp
{
    public class DynaCacheBenchmark
    {
        public DynaCacheBenchmark()
        {
            OriginTimeService = new OriginTimeService();
            CachedTimeService = CacheableActivator<CachedTimeService>.CreateInstance(OriginTimeService);
        }

        private OriginTimeService OriginTimeService { get; set; }
        private CachedTimeService CachedTimeService { get; set; }

        [Benchmark]
        public DateTime OriginTimeServiceBenchmark()
        {
            return OriginTimeService.GetTime();
        }

        [Benchmark]
        public DateTime CachedTimeServiceBenchmark()
        {
            return CachedTimeService.GetTime();
        }
    }

    public sealed class OriginTimeService
    {
        public DateTime GetTime()
        {
            return DateTime.UtcNow;
        }
    }

    public class CachedTimeService
    {
        private readonly OriginTimeService _decorated;

        public CachedTimeService(OriginTimeService decorated)
        {
            _decorated = decorated;
        }

        [CacheableMethod(3600)]
        public DateTime GetTime()
        {
            return _decorated.GetTime();
        }
    }
}