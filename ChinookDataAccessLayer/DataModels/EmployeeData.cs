using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChinookDataAccessLayer.Models
{
    public partial class EmployeeData
    {
        public EmployeeData()
        {
            Customers = new HashSet<CustomerData>();
            InverseReportsToNavigation = new HashSet<EmployeeData>();
        }

        [Key]
        public long EmployeeId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Title { get; set; }
        public long? ReportsTo { get; set; }
        public byte[]? BirthDate { get; set; }
        public byte[]? HireDate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }

        public virtual EmployeeData? ReportsToNavigation { get; set; }
        public virtual ICollection<CustomerData> Customers { get; set; }
        public virtual ICollection<EmployeeData> InverseReportsToNavigation { get; set; }
    }
}
