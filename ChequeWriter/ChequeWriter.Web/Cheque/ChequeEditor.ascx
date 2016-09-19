<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChequeEditor.ascx.cs" Inherits="ChequeWriter.Web.Cheque.ChequeEditor" %>

<div class="form-horizontal">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3><%: this.Title %></h3>
        </div>
        <div class="panel-body">
            <asp:ValidationSummary ShowModelStateErrors="true" runat="server" CssClass="text-danger" />
            <input type="hidden" id="ChequeIDHidden" runat="server" />
            <input type="hidden" id="CustomerIDHidden" runat="server" />
            <div class="form-group">
                <label runat="server" for="ChequeNo" class="col-md-2 control-label" id="ChequeNoLabel"></label>
                <div class="col-md-3">
                    <input readonly runat="server" type="text" class="form-control" id="ChequeNo">
                </div>
            </div>
            <div class="form-group">
                <label runat="server" for="Payee" class="col-md-2 control-label" id="PayeeLabel"></label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="PayeeDdl" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <label runat="server" for="Amount" class="col-md-2 control-label" id="AmountLabel"></label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" TextMode="Number" CssClass="form-control" ID="Amount"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label runat="server" for="PostingDate" class="col-md-2 control-label" id="PostingDateLabel"></label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" TextMode="DateTime" CssClass="form-control" ID="PostingDate"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label runat="server" for="Memo" class="col-md-2 control-label" id="MemoLabel"></label>
                <div class="col-md-3">
                    <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="200" 
                        CssClass="form-control" ID="Memo"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label runat="server" for="Status" class="col-md-2 control-label" id="StatusLabel"></label>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" Enabled="false" ID="Status" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-3">
                    <asp:Button runat="server" ID="Delete" CssClass="btn btn-default btn-danger" OnClick="Delete_Click" />
                    <asp:Button runat="server" ID="Cancel" CssClass="btn btn-warning" OnClick="Cancel_Click" />
                    <asp:Button runat="server" ID="Print" CssClass="btn btn-Info" OnClick="Print_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button runat="server" ID="Submit" CssClass="btn btn-primary" OnClick="Submit_Click" />
                </div>
            </div>
        </div>
    </div>
</div>