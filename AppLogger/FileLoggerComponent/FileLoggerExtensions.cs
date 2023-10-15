using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AppLogger.FileLoggerComponent;

namespace AppLogger.FileLoggerComponent
{
    /// <summary>
    /// Extension methods for ILogger
    /// </summary>
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, IConfiguration configuration, Action<FileLoggerOptions> configure = null)
        {
            var fileLoggerProvider = Configure(configuration);
            if (fileLoggerProvider != null)
            {
                builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>(
                    (srvPrv) =>
                    {
                        return fileLoggerProvider;
                    }
                );
            }
            return builder;
        }


        public static ILoggerFactory AddFile(this ILoggerFactory factory, IConfiguration configuration)
        {
            var providerFactory = factory;
            var fileLoggerProvider = Configure(configuration);
            if (fileLoggerProvider == null)
                return factory;
            providerFactory.AddProvider(fileLoggerProvider);
            return factory;
        }

        private static FileLoggerProvider Configure(IConfiguration configuration)
        {
            var config = new FileLoggerConfig();

            configuration.Bind(config);

            if (string.IsNullOrWhiteSpace(config.Path))
                return null;

            FileLoggerOptions options = new FileLoggerOptions();

            options.MinLevel = config.MinLevel;
            options.MaxLogFileSize = config.MaxLogFileSize;
            options.FileName = config.FileName;
            options.FileMask = config.FileMask;

            return new FileLoggerProvider(config.Path, options);

        }

    }
}
