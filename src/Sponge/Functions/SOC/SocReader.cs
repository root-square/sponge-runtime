using Sponge.Functions.SOC.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Functions.SOC
{
    public class SocReader : IDisposable
    {
        private bool _disposedValue;

        public void Read()
        {
            var a = Marshal.PtrToStructure<SocV1>(IntPtr.Zero);
            Marshal.StructureToPtr<SocV1>(a, IntPtr.Zero, true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: Remove managed resources.
                }

                // TODO: Release unmanaged resources, and re-define the destructor.
                // TODO: Set large fields to null.
                _disposedValue = true;
            }
        }

        // // TODO: Only if 'Dispose(bool disposing)' contains a logic to release unmanaged resources, re-define the destructor. 
        // ~SocReader()
        // {
        //     // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // DO NOT CHANGE THIS CODE. It inputs a disposing code to the 'Dispose(bool disposing)' method.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
