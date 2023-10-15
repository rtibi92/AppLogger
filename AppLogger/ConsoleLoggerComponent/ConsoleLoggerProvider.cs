using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.ConsoleLoggerComponent
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        private readonly ConsoleLoggerOptions _options;
        private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers = new ConcurrentDictionary<string, ConsoleLogger>();

        public ConsoleLoggerProvider(ConsoleLoggerOptions options)
        {
            _options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new ConsoleLogger( _options));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
