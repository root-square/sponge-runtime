using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sponge.Services.Abstractions;
using Serilog;

namespace Sponge.Services.Internals
{
    public class Server : HttpServer
    {
        private Dictionary<string, RouteDelegate>? _routes = null;

        public Server(IPAddress address, int port, Dictionary<string, RouteDelegate> routes) : base(address, port)
        {
            _routes = routes;
        }

        protected override TcpSession CreateSession() { return new Session(this, _routes ?? new Dictionary<string, RouteDelegate>()); }

        protected override void OnError(SocketError error)
        {
            Log.Error($"HTTP session caught an error: {error}");
        }
    }
}
