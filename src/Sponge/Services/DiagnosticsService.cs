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
    public class DiagnosticsService : IService
    {
        #region ::Variables::

        public bool IsRoutable { get; set; } = true;

        public Dictionary<Route, RouteDelegate> Routes { get; init; } = new Dictionary<Route, RouteDelegate>();

        public Configuration Instance { get; private set; } = new Configuration();

        #endregion

        #region ::Constructors::

        public DiagnosticsService()
        {
            Routes.Add(new Route("/api/status"), HandleStatusRequest);
        }

        #endregion

        #region ::Functions::

        public void Start()
        {

        }

        public void Stop()
        {

        }

        #endregion

        #region ::Handlers::

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
                    var json = JsonSerializer.Serialize(Instance, SourceGenerationContext.Default.Configuration);
                    session.SendResponseAsync(session.Response.MakeGetResponse(json, "application/json; charset=UTF-8"));
                    break;
                default:
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, "501 - Unsupported HTTP method: " + request.Method));
                    break;
            }
        }

        #endregion

        #region ::IDisposable Components::

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: Remove managed resources.
                }

                // TODO: Release unmanaged resources, and re-define the destructor.
                // TODO: Set large fields to null.
                _disposedValue = true;
            }
        }

        // // TODO: Only if 'Dispose(bool disposing)' contains a logic to release unmanaged resources, re-define the destructor. 
        // ~DiagnosticsService()
        // {
        //     // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
