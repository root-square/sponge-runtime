using NetCoreServer;
using Serilog;
using Sponge.Entities.Configurations;
using Sponge.Entities.Responses;
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
    public class ConfigurationService : Service
    {
        public Configuration Instance { get; private set; } = new Configuration();

        public ConfigurationService() : base(isRoutable: true)
        {
            Routes.Add(new Route("/api/config"), HandleConfigRequest);
        }

        public override void Start()
        {
            try
            {
                if (Path.Exists(VariableBuilder.GetConfigurationPath()))
                {
                    var json = TextFileHelper.ReadTextFile(VariableBuilder.GetConfigurationPath(), Encoding.UTF8);
                    json = Encoding.UTF8.GetString(Convert.FromBase64String(json));
                    Instance = JsonSerializer.Deserialize(json, SourceGenerationContext.Default.Configuration) ?? new Configuration();

                    Exception? exception = null;
                    if (!Validate(out exception))
                    {
                        Log.Error(exception, "Unable to load configurations.");
                        ServiceProvider.Instance.Stop(1);
                    }
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

        public override void Stop()
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

        private bool Validate(out Exception? exception)
        {
            exception = null;

            if (Instance.Runtime.Port < 0 || Instance.Runtime.Port > 65535)
            {
                exception = new ArgumentException("The valid range of the port number is from 0 to 65535.", "RUNTIME_PORT");
            }

            if (Instance.Runtime.Timeout < 10 || Instance.Runtime.Timeout > 1440)
            {
                exception = new ArgumentException("The valid range of the timeout is from 10 to 1440.", "RUNTIME_TIMEOUT");
            }

            if (Instance.Link.Enable)
            {
                if (!(Instance.Link.Priority == 0 || Instance.Link.Priority == 1))
                {
                    exception = new ArgumentException("Unable to parse a configuration item.", "LINK_PRIORITY");
                }

                if (string.IsNullOrEmpty(Instance.Link.Target))
                {
                    exception = new ArgumentException("The link target cannot be null or empty.", "LINK_TARGET");
                }

                if (!File.Exists(Instance.Link.Target))
                {
                    exception = new ArgumentException("The link target cannot be found.", "LINK_TARGET");
                }
            }

            if (Instance.Caching.Enable)
            {
                if (!(Instance.Caching.Strategy == "lru" || Instance.Caching.Strategy ==  "lfu"))
                {
                    exception = new ArgumentException("An invalid caching strategy was inputted.", "CACHING_STRATEGY");
                }

                if (Instance.Caching.Capacity < 100 || Instance.Caching.Capacity > 10000)
                {
                    exception = new ArgumentException("The valid range of the caching capacity is from 100 to 10000.", "CACHING_CAPACITY");
                }

                if (Instance.Caching.Duration < 1 || Instance.Caching.Duration > 1440)
                {
                    exception = new ArgumentException("The valid range of the caching duration is from 1 to 1440.", "CACHING_DURATION");
                }
            }

            if (Instance.Audio.Enable)
            {
                if (Instance.Audio.UseMultiThreading && (Instance.Audio.MinThreads < 1 || Instance.Audio.MaxThreads > 32))
                {
                    exception = new ArgumentException("The valid range of the number of threads is from 1 to 32.", "AUDIO_THREADS");
                }

                if (Instance.Audio.UseMultiThreading && Instance.Audio.MinThreads >= Instance.Audio.MaxThreads)
                {
                    exception = new ArgumentException("The start of range cannot be greater than or equal to the end of range.", "AUDIO_THREADS");
                }
            }

            if (Instance.Image.Enable)
            {
                if (Instance.Image.UseMultiThreading && (Instance.Image.MinThreads < 1 || Instance.Image.MaxThreads > 32))
                {
                    exception = new ArgumentException("The valid range of the number of threads is from 1 to 32.", "IMAGE_THREADS");
                }

                if (Instance.Image.UseMultiThreading && Instance.Image.MinThreads >= Instance.Image.MaxThreads)
                {
                    exception = new ArgumentException("The start of range cannot be greater than or equal to the end of range.", "IMAGE_THREADS");
                }

                foreach (var pair in Instance.Image.Parameters) {
                    var format = pair.Key.ToUpper();
                    var parameters = pair.Value;

                    if (parameters.Q != null && (parameters.Q < 1 || parameters.Q > 100))
                    {
                        exception = new ArgumentException("The valid range of the Q-value is from 1 to 100.", $"IMAGE_{format}_Q");
                    }

                    if (parameters.Effort != null && (parameters.Effort < 1 || parameters.Effort > 10))
                    {
                        exception = new ArgumentException("The valid range of the effort is from 1 to 10.", $"IMAGE_{format}_EFFORT");
                    }

                    if (parameters.Compression != null && (parameters.Compression < 1 || parameters.Compression > 10))
                    {
                        exception = new ArgumentException("The valid range of the compression is from 1 to 10.", $"IMAGE_{format}_COMPRESSION");
                    }
                }
            }

            return exception == null;
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
                    session.SendResponseAsync(session.Response.MakeOptionsResponse("HEAD,GET,PUT,DELETE,OPTIONS,TRACE"));
                    break;
                case "GET":
                    var getResponse = JsonSerializer.Serialize(new ConfigurationResponse(ResponseCode.OK, "CONFIGURATION_SERVICE_API_READ", Instance), SourceGenerationContext.Default.ConfigurationResponse);
                    session.SendResponseAsync(session.Response.MakeGetResponse(getResponse, "application/json; charset=UTF-8"));
                    break;
                case "PUT":
                    try
                    {
                        var config = JsonSerializer.Deserialize(request.Body, SourceGenerationContext.Default.Configuration);
                        var putResponse = JsonSerializer.Serialize(new Response(ResponseCode.OK, "CONFIGURATION_SERVICE_API_WRITE"), SourceGenerationContext.Default.Response);
                        session.SendResponseAsync(session.Response.MakeGetResponse(putResponse, "application/json; charset=UTF-8"));
                    }
                    catch (Exception ex)
                    {
                        var putResponse = JsonSerializer.Serialize(new Response(ResponseCode.InternalServerError, "CONFIGURATION_SERVICE_INTERNAL_SERVER_ERROR"), SourceGenerationContext.Default.Response);
                        session.SendResponseAsync(session.Response.MakeErrorResponse(500, putResponse, "application/json; charset=UTF-8"));
                        Log.Error(ex, "Unable to write configurations.");
                    }
                    break;
                case "DELETE":
                    Instance = new Configuration();
                    var deleteResponse = JsonSerializer.Serialize(new Response(ResponseCode.OK, "CONFIGURATION_SERVICE_API_RESET"), SourceGenerationContext.Default.Response);
                    session.SendResponseAsync(session.Response.MakeGetResponse(deleteResponse, "application/json; charset=UTF-8"));
                    break;
                default:
                    var defaultResponse = JsonSerializer.Serialize(new Response(ResponseCode.NotImplemented, $"Unsupported HTTP Method: {request.Method}"), SourceGenerationContext.Default.Response);
                    session.SendResponseAsync(session.Response.MakeErrorResponse(501, defaultResponse, "application/json; charset=UTF-8"));
                    break;
            }
        }
    }
}
