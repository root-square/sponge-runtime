using NetCoreServer;
using Sponge.Entities.Configurations;
using Sponge.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class DiagnosticsService : Service
    {
        public DiagnosticsService() : base(isRoutable: true)
        {
            Routes.Add(new Route("/api/status"), HandleStatusRequest);
            Routes.Add(new Route("/api/doctor"), HandleDoctorRequest);
            IsInitialized = true;
        }

        public override void Start()
        {
            IsRunning = true;
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        private void HandleStatusRequest(HttpSession session, HttpRequest request)
        {
            switch (request.Method)
            {
                case "HEAD":
                    session.SendResponseAsync(session.Response.MakeHeadResponse());
                    break;
                case "TRACE":
                    session.SendResponseAsync(session.Response.MakeTraceResponse(request.Cache.Data));
                    break;
                case "OPTIONS":
                    session.SendResponseAsync(session.Response.MakeOptionsResponse("HEAD,GET,OPTIONS,TRACE"));
                    break;
                case "GET":
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, "501 - Unsupported HTTP method: " + request.Method));
                    break;
                default:
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, "501 - Unsupported HTTP method: " + request.Method));
                    break;
            }
        }

        private void HandleDoctorRequest(HttpSession session, HttpRequest request)
        {
            switch (request.Method)
            {
                case "HEAD":
                    session.SendResponseAsync(session.Response.MakeHeadResponse());
                    break;
                case "TRACE":
                    session.SendResponseAsync(session.Response.MakeTraceResponse(request.Cache.Data));
                    break;
                case "OPTIONS":
                    session.SendResponseAsync(session.Response.MakeOptionsResponse("HEAD,GET,OPTIONS,TRACE"));
                    break;
                case "GET":
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, "501 - Unsupported HTTP method: " + request.Method));
                    break;
                default:
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, "501 - Unsupported HTTP method: " + request.Method));
                    break;
            }
        }
    }
}
