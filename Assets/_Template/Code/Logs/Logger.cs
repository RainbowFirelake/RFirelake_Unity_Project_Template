using System;
using UnityEngine;

namespace RFirelake.Infrastructure.Logs
{
    public class Logger<T> : ILogger<T>
    {
        private readonly string LogPrefix;
        private readonly ILoggerConfiguration _loggerConfiguration;

        public Logger(ILoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
            LogPrefix = "[" + typeof(T).Name + "] ";
        }

        public void LogDebug(string message)
        {
            if (IsLogLevelIncluded(LogLevel.Debug))
                Debug.Log(LogPrefix + message);
        }

        public void LogInfo(string message)
        {
            if (IsLogLevelIncluded(LogLevel.Info))
                Debug.Log(LogPrefix + message);
        }

        public void LogWarning(string message)
        {
            if (IsLogLevelIncluded(LogLevel.Warning))
                Debug.LogWarning(LogPrefix + message);
        }

        public void LogError(string message)
        {
            Debug.LogError(LogPrefix + message);
        }

        public void LogException(Exception e)
        {
            Debug.LogException(e);
        }

        private bool IsLogLevelIncluded(LogLevel logLevel)
        {
            if (logLevel >= _loggerConfiguration.LogLevel)
                return true;
            else
                return false;
        }
    }
}
