using BenchmarkDotNet.Running;
using Sponge.Benchmarks.Jobs;

namespace Sponge.Benchmarks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Signature();
            Run();
        }

        private static void Signature()
        {
            Console.WriteLine($"Sponge Benchmark");
            Console.WriteLine($"Copyright (c) 2024 Sponge Contributors all rights reserved.");
            Console.WriteLine();
        }
        private static void Run()
        {
            var avifSummary = BenchmarkRunner.Run<AVIF>();
            var gifSummary = BenchmarkRunner.Run<GIF>();
            var jpegSummary = BenchmarkRunner.Run<JPEG>();
            var webpSummary = BenchmarkRunner.Run<WEBP>();
        }
    }
}