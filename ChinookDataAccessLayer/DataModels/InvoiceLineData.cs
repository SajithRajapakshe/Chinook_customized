using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class InvoiceLineData
    {
        [Key]
        public long InvoiceLineId { get; set; }
        public long InvoiceId { get; set; }
        public long TrackId { get; set; }
        public byte[] UnitPrice { get; set; } = null!;
        public long Quantity { get; set; }

        public virtual InvoiceData Invoice { get; set; } = null!;
        public virtual TrackData Track { get; set; } = null!;
    }
}
