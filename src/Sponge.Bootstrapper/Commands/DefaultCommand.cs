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
        [CommandOption("launch-proc", 'p', Description = "Launches a Sponge Processor instance.")]
        public bool LaunchProcessor { get; init; } = true;

        [CommandOption("launch-game", 'g', Description = "Launches the specified game.")]
        public bool LaunchGame { get; init; } = true;

        public ValueTask ExecuteAsync(IConsole console)
        {
            Log.Warning("BST COM NOT IMPL");
            return default;
            //throw new NotImplementedException();
        }
    }
}
