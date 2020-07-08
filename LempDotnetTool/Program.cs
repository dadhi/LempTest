using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace LempDotnetTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsString = string.Join(' ', args);
            var process = Process.Start(".\\LEMP\\LEMP.exe", argsString);
            process.WaitForExit();
        }
    }
}
