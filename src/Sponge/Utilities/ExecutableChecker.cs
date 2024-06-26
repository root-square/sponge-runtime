﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Utilities
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_DOS_HEADER
    {
        public ushort e_magic;    // Magic number
        public ushort e_cblp;     // Bytes on last page of file
        public ushort e_cp;       // Pages in file
        public ushort e_crlc;     // Relocations
        public ushort e_cparhdr;  // Size of header in paragraphs
        public ushort e_minalloc; // Minimum extra paragraphs needed
        public ushort e_maxalloc; // Maximum extra paragraphs needed
        public ushort e_ss;       // Initial (relative) SS value
        public ushort e_sp;       // Initial SP value
        public ushort e_csum;     // Checksum
        public ushort e_ip;       // Initial IP value
        public ushort e_cs;       // Initial (relative) CS value
        public ushort e_lfarlc;   // File address of relocation table
        public ushort e_ovno;     // Overlay number
        public uint e_res1;       // Reserved
        public uint e_res2;       // Reserved
        public ushort e_oemid;    // OEM identifier (for e_oeminfo)
        public ushort e_oeminfo;  // OEM information; e_oemid specific
        public uint e_res3;       // Reserved
        public uint e_res4;       // Reserved
        public uint e_res5;       // Reserved
        public uint e_res6;       // Reserved
        public uint e_res7;       // Reserved
        public int e_lfanew;      // File address of new exe header
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_FILE_HEADER
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_NT_HEADERS_COMMON
    {
        public uint Signature;
        public IMAGE_FILE_HEADER FileHeader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_NT_HEADERS32
    {
        public uint Signature;
        public IMAGE_FILE_HEADER FileHeader;
        public IMAGE_OPTIONAL_HEADER32 OptionalHeader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_NT_HEADERS64
    {
        public uint Signature;
        public IMAGE_FILE_HEADER FileHeader;
        public IMAGE_OPTIONAL_HEADER64 OptionalHeader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_OPTIONAL_HEADER32
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData;
        public uint ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStackReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_OPTIONAL_HEADER64
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public ulong ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public ulong SizeOfStackReserve;
        public ulong SizeOfStackCommit;
        public ulong SizeOfHeapReserve;
        public ulong SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
    }

    public static class ExecutableChecker
    {
        public const ushort IMAGE_DOS_SIGNATURE = 0x5A4D;  // MZ
        public const uint IMAGE_NT_SIGNATURE = 0x00004550; // PE00

        public const ushort IMAGE_FILE_MACHINE_I386 = 0x014C;  // Intel 386
        public const ushort IMAGE_FILE_MACHINE_IA64 = 0x0200;  // Intel 64
        public const ushort IMAGE_FILE_MACHINE_AMD64 = 0x8664; // AMD64

        public const ushort IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10B; // PE32
        public const ushort IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20B; // PE32+

        public const ushort IMAGE_FILE_DLL = 0x2000;

        public static bool IsValidExecutable(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            try
            {
                using (var stream = File.OpenRead(fileName))
                {
                    IMAGE_DOS_HEADER dosHeader = GetDosHeader(stream);
                    if (dosHeader.e_magic != IMAGE_DOS_SIGNATURE)
                        return false;

                    IMAGE_NT_HEADERS_COMMON ntHeader = GetCommonNtHeader(stream, dosHeader);
                    if (ntHeader.Signature != IMAGE_NT_SIGNATURE)
                        return false;

                    if ((ntHeader.FileHeader.Characteristics & IMAGE_FILE_DLL) != 0)
                        return false;

                    switch (ntHeader.FileHeader.Machine)
                    {
                        case IMAGE_FILE_MACHINE_I386:
                            return IsValidExecutable32(GetNtHeader32(stream, dosHeader));

                        case IMAGE_FILE_MACHINE_IA64:
                        case IMAGE_FILE_MACHINE_AMD64:
                            return IsValidExecutable64(GetNtHeader64(stream, dosHeader));
                    }
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return true;
        }

        public static bool IsValidExecutable32(IMAGE_NT_HEADERS32 ntHeader)
        {
            return ntHeader.OptionalHeader.Magic == IMAGE_NT_OPTIONAL_HDR32_MAGIC;
        }

        public static bool IsValidExecutable64(IMAGE_NT_HEADERS64 ntHeader)
        {
            return ntHeader.OptionalHeader.Magic == IMAGE_NT_OPTIONAL_HDR64_MAGIC;
        }

        public static IMAGE_DOS_HEADER GetDosHeader(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            int structSize = Marshal.SizeOf<IMAGE_DOS_HEADER>();
            IntPtr memory = IntPtr.Zero;

            try
            {
                memory = Marshal.AllocCoTaskMem(structSize);
                if (memory == IntPtr.Zero)
                    throw new InvalidOperationException();

                byte[] buffer = new byte[structSize];
                int bytesRead = stream.Read(buffer, 0, structSize);
                if (bytesRead != structSize)
                    throw new InvalidOperationException();

                Marshal.Copy(buffer, 0, memory, structSize);

                return Marshal.PtrToStructure<IMAGE_DOS_HEADER>(memory)!;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(memory);
            }
        }

        public static IMAGE_NT_HEADERS_COMMON GetCommonNtHeader(Stream stream, IMAGE_DOS_HEADER dosHeader)
        {
            stream.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);

            int structSize = Marshal.SizeOf<IMAGE_NT_HEADERS_COMMON>();
            IntPtr memory = IntPtr.Zero;

            try
            {
                memory = Marshal.AllocCoTaskMem(structSize);
                if (memory == IntPtr.Zero)
                    throw new InvalidOperationException();

                byte[] buffer = new byte[structSize];
                int bytesRead = stream.Read(buffer, 0, structSize);
                if (bytesRead != structSize)
                    throw new InvalidOperationException();

                Marshal.Copy(buffer, 0, memory, structSize);

                return Marshal.PtrToStructure<IMAGE_NT_HEADERS_COMMON>(memory)!;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(memory);
            }
        }

        public static IMAGE_NT_HEADERS32 GetNtHeader32(Stream stream, IMAGE_DOS_HEADER dosHeader)
        {
            stream.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);

            int structSize = Marshal.SizeOf<IMAGE_NT_HEADERS32>();
            IntPtr memory = IntPtr.Zero;

            try
            {
                memory = Marshal.AllocCoTaskMem(structSize);
                if (memory == IntPtr.Zero)
                    throw new InvalidOperationException();

                byte[] buffer = new byte[structSize];
                int bytesRead = stream.Read(buffer, 0, structSize);
                if (bytesRead != structSize)
                    throw new InvalidOperationException();

                Marshal.Copy(buffer, 0, memory, structSize);

                return Marshal.PtrToStructure<IMAGE_NT_HEADERS32>(memory)!;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(memory);
            }
        }

        public static IMAGE_NT_HEADERS64 GetNtHeader64(Stream stream, IMAGE_DOS_HEADER dosHeader)
        {
            stream.Seek(dosHeader.e_lfanew, SeekOrigin.Begin);

            int structSize = Marshal.SizeOf<IMAGE_NT_HEADERS64>();
            IntPtr memory = IntPtr.Zero;

            try
            {
                memory = Marshal.AllocCoTaskMem(structSize);
                if (memory == IntPtr.Zero)
                    throw new InvalidOperationException();

                byte[] buffer = new byte[structSize];
                int bytesRead = stream.Read(buffer, 0, structSize);
                if (bytesRead != structSize)
                    throw new InvalidOperationException();

                Marshal.Copy(buffer, 0, memory, structSize);

                return Marshal.PtrToStructure<IMAGE_NT_HEADERS64>(memory)!;
            }
            finally
            {
                if (memory != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(memory);
            }
        }
    }
}
