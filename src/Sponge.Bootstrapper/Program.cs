using CliFx;
using CliWrap;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Sponge.Bootstrapper.Utilities;

namespace Sponge.Bootstrapper
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await InitializeAsync(args);
            return await RunAsync();
        }

        private static async Task InitializeAsync(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                // Parse options temporarily.
                bool enableDebugMode = ConsoleHelper.Parse<bool>(args, "debug", 'd', false);

                // Initialize the Serilog logger.
                string fileName = Path.Combine(Environment.CurrentDirectory, @"logs\spgboot-.log");
                string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u4}] {Message}{NewLine}{Exception}";

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(enableDebugMode ? LogEventLevel.Verbose : LogEventLevel.Information)
                    .WriteTo.Async(a => a.Console(outputTemplate: outputTemplateString, theme: AnsiConsoleTheme.Code))
                    .WriteTo.Async(a => a.File(fileName, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                    .CreateLogger();

                Log.Verbose("Initialized the Serilog logger.");

                // Initialize the global exception handler.
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception has been occurred. If the same problem persists, please report it to the program provider.");
                    Log.CloseAndFlush();
                };

                Log.Verbose("Initialized the global exception handler.");
            });

            await task;
        }

        private static async Task<int> RunAsync()
        {
            int result = await new CliApplicationBuilder()
                .SetTitle("Sponge Bootstrapper")
                .SetDescription("An application bootstrapping the specified game executable file and the Sponge system.")
                .SetExecutableName("./" + (Path.GetFileName(Environment.ProcessPath) ?? "spgboot.exe"))
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(commandTypes =>
                {
                    var services = new ServiceCollection();

                    // Register all commands as transient services
                    foreach (var commandType in commandTypes)
                        services.AddTransient(commandType);

                    return services.BuildServiceProvider();
                })
                .Build()
                .RunAsync();

            Log.Debug("The process exited(CODE : {result}).", result);

            await Log.CloseAndFlushAsync();

            return result;
        }
    }
}