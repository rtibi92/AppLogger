using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.ConsoleLoggerComponent
{
    public class ConsoleLoggerOptions
    {
        public LogLevel MinLevel { get; set; } = LogLevel.Debug;

        public LogLevel LogLevel { get; set; } = LogLevel.Debug;
        //public int EventId { get; set; } = 0;
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;
    }
}
