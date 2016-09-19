using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Customer
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var routeData = Page.RouteData.DataTokens["FriendlyUrlSegments"] as List<string>;

            if (routeData == null || routeData.Count == 0)
            {
                Response.Redirect("~/Customer/List");
                return;
            }
            long customerId;
            if (long.TryParse(routeData[0], out customerId))
            {
                this.CustomerEditor.CustomerID = customerId;
            }
            else
            {
                Response.Redirect("~/Customer/List");
                return;
            }
        }
    }
}