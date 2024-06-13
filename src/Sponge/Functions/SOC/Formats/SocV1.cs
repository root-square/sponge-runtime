using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Functions.SOC.Types
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct SocV1
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Signature;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte Version;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte Revision;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte SourceFormat;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte CurrentFormat;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte BitFlagsA;

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public byte BitFlagsB;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] Reserved3;

        [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
        public uint FileSize;

        [MarshalAs(UnmanagedType.U4, SizeConst = 4)]
        public uint FileChecksum;

        [MarshalAs(UnmanagedType.U2, SizeConst = 2)]
        public ushort MetadataChecksum;
    }
}
