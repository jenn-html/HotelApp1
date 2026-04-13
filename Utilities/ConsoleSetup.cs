using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HotelApp1.Utilities;
static class ConsoleSetup
{
    [DllImport("kernel32.dll")] static extern bool SetConsoleOutputCP(uint wCodePageID);
    [DllImport("kernel32.dll")] static extern bool SetConsoleCP(uint wCodePageID);
    [DllImport("kernel32.dll")] static extern IntPtr GetStdHandle(int nStdHandle);
    [DllImport("kernel32.dll")] static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);
    [DllImport("kernel32.dll")] static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    const int STD_OUTPUT_HANDLE = -11;
    const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    public static void EnableUtf8AndAnsi()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        // Set Windows console code page to UTF-8
        SetConsoleOutputCP(65001);
        SetConsoleCP(65001);

        // Try enable ANSI VT processing (colors)
        var handle = GetStdHandle(STD_OUTPUT_HANDLE);
        if (GetConsoleMode(handle, out var mode))
        {
            SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
        }
    }
}