<%@ Page Title="商户账号" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户账号.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.商户账号" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

            <table style="width: 100%">
                <tr>
                    <td style="width:30%">商户名称:</td>
                    <td><asp:Label ID="Label_商户名称" runat="server" Text="商户名称"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width:30%">商户ID:</td>
                    <td><asp:Label ID="Label_商户ID" runat="server" Text="商户ID"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width:30%">商户状态:</td>
                    <td><asp:Label ID="Label_商户状态" runat="server" Text="商户状态"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width:30%">时间注册:</td>
                    <td><asp:Label ID="Label_时间注册" runat="server" Text="时间注册"></asp:Label></td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="width:30%">
                        登入密码</td>
                    <td>

                        <asp:Button ID="Button_登入密码" runat="server" Text="设置登入密码" class="btn btn-info btn-fw" OnClick="Button_登入密码_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width:30%">
                        登入密码(API)</td>
                    <td>

                        <asp:Button ID="Button1" runat="server" Text="设置登入密码(API)" class="btn btn-info btn-fw" OnClick="Button_登入密码API_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="width:30%">
                        支付密码</td>
                    <td>
                        <asp:Button ID="Button_支付密码" runat="server" Text="设置支付密码" class="btn btn-info btn-fw" OnClick="Button_支付密码_Click" />
                </tr>
                <tr>
                    <td style="width:30%">
                        绑定邮箱</td>
                    <td>
                        <asp:Button ID="Button_设置绑定邮箱" runat="server" Text="设置绑定邮箱" class="btn btn-info btn-fw" OnClick="Button_设置绑定邮箱_Click" />
                    </td>
                </tr>
                </table>
        </div>
</asp:Content>
