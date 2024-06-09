using NetCoreServer;
using Serilog;
using Serilog.Events;
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
    public class LoggingService : IService
    {
        #region ::Variables::

        public bool IsRoutable { get; set; } = false;

        public Dictionary<Route, RouteDelegate> Routes { get; init; } = new Dictionary<Route, RouteDelegate>();

        #endregion

        #region ::Functions::

        public void Start()
        {
            string fileName = Path.Combine(VariableBuilder.GetBaseDirectory(), @"logs\.log");
            string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u3}] {Message}{NewLine}{Exception}";

            var log = new LoggerConfiguration()
                .WriteTo.Async(sink => sink.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: outputTemplateString))
                .WriteTo.Async(sink => sink.File(fileName, restrictedToMinimumLevel: LogEventLevel.Warning, shared: true, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                .CreateLogger();

            Log.Logger = log;


        }

        public void Stop()
        {
            Log.CloseAndFlush();
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
        // ~LoggingService()
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
