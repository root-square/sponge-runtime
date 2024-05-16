using NetCoreServer;
using Serilog;
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
    class Session : HttpSession
    {
        public Session(HttpServer server) : base(server) { }

        protected override void OnReceivedRequest(HttpRequest request)
        {
            var path = request.Url.IndexOf("?") == -1 ? request.Url : request.Url.Substring(0, request.Url.IndexOf("?"));
            var queries = HttpUtility.ParseQueryString(request.Url);

            switch (request.Method)
            {
                case "HEAD":
                    SendResponseAsync(Response.MakeHeadResponse());
                    break;
                case "TRACE":
                    SendResponseAsync(Response.MakeTraceResponse(request.Cache.Data));
                    break;
                case "OPTIONS":
                    SendResponseAsync(Response.MakeOptionsResponse("HEAD,GET,POST,OPTIONS,TRACE"));
                    break;
                case "GET":

                    break;
                case "POST":

                    break;
                default:
                    SendResponseAsync(Response.MakeErrorResponse("Unsupported HTTP method: " + request.Method));
                    break;
            }
            /*
            else if (request.Method == "GET")
            {
                string key = request.Url;

                // Decode the key value
                key = Uri.UnescapeDataString(key);
                key = key.Replace("/api/cache", "", StringComparison.InvariantCultureIgnoreCase);
                key = key.Replace("?key=", "", StringComparison.InvariantCultureIgnoreCase);

                if (string.IsNullOrEmpty(key))
                {
                    // Response with all cache values
                    SendResponseAsync(Response.MakeGetResponse(CommonCache.GetInstance().GetAllCache(), "application/json; charset=UTF-8"));
                }
                // Get the cache value by the given key
                else if (CommonCache.GetInstance().GetCacheValue(key, out var value))
                {
                    // Response with the cache value
                    SendResponseAsync(Response.MakeGetResponse(value));
                }
                else
                    SendResponseAsync(Response.MakeErrorResponse(404, "Required cache value was not found for the key: " + key));
            }
            else if ((request.Method == "POST") || (request.Method == "PUT"))
            {
                string key = request.Url;
                string value = request.Body;

                // Decode the key value
                key = Uri.UnescapeDataString(key);
                key = key.Replace("/api/cache", "", StringComparison.InvariantCultureIgnoreCase);
                key = key.Replace("?key=", "", StringComparison.InvariantCultureIgnoreCase);

                // Put the cache value
                CommonCache.GetInstance().PutCacheValue(key, value);

                // Response with the cache value
                SendResponseAsync(Response.MakeOkResponse());
            }*/
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
