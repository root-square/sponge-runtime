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
    public class WEBP
    {
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
