using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace proOfficeTask.Helpers
{
    public interface IDownload
    {
        Task<MemoryStream> GetContent(string url);
    }
}
