<%@ Page Title="商户银行卡更新" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="银行卡管理详情.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.银行卡管理详情" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >



         <table class="auto-style1">
            <tr>
                <td style="width:  30%">编号</td>
                <td>
                    <asp:Label ID="Label_编号" runat="server" Text="Label_编号"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">商户ID</td>
                <td>
                    <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">银行卡卡号</td>
                <td>
                    <asp:Label ID="Label_银行卡卡号" runat="server" Text="银行卡卡号"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">银行名称</td>
                <td>
                    <asp:Label ID="Label_银行名称" runat="server" Text="银行名称"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">银行卡主姓名</td>
                <td>
                    <asp:Label ID="Label_银行卡主姓名" runat="server" Text="银行卡主姓名"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                    <asp:Button ID="Button_操作更新传出" runat="server" Text="删除这张银行卡" class="btn btn-info btn-fw" OnClick="Button_操作更新传出_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

</div>

</asp:Content>
