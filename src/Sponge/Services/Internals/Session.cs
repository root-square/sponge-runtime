using NetCoreServer;
using Serilog;
using Sponge.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sponge.Services.Internals
{
    public class Session : HttpSession
    {
        private Dictionary<string, RouteDelegate> _routes = new Dictionary<string, RouteDelegate>();

        public Session(HttpServer server) : base(server)
        {
            _routes = ServiceProvider.Instance.Routes;
        }

        protected override void OnReceivedRequest(HttpRequest request)
        {
            var path = request.Url.IndexOf("?") == -1 ? request.Url : request.Url.Substring(0, request.Url.IndexOf("?"));
            var queries = HttpUtility.ParseQueryString(request.Url);

            RouteDelegate? requestHandler = null;

            if (_routes.TryGetValue(path.ToLower(), out requestHandler))
            {
                requestHandler(this, request);
            }
            else
            {
                SendResponseAsync(Response.MakeErrorResponse(404, content: "404 - File Not Found"));
            }
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)
        {
            Console.WriteLine($"HTTP request error: {error}");
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"HTTP session caught an error: {error}");
        }
    }
}
