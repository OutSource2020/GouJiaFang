﻿<%@ Page Title="商户账号绑定邮箱" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户账号绑定邮箱.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.商户账号绑定邮箱" %>

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
                <td style="width: 30%">商户旧邮箱</td>
                <td>
                    <asp:TextBox ID="TextBox_商户旧绑定邮箱" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">商户新邮箱</td>
                <td>
                    <asp:TextBox ID="TextBox_商户新绑定邮箱" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">商户新邮箱-再次确认</td>
                <td>
                    <asp:TextBox ID="TextBox_商户新绑定邮箱再次确认" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">KEY</td>
                <td>
                    <asp:TextBox ID="TextBox_KEY" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                    <asp:Button ID="Button_更新邮箱" runat="server" Text="更新邮箱" class="btn btn-info btn-fw" OnClick="Button_更新邮箱_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

    </div>

</asp:Content>
