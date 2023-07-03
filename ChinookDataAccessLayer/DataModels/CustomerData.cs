using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class CustomerData
    {
        public CustomerData()
        {
            Invoices = new HashSet<InvoiceData>();
        }

        [Key]
        public long CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string Email { get; set; } = null!;
        public long? SupportRepId { get; set; }

        public virtual EmployeeData? SupportRep { get; set; }
        public virtual ICollection<InvoiceData> Invoices { get; set; }
    }
}
