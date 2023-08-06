using CliFx;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Sponge.Processor
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            await InitializeAsync();
            return await RunAsync();
        }

        private static async Task InitializeAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                // Initialize a Serilog logger.
                string fileName = Path.Combine(Environment.CurrentDirectory, @"logs\.log");
                string outputTemplateString = "{Timestamp:HH:mm:ss.ms} [{Level:u4}] {Message}{NewLine}{Exception}";

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Async(a => a.Console(restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: outputTemplateString))
                    .WriteTo.Async(a => a.File(fileName, restrictedToMinimumLevel: LogEventLevel.Verbose, outputTemplate: outputTemplateString, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10485760))
                    .CreateLogger();

                // Initialize an unhandled exception handler.
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    Log.Fatal(e.ExceptionObject as Exception, "An unhandled exception has been occurred. If the same problem persists, please report it to the program provider.");
                    Log.CloseAndFlush();
                };
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

            await Log.CloseAndFlushAsync();

            return result;
        }
    }
}