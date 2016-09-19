<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ChequeWriter.Web.Customer.Edit" %>

<%@ Register Src="~/Customer/CustomerEditor.ascx" TagPrefix="uc1" TagName="CustomerEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:CustomerEditor runat="server" ID="CustomerEditor" />
</asp:Content>
