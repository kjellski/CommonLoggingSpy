using Common.Logging;

namespace ExternalProcessWithCommonLogging
{
    internal class LoggingClassA
    {
        private readonly ILog _log = LogManager.GetCurrentClassLogger();

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