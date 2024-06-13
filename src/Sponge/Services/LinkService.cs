using Sponge.Entities.Configurations;
using Sponge.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services
{
    public class LinkService : Service
    {
        public LinkService() : base(isRoutable: false)
        {
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
    }
}
