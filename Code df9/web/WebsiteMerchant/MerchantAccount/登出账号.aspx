<%@ Page Title="登出账号" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="登出账号.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.登出账号" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1">
     <table class="auto-style1">
        <tr>
            <td>商户ID <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_立即登出账号" runat="server" Text="立即登出账号" class="btn btn-info btn-fw" OnClick="Button_立即登出账号_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

</div>

</asp:Content>
