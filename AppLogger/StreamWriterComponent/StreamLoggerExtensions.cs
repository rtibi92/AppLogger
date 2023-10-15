using AppLogger.ConsoleLoggerComponent;
using AppLogger.FileLoggerComponent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.StreamWriterComponent
{
    public static class StreamLoggerExtensions
    {
        public static ILoggingBuilder AddStreamLogger(this ILoggingBuilder builder, IConfiguration configuration, Type type, Action<StreamLoggerConfig> configure = null)
        {
            var loggerProvider = Configure(configuration, type);
            if (loggerProvider != null)
            {
                builder.Services.AddSingleton<ILoggerProvider, StreamLoggerProvider>(
                    (srvPrv) =>
                    {
                        return loggerProvider;
                    }
                );
            }
            return builder;
        }

        private static StreamLoggerProvider Configure(IConfiguration configuration, Type type)
        {
            if (!type.IsSubclassOf(typeof(Stream)))
            {
                return null;
            }

            var config = new StreamLoggerConfig();
            configuration.Bind(config);

            return new StreamLoggerProvider(config, type);

        }
    }
}
