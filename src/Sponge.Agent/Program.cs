using Serilog;
using Serilog.Events;
using Sponge.Agent.Utilities;
using System.Net;

namespace Sponge.Agent
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Signature();
            InitializeSerilog();
            InitializeNetVips();
            InitializeServices();
        }

        private static void Signature()
        {
            Console.WriteLine($"Sponge Agent");
            Console.WriteLine($"Copyright (c) 2024 Sponge Contributors all rights reserved.");
            Console.WriteLine();
        }

        private static void InitializeSerilog()
        {
            string fileName = Path.Combine(VariableBuilder.GetBaseDirectory(), @"logs\.log");
            string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u3}] {Message}{NewLine}{Exception}";

            var log = new LoggerConfiguration()
                .WriteTo.Async(sink => sink.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: outputTemplateString))
                .WriteTo.Async(sink => sink.File(fileName, restrictedToMinimumLevel: LogEventLevel.Warning, shared: true, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                .CreateLogger();

            Log.Logger = log;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                var exception = e.ExceptionObject as Exception;
                Log.Fatal(exception ?? new Exception("Failed to load the exception information."), "An unhandled exception has occurred.");
                Log.CloseAndFlush();
            };
        }

        private static void InitializeNetVips()
        {
            NetVips.ModuleInitializer.Initialize();

            if (!NetVips.ModuleInitializer.VipsInitialized)
            {
                Log.Fatal(NetVips.ModuleInitializer.Exception, "Unable to load NetVips components.");
                Log.CloseAndFlush();
                Environment.Exit(2); // ENOENT: No such file or directory.
            }
        }

        private static void InitializeSystem()
        {
            /*int port = 40126;

            Log.Information($"HTTP server port: {port}");
            Log.Information($"HTTP server website: http://localhost:{port}/api/index.html");


            // Create a new HTTP server
            var server = new Server(IPAddress.Any, port);
            //server.AddStaticContent(www, "/api");

            // Start the server
            Log.Information("Server starting...");
            server.Start();
            Log.Information("Done!");

            Log.Information("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Log.Information("Server restarting...");
                    server.Restart();
                    Log.Information("Done!");
                }
            }

            // Stop the server
            Log.Information("Server stopping...");
            server.Stop();
            Log.Information("Done!");*/
        }

        private static void InitializeServices()
        {

        }
    }
}
