using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.ConsoleLoggerComponent
{
    public class ConsoleLoggerConfig
    {

       /// <summary>
       /// Text color for of the log level
       /// </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;

        /// <summary>
        /// Log level for the logger
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        /// Minimum level to log.
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Debug;
    }
}
