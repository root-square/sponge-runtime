using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Agent.Utilities
{
    internal static class AsyncHelper
    {
        private static readonly TaskFactory TASK_FACTORY = new
          TaskFactory(CancellationToken.None,
                      TaskCreationOptions.None,
                      TaskContinuationOptions.None,
                      TaskScheduler.Default);

        internal static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return TASK_FACTORY
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        internal static void RunSync(Func<Task> func)
        {
            TASK_FACTORY
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }
    }
}
