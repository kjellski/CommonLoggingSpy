using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using Common.Logging;

namespace CommonLoggingSpy
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Log.Info("Scanning processes for Common.Logging usage.");
            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
            using (var results = searcher.Get())
            {
                var query = from p in Process.GetProcesses()
                            join mo in results.Cast<ManagementObject>()
                            on p.Id equals (int)(uint)mo["ProcessId"]
                            select new
                            {
                                Process = p,
                                Path = (string)mo["ExecutablePath"],
                                CommandLine = (string)mo["CommandLine"],
                            };
                //var query2 = results.Cast<ManagementObject>().Select(m => Process.GetProcesses().Single(n => n.Id == (int)(uint)m["ProcessId"])).Select(m => new {Process = p})
                foreach (var item in query)
                {
                    Log.DebugFormat("[{0}] [{1}] [{2}]\n\n", item.Process, item.CommandLine, item.Path);
                }
            }

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();
        }
    }
}
