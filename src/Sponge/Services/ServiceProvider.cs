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
    /// <summary>
    /// Manages <see cref="Service">services</see> for Sponge, and provides a runtime environment server.
    /// </summary>
    public class ServiceProvider
    {
        /// <summary>
        /// A lazy instance to provide a singleton instance.
        /// </summary>
        private static readonly Lazy<ServiceProvider> _lazy = new Lazy<ServiceProvider>(() => new ServiceProvider());

        /// <summary>
        /// The singleton instance of the <see cref="ServiceProvider"/>.
        /// </summary>
        public static ServiceProvider Instance => _lazy.Value;

        /// <summary>
        /// The server instance which is the <see cref="ServiceProvider"/> using.
        /// </summary>
        public Server? Server { get; private set; } = null;

        /// <summary>
        /// The services are provided by the <see cref="ServiceProvider"/>.
        /// </summary>
        public Dictionary<string, Service> Services { get; init; }

        /// <summary>
        /// Initialize an instance of <see cref="ServiceProvider"/>.
        /// </summary>
        private ServiceProvider()
        {
            // SET-UP: Add services to the dictionary.
            Services = new Dictionary<string, Service>
            {
                // WARNING: DO NOT CHANGE THE ORDER OF SERVICES. A fatal error can be occurred.
                { "SVC_LOGGING", new LoggingService() },
                { "SVC_CONFIG", new ConfigurationService() },
                { "SVC_DIAGNOSTICS", new DiagnosticsService() },
                { "SVC_LINK", new LinkService() },
                { "SVC_CACHING", new CachingService() },
                { "SVC_AUDIO", new AudioService() },
                { "SVC_IMAGE", new ImageService() }
            };

            // INIT: Initialize an unhandled exception handler.
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                Log.Fatal(exception ?? new Exception("Failed to load an exception information."), "An unhandled exception has occurred.");
                Stop();
            };
        }

        /// <summary>
        /// Start the <see cref="ServiceProvider"/>.
        /// </summary>
        public void Start()
        {
            // INIT: Start services.
            foreach (var pair in Services)
            {
                var service = pair.Value;
                service.Start();
            }

            // INIT: Initialize a routing table.
            var routes = new Dictionary<string, RouteDelegate>();

            foreach (var service in Services)
            {
                if (!service.Value.IsRoutable)
                {
                    continue;
                }

                var subroutes = service.Value.Routes;
                foreach (var subroute in subroutes)
                {
                    var routeKeys = subroute.Key.ToArray();
                    routeKeys.All((key) => routes.TryAdd(key, subroute.Value));
                }
            }

            // INIT: Get an available port.
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
                    Stop();
                }
            }

            // INIT: Start the server, and listen user inputs.
            Server = new Server(IPAddress.Any, (int)port!, routes);
            Server.Start();

            Log.Information($"Server listening on port: {port}");
            Log.Information("Press Enter to stop the server or '!' to restart the server...");

            for (; ; )
            {
                string? line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                if (line == "!")
                {
                    Restart();
                }
            }
        }

        /// <summary>
        /// Restart the <see cref="ServiceProvider"/>.
        /// </summary>
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

        /// <summary>
        /// Stop the <see cref="ServiceProvider"/>.
        /// </summary>
        /// <param name="exitCode">An exit code to emit</param>
        public void Stop(int exitCode = 0)
        {
            try
            {
                if (Server?.Stop() == true)
                {
                    Log.Information("The server has been stopped successfully.");
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unable to stop the server.");
            }

            foreach (var pair in Services.Reverse())
            {
                var service = pair.Value;
                service.Stop();
            }

            Environment.Exit(exitCode);
        }
    }
}
