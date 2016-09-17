using System;
using System.Collections.Generic;

namespace ChequeWriter.DTO.Models
{
    public partial class Cheque
    {
        public long ChequeID { get; set; }
        public string ChequeNo { get; set; }
        public System.DateTime PostingDate { get; set; }
        public long CustomerID { get; set; }
        public long PayeeID { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Payee Payee { get; set; }
    }
}
