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
    public partial class ChequeEditor : System.Web.UI.UserControl
    {
        [Dependency]
        public IChequeService ChequeService { get; set; }
        [Dependency]
        public IPayeeService PayeeService { get; set; }

        public string Title
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

        public long ChequeID { get; set; }
        public long CustomerID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslation();
                InitControl();

                if (ChequeID > 0)
                {
                    GoToEditMode();
                }
                else
                {
                    GoToAddMode();
                }

                InitPayeeDdl();
            }
        }

        private void InitPayeeDdl()
        {
            var pageResult = PayeeService.Retrieve(1, int.MaxValue,
                new Dictionary<string, string> { { "CustomerID", this.CustomerID.ToString() } });

            if (pageResult.TotalCount > 0)
            {
                this.PayeeDdl.DataSource = pageResult.Items.Select(a =>
                    new
                    {
                        a.PayeeID,
                        FullName = a.FirstName + (string.IsNullOrWhiteSpace(a.LastName) ? "" : " " + a.LastName)
                    });
                this.PayeeDdl.DataTextField = "FullName";
                this.PayeeDdl.DataValueField = "PayeeID";
                this.PayeeDdl.DataBind();
            }
        }

        private void GoToAddMode()
        {
            this.ChequeID = 0;
            var dateNow = DateTime.UtcNow;
            this.PostingDate.Text = dateNow.ToString();
            this.ChequeNo.Value = ChequeService.GenerateChequeNumber(dateNow);
            this.Status.SelectedValue = ChequeStatus.A.ToString();
            this.CustomerIDHidden.Value = this.CustomerID.ToString();
            this.Delete.Visible = false;
            this.Cancel.Visible = false;
            this.Print.Visible = false;
            this.Title = string.Format(CommonsRes.Add_, EntitiesRes.Cheque);
        }

        private void GoToEditMode()
        {
            var cheque = ChequeService.Retrieve(ChequeID);

            if (cheque == null || cheque.Status == ChequeStatus.R.ToString())
            {
                Page.ModelState.AddModelError("Cheque",
                    string.Format(MessagesRes._NotFound, EntitiesRes.Cheque));
                GoToAddMode();
            }
            else
            {
                BindToView(cheque);
                this.Title = string.Format(CommonsRes.Edit_, EntitiesRes.Cheque);
            }
        }

        private void InitControl()
        {
            var statusNames = Enum.GetNames(typeof(ChequeStatus));
            var statusValues = Enum.GetValues(typeof(ChequeStatus));

            for (var i = 1; i < statusNames.Length; i++)
            {
                var value = statusValues.GetValue(i);
                var name = statusNames[i];
                var description = ((ChequeStatus)Enum.Parse(typeof(ChequeStatus), value.ToString())).GetDescription();
                this.Status.Items.Add(new ListItem(description, name));
            }
            this.Status.DataBind();
        }

        private void BindToView(DTO.Models.Cheque cheque)
        {
            this.ChequeNo.Value = cheque.ChequeNo;
            this.PayeeDdl.SelectedValue = cheque.PayeeID.ToString();
            this.Status.SelectedValue = cheque.Status;
            this.Amount.Text = cheque.Amount.ToString();
            this.PostingDate.Text = cheque.PostingDate.ToString();
            this.CustomerIDHidden.Value = cheque.CustomerID.ToString();
            this.Memo.Text = cheque.Memo;
            this.ChequeIDHidden.Value = cheque.ChequeID.ToString();
            this.CustomerIDHidden.Value = cheque.CustomerID.ToString();
            this.CustomerID = cheque.CustomerID;
        }

        private void SetTranslation()
        {
            this.StatusLabel.InnerText = EntitiesRes.Status;
            this.ChequeNoLabel.InnerText = EntitiesRes.ChequeNo;
            this.PayeeLabel.InnerText = EntitiesRes.Payee;
            this.AmountLabel.InnerText = EntitiesRes.Amount;
            this.PostingDateLabel.InnerText = EntitiesRes.PostingDate;
            this.MemoLabel.InnerText = EntitiesRes.Memo;
            this.Submit.Text = CommonsRes.Submit;
            this.Delete.Text = CommonsRes.Delete;
            this.Print.Text = CommonsRes.Print;
            this.Print.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Print, EntitiesRes.Cheque) + "');";
            this.Delete.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Delete, EntitiesRes.Cheque) + "');";
            this.Cancel.Text = CommonsRes.Cancel;
            this.Cancel.OnClientClick = "return confirm('" +
                string.Format(MessagesRes.AreYouSureTo_This_, CommonsRes.Cancel, EntitiesRes.Cheque) + "');";
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            long chequeId;
            long.TryParse(this.ChequeIDHidden.Value, out chequeId);
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            long payeeId;
            long.TryParse(this.PayeeDdl.SelectedValue, out payeeId);
            decimal amount;
            decimal.TryParse(this.Amount.Text, out amount);
            var model = new DTO.Models.Cheque
            {
                Status = this.Status.SelectedValue,
                ChequeNo = this.ChequeNo.Value,
                Amount = amount,
                ChequeID = chequeId,
                CustomerID = customerId,
                Memo = this.Memo.Text,
                PayeeID = payeeId,
                PostingDate = DateTime.Parse(this.PostingDate.Text)
            };

            IDictionary<string, string> errorMessages;
            if (model.ChequeID > 0)
            {
                var result = ChequeService.Update(model);
                errorMessages = result.ErrorMessages;
            }
            else
            {
                var result = ChequeService.Create(model);
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
                Response.Redirect("~/Cheque/Default/" + customerId);
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            long chequeId;
            long.TryParse(this.ChequeIDHidden.Value, out chequeId);
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            if (chequeId > 0)
            {
                var result = ChequeService.Delete(chequeId);
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
                Response.Redirect("~/Cheque");
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            long customerId;
            long.TryParse(this.CustomerIDHidden.Value, out customerId);
            Response.Redirect("~/Cheque/Default/" + customerId);
        }

        protected void Print_Click(object sender, EventArgs e)
        {

        }
    }
}