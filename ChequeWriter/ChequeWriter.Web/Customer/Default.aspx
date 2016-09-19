<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChequeWriter.Web.Customer.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3><%= this.PanelTitle %></h3>
        </div>
        <div class="panel-body">
            <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/Customer/Add" ID="AddCustomer" runat="server" />
            <br />
            <br />
            <div class="table-responsive">
                <asp:GridView CssClass="table table-hover table-bordered" ID="Customer_GridView" AllowCustomPaging="true"
                    AllowPaging="true" AutoGenerateColumns="false" runat="server"
                    PageSize="10" OnPageIndexChanging="Customer_GridView_PageIndexChanging"
                    OnRowDeleting="Customer_GridView_RowDeleting" OnRowEditing="Customer_GridView_RowEditing"
                    OnRowDataBound="Customer_GridView_RowDataBound"
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
                                <%= CustGridHeaderText["CustomerNo"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("CustomerNo") %>
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
                                <%= CustGridHeaderText["Address"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Address") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["ContactNo"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("ContactNo") %>
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
                        No Data
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
