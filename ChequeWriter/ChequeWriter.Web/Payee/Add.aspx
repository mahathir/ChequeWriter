<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ChequeWriter.Web.Payee.Add" %>

<%@ Register Src="~/Payee/PayeeEditor.ascx" TagPrefix="uc1" TagName="PayeeEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:PayeeEditor runat="server" id="PayeeEditor" />
</asp:Content>
