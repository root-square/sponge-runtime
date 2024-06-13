using NetCoreServer;
using Sponge.Entities.Configurations;
using Sponge.Services.Abstractions;
using Sponge.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class AudioService : Service
    {
        public AudioService(ServiceProvider provider) : base(provider, isRoutable: false)
        {
            IsInitialized = true;
        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Stop()
        {
            IsRunning = false;
        }
    }
}
