<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ChequeWriter.Web.Customer.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <h4>Create a new Customer</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <asp:FormView runat="server" ID="addCustomerForm"
            ItemType="ChequeWriter.Web.Models.CustomerAddModel"
            InsertMethod="addCustomerForm_InsertItem" DefaultMode="Insert"
            RenderOuterTable="false">
            <InsertItemTemplate>
                <div class="form-group">
                    <label runat="server" for="CustomerNo" class="col-md-2 control-label" id="CustomerNoLabel"></label>
                    <div class="col-md-3">
                        <input readonly runat="server" type="text" class="form-control" id="CustomerNo">
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
                    <div class="col-md-offset-2 col-md-3">
                        <asp:Button runat="server" ID="Submit" CssClass="btn btn-default" CommandName="Insert" />
                    </div>
                </div>
            </InsertItemTemplate>
        </asp:FormView>
        <%--<div class="form-group">
            <label runat="server" for="CustomerNo" class="col-md-2 control-label" id="CustomerNoLabel"></label>
            <div class="col-md-3">
                <input readonly runat="server" type="text" class="form-control" id="CustomerNo">
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
            <div class="col-md-offset-2 col-md-3">
                <asp:Button runat="server" ID="Submit" CssClass="btn btn-default" OnClick="Submit_Click" />
            </div>
        </div>--%>
    </div>
</asp:Content>
