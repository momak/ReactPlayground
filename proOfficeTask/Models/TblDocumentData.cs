﻿using System;
using System.Collections.Generic;

namespace proOfficeTask.Models
{
    public partial class TblDocumentData
    {
        public Guid IdDocument { get; set; }
        public byte[] BinaryData { get; set; }

        public virtual TblDocument IdDocumentNavigation { get; set; }
    }
}
