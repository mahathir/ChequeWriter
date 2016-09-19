<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ChequeWriter.Web.Cheque.Add" %>

<%@ Register Src="~/Cheque/ChequeEditor.ascx" TagPrefix="uc1" TagName="ChequeEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <uc1:ChequeEditor runat="server" id="ChequeEditor" />
</asp:Content>
