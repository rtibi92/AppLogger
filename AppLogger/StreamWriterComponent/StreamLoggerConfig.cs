using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.StreamWriterComponent
{
    public class StreamLoggerConfig
    {
        public LogLevel MinLevel { get; set; } = LogLevel.Debug;
    }
}
