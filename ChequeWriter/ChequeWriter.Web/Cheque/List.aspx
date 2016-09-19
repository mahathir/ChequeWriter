<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="ChequeWriter.Web.Cheque.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink NavigateUrl="~/Cheque/Add" Text="Add Cheque" runat="server" />
    <asp:GridView ID="Cheque_GridView" AllowCustomPaging="true" AllowPaging="true" AutoGenerateColumns="true" runat="server"
        PageSize="10" OnPageIndexChanging="Cheque_GridView_PageIndexChanging"></asp:GridView>
</asp:Content>
