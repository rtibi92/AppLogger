using AppLogger.FileLoggerComponent;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AppLogger.StreamWriterComponent
{
    public class StreamLogger : ILogger
    {
        private readonly StreamLoggerProvider _loggerProvider;
        private readonly Type _type;

        public StreamLogger(Type type, StreamLoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
            _type = type;
        }


        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _loggerProvider.MinLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            Stream stream = (Stream)Activator.CreateInstance(_type);


            string exc = "";
            var n = Environment.NewLine;

            if (exception != null)
            {
                exc = exception.GetType() + ": " + exception.Message + n + exception.StackTrace;
            }

            string message = $"{DateTime.Now.ToString()} - {logLevel.ToString()}: {formatter(state, exception)}" + n + exc;
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(message);
            }
     
        }
    }
}
