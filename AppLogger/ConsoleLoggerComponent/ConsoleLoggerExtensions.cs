using AppLogger.FileLoggerComponent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.ConsoleLoggerComponent
{
    /// <summary>
    /// Extension methods for ILogger
    /// </summary>
    public static class ConsoleLoggerExtensions
    {

        public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder, ConsoleLoggerConfig configuration, Action<ConsoleLoggerOptions> configure = null)
        {
            var consoleLoggerProvider = Configure(configuration);
            if (consoleLoggerProvider != null)
            {
                builder.Services.AddSingleton<ILoggerProvider, ConsoleLoggerProvider>(
                    (srvPrv) =>
                    {
                        return consoleLoggerProvider;
                    }
                );
            }
            return builder;
        }


        public static ILoggerFactory AddConsoleLogger(this ILoggerFactory loggerFactory, ConsoleLoggerOptions options)
        {
            loggerFactory.AddProvider(new ConsoleLoggerProvider(options));
            return loggerFactory;
        }

        public static ILoggerFactory AddConsoleLogger(this ILoggerFactory loggerFactory)
        {
            var options = new ConsoleLoggerOptions();
            return loggerFactory.AddConsoleLogger(options);
        }

        private static ConsoleLoggerProvider Configure(ConsoleLoggerConfig configuration)
        {
            ConsoleLoggerOptions options = new ConsoleLoggerOptions();

            options.MinLevel = configuration.MinLevel;
            options.LogLevel = configuration.LogLevel;
            options.Color = configuration.Color;

            return new ConsoleLoggerProvider(options);

        }

    }
}
