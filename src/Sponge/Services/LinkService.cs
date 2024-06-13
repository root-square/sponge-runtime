using Serilog;
using Sponge.Entities.Configurations;
using Sponge.Services.Abstractions;
using Sponge.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class LinkService : Service
    {
        private Thread? _workerThread = null;

        public LinkService(ServiceProvider provider) : base(provider, isRoutable: false)
        {
            IsInitialized = true;
        }

        public override void Start()
        {
            if (!Validate())
            {
                Provider?.Stop();
            }

            IsRunning = true;
        }

        public override void Stop()
        {
            IsRunning = false;
        }

        private bool Validate()
        {
            bool result = true;

            var config = (Provider?.Services["SVC_CONFIG"] as ConfigurationService)!.Instance;

            if (!File.Exists(config.Link.Target))
            {
                var exception = new FileNotFoundException("The file is not found.", config.Link.Target);
                Log.Fatal(exception, "The link target cannot be found.");
                result = false;
            }

            if (!ExecutableChecker.IsValidExecutable(config.Link.Target!))
            {
                var exception = new InvalidDataException("The file is not a valid PE(Portable Executable), or the component of the program could not be found.");
                Log.Fatal(exception, "The link target is not a valid PE(Portable Executable).");
                result = false;
            }

            return result;
        }
    }
}
