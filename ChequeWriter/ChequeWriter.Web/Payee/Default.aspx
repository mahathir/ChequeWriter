<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChequeWriter.Web.Payee.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3><%= this.PanelTitle %></h3>
        </div>
        <div class="panel-body">
            <asp:Label runat="server" ID="FreeLabel"></asp:Label>
            <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/Customer/Add" ID="AddCustomer" runat="server" />
            <div class="form-horizontal">
                <div class="form-group">
                    <label runat="server" for="CustomerDdl" class="col-md-1 control-label" id="CustomerLabel"></label>
                    <div class="col-md-3">
                        <asp:DropDownList AutoPostBack="true" runat="server" OnSelectedIndexChanged="CustomerDdl_SelectedIndexChanged"
                             ID="CustomerDdl" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <asp:Button CssClass="btn btn-primary" runat="server" ID="AddPayee" OnClick="AddPayee_Click" />
            <br />
            <br />
            <asp:ValidationSummary ShowModelStateErrors="true" runat="server" CssClass="text-danger" />
            <div class="table-responsive">
                <asp:GridView CssClass="table table-hover table-bordered" ID="Payee_GridView" AllowCustomPaging="true"
                    AllowPaging="true" AutoGenerateColumns="false" runat="server"
                    PageSize="10" OnPageIndexChanging="Payee_GridView_PageIndexChanging"
                    OnRowDeleting="Payee_GridView_RowDeleting" OnRowEditing="Payee_GridView_RowEditing"
                    OnRowDataBound="Payee_GridView_RowDataBound"
                    GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["Command"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" CssClass="btn btn-xs btn-primary" runat="server"
                                    CommandName="Edit"><%= CustGridHeaderText["Edit"] %></asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" OnClientClick="" CssClass="btn btn-xs btn-danger" runat="server"
                                    CommandName="Delete"><%= CustGridHeaderText["Delete"] %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["FirstName"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("FirstName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["LastName"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("LastName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["Status"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Status") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Payee Data
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
