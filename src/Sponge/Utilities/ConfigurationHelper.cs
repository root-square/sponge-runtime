using Serilog;
using Sponge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Utilities
{
    /// <summary>
    /// A source generation context for configurations.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = false, PropertyNamingPolicy = JsonKnownNamingPolicy.KebabCaseLower, AllowTrailingCommas = true)]
    [JsonSerializable(typeof(Configuration))]

    internal partial class SourceGenerationContext : JsonSerializerContext { }

    public static class ConfigurationHelper
    {
        private static Configuration? _instance = null;

        public static Configuration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Configuration();
                }

                return _instance;
            }
        }

        public static void Write()
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

        public static void Read()
        {
            try
            {
                var json = TextFileHelper.ReadTextFile(VariableBuilder.GetConfigurationPath(), Encoding.UTF8);
                json = Encoding.UTF8.GetString(Convert.FromBase64String(json));
                _instance = JsonSerializer.Deserialize(json, SourceGenerationContext.Default.Configuration);
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
    }
}
