using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.FileLoggerComponent
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string _filename;
        public FileLoggerOptions _options { get; private set; }

        public long MaxLogFileSize => _options.MaxLogFileSize;
        public string? FileName => _options.FileName;
        public string? FileMask => _options.FileMask;


        public FileLoggerProvider(string filename, FileLoggerOptions options)
        {
            _filename = filename;
            _options = options;
        }
        public ILogger CreateLogger(string path)
        {
            return new FileLogger(_filename, this);
        }

        public LogLevel MinLevel
        {
            get => _options.MinLevel;
            set { _options.MinLevel = value; }
        }

        public void Dispose()
        {
        }
    }
}
