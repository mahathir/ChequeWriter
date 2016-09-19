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

                (this.addCustomerForm.FindControl("CustomerNo") as HtmlInputText).Value = 
                    CustomerService.GenerateNewCustomerNo();
            }
        }

        private void SetTranslation()
        {
            Page.Title = string.Format(CommonsRes.Add_, EntitiesRes.Customer);
            (this.addCustomerForm.FindControl("FirstNameLabel") as HtmlGenericControl).InnerText = EntitiesRes.FirstName;
            (this.addCustomerForm.FindControl("LastNameLabel") as HtmlGenericControl).InnerText = EntitiesRes.LastName;
            (this.addCustomerForm.FindControl("AddressLabel") as HtmlGenericControl).InnerText = EntitiesRes.Address;
            (this.addCustomerForm.FindControl("ContactNoLabel") as HtmlGenericControl).InnerText = EntitiesRes.ContactNo;
            (this.addCustomerForm.FindControl("Submit") as Button).Text = CommonsRes.Submit;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var model = new CustomerAddModel();
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                CustomerService.Create(model.ToDTO());
            }
        }

        public void addCustomerForm_InsertItem()
        {
            var model = new CustomerAddModel();
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                CustomerService.Create(model.ToDTO());
            }
        }
    }
}