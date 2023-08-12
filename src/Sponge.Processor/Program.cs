using CliFx;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using Sponge.Processor.Utilities;

namespace Sponge.Processor
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
                // Parse global options.
                bool enableDebugMode = ConsoleHelper.Parse<bool>(args, "debug", 'd', false);

                // Initialize the Serilog logger.
                string fileName = Path.Combine(Environment.CurrentDirectory, @"logs\.log");
                string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u4}] {Message}{NewLine}{Exception}";

                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(enableDebugMode ? LogEventLevel.Verbose : LogEventLevel.Information)
                    .WriteTo.Async(a => a.Console(outputTemplate: outputTemplateString, theme: AnsiConsoleTheme.Code))
                    .WriteTo.Async(a => a.File(fileName, outputTemplate: outputTemplateString, shared: true, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                    .CreateLogger();

                Log.Verbose("The logging system has initialized.");

                // Initialize the global exception handler.
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception has been occurred. If the same problem persists, please report it to the program provider.");
                    Log.CloseAndFlush();
                };

                Log.Verbose("The global exception handler has initialized.");
            });

            await task;
        }

        private static async Task<int> RunAsync()
        {
            int result = await new CliApplicationBuilder()
                .SetTitle("Sponge Processor")
                .SetDescription("An application providing core functions of Sponge.")
                .SetExecutableName("./" + (Path.GetFileName(Environment.ProcessPath) ?? "spgproc.exe"))
                .AddCommandsFromThisAssembly()
                .UseTypeActivator(commandTypes =>
                {
                    // We use Microsoft.Extensions.DependencyInjection for injecting dependencies in commands
                    var services = new ServiceCollection();
                    // services.AddSingleton<LibraryProvider>();

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