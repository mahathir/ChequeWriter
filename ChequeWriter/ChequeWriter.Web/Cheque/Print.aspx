<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="ChequeWriter.Web.Cheque.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="width: 800px; margin: 10% auto; border: solid; padding: 25px;">
        <div style="font-size: 13px; width: 150px; float: left; padding-bottom: 20px;">
            <b><%= CustomerName %></b>
            <br />
            <%= this.Cheque.Customer.Address %>

        </div>
        <div style="float: right; border: solid; padding: 10px; width: 175px;">
            <b><%= this.Cheque.PostingDate.ToString("dd/MM/yyyy") %></b>
        </div>
        <div style="float: right; margin-right: 10px; padding: 10px;">
            <%= Translations["Date"] %>
        </div>
        <div style="clear: both;">
        </div>
        <div style="width: 150px; float: left; padding: 10px 0 40px 0; margin-right: 20px;">
            <%= Translations["PayToTheOrderOf"] %>
        </div>
        <div style="float: left; width: 300px; border: solid; padding: 10px;">
            <%= PayeeName %>
        </div>
        <div style="float: right; border: solid; padding: 10px; width: 175px">
            <b>$ <%= this.Cheque.Amount.ToString("N03") %></b>
        </div>
        <div style="float: right; margin-right: 10px; padding: 10px;">
            <%= Translations["Amount"] %>
        </div>
        <div style="clear: both;">
        </div>
        <div style="width: 150px; float: left; padding: 10px 0 40px 0; margin-right: 20px;">
            <%= Translations["AmountInWords"] %>
        </div>
        <div style="margin-bottom: 10px;float: left; width: 500px; border: solid; padding: 10px; margin-right: 20px;">
            <%= AmountInWords %>
        </div>
        <div style="padding: 10px;">
            dollars
        </div>
        <div style="clear: both;">
        </div>
        <div style="font-size: 13px; width: 150px; float: left; padding-bottom: 20px; margin-right: 20px;margin-top: 15px;">
            <b>RUBIK FINANCIAL LTD.</b>
            <br />
            1 Eden Park, Macquarie Park NSW, 2113
        </div>
        <div style="float: left; margin-right: 20px; padding: 10px;margin-top: 15px;">
            <%= Translations["Memo"] %>
        </div>
        <div style="float: left; width: 200px; border: solid; padding: 10px;margin-bottom: 15px;margin-top: 15px;">
            <%= this.Cheque.Memo %>
        </div>
        <div style="float: right; height:50px; width: 100px; border: solid; padding: 10px;margin-top: 15px;">
            &nbsp;
        </div>
        <div style="float: right; margin-right: 20px; padding: 10px;margin-top: 15px;">
            <%= Translations["Signature"] %>
        </div>
        <div style="clear: both;">
        </div>
        <div style="font-size: 20px;"><%= Translations["ChequeNo"] %>: <%= this.Cheque.ChequeNo %></div>
        <div style="clear: both;">
        </div>
    </form>
    <script>
        window.onload = function () {
            window.print();
        };
    </script>
</body>
</html>
