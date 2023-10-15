using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogger.StreamWriterComponent
{
    public class FileStreamProvider
    {


        public FileStreamProvider()
        {


        }


        void GetLogger()
        {
            Stream stream = new MemoryStream();

            

            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write("Hello StreamWriter");
            }
        }

    }
}
