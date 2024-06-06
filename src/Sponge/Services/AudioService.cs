﻿using NetCoreServer;
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
    public class AudioService : IService
    {
        #region ::Variables::

        public bool IsRoutable { get; set; } = true;

        public Dictionary<Route, RouteDelegate> Routes { get; init; } = new Dictionary<Route, RouteDelegate>();

        public Configuration Instance { get; private set; } = new Configuration();

        #endregion

        #region ::Constructors::

        public AudioService()
        {
            
        }

        #endregion

        #region ::Functions::

        public void Start()
        {
            
        }

        public void Stop()
        {

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
        // ~AudioService()
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
