using System;

namespace RFirelake.Infrastructure.Logs
{

    public interface ILogger<T>
    {
        public void LogDebug(string message);
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
        public void LogException(Exception e);
    }
}
