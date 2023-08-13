using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Worker.Commands
{
    [Command("serve", Description = "Starts the processing server.")]
    public class ServeCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            return default;
        }
    }
}
