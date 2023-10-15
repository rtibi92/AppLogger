using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.ConsoleLoggerComponent
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ConsoleLoggerOptions _options;

        public ConsoleLogger(ConsoleLoggerOptions options)
        {      
            _options = options;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _options.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var n = Environment.NewLine;
            string exc = "";

            string message;

            if (formatter(state, exception).Length > 1000)
            {
                message = "Message text is longer than 1000 characters!";
                exception = new InvalidDataException("Text too long");
            }
            else {
                message = formatter(state, exception);
            }         

            if (exception != null)
            {
                exc = exception.GetType() + ": " + exception.Message + n + exception.StackTrace;
            }

            var color = Console.ForegroundColor;
            Console.ForegroundColor = _options.Color;
            Console.WriteLine($"{DateTime.Now.ToString()} - {logLevel.ToString()}: {message}" + n + exc);
            Console.ForegroundColor = color;

            /*
            string fullMessage = $"{DateTime.Now.ToString()} - {logLevel.ToString()}: {message}" + n + exc;


            using (_memoryStream)
            {
                using (var sw = new StreamWriter(_memoryStream, leaveOpen: true))
                {
                    sw.WriteLine("data");
                    sw.WriteLine("data 2");
                }

                _memoryStream.Position = 0;
                using (var sr = new StreamReader(_memoryStream))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            */

           /* using (_memoryStream)
            {
                _memoryStream.Write(System.Text.Encoding.ASCII.GetBytes(fullMessage), 0, fullMessage.Length);
            }*/

        }
    }
}
