<%@ Page Title="商户列表设置安全密码" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表设置安全密码.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表设置安全密码" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

    <table class="auto-style1">
            
        <tr> 
            <td style="width: 30%">商户ID</td>
            <td>
                <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户账号密码和支付密码</td>
            <td>
                <asp:Button ID="Button_发送商户账号密码和支付密码" runat="server" class="btn btn-info btn-fw" Text="发送商户账号密码和支付密码" OnClick="Button_发送商户账号密码和支付密码_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button_重置商户账号密码和支付密码" runat="server" class="btn btn-info btn-fw" Text="重置商户账号密码和支付密码"  OnClick="Button_重置商户账号密码和支付密码_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户账号密码-API</td>
            <td>
                <asp:Button ID="Button_发送商户账号密码API" runat="server" class="btn btn-info btn-fw" Text="发送商户账号密码API" OnClick="Button_发送商户账号密码API_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button_重置商户账号密码API" runat="server" class="btn btn-info btn-fw" Text="重置商户账号密码API"  OnClick="Button_重置商户账号密码API_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />

</div>


</asp:Content>