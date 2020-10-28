using System;
using System.Collections.Generic;
using System.Text;

namespace ServerEntities.Logging
{
    public class ConsoleLogger : ILogger
    {
        public LogLevel Level { get; }

        public void LogMessage(LogLevel level, string message, Exception exception = null)
        {
            throw new NotImplementedException();
        }
    }
}
