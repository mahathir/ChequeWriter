using ChequeWriter.Commons.Translations;
using ChequeWriter.IBusinessLogic;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Cheque
{
    public partial class Print : System.Web.UI.Page
    {
        public DTO.Models.Cheque Cheque { get; set; }
        public Dictionary<string, string> Translations = new Dictionary<string, string>();
        public string CustomerName { get; set; }
        public string PayeeName { get; set; }
        public string AmountInWords { get; set; }

        [Dependency]
        public IChequeService ChequeService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var routeData = Page.RouteData.DataTokens["FriendlyUrlSegments"] as List<string>;

            if (routeData == null || routeData.Count == 0)
            {
                Response.Redirect("~/Cheque");
                return;
            }
            long chequeId;
            if (long.TryParse(routeData[0], out chequeId))
            {
                Cheque = ChequeService.Retrieve(chequeId);
                CustomerName = Cheque.Customer.FirstName +
                    (string.IsNullOrEmpty(Cheque.Customer.LastName) ? "" : " " + Cheque.Customer.LastName);
                PayeeName = Cheque.Payee.FirstName + 
                    (string.IsNullOrEmpty(Cheque.Payee.LastName) ? "" : " " + Cheque.Payee.LastName);
                Translations["Date"] = EntitiesRes.Date;
                Translations["PayToTheOrderOf"] = MessagesRes.PayToTheOrderOf;
                Translations["Amount"] = EntitiesRes.Amount;
                Translations["AmountInWords"] = MessagesRes.AmountInWords;
                Translations["Memo"] = EntitiesRes.Memo;
                Translations["Signature"] = EntitiesRes.Signature;
                Translations["ChequeNo"] = EntitiesRes.ChequeNo;
                AmountInWords = ChequeService.GetAmountWords(Cheque.Amount);
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                AmountInWords = textInfo.ToTitleCase(AmountInWords);
            }
            else
            {
                Response.Redirect("~/Cheque");
                return;
            }
        }
    }
}