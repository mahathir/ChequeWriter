using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Cheque
{
    public partial class Edit : System.Web.UI.Page
    {
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
                this.ChequeEditor.ChequeID = chequeId;
            }
            else
            {
                Response.Redirect("~/Cheque");
                return;
            }
        }
    }
}