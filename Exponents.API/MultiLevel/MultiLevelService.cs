namespace Exponents.API.MultiLevel;

using System.Numerics;
using Microsoft.Extensions.Caching.Memory;

public class MultiLevelService
{
    private readonly IMemoryCache cache;

    public MultiLevelService(IMemoryCache cache)
    {
        this.cache = cache;
    }

    public string MultiLevelExponentCalculation(int a, int b, int c)
    {
        var topLevelResult = (int)BigInteger.Pow(b, c);

        return $"{BigInteger.Pow(a, topLevelResult)}";
    }

    public string MultiLevelExponentWithCache(int a, int b, int c)
    {
        var cacheKey = $"{a}-{b}-{c}";
        var cacheItem = this.cache.Get<string>(cacheKey);

        if (cacheItem is not null)
        {
            return cacheItem;
        }

        var result = this.MultiLevelExponentCalculation(a, b, c);

        _ = this.cache.Set(cacheKey, result);

        return result;
    }
}

