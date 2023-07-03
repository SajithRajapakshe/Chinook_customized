using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class InvoiceData
    {
        public InvoiceData()
        {
            InvoiceLines = new HashSet<InvoiceLineData>();
        }

        [Key]
        public long InvoiceId { get; set; }
        public long CustomerId { get; set; }
        public byte[] InvoiceDate { get; set; } = null!;
        public string? BillingAddress { get; set; }
        public string? BillingCity { get; set; }
        public string? BillingState { get; set; }
        public string? BillingCountry { get; set; }
        public string? BillingPostalCode { get; set; }
        public byte[] Total { get; set; } = null!;

        public virtual CustomerData Customer { get; set; } = null!;
        public virtual ICollection<InvoiceLineData> InvoiceLines { get; set; }
    }
}
