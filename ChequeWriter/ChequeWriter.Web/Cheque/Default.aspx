<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChequeWriter.Web.Cheque.Default" %>
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
            <asp:Button CssClass="btn btn-primary" runat="server" ID="AddCheque" OnClick="AddCheque_Click" />
            <br />
            <br />
            <asp:ValidationSummary ShowModelStateErrors="true" runat="server" CssClass="text-danger" />
            <div class="table-responsive">
                <asp:GridView CssClass="table table-hover table-bordered" ID="Cheque_GridView" AllowCustomPaging="true"
                    AllowPaging="true" AutoGenerateColumns="false" runat="server"
                    PageSize="10" OnPageIndexChanging="Cheque_GridView_PageIndexChanging"
                    OnRowDeleting="Cheque_GridView_RowDeleting" OnRowEditing="Cheque_GridView_RowEditing"
                    OnRowDataBound="Cheque_GridView_RowDataBound" OnRowCommand="Cheque_GridView_RowCommand"
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
                                <asp:LinkButton ID="btnCancel" CssClass="btn btn-xs btn-warning" runat="server"
                                    CommandName="CancelCheque" CommandArgument='<%# Eval("ChequeID") %>' ><%= CustGridHeaderText["Cancel"] %></asp:LinkButton>
                                <asp:LinkButton ID="btnPrint" CssClass="btn btn-xs btn-info" runat="server"
                                    CommandName="Print" CommandArgument='<%# Eval("ChequeID") %>'><%= CustGridHeaderText["Print"] %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["ChequeNo"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("ChequeNo") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["Payee"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Payee") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["Amount"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Amount") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["PostingDate"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("PostingDate") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <%= CustGridHeaderText["Memo"] %>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Memo") %>
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
                        No Cheque Data
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
