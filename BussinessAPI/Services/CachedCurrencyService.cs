using System;
using BussinessAPI.Interfaces;
using BussinessAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace BussinessAPI
{
    public class CachedCurrencyService : ICurrencyServices
    {   
        private const string VehicleListCacheKey = "VehicleList";
        private readonly IMemoryCache _memoryCache;
        private readonly ICurrencyServices _vehicleService;

        public CachedCurrencyService(
            ICurrencyServices vehicleService,
            IMemoryCache memoryCache)
        {
            _vehicleService = vehicleService;
            _memoryCache = memoryCache;
        }

        public async Task<List<Currency>> GetAllAsync()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

            if (_memoryCache.TryGetValue(VehicleListCacheKey, out List<Currency> query))
                return query;

            query = await _vehicleService.GetAllAsync();

            _memoryCache.Set(VehicleListCacheKey, query, cacheOptions);

            return query;

        }
    }
}
