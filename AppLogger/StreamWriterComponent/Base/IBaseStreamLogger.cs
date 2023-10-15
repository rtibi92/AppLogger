using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AppLogger.StreamWriterComponent.Base
{
    public interface IBaseStreamLogger : IStreamProvider
    {
        // void Log((LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter));

        void Log(string logLevel, Exception exception);
    }
}
