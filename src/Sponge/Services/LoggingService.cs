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
    public class LoggingService : Service
    {
        public LoggingService() : base(isRoutable: false)
        {
            IsInitialized = true;
        }

        public override void Start()
        {
            string fileName = Path.Combine(VariableBuilder.GetBaseDirectory(), @"logs\.log");
            string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u3}] {Message}{NewLine}{Exception}";

            var log = new LoggerConfiguration()
                .WriteTo.Async(sink => sink.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: outputTemplateString))
                .WriteTo.Async(sink => sink.File(fileName, restrictedToMinimumLevel: LogEventLevel.Warning, shared: true, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                .CreateLogger();

            Log.Logger = log;

            IsRunning = true;
        }

        public override void Stop()
        {
            Log.CloseAndFlush();

            IsRunning = false;
        }
    }
}
