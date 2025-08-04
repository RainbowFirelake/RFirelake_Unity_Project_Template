using UnityEngine;

namespace RFirelake.Infrastructure.Logs
{
    public class LoggerConfiguration : ScriptableObject, ILoggerConfiguration
    {
        [SerializeField]
        private LogLevel _logLevel;

        public LogLevel LogLevel => _logLevel;
    }
}
