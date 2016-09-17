using System;
using System.Collections.Generic;

namespace ChequeWriter.DTO.Models
{
    public partial class Customer
    {
        public Customer()
        {
            this.Cheques = new List<Cheque>();
            this.Payees = new List<Payee>();
        }

        public long CustomerID { get; set; }
        public string CustomerNo { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Cheque> Cheques { get; set; }
        public virtual ICollection<Payee> Payees { get; set; }
    }
}
