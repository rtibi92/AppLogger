using AppLogger.FileLoggerComponent;
using AppLogger.StreamWriterComponent.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.StreamWriterComponent
{
    public class StreamLoggerProvider : ILoggerProvider
    {
        StreamLoggerConfig _config { get; set; }
        Type _type { get; set; }

        public StreamLoggerProvider(StreamLoggerConfig config, Type type)
        {
            _config = config;
            _type = type;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new StreamLogger(_type, this);
        }

        public LogLevel MinLevel
        {
            get => _config.MinLevel;
            set { _config.MinLevel = value; }
        }
    }
}
