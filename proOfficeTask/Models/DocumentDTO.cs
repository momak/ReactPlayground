using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proOfficeTask.Models
{
    public class DocumentDTO
    {
        public Guid IdDoc { get; set; }
        public string Type { get; set; }
        public DateTime? Downloaded { get; set; }
        public byte[] BinaryData { get; set; }
    }
}
