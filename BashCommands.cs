using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldBackuper3500
{
    public static class BashCommands
    {
        public static void NoReturn(string program, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = program, Arguments = args, };
            Process proc = new Process() { StartInfo = startInfo, };
            proc.Start();
            proc.WaitForExit();
        }
        public static string Return(string program, string args)
        {
            Console.WriteLine(program + " " + args);
            ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = program, Arguments = args, RedirectStandardOutput = true };
            Process proc = new Process() { StartInfo = startInfo, };
            proc.Start();
            string result = "";
            while (!proc.StandardOutput.EndOfStream)
            {
                result += proc.StandardOutput.ReadLine() + "\n";
            }
            return result;
        }
    }
}
