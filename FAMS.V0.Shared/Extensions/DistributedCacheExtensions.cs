using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FAMS.V0.Shared.Extensions;

public static class DistributedCacheExtensions
{
    /// <summary>
    /// Serialize and store data into redis database
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="recordId">Id of the record to be stored</param>
    /// <param name="data">Actual data to store</param>
    /// <param name="absoluteExpireTime">Expire time of cache (default will be set to 60 seconds if not provided)</param>
    /// <param name="unusedExpireTime">Expire time of cache if data is not used in this amount of time</param>
    /// <typeparam name="T">Type of data</typeparam>
    public static async Task SetRecordAsync<T>(
        this IDistributedCache cache,
        string recordId,
        T data,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? unusedExpireTime = null
    )
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60),
            SlidingExpiration = unusedExpireTime
        };

        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, jsonData, options);
    }

    /// <summary>
    /// Get cached data from redis database
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="recordId">Id of the record be read</param>
    /// <typeparam name="T">Type of data</typeparam>
    /// <returns>Data of type T if record id exists, if not return default value</returns>
    public static async Task<T?> GetRecordAsync<T>(
        this IDistributedCache cache,
        string recordId
    )
    {
        var jsonData =  await cache.GetStringAsync(recordId);
        return jsonData is null ? default : JsonSerializer.Deserialize<T>(jsonData);
    }
}