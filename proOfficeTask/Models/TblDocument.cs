using System;
using System.Collections.Generic;

namespace proOfficeTask.Models
{
    public partial class TblDocument
    {
        public Guid IdDoc { get; set; }
        public Guid ProductId { get; set; }
        public string Type { get; set; }
        public DateTime? Downloaded { get; set; }

        public TblProduct Product { get; set; }
        public virtual TblDocumentData TblDocumentData { get; set; }
    }
}
