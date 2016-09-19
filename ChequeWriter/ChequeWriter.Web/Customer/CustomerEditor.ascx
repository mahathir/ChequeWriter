<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEditor.ascx.cs" Inherits="ChequeWriter.Web.Customer.CustomerEditor" %>

<div class="form-horizontal">
    <h4><%: this.Title %></h4>
    <hr />
    <asp:ValidationSummary ShowModelStateErrors="true" runat="server" CssClass="text-danger" />
    <input type="hidden" id="CustomerIDHidden" runat="server" />
    <div class="form-group">
        <label runat="server" for="CustomerNo" class="col-md-2 control-label" id="CustomerNoLabel"></label>
        <div class="col-md-3">
            <input readonly runat="server" type="text" class="form-control" id="CustomerNo">
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="Password" class="col-md-2 control-label" id="PasswordLabel"></label>
        <div class="col-md-3">
            <input runat="server" type="password" class="form-control" id="Password">
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="ConfirmPassword" class="col-md-2 control-label" id="ConfirmPasswordLabel"></label>
        <div class="col-md-3">
            <input runat="server" type="password" class="form-control" id="ConfirmPassword">
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="FirstName" class="col-md-2 control-label" id="FirstNameLabel"></label>
        <div class="col-md-3">
            <asp:TextBox runat="server" MaxLength="50" CssClass="form-control" ID="FirstName"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="LastName" class="col-md-2 control-label" id="LastNameLabel"></label>
        <div class="col-md-3">
            <asp:TextBox runat="server" MaxLength="50" CssClass="form-control" ID="LastName"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="Address" class="col-md-2 control-label" id="AddressLabel"></label>
        <div class="col-md-3">
            <asp:TextBox runat="server" TextMode="MultiLine" ID="Address" CssClass="form-control" MaxLength="500"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="ContactNo" class="col-md-2 control-label" id="ContactNoLabel"></label>
        <div class="col-md-3">
            <asp:TextBox runat="server" TextMode="Phone" MaxLength="50" CssClass="form-control" ID="ContactNo"></asp:TextBox>
        </div>
    </div>
    <div class="form-group">
        <label runat="server" for="Status" class="col-md-2 control-label" id="StatusLabel"></label>
        <div class="col-md-3">
            <asp:DropDownList runat="server" ID="Status" CssClass="form-control"></asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-3">
            <asp:Button runat="server" ID="Submit" CssClass="btn btn-default" OnClick="Submit_Click" />
            <asp:Button runat="server" ID="Delete" CssClass="btn btn-default btn-danger" OnClick="Delete_Click" />
        </div>
    </div>
</div>
