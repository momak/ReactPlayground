using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proOfficeTask.Models
{
    public class ProductDTO
    {
        public Guid IdProduct { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string Url { get; set; }
        public Guid? IdDoc { get; set; }
        public string Type { get; set; }
        public DateTime? Downloaded { get; set; }
    }
}
