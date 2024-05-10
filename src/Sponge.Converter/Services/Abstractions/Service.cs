using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Agent.Services.Abstractions
{
    public interface IService : IDisposable
    {
        public static IService? Instance { get; private set; }

        public void Start();

        public Task StartAsync();

        public void Stop();

        public Task StopAsync();
    }
}
