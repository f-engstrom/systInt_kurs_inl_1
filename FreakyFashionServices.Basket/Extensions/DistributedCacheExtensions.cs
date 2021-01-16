using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace FreakyFashionServices.Basket.Extensions
{
    public static class DistributedCacheExtensions
    {

        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data,
            TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTIme = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(600);
            options.SlidingExpiration = unusedExpireTIme;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
            
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            var jsonData = await cache.GetStringAsync(recordId);
            if (jsonData == null)
            {
                return default(T);

            }

            return JsonSerializer.Deserialize<T>(jsonData);

        }
        
    }
}