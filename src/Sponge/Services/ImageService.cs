using NetCoreServer;
using Serilog;
using Sponge.Entities;
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
    public class ImageService : Service
    {
        public ImageService(ServiceProvider provider) : base(provider, isRoutable: true)
        {
            //Routes.Add(new Route("/api/image"), HandleConfigRequest);
            IsInitialized = true;
        }

        public override void Start()
        {
            NetVips.ModuleInitializer.Initialize();

            if (!NetVips.ModuleInitializer.VipsInitialized)
            {
                Log.Fatal(NetVips.ModuleInitializer.Exception, "Unable to load NetVips components.");

                ServiceProvider.Instance.Stop();
            }

            IsRunning = true;
        }

        public override void Stop()
        {
            if (NetVips.ModuleInitializer.Exception == null)
            {
                NetVips.NetVips.Shutdown();
            }

            IsRunning = false;
        }
    }
}
