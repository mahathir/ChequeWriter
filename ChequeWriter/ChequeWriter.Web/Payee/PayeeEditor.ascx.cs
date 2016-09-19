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
    public partial class PayeeEditor : System.Web.UI.UserControl
    {
        [Dependency]
        public IPayeeService PayeeService { get; set; }
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

        public long PayeeID { get; set; }
        public long CustomerID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslation();
                InitControl();

                if (PayeeID > 0)
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
            this.PayeeID = 0;
            this.Status.SelectedValue = PayeeStatus.A.ToString();
            this.CustomerIDHidden.Value = this.CustomerID.ToString();
            this.Status.Enabled = false;
            this.Delete.Visible = false;
            this._panelTitle = string.Format(CommonsRes.Add_, EntitiesRes.Payee);
        }

        private void GoToEditMode()
        {
            var payee = PayeeService.Retrieve(PayeeID);

            if (payee == null || payee.Status == PayeeStatus.R.ToString())
            {
                Page.ModelState.AddModelError("Payee",
                    string.Format(MessagesRes._NotFound, EntitiesRes.Payee));
                GoToAddMode();
            }
            else
            {
                BindToView(payee);
                this._panelTitle = string.Format(CommonsRes.Edit_, EntitiesRes.Payee);
            }
        }

        private void InitControl()
        {
            var statusNames = Enum.GetNames(typeof(PayeeStatus));
            var statusValues = Enum.GetValues(typeof(PayeeStatus));

            for (var i = 1; i < statusNames.Length; i++)
            {
                var value = statusValues.GetValue(i);
                var name = statusNames[i];
                var description = ((PayeeStatus)Enum.Parse(typeof(PayeeStatus), value.ToString())).GetDescription();
                this.Status.Items.Add(new ListItem(description, name));
            }
            this.Status.DataBind();
        }

        private void BindToView(DTO.Models.Payee payee)
        {
            this.Status.SelectedValue = payee.Status;
            this.CustomerIDHidden.Value = payee.CustomerID.ToString();
            this.FirstName.Text = payee.FirstName;
            this.LastName.Text = payee.LastName;
            this.PayeeIDHidden.Value = payee.PayeeID.ToString();
        }

        private void SetTranslation()
        {
            this.StatusLabel.InnerText = EntitiesRes.Status;
            this.FirstNameLabel.InnerText = EntitiesRes.FirstName;
            this.LastNameLabel.InnerText = EntitiesRes.LastName;
            this.Submit.Text = CommonsRes.Submit;
            this.Delete.Text = CommonsRes.Delete;
            this.Delete.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Payee) + "');";
            this.Cancel.Text = CommonsRes.Cancel;
            this.Cancel.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_, CommonsRes.Cancel) + "');";
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            long payeeId;
            long.TryParse(this.PayeeIDHidden.Value, out payeeId);
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            var model = new DTO.Models.Payee
            {
                Status = this.Status.SelectedValue,
                FirstName = this.FirstName.Text,
                LastName = this.LastName.Text,
                PayeeID = payeeId,
                CustomerID = customerId
            };

            IDictionary<string, string> errorMessages;
            if (model.PayeeID > 0)
            {
                var result = PayeeService.Update(model);
                errorMessages = result.ErrorMessages;
            }
            else
            {
                var result = PayeeService.Create(model);
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
                Response.Redirect("~/Payee/Default/" + customerId);
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            long payeeId;
            long.TryParse(this.PayeeIDHidden.Value, out payeeId);
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            if (payeeId > 0)
            {
                var result = PayeeService.Delete(payeeId);
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
                Response.Redirect("~/Payee");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            Response.Redirect("~/Payee/Default/" + customerId);
        }
    }
}