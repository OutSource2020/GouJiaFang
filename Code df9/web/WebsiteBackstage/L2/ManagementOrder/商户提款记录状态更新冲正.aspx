<%@ Page Title="商户提款记录状态更新冲正" Language="C#" MasterPageFile="~/WebsiteBackstage/L2/SiteTemplateBackstageL2.Master" AutoEventWireup="true" CodeBehind="商户提款记录状态更新冲正.aspx.cs" Inherits="web1.WebsiteBackstage.L2.ManagementOrder.商户提款记录状态更新冲正" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

     <table class="auto-style1">
        <tr>
            <td>
                <asp:Label Text="订单号" runat="server" />
            </td>
            <td>
                <asp:Label ID="Label_订单号" runat="server" Text="Label_订单号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>交易方</td>
            <td>
                <asp:Label ID="Label_交易方" runat="server" Text="Label_交易方"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>交易方卡号</td>
            <td>
                <asp:Label ID="Label_交易方卡号" runat="server" Text="Label_交易方卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>交易方银行

            </td>
            <td>
                <asp:Label ID="Label_交易方银行" runat="server" Text="Label_交易方银行"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>金额

            </td>
            <td>
                <asp:Label ID="Label_金额" runat="server" Text="Label_金额"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                目前状态
            </td>
            <td>
                <asp:Label ID="Label_状态" runat="server" Text="Label_状态"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                银行卡

            </td>
            <td>
                <asp:Label ID="Label_银行卡" runat="server" Text="Label_银行卡"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label Text="备注" runat="server" />
            </td>
            <td>
                <asp:Label ID="Label_备注" runat="server" Text="Label_备注"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_冲正" runat="server" Text="冲正"  class="btn btn-info btn-fw" OnClick="Button_冲正_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
            <td>

            </td>
        </tr>

    </table>


</div>

</asp:Content>