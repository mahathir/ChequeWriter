using ChequeWriter.IBusinessLogic;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Cheque
{
    public partial class List : System.Web.UI.Page
    {
        [Dependency]
        public IChequeService ChequeService { get; set; }
        int chequePageSize = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetChequeData(1, chequePageSize);
            }
        }

        private void GetChequeData(int current, int pageSize)
        {
            var chequeData = ChequeService.Retrieve(current, pageSize);
            Cheque_GridView.VirtualItemCount = (int) chequeData.TotalCount;
            Cheque_GridView.DataSource = chequeData.Items;
            Cheque_GridView.DataBind();
        }

        protected void Cheque_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Cheque_GridView.PageIndex = e.NewPageIndex;
            GetChequeData(e.NewPageIndex + 1, chequePageSize);
        }
    }
}