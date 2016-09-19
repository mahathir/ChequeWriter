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

namespace ChequeWriter.Web.Cheque
{
    public partial class Default : System.Web.UI.Page
    {
        [Dependency]
        public IChequeService ChequeService { get; set; }
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
                GetGridData(1, Cheque_GridView.PageSize);
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
                this.AddCheque.Visible = false;
                this.AddCustomer.Visible = true;
                this.FreeLabel.Visible = true;
                this.FreeLabel.Text = string.Format(MessagesRes._NotFound, EntitiesRes.Customer) + " " +
                    string.Format(MessagesRes.Please_, string.Format(CommonsRes.Add_, EntitiesRes.Customer));
            }
        }

        private void SetTranslations()
        {
            this.AddCheque.Text = string.Format(CommonsRes.Add_, EntitiesRes.Cheque);
            this.AddCustomer.Text = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
            this.PanelTitle = EntitiesRes.Cheque;
            CustGridHeaderText["FirstName"] = EntitiesRes.FirstName;
            CustGridHeaderText["LastName"] = EntitiesRes.LastName;
            CustGridHeaderText["Status"] = EntitiesRes.Status;
            CustGridHeaderText["Command"] = CommonsRes.Command;
            CustGridHeaderText["Edit"] = CommonsRes.Edit;
            CustGridHeaderText["Delete"] = CommonsRes.Delete;
            CustGridHeaderText["Print"] = CommonsRes.Print;
            CustGridHeaderText["Cancel"] = CommonsRes.Cancel;
            CustGridHeaderText["ChequeNo"] = EntitiesRes.ChequeNo;
            CustGridHeaderText["Payee"] = EntitiesRes.Payee;
            CustGridHeaderText["Amount"] = EntitiesRes.Amount;
            CustGridHeaderText["Memo"] = EntitiesRes.Memo;
            CustGridHeaderText["PostingDate"] = EntitiesRes.PostingDate;
            CustomerLabel.InnerText = EntitiesRes.Customer;
        }

        private void GetGridData(int current, int pageSize)
        {
            var data = ChequeService.Retrieve(current, pageSize,
                new Dictionary<string, string> { 
                { "CustomerID", string.IsNullOrWhiteSpace(this.CustomerDdl.SelectedValue) ? "0" : 
                    this.CustomerDdl.SelectedValue } });
            Cheque_GridView.VirtualItemCount = (int)data.TotalCount;
            Cheque_GridView.DataSource = data.Items.Select(a => new
            {
                a.ChequeID,
                a.CustomerID,
                a.PayeeID,
                a.ChequeNo,
                a.Amount,
                a.Memo,
                Payee = a.Payee.FirstName + (string.IsNullOrEmpty(a.Payee.LastName) ? "" : " " + a.Payee.LastName),
                a.PostingDate,
                Status = ((ChequeStatus)Enum.Parse(typeof(ChequeStatus), a.Status)).GetDescription()
            }).ToList();
            Cheque_GridView.DataKeyNames = new string[] { "ChequeID", "CustomerID" };
            Cheque_GridView.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Cheque_GridView.HeaderRow != null)
            {
                Cheque_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            base.OnPreRender(e);
        }

        protected void Cheque_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Cheque_GridView.PageIndex = e.NewPageIndex;
            GetGridData(e.NewPageIndex + 1, Cheque_GridView.PageSize);
        }

        protected void Cheque_GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var ChequeId = Cheque_GridView.DataKeys[e.NewEditIndex].Value.ToString();

            Response.Redirect("~/Cheque/Edit/" + ChequeId);
        }

        protected void Cheque_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var ChequeIdStr = Cheque_GridView.DataKeys[e.RowIndex].Value.ToString();
            long ChequeId;
            long.TryParse(ChequeIdStr, out ChequeId);
            var customerIdStr = Cheque_GridView.DataKeys[e.RowIndex].Values["CustomerID"].ToString();
            long customerId;
            long.TryParse(customerIdStr, out customerId);
            if (ChequeId > 0)
            {
                var result = ChequeService.Delete(ChequeId);
                if (result.ErrorMessages.Count > 0)
                {
                    foreach (var error in result.ErrorMessages)
                    {
                        Page.ModelState.AddModelError(error.Key, error.Value);
                    }
                }
                else
                {
                    Response.Redirect("~/Cheque/Default/" + customerId);
                }
            }
            else
            {
                Response.Redirect("~/Cheque/Default/" + customerId);
            }
        }

        protected void Cheque_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                btnDelete.OnClientClick = "return confirm('" +
                    string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Cheque) + "');";
                var btnCancel = e.Row.FindControl("btnCancel") as LinkButton;
                btnCancel.OnClientClick = "return confirm('" +
                    string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Cancel, EntitiesRes.Cheque) + "');";
                var btnPrint = e.Row.FindControl("btnPrint") as LinkButton;
                btnPrint.OnClientClick = "return confirm('" +
                    string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Print, EntitiesRes.Cheque) + "');";
            }
        }

        protected void AddCheque_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cheque/Add/" + this.CustomerDdl.SelectedValue);
        }

        protected void CustomerDdl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetGridData(Cheque_GridView.PageIndex + 1, Cheque_GridView.PageSize);
        }

        protected void Cheque_GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            long chequeId;
            long.TryParse(e.CommandArgument.ToString(), out chequeId);
            if (chequeId > 0)
            {
                IDictionary<string, string> errorMessages = new Dictionary<string,string>();
                if (e.CommandName == "CancelCheque")
                {
                    var result = ChequeService.CancelCheque(chequeId);
                    errorMessages = result.ErrorMessages;
                }
                else if (e.CommandName == "Print")
                {
                    var result = ChequeService.PrintCheque(chequeId);
                    errorMessages = result.ErrorMessages;
                }

                if (errorMessages.Count > 0)
                {
                    foreach (var error in errorMessages)
                    {
                        Page.ModelState.AddModelError(error.Key, error.Value);
                    }
                }
                else
                {
                    Response.Redirect("~/Cheque/Default/" + this.CustomerDdl.SelectedValue);
                }
            }
        }
    }
}