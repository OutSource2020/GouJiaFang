<%@ Page Title="商户对账" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="对账页面.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ControlCenter.对账页面" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">
</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

    <%--弹窗--%>
    <style>
        .black_overlay {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=88);
        }

        .white_content {
            display: none;
            position: absolute;
            top: 20%;
            left: 20%;
            width: 50%;
            height: 50%;
            padding: 20px;
            border: 10px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>
    <div id="gridTable">
        <asp:GridView
            ID="GridView1"
            runat="server"
            class="auto-style1"
            AutoGenerateColumns="False"
            ShowHeaderWhenEmpty="true"
            >
            <Columns>
                <asp:BoundField DataField="MerchantID" HeaderText="商户ID" />
                <asp:BoundField DataField="Balance" HeaderText="商户余额（上一天）" />
                <asp:BoundField DataField="Reverse" HeaderText="冲正（当天）" />
                <asp:BoundField DataField="Deposit" HeaderText="充值总额（当天）" />
                <asp:BoundField DataField="Withdraw" HeaderText="出款总额（当天）" />
                <asp:BoundField DataField="Diff" HeaderText="差值" />
                <asp:BoundField DataField="CreateTime" HeaderText="记录时间" />
            </Columns>
            <EmptyDataTemplate>No Record Available 沒有可用記錄</EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
