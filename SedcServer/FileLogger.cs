using ServerEntities.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;

namespace SedcServer
{
    public class FileLogger : ILogger, IDisposable
    {
        public string LogFile { get; private set; }
        public LogLevel Level { get; private set; }

        private StreamWriter stream;
        private bool disposedValue;

        public FileLogger(string logFile, LogLevel level = LogLevel.Debug)
        {
            Level = level;
            LogFile = logFile;
            stream = new StreamWriter(File.OpenWrite(logFile));
        }

        public void LogMessage(LogLevel level, string message, Exception exception = null)
        {
            if (level >= Level)
            {
                var levelstr = level.ToString().ToUpperInvariant();
                stream.WriteLine($"[{levelstr}] {message}");
                if (exception != null)
                {
                    stream.WriteLine($"[{levelstr}]    {exception.Message}");
                    stream.WriteLine($"{exception.StackTrace}");
                }
                stream.Flush();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stream.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FileLogger()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
