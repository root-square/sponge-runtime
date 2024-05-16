using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Benchmarks.Jobs
{
    [SimpleJob(RuntimeMoniker.Net80)]
    [SimpleJob(RuntimeMoniker.NativeAot80)]
    [RPlotExporter]
    public class AVIF
    {
        private readonly static string[] FILES = { "BARS_8bit_CMYKWRGB.png", "RAINBOW_8bit_RGB.png", "TESTCARD_8bit_RGB.png" };

        [Params(false, true)]
        public bool UseMultithreading;

        [GlobalSetup]
        public void Setup()
        {

        }

        [Benchmark]
        public void Encode()
        {

        }

        [Benchmark]
        public void Decode()
        {

        }
    }
}
