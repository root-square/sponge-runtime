using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Agent.Services.Internals
{
    class Server : HttpServer
    {
        public Server(IPAddress address, int port) : base(address, port) { }

        protected override TcpSession CreateSession() { return new Session(this); }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"HTTP session caught an error: {error}");
        }
    }
}
