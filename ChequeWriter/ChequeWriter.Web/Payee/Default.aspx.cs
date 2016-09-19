using ChequeWriter.Commons;
using ChequeWriter.Commons.Translations;
using ChequeWriter.IBusinessLogic;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Payee
{
    public partial class Default : System.Web.UI.Page
    {
        [Dependency]
        public IPayeeService PayeeService { get; set; }
        [Dependency]
        public ICustomerService CustomerService { get; set; }
        public string PanelTitle
        {
            get
            {
                return ViewState["PanelTitle"].ToString();
            }
            set
            {
                ViewState["PanelTitle"] = value;
            }
        }
        public static Dictionary<string, string> CustGridHeaderText = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslations();
                InitCustomerDddl();
                GetGridData(1, Payee_GridView.PageSize);
            }
        }

        private void InitCustomerDddl()
        {
            var pageResult = CustomerService.Retrieve(1, int.MaxValue);
            if (pageResult.TotalCount > 0)
            {
                var routeData = Page.RouteData.DataTokens["FriendlyUrlSegments"] as List<string>;

                if (routeData != null && routeData.Count > 0)
                {
                    long customerId;
                    if (long.TryParse(routeData[0], out customerId))
                    {
                        this.CustomerDdl.SelectedValue = customerId.ToString();
                    }
                    else
                    {
                        this.CustomerDdl.SelectedValue = pageResult.Items.FirstOrDefault().CustomerID.ToString();
                    }
                }

                this.CustomerDdl.DataSource = pageResult.Items.Select(a =>
                    new
                    {
                        a.CustomerID,
                        FullName = a.FirstName + (string.IsNullOrWhiteSpace(a.LastName) ? "" : " " + a.LastName)
                    });
                this.CustomerDdl.DataTextField = "FullName";
                this.CustomerDdl.DataValueField = "CustomerID";
                this.CustomerDdl.DataBind();
                this.FreeLabel.Visible = false;
                this.AddCustomer.Visible = false;
            }
            else
            {
                this.AddPayee.Visible = false;
                this.AddCustomer.Visible = true;
                this.FreeLabel.Visible = true;
                this.FreeLabel.Text = string.Format(MessagesRes._NotFound, EntitiesRes.Customer) + " " +
                    string.Format(MessagesRes.Please_, string.Format(CommonsRes.Add_, EntitiesRes.Customer));
            }
        }

        private void SetTranslations()
        {
            this.AddPayee.Text = string.Format(CommonsRes.Add_, EntitiesRes.Payee);
            this.AddCustomer.Text = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
            this.PanelTitle = EntitiesRes.Payee;
            CustGridHeaderText["FirstName"] = EntitiesRes.FirstName;
            CustGridHeaderText["LastName"] = EntitiesRes.LastName;
            CustGridHeaderText["Status"] = EntitiesRes.Status;
            CustGridHeaderText["Command"] = CommonsRes.Command;
            CustGridHeaderText["Edit"] = CommonsRes.Edit;
            CustGridHeaderText["Delete"] = CommonsRes.Delete;
            CustomerLabel.InnerText = EntitiesRes.Customer;
        }

        private void GetGridData(int current, int pageSize)
        {
            var data = PayeeService.Retrieve(current, pageSize,
                new Dictionary<string, string> { { "CustomerID", string.IsNullOrWhiteSpace(this.CustomerDdl.SelectedValue) ? "0" : 
                    this.CustomerDdl.SelectedValue } });
            Payee_GridView.VirtualItemCount = (int)data.TotalCount;
            Payee_GridView.DataSource = data.Items.Select(a => new
            {
                a.PayeeID,
                a.CustomerID,
                a.FirstName,
                a.LastName,
                Status = ((PayeeStatus)Enum.Parse(typeof(PayeeStatus), a.Status)).GetDescription()
            }).ToList();
            Payee_GridView.DataKeyNames = new string[] { "PayeeID", "CustomerID" };
            Payee_GridView.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Payee_GridView.HeaderRow != null)
            {
                Payee_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            base.OnPreRender(e);
        }

        protected void Payee_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Payee_GridView.PageIndex = e.NewPageIndex;
            GetGridData(e.NewPageIndex + 1, Payee_GridView.PageSize);
        }

        protected void Payee_GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var PayeeId = Payee_GridView.DataKeys[e.NewEditIndex].Value.ToString();

            Response.Redirect("~/Payee/Edit/" + PayeeId);
        }

        protected void Payee_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var PayeeIdStr = Payee_GridView.DataKeys[e.RowIndex].Value.ToString();
            long PayeeId;
            long.TryParse(PayeeIdStr, out PayeeId);
            var customerIdStr = Payee_GridView.DataKeys[e.RowIndex].Values["CustomerID"].ToString();
            long customerId;
            long.TryParse(customerIdStr, out customerId);
            if (PayeeId > 0)
            {
                var result = PayeeService.Delete(PayeeId);
                if (result.ErrorMessages.Count > 0)
                {
                    foreach (var error in result.ErrorMessages)
                    {
                        Page.ModelState.AddModelError(error.Key, error.Value);
                    }
                }
                else
                {
                    Response.Redirect("~/Payee/Default/" + customerId);
                }
            }
            else
            {
                Response.Redirect("~/Payee/Default/" + customerId);
            }
        }

        protected void Payee_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                btnDelete.OnClientClick = "return confirm('" +
                    string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Payee) + "');";
            }
        }

        protected void AddPayee_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Payee/Add/" + this.CustomerDdl.SelectedValue);
        }

        protected void CustomerDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridData(Payee_GridView.PageIndex + 1, Payee_GridView.PageSize);
        }
    }
}