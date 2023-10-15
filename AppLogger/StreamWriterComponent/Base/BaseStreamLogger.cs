using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.StreamWriterComponent.Base
{
    public class BaseStreamLogger : IBaseStreamLogger
    {
        public Stream GetStream()
        {
            throw new NotImplementedException();
        }

        public void Log(string logLevel, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void ReleaseStream(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
