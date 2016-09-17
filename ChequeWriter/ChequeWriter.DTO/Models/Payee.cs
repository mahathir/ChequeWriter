using System;
using System.Collections.Generic;

namespace ChequeWriter.DTO.Models
{
    public partial class Payee
    {
        public Payee()
        {
            this.Cheques = new List<Cheque>();
        }

        public long PayeeID { get; set; }
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Cheque> Cheques { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
