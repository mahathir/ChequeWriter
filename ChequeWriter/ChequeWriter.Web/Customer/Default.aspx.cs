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

namespace ChequeWriter.Web.Customer
{
    public partial class Default : System.Web.UI.Page
    {
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
                GetGridData(1, Customer_GridView.PageSize);
            }
        }

        private void SetTranslations()
        {
            this.AddCustomer.Text = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
            this.PanelTitle = EntitiesRes.Customer;
            CustGridHeaderText["CustomerNo"] = EntitiesRes.CustomerNo;
            CustGridHeaderText["FirstName"] = EntitiesRes.FirstName;
            CustGridHeaderText["LastName"] = EntitiesRes.LastName;
            CustGridHeaderText["Address"] = EntitiesRes.Address;
            CustGridHeaderText["ContactNo"] = EntitiesRes.ContactNo;
            CustGridHeaderText["Status"] = EntitiesRes.Status;
            CustGridHeaderText["Command"] = CommonsRes.Command;
            CustGridHeaderText["Edit"] = CommonsRes.Edit;
            CustGridHeaderText["Delete"] = CommonsRes.Delete;
        }

        private void GetGridData(int current, int pageSize)
        {
            var data = CustomerService.Retrieve(current, pageSize);
            Customer_GridView.VirtualItemCount = (int)data.TotalCount;
            Customer_GridView.DataSource = data.Items.Select(a => new
            {
                a.CustomerID,
                a.CustomerNo,
                a.FirstName,
                a.LastName,
                a.Address,
                a.ContactNo,
                Status = ((CustomerStatus)Enum.Parse(typeof(CustomerStatus), a.Status)).GetDescription()
            }).ToList();
            Customer_GridView.DataKeyNames = new string[] { "CustomerID" };
            Customer_GridView.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Customer_GridView.HeaderRow != null)
            {
                Customer_GridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            base.OnPreRender(e);
        }

        protected void Customer_GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Customer_GridView.PageIndex = e.NewPageIndex;
            GetGridData(e.NewPageIndex + 1, Customer_GridView.PageSize);
        }

        protected void Customer_GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var customerId = Customer_GridView.DataKeys[e.NewEditIndex].Value.ToString();

            Response.Redirect("~/Customer/Edit/" + customerId);
        }

        protected void Customer_GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var customerIdStr = Customer_GridView.DataKeys[e.RowIndex].Value.ToString();
            long customerId;
            long.TryParse(customerIdStr, out customerId);
            if (customerId > 0)
            {
                var result = CustomerService.Delete(customerId);
                if (result.ErrorMessages.Count > 0)
                {
                    foreach (var error in result.ErrorMessages)
                    {
                        Page.ModelState.AddModelError(error.Key, error.Value);
                    }
                }
                else
                {
                    Response.Redirect("~/Customer");
                }
            }
            else
            {
                Response.Redirect("~/Customer");
            }
        }

        protected void Customer_GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                btnDelete.OnClientClick = "return confirm('" +
                    string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Customer) + "');";
            }
        }
    }
}