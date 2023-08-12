using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Bootstrapper.Commands
{
    [Command()]
    public class DefaultCommand : ICommand
    {
        [CommandOption("debug", 'd', Description = "Enables debug mode.")]
        public bool EnableDebugMode { get; init; } = false;

        public ValueTask ExecuteAsync(IConsole console)
        {
            return default;
        }
    }
}
