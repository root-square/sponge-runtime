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
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }

                return _instance;
            }
        }

        public static void Write()
        {
            try
            {
                var json = JsonSerializer.Serialize(Instance, SourceGenerationContext.Default.Settings);
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

        #region ::APP::

        [JsonPropertyName("app-mode")]
        public int AppMode { get; set; } = 0;

        [JsonPropertyName("app-use-dynamic-port")]
        public bool AppUseDynamicPort { get; set; } = false;

        [JsonPropertyName("app-port")]
        public ushort AppPort { get; set; } = 40126;

        #endregion

        #region ::BOOT::

        [JsonPropertyName("boot-enable")]
        public bool EnableBoot { get; set; } = true;

        [JsonPropertyName("boot-target")]
        public string? BootTarget { get; set; } = "base.bin";

        [JsonPropertyName("boot-priority")]
        public int BootPriority { get; set; } = 1;

        [JsonPropertyName("boot-sync-io")]
        public bool BootSyncIO { get; set; } = true;

        [JsonPropertyName("boot-sync-lifecycle")]
        public bool BootSyncLifecycle { get; set; } = true;

        #endregion

        #region ::CACHE::

        [JsonPropertyName("cache-enable")]
        public bool EnableCache { get; set; } = true;

        [JsonPropertyName("cache-strategy")]
        public string? CacheStrategy { get; set; } = "lfu";

        [JsonPropertyName("cache-capacity")]
        public int CacheCapacity { get; set; } = 1000;

        [JsonPropertyName("cache-duration")]
        public int CacheDuration { get; set; } = 10;

        [JsonPropertyName("preload-enable")]
        public bool EnablePreload { get; set; } = false;

        [JsonPropertyName("preload-to")]
        public string? PreloadTo { get; set; } = "png";

        #endregion

        #region ::TASK::

        [JsonPropertyName("task-use-strict-rule")]
        public bool TaskUseStrictRule { get; set; } = true;

        [JsonPropertyName("task-use-validator")]
        public bool TaskUseValidator { get; set; } = true;

        [JsonPropertyName("task-use-multi-threading")]
        public bool TaskUseMultiThreading { get; set; } = true;

        [JsonPropertyName("task-max-concurrency")]
        public int TaskMaxConcurrency { get; set; } = 4;

        [JsonPropertyName("task-use-timeout")]
        public bool TaskUseTimeout { get; set; } = true;

        [JsonPropertyName("task-timeout")]
        public int TaskTimeout { get; set; } = 360;

        [JsonPropertyName("task-params")]
        public Dictionary<string, string> TaskParameters { get; set; } = new Dictionary<string, string>();

        #endregion
    }
}
