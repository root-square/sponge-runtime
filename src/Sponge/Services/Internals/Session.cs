﻿using NetCoreServer;
using Serilog;
using Sponge.Entities.Responses;
using Sponge.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace Sponge.Services.Internals
{
    public class Session : HttpSession
    {
        private Dictionary<string, RouteDelegate> _routes = new Dictionary<string, RouteDelegate>();

        public Session(HttpServer server, Dictionary<string, RouteDelegate> routes) : base(server)
        {
            _routes = routes;
        }

        protected override void OnReceivedRequest(HttpRequest request)
        {
            var path = request.Url.IndexOf("?") == -1 ? request.Url : request.Url.Substring(0, request.Url.IndexOf("?"));
            var queries = HttpUtility.ParseQueryString(request.Url);

            RouteDelegate? requestHandler = null;

            if (_routes.TryGetValue(path.ToLower(), out requestHandler))
            {
                try
                {
                    requestHandler(this, request);
                }
                catch (Exception ex)
                {
                    var errorResponse = JsonSerializer.Serialize(new Response(ResponseCode.InternalServerError, "SESSION_INTERNAL_SERVER_ERROR"), SourceGenerationContext.Default.Response);
                    SendResponseAsync(Response.MakeErrorResponse(500, errorResponse, "application/json; charset=UTF-8"));
                    Log.Error(ex, "An unknown exception has occurred. Unable to process the request.");
                }
            }
            else
            {
                var errorResponse = JsonSerializer.Serialize(new Response(ResponseCode.NotFound, "SESSION_FILE_NOT_FOUND"), SourceGenerationContext.Default.Response);
                SendResponseAsync(Response.MakeErrorResponse(404, errorResponse, "application/json; charset=UTF-8"));
            }
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)
        {
            Log.Error($"HTTP request error: {error}");
        }

        protected override void OnError(SocketError error)
        {
            Log.Error($"HTTP session caught an error: {error}");
        }
    }
}
