using Serilog;
using Sponge.Agent.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sponge.Agent
{
    /// <summary>
    /// A source generation context for Settings.
    /// </summary>
    [JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.KebabCaseLower, AllowTrailingCommas = true)]
    [JsonSerializable(typeof(Settings))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }

    public class Settings
    {
        #region ::Singleton Components / Functions::

        private static Settings? _instance = null;

        public static Settings Instance
        {
            get => _instance ?? (_instance = new Settings());
        }

        public static void Write()
        {
            try
            {
                var json = JsonSerializer.Serialize(_instance!, SourceGenerationContext.Default.Settings);
                FileManager.WriteTextFile(VariableBuilder.GetSettingsPath(), json, Encoding.UTF8);
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
                var json = FileManager.ReadTextFile(VariableBuilder.GetSettingsPath(), Encoding.UTF8);
                _instance = JsonSerializer.Deserialize<Settings>(json, SourceGenerationContext.Default.Settings);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unknown exception has occurred.");
            }
        }

        #endregion

        // APP
        [JsonPropertyName("app-mode")]
        public int Mode { get; set; }

        [JsonPropertyName("app-port")]
        public ushort Port { get; set; }

        // BOOT
        [JsonPropertyName("boot-enable")]
        public bool EnableBoot { get; set; }

        [JsonPropertyName("boot-target")]
        public string? Target { get; set; }

        [JsonPropertyName("boot-priority")]
        public int Priority { get; set; }

        [JsonPropertyName("boot-sync-io")]
        public bool SyncIO { get; set; }

        [JsonPropertyName("boot-sync-lifecycle")]
        public bool SyncLifecycle { get; set; }

        //TASK
        [JsonPropertyName("task-use-strict-rule")]
        public bool UseStrictRule { get; set; }

        [JsonPropertyName("task-use-validator")]
        public bool UseValidator { get; set; }

        public bool UseMultiThreading { get; set; }

        public int MaxConcurrency { get; set; }

        public bool UseTimeout { get; set; }

        public int Timeout { get; set; }

        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
