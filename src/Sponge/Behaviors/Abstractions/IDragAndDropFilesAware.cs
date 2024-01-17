using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Behaviors.Abstractions
{
    public interface IDragAndDropFilesAware
    {
        void OnFilesDropped(string[] files);
    }
}
