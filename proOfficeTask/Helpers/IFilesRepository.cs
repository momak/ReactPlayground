using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proOfficeTask.Helpers
{
    public interface IFilesRepository
    {
        string GetContentType(string path);

        string GetFileName(string path);
        
    }
}
