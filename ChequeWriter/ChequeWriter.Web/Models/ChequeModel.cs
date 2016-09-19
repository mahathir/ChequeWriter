using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChequeWriter.Web.Models
{
    public class ChequeAddModel
    {
        [Required, MinLength(16), MaxLength(16)]
        public string ChequeNo { get; set; }
        [Required]
        public long CustomerID { get; set; }
        [Required]
        public long PayeeID { get; set; }
        [Required]
        public decimal ChequeAmount { get; set; }
        public string ChequeMemo { get; set; }

        public DTO.Models.Cheque ToDTO()
        {
            return new DTO.Models.Cheque
            {
                ChequeNo = this.ChequeNo,
                CustomerID = this.CustomerID,
                PayeeID = this.PayeeID,
                Amount = this.ChequeAmount,
                Memo = this.ChequeMemo
            };
        }
    }
}