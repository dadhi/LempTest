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
            Console.WriteLine(string.Join(' ', args));
            // Console.WriteLine(Directory.GetCurrentDirectory());
            Process.Start(".\\LEMP\\LEMP.exe");
        }
    }
}
