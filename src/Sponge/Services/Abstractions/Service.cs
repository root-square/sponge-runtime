using NetCoreServer;
using Sponge.Services.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services.Abstractions
{
    public abstract class Service
    {
        public ServiceProvider? Provider { get; set; } = null;

        public bool IsInitialized { get; set; } = false;

        public bool IsRunning { get; set; } = false;

        public bool IsRoutable { get; } = false;

        public Dictionary<Route, RouteDelegate> Routes { get; } = new Dictionary<Route, RouteDelegate>();

        public Service(ServiceProvider provider, bool isRoutable) 
        {
            Provider = provider;
            IsRoutable = isRoutable;
        }

        public abstract void Start();

        public abstract void Stop();
    }
}
