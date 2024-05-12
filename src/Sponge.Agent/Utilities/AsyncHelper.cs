using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Agent.Utilities
{
    /// <summary>
    /// Provides useful functions for async code.
    /// </summary>
    internal static class AsyncHelper
    {
        private static readonly TaskFactory TASK_FACTORY = new
          TaskFactory(CancellationToken.None,
                      TaskCreationOptions.None,
                      TaskContinuationOptions.None,
                      TaskScheduler.Default);

        /// <summary>
        /// Run an async function synchronously.
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="func">Function to run</param>
        /// <returns>Result</returns>
        internal static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return TASK_FACTORY
              .StartNew(func)
              .Unwrap()
              .GetAwaiter()
              .GetResult();
        }

        /// <summary>
        /// Run an async function synchronously.
        /// </summary>
        /// <param name="func">Function to run</param>
        /// <returns>Result</returns>
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
