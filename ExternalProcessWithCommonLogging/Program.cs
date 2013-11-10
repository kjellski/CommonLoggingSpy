using System;
using System.Threading;
using Common.Logging;

namespace ExternalProcessWithCommonLogging
{
    class Program
    {
        static void Main()
        {
            var thread = new Thread(() =>
            {
                var a = new ALogged();

                try
                {

                    while (true)
                    {
                        a.AMethodDebug();
                        a.AMethodInfo();
                        a.AMethodWarn();
                        a.AMethodError();
                        Console.WriteLine("Logged something with Common.Logging.");
                        Thread.Sleep(1000);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    Console.WriteLine("Stopping Debugging Thread.");
                }
                // ReSharper disable once FunctionNeverReturns
            });
            thread.Start();

            Console.WriteLine("Press <Enter> to exit...");
            Console.ReadLine();

            thread.Interrupt();
            thread.Join();
        }
    }

    class ALogged
    {
        private ILog _log = LogManager.GetCurrentClassLogger();

        internal void AMethodDebug()
        {
            _log.Debug("AMethodDebug()");
        }

        internal void AMethodInfo()
        {
            _log.Info("AMethodInfo()");
        }

        internal void AMethodWarn()
        {
            _log.Warn("AMethodWarn()");
        }

        internal void AMethodError()
        {
            _log.Error("AMethodError()");
        }
    }

}
