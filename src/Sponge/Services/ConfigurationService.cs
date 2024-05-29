using NetCoreServer;
using Serilog;
using Sponge.Entities;
using Sponge.Services.Abstractions;
using Sponge.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class ConfigurationService : IService
    {
        #region ::Variables::

        public bool IsRoutable { get; set; } = true;

        public Dictionary<Route, RouteDelegate> Routes { get; init; } = new Dictionary<Route, RouteDelegate>();

        public Configuration Instance { get; private set; } = new Configuration();

        #endregion

        #region ::Constructors::

        public ConfigurationService()
        {
            Routes.Add(new Route("/api/config"), HandleConfigRequest);
        }

        #endregion

        #region ::Functions::

        public void Start()
        {
            try
            {
                if (Path.Exists(VariableBuilder.GetConfigurationPath()))
                {
                    var json = TextFileHelper.ReadTextFile(VariableBuilder.GetConfigurationPath(), Encoding.UTF8);
                    json = Encoding.UTF8.GetString(Convert.FromBase64String(json));
                    Instance = JsonSerializer.Deserialize(json, SourceGenerationContext.Default.Configuration) ?? new Configuration();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error(ex, "Unable to access the file. Failed to write the configuration data.");
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                Log.Error(ex, "The specified directory or file is not found.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unknown exception has occurred.");
            }
        }

        public void Stop()
        {
            try
            {
                var json = JsonSerializer.Serialize(Instance, SourceGenerationContext.Default.Configuration);
                json = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
                TextFileHelper.WriteTextFile(VariableBuilder.GetConfigurationPath(), json, Encoding.UTF8);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error(ex, "Unable to access the file. Failed to write the configuration data.");
            }
            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
            {
                Log.Error(ex, "The specified directory or file is not found.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unknown exception has occurred.");
            }
        }

        private void HandleConfigRequest(HttpSession session, HttpRequest request)
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
        // ~ConfigurationService()
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
