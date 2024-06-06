using Serilog;
using Sponge.Services.Abstractions;
using Sponge.Services.Internals;
using Sponge.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class ServiceProvider
    {
        #region ::Singleton Components::

        private static readonly Lazy<ServiceProvider> _lazy = new Lazy<ServiceProvider>(() => new ServiceProvider());

        public static ServiceProvider Instance => _lazy.Value;

        #endregion

        #region ::Variables::

        public Server? Server { get; private set; } = null;

        public Dictionary<string, IService> Services { get; init; }

        public Dictionary<string, RouteDelegate> Routes { get; init; }

        #endregion

        #region ::Constructor::

        private ServiceProvider()
        {
            // SET-UP: Add services to the dictionary.
            Services = new Dictionary<string, IService>
            {
                // DO NOT CHANGE THE ORDER OF SERVICES.
                { "SVC_LOGGING", new LoggingService() },
                { "SVC_CONFIG", new ConfigurationService() },
                { "SVC_DIAGNOSTICS", new DiagnosticsService() },
                { "SVC_LINK", new LinkService() },
                { "SVC_CACHING", new CachingService() },
                { "SVC_AUDIO", new AudioService() },
                { "SVC_IMAGE", new ImageService() }
            };

            // INIT: Initialize a routing table.
            Routes = new Dictionary<string, RouteDelegate>();

            foreach (var service in Services)
            {
                if (!service.Value.IsRoutable)
                {
                    continue;
                }

                var routes = service.Value.Routes;
                foreach (var route in routes)
                {
                    var routePaths = route.Key.ToArray();
                    routePaths.All((path) => Routes.TryAdd(path, route.Value));
                }
            }
        }

        #endregion

        #region ::Functions::

        public void Start()
        {
            // INIT: Initialize services.
            foreach (var pair in Services)
            {
                var service = pair.Value;
                service.Start();
            }

            // INIT: Get an available port, and initialize a server.
            var configurationService = Services["SVC_CONFIG"] as ConfigurationService;
            var port = configurationService?.Instance.Runtime.Port;

            if (port == null || !NetworkHelper.IsPortAvailable((int)port))
            {
                if (configurationService?.Instance.Runtime.UseDynamicPort == true)
                {
                    port = NetworkHelper.GetAvailablePort(new Range(49152, 65535));
                }
                else
                {
                    Log.Fatal($"The port {port} is already in use. Unable to start a server.");
                    Stop(-1);
                }
            }

            Server = new Server(IPAddress.Any, (int)port!);
            Server.Start();

            Log.Information($"Server listening on port: {port}");
            Log.Information("Press Enter to stop the server or '!' to restart the server...");

            for (; ; )
            {
                Console.CursorVisible = false;
                string? line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                if (line == "!")
                {
                    Restart();
                }
            }
        }

        public void Restart()
        {
            if (Server?.Restart() == true)
            {
                Log.Information("The server was restarted successfully.");
            }
            else
            {
                Log.Fatal("Failed to restart the server.");
            }
        }

        public void Stop(int exitCode = 0)
        {
            if (Server?.Stop() == true)
            {
                Log.Information("The server has been stopped successfully.");
            }
            else
            {
                if (exitCode != -1)
                {
                    Log.Error("The server has already stopped.");
                }
            }

            foreach (var pair in Services.Reverse())
            {
                var service = pair.Value;
                service.Stop();
                service.Dispose();
            }

            Environment.Exit(exitCode);
        }

        #endregion
    }
}
