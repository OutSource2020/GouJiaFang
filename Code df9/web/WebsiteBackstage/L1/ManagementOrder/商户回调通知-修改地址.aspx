<%@ Page Title="商户列表充值手续费" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户回调通知-修改地址.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementOrder.商户回调通知_修改地址" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >





     <table class="auto-style1">
        <tr>
            <td style="width: 30%">目标商户账号</td>
            <td>
                <asp:Label ID="Label_目标商户账号" runat="server" Text="目标商户账号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">原始地址</td>
            <td>
                <asp:Label ID="Label_原始地址" runat="server" Text="原始地址"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">地址</td>
            <td>
                <asp:TextBox ID="TextBox_地址" runat="server" Width="500px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">输入管理员密码</td>
            <td>
                <asp:TextBox ID="TextBox_管理员密码" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_操作更新" runat="server" class="btn btn-info btn-fw" OnClick="Button_操作更新_Click" Text="操作更新" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</div>

</asp:Content>
