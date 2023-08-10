﻿using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Processor.Commands
{
    [Command()]
    public class DefaultCommand : ICommand
    {
        [CommandOption("debug", 'd', Description = "Enables debug mode.")]
        public bool EnableDebugMode { get; init; } = false;

        [CommandOption("silent", 's', Description = "Enables silent mode.")]
        public bool EnableSilentMode { get; init; } = true;

        public ValueTask ExecuteAsync(IConsole console)
        {
            return default;
        }
    }
}