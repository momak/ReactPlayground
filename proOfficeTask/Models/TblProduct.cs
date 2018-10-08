using System;
using System.Collections.Generic;

namespace proOfficeTask.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblDocument = new HashSet<TblDocument>();
        }

        public Guid IdProduct { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public string Url { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICollection<TblDocument> TblDocument { get; set; }
    }
}
