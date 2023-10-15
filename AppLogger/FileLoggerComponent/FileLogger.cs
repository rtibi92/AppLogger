using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace AppLogger.FileLoggerComponent
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private static object _lock = new object();
        private readonly FileLoggerProvider _loggerProvider;


        public FileLogger(string path, FileLoggerProvider loggerProvider)
        {
            filePath = path;
            _loggerProvider = loggerProvider;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _loggerProvider.MinLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {          
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter != null)
            {
                lock (_lock)
                {
                    string fullFilePath = Path.Combine(filePath, _loggerProvider.FileName ?? "log.txt");
                    var n = Environment.NewLine;
                    string exc = "";

                    //If the dir not exists, create is
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    CheckFileSize(fullFilePath);

                    if (exception != null)
                    {
                        exc = exception.GetType() + ": " + exception.Message + n + exception.StackTrace;
                    }
                    File.AppendAllText(fullFilePath, $"{DateTime.Now.ToString()} - {logLevel.ToString()}: {formatter(state, exception)}" + n + exc);
                }
            }
        }

        /// <summary>
        /// Check the size of file. If reached the maximum size, archive it.
        /// </summary>
        /// <param name="fullFilePath"></param>
        private void CheckFileSize(string fullFilePath)
        {
            if (File.Exists(fullFilePath))
            {
                //If file size reached the max size
                if (IsFileReachedMaxSize(fullFilePath))
                {
                    long fileNumber = GetLastLogFileNumber(filePath);

                    string mask = _loggerProvider.FileMask ?? "log.{0}.txt";
                    string newFile = string.Format(mask, ++fileNumber);

                    File.Move(fullFilePath, Path.Combine(filePath, newFile));
                }
            }
        }

        /// <summary>
        /// Gets the number of the latest log file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private long GetLastLogFileNumber(string path)
        {
            string maskForFileSearch = !string.IsNullOrEmpty(_loggerProvider.FileMask) ? string.Format(_loggerProvider.FileMask, "*") : "log.*.txt";
            var logDirName = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(logDirName))
                logDirName = Directory.GetCurrentDirectory();
            var logFiles = Directory.Exists(path) ? Directory.GetFiles(path, maskForFileSearch, SearchOption.TopDirectoryOnly) : Array.Empty<string>();

            string LogFileName;

            if (logFiles.Length > 0)
            {
                var lastFileInfo = logFiles
                        .Select(fName => new FileInfo(fName))
                        .OrderByDescending(fInfo => fInfo.Name)
                        .OrderByDescending(fInfo => fInfo.LastWriteTime).First();
                LogFileName = lastFileInfo.FullName;

                var number = LogFileName.Split('.')[1];
                long value;

                if (long.TryParse(number, out value))
                {
                    return value;
                }
            }
            return 0;
        }

        /// <summary>
        /// Check if file reached it's max size
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsFileReachedMaxSize(string path)
        {
            FileInfo logFile = new FileInfo(path);

            if (_loggerProvider.MaxLogFileSize > 0)
            {
                return logFile.Length >= _loggerProvider.MaxLogFileSize;
            }
            return false;
        }


    }
}