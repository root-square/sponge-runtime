using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sponge.Bootstrapper.Utilities
{
    internal static class ConsoleHelper
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        internal static bool HideWindow()
        {
            IntPtr hWnd = GetConsoleWindow();

            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool ShowWindow()
        {
            IntPtr hWnd = GetConsoleWindow();

            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
