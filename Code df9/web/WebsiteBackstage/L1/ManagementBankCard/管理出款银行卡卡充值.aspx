<%@ Page Title="管理出款银行卡卡充值" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理出款银行卡卡充值.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理出款银行卡卡充值" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div  class="auto-style1" >
     <table class="auto-style1">
        <tr>
            <td style="width: 30%">目标名称</td>
            <td>
                <asp:Label ID="Label_目标目标名称" runat="server" Text="Label_目标目标名称"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">目标银行卡卡号</td>
            <td>
                <asp:Label ID="Label_目标银行卡卡号" runat="server" Text="Label_目标银行卡卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">目标银行卡充值金额</td>
            <td>
                <asp:TextBox ID="TextBox_目标银行卡充值金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">备注</td>
            <td>
                <asp:TextBox ID="TextBox_备注" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_确认充值" runat="server" Text="确认充值" class="btn btn-info btn-fw" OnClick="Button_确认充值_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

</div>

</asp:Content>
