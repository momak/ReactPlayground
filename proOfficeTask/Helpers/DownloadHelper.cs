using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using proOfficeTask.Models;

namespace proOfficeTask.Helpers
{
    public static class DownloadHelper
    {
        public static async Task<MemoryStream> GetContent(string url)
        {
            var client = new System.Net.WebClient();

            byte[] data = await client.DownloadDataTaskAsync(url);
            MemoryStream content = new MemoryStream(data);
            return content;
        }
    }
}
