using Sponge.Services.Abstractions;
using Sponge.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services
{
    internal class LinkService : IService
    {
        public void Start() => AsyncHelper.RunSync(() => StartAsync());

        public async Task StartAsync()
        {

        }

        public void Stop() => AsyncHelper.RunSync(() => StopAsync());

        public async Task StopAsync()
        {

        }

        #region ::IDisposable Components::

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: Release managed resources.
                }

                // TODO: Release unmanaged resources.
                // TODO: Set large objects to null.
                _disposedValue = true;
            }
        }

        // // TODO: Only if the 'Dispose(bool disposing)' has a logic to release unmanaged resources, re-define the destructor.
        // ~LinkService()
        // {
        //     // DO NOT CHANGE THIS CODE. It input a disposing code to the 'Dispose(bool disposing)' method.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
