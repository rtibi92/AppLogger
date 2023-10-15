using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.FileLoggerComponent
{
    public class FileLoggerOptions
    {
        /// <summary>
        /// Maximum size of the log file
        /// </summary>
        /// <remarks>
        /// It is used if >0. When the file reached the max size, a new file will be created. 
        /// </remarks>
        public long MaxLogFileSize { get; set; } = 0;

        /// <summary>
        /// Minimal level to log.
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;

        /// <summary>
        /// File name used for log files
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// The newly created log files's format to search for.
        /// </summary>
        public string? FileMask { get; set; }
    }
}
