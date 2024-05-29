using Serilog;
using Serilog.Events;
using Sponge.Services;
using Sponge.Utilities;
using System.Net;

namespace Sponge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Sponge Runtime Environment ({VariableBuilder.GetFileVersion()})");
            Console.WriteLine($"Copyright (c) 2024 Sponge Contributors all rights reserved.");
            Console.WriteLine();

            var serviceProvider = ServiceProvider.Instance;
            serviceProvider.Start();
            serviceProvider.Stop();
        }
    }
}
