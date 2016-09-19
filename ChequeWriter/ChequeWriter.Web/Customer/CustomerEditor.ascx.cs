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
    public partial class CustomerEditor : System.Web.UI.UserControl
    {
        [Dependency]
        public ICustomerService CustomerService { get; set; }
        private string _panelTitle;
        public string Title
        {
            get
            {
                return (ViewState["PanelTitle"] = ViewState["PanelTitle"] ?? _panelTitle).ToString();
            }
            set
            {
                ViewState["PanelTitle"] = value;
            }
        }

        public long CustomerID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslation();
                InitControl();

                this.CustomerNo.Value = CustomerService.GenerateNewCustomerNo();
                if (CustomerID > 0)
                {
                    GoToEditMode();
                }
                else
                {
                    GoToAddMode();
                }
            }
        }

        private void GoToAddMode()
        {
            this.CustomerID = 0;
            this.Status.SelectedValue = CustomerStatus.A.ToString();
            this.Status.Enabled = false;
            this.Delete.Visible = false;
            this._panelTitle = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
        }

        private void GoToEditMode()
        {
            var customer = CustomerService.Retrieve(CustomerID);

            if (customer == null || customer.Status == CustomerStatus.R.ToString())
            {
                Page.ModelState.AddModelError("Customer",
                    string.Format(MessagesRes._NotFound, EntitiesRes.Customer));
                GoToAddMode();
            }
            else
            {
                BindToView(customer);
                this._panelTitle = string.Format(CommonsRes.Edit_, EntitiesRes.Customer);
            }
        }

        private void InitControl()
        {
            var statusNames = Enum.GetNames(typeof(CustomerStatus));
            var statusValues = Enum.GetValues(typeof(CustomerStatus));

            for (var i = 1; i < statusNames.Length; i++)
            {
                var value = statusValues.GetValue(i);
                var name = statusNames[i];
                var description = ((CustomerStatus)Enum.Parse(typeof(CustomerStatus), value.ToString())).GetDescription();
                this.Status.Items.Add(new ListItem(description, name));
            }
            this.Status.DataBind();
        }

        private void BindToView(DTO.Models.Customer customer)
        {
            this.Status.SelectedValue = customer.Status;
            this.CustomerNo.Value = customer.CustomerNo;
            this.FirstName.Text = customer.FirstName;
            this.LastName.Text = customer.LastName;
            this.Address.Text = customer.Address;
            this.ContactNo.Text = customer.ContactNo;
            this.Password.Value = customer.Password;
            this.CustomerIDHidden.Value = customer.CustomerID.ToString();
        }

        private void SetTranslation()
        {
            this.StatusLabel.InnerText = EntitiesRes.Status;
            this.CustomerNoLabel.InnerText = EntitiesRes.CustomerNo;
            this.PasswordLabel.InnerText = EntitiesRes.Password;
            this.ConfirmPasswordLabel.InnerText = CommonsRes.ConfirmPassword;
            this.FirstNameLabel.InnerText = EntitiesRes.FirstName;
            this.LastNameLabel.InnerText = EntitiesRes.LastName;
            this.AddressLabel.InnerText = EntitiesRes.Address;
            this.ContactNoLabel.InnerText = EntitiesRes.ContactNo;
            this.Submit.Text = CommonsRes.Submit;
            this.Delete.Text = CommonsRes.Delete;
            this.Delete.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Customer) + "');";
            this.Cancel.Text = CommonsRes.Cancel;
            this.Cancel.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_, CommonsRes.Cancel) + "');";
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var validatePassword = CustomerID > 0 ?
                string.IsNullOrWhiteSpace(this.Password.Value) ? false : true
                : true;

            if (validatePassword && this.ConfirmPassword.Value != this.Password.Value)
            {
                Page.ModelState.AddModelError("ConfirmPassword",
                    string.Format(MessagesRes._And_Doesnt_, CommonsRes.ConfirmPassword,
                    EntitiesRes.Password, CommonsRes.Match));
                return;
            }
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            var model = new DTO.Models.Customer
            {
                Status = this.Status.SelectedValue,
                CustomerNo = this.CustomerNo.Value,
                FirstName = this.FirstName.Text,
                LastName = this.LastName.Text,
                Address = this.Address.Text,
                ContactNo = this.ContactNo.Text,
                Password = this.Password.Value,
                CustomerID = customerId
            };

            IDictionary<string, string> errorMessages;
            if (model.CustomerID > 0)
            {
                var result = CustomerService.Update(model);
                errorMessages = result.ErrorMessages;
            }
            else
            {
                var result = CustomerService.Create(model);
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
                Response.Redirect("~/Customer");
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Customer");
        }
    }
}