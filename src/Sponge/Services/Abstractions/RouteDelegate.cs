using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Services.Abstractions
{
    public delegate void RouteDelegate(HttpSession session, HttpRequest request);
}
