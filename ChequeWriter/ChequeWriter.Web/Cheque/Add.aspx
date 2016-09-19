<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ChequeWriter.Web.Cheque.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <h4>Create a new cheque</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <label runat="server" for="ChequeNo" class="col-md-2 control-label" id="CheckNoLabel"></label>
            <div class="col-md-3">
                <input readonly runat="server" type="text" class="form-control" id="ChequeNo">
            </div>
        </div>
        <div class="form-group">
            <label runat="server" for="Payee" class="col-md-2 control-label" id="PayeeLabel"></label>
            <div class="col-md-3">
                <asp:DropDownList ID="PayeeID" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label runat="server" for="ChequeAmount" class="col-md-2 control-label" id="ChequeAmountLabel"></label>
            <div class="col-md-3">
                <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="ChequeAmount"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label runat="server" for="ChequeMemo" class="col-md-2 control-label" id="ChequeMemoLabel"></label>
            <div class="col-md-3">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="ChequeMemo" CssClass="form-control" MaxLength="200"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-3">
                <asp:Button runat="server" ID="Submit" CssClass="btn btn-default" OnClick="Submit_Click" />
            </div>
        </div>
    </div>
</asp:Content>
