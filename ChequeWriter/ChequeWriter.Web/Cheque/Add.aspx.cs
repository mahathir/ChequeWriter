using ChequeWriter.Commons;
using ChequeWriter.Commons.Translations;
using ChequeWriter.DTO.Models;
using ChequeWriter.IBusinessLogic;
using ChequeWriter.Web.Models;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChequeWriter.Web.Cheque
{
    public partial class Add : System.Web.UI.Page
    {
        [Dependency]
        public IChequeService ChequeService { get; set; }
        [Dependency]
        public IPayeeService PayeeService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetTranslation();

                this.ChequeNo.Value = ChequeService.GenerateChequeNumber();
                var payeePagedResult = PayeeService.Retrieve(1, int.MaxValue);
                this.PayeeID.DataSource = payeePagedResult.Items.Select(a => new
                {
                    a.CustomerID,
                    a.PayeeID,
                    Status = ((CustomerStatus)Enum.Parse(typeof(CustomerStatus), a.Status)).GetDescription(),
                    FullName = a.FirstName + (string.IsNullOrWhiteSpace(a.LastName) ? string.Empty : " " + a.LastName)
                });
                this.PayeeID.DataTextField = "FullName";
                this.PayeeID.DataValueField = "PayeeID";
            }
        }

        private void SetTranslation()
        {
            Page.Title = string.Format(CommonsRes.Add_, EntitiesRes.Cheque);
            this.CheckNoLabel.InnerText = EntitiesRes.ChequeNo;
            this.PayeeLabel.InnerText = EntitiesRes.Payee;
            this.ChequeAmountLabel.InnerText = EntitiesRes.Amount;
            this.ChequeMemoLabel.InnerText = EntitiesRes.Memo;
            this.Submit.Text = CommonsRes.Submit;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            var model = new ChequeAddModel();
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                ChequeService.Create(model.ToDTO());
            }
        }
    }
}