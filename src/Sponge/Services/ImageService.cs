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
    public class ImageService : IService
    {
        #region ::Variables::

        public bool IsRoutable { get; set; } = true;

        public Dictionary<Route, RouteDelegate> Routes { get; init; } = new Dictionary<Route, RouteDelegate>();

        #endregion

        #region ::Constructors::

        public ImageService()
        {
            //Routes.Add(new Route("/api/image"), HandleConfigRequest);
        }

        #endregion

        #region ::Functions::

        public void Start()
        {
            NetVips.ModuleInitializer.Initialize();

            if (!NetVips.ModuleInitializer.VipsInitialized)
            {
                Log.Fatal(NetVips.ModuleInitializer.Exception, "Unable to load NetVips components.");

                ServiceProvider.Instance.Stop();
            }
        }

        public void Stop()
        {
            if (NetVips.ModuleInitializer.Exception == null)
            {
                NetVips.NetVips.Shutdown();
            }
        }

        #endregion

        #region ::IDisposable Components::

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: Remove managed resources.
                }

                // TODO: Release unmanaged resources, and re-define the destructor.
                // TODO: Set large fields to null.
                _disposedValue = true;
            }
        }

        // // TODO: Only if 'Dispose(bool disposing)' contains a logic to release unmanaged resources, re-define the destructor. 
        // ~ImageService()
        // {
        //     // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
