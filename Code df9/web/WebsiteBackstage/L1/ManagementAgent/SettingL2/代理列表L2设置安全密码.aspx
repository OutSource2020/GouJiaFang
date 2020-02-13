<%@ Page Title="代理列表L2设置安全密码" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="代理列表L2设置安全密码.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementAgent.SettingL2.代理列表L2设置安全密码" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

     <table class="auto-style1">
            <tr>
                <td style="width: 30%">代理ID</td>
                <td>
                    <asp:Label ID="Label_代理ID" runat="server" Text="Label_代理ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">代理密码</td>
                <td>
                    <asp:TextBox ID="TextBox_代理密码" runat="server"></asp:TextBox>
                </td>
            </tr>
                    </table>


    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
    <asp:Button ID="Button_操作更新" runat="server" Text="更新密码" class="btn btn-info btn-fw" OnClick="Button_操作更新_Click" />


</div>


</asp:Content>
