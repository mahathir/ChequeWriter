using ChequeWriter.Commons.Translations;
using ChequeWriter.IBusinessLogic;
using ChequeWriter.Web.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Customer
{
    public partial class Add : System.Web.UI.Page
    {
        [Dependency]
        public ICustomerService CustomerService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslation();

                this.CustomerNo.Value = CustomerService.GenerateNewCustomerNo();
            }
        }

        private void SetTranslation()
        {
            Page.Title = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
            this.CustomerNoLabel.InnerText = EntitiesRes.CustomerNo;
            this.PasswordLabel.InnerText = EntitiesRes.Password;
            this.ConfirmPasswordLabel.InnerText = CommonsRes.ConfirmPassword;
            this.FirstNameLabel.InnerText = EntitiesRes.FirstName;
            this.LastNameLabel.InnerText = EntitiesRes.LastName;
            this.AddressLabel.InnerText = EntitiesRes.Address;
            this.ContactNoLabel.InnerText = EntitiesRes.ContactNo;
            this.Submit.Text = CommonsRes.Submit;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (this.ConfirmPassword.Value != this.Password.Value)
            {
                ModelState.AddModelError("ConfirmPassword", 
                    string.Format(MessagesRes._And_Doesnt_, CommonsRes.ConfirmPassword, 
                    EntitiesRes.Password, CommonsRes.Match));
                return;
            }
            var model = new DTO.Models.Customer();
            model.CustomerNo = this.CustomerNo.Value;
            model.FirstName = this.FirstName.Text;
            model.LastName = this.LastName.Text;
            model.Address = this.Address.Text;
            model.ContactNo = this.ContactNo.Text;
            model.Password = this.Password.Value;
            var result = CustomerService.Create(model);

            if (result.ErrorMessages.Count > 0)
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
            else
            {
                Response.Redirect("~/Customer/Add.aspx");
            }
        }
    }
}