using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Agent.Services.Abstractions
{
    internal interface IService : IDisposable
    {
        internal Task<bool> Start();

        internal Task<bool> Stop();
    }
}
