namespace Exponents.Benchmarks;

using System;
using System.Numerics;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Order;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

[RankColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 5, iterationCount: 10, id: "FastAndDirtyJob")]
public class Benchmark
{
    [Params(5)]
    public int Iterations;

    private readonly MemoryCache cache = new(Options.Create(new MemoryCacheOptions()));

    [Benchmark]
    public void NoCache()
    {
        _ = ExponentsCalculation(5, 6, 7);
    }

    [Benchmark]
    public void Cache()
    {
        _ = this.CachedExponentsCalculation(5, 6, 7);
    }

    private static string ExponentsCalculation(int a, int b, int c)
    {
        return $"{BigInteger.Pow(a, (int)Math.Pow(b, c))}";
    }

    private string CachedExponentsCalculation(int a, int b, int c)
    {
        var cacheKey = $"{a}-{b}-{c}";
        var cacheItem = this.cache.Get<string>(cacheKey);

        if (cacheItem is not null)
        {
            return cacheItem;
        }

        var result = ExponentsCalculation(a, b, c);

        _ = this.cache.Set(cacheKey, result, TimeSpan.FromSeconds(30));

        return result;
    }
}
