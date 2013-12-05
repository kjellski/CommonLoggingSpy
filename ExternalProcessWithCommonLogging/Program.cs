using System;
using System.Threading;
using Common.Logging;

namespace ExternalProcessWithCommonLogging
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            Log.Debug("Starting application");
            var thread = new Thread(() =>
            {
                var a = new LoggingClassA();

                try
                {

                    while (true)
                    {
                        a.AMethodDebug();
                        a.AMethodInfo();
                        a.AMethodWarn();
                        a.AMethodError();
                        Console.WriteLine("Logged something with Common.Logging.");
                        Thread.Sleep(10000);
                    }
                }
                catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine("Stopping Debugging Thread.");
                    Log.Fatal("Intentionally logging exception thrown by interrupted thread", ex);
                }
                // ReSharper disable once FunctionNeverReturns
            });
            thread.Start();

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();

            thread.Interrupt();
            thread.Join();

            Log.Debug("Sleeping 500ms before closing...");
            Thread.Sleep(500);
        }
    }
}
