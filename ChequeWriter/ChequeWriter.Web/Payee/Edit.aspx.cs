using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Payee
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var routeData = Page.RouteData.DataTokens["FriendlyUrlSegments"] as List<string>;

            if (routeData == null || routeData.Count == 0)
            {
                Response.Redirect("~/Payee");
                return;
            }
            long payeeId;
            if (long.TryParse(routeData[0], out payeeId))
            {
                this.PayeeEditor.PayeeID = payeeId;
            }
            else
            {
                Response.Redirect("~/Payee");
                return;
            }

        }
    }
}