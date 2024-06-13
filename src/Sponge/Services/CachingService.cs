using BitFaster.Caching;
using BitFaster.Caching.Lfu;
using BitFaster.Caching.Lru;
using Sponge.Entities.Configurations;
using Sponge.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class CachingService : Service
    {
        public IAsyncCache<string, byte[]>? Instance { get; private set; }

        public CachingService() : base(isRoutable: true)
        {
            //Routes.Add(new Route("/api/cache"), HandleStatusRequest);
            IsInitialized = true;
        }

        public override void Start()
        {
            var a = ServiceProvider.Instance.Services["SVC_LOGGING"];

            IsRunning = true;
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        private IAsyncCache<string, byte[]> BuildLRUCache(int capacity, int expirationInterval = 10)
        {
            var cache = new ConcurrentLruBuilder<string, byte[]>()
                .WithCapacity(capacity)
                .WithAtomicGetOrAdd()
                .WithExpireAfterWrite(TimeSpan.FromMinutes(expirationInterval))
                .AsAsyncCache()
                .Build();

            return cache;
        }

        private IAsyncCache<string, byte[]> BuildLFUCache(int capacity)
        {
            var cache = new ConcurrentLfuBuilder<string, byte[]>()
                .WithCapacity(capacity)
                .WithAtomicGetOrAdd()
                .AsAsyncCache()
                .Build();

            return cache;
        }
    }
}
