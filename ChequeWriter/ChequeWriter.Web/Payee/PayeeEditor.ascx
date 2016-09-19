<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayeeEditor.ascx.cs" Inherits="ChequeWriter.Web.Payee.PayeeEditor" %>

<div class="form-horizontal">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3><%: this.Title %></h3>
        </div>
        <div class="panel-body">
            <asp:ValidationSummary ShowModelStateErrors="true" runat="server" CssClass="text-danger" />
            <input type="hidden" id="PayeeIDHidden" runat="server" />
            <input type="hidden" id="CustomerIDHidden" runat="server" />
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
                <label runat="server" for="Status" class="col-md-2 control-label" id="StatusLabel"></label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="Status" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-1">
                    <asp:Button runat="server" ID="Delete" CssClass="btn btn-default btn-danger" OnClick="Delete_Click" />
                </div>
                <div class="col-md-2 col-md-push-2">
                    <asp:Button runat="server" ID="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" />
                    <asp:Button runat="server" ID="Cancel" CssClass="btn btn-warning" OnClick="Cancel_Click" />
                </div>
            </div>
        </div>
    </div>
</div>