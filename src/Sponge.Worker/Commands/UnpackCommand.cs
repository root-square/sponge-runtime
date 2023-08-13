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
    [Command("unpack", Description = "Unpacks the game in the current directory.")]
    public class UnpackCommand : ICommand
    {
        public ValueTask ExecuteAsync(IConsole console)
        {
            return default;
        }
    }
}
