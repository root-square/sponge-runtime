using NetCoreServer;
using Sponge.Services.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services.Abstractions
{
    public interface IService : IDisposable
    {
        #region ::Variables::

        public bool IsRoutable { get; }

        public Dictionary<Route, RouteDelegate> Routes { get; init; }

        #endregion

        #region ::Functions::

        public void Start();

        public void Stop();

        #endregion
    }
}
