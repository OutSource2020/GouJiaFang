<%@ Page Title="商户列表设置费率" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表设置费率.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表设置费率" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >
     <table class="auto-style1">
        <tr>
            <td colspan="2">
                <h3>设置商户费率</h3>
            </td>
        </tr>
        <tr>
            <td style="width: 286px">
                商户ID</td>
            <td>
                <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                提款最低单笔金额</td>
            <td >
                <asp:TextBox ID="TextBox_提款最低单笔金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                提款最高单笔金额</td>
            <td >
                <asp:TextBox ID="TextBox_提款最高单笔金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                充值最低手续费</td>
            <td >
                <asp:TextBox ID="TextBox_充值最低手续费" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                充值最低余额</td>
            <td >
                <asp:TextBox ID="TextBox_充值最低余额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                手续费比率</td>
            <td >
                <asp:TextBox ID="TextBox_手续费比率" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                单笔手续费</td>
            <td >
                <asp:TextBox ID="TextBox_单笔手续费" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第一阶梯起</td>
            <td >
                <asp:TextBox ID="TextBox_第一阶梯起" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第一阶梯止</td>
            <td >
                <asp:TextBox ID="TextBox_第一阶梯止" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第一阶梯百分比</td>
            <td >
                <asp:TextBox ID="TextBox_第一阶梯百分比" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
                    <td style=" width: 30%;">
                第二阶梯起</td>
            <td >
                <asp:TextBox ID="TextBox_第二阶梯起" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第二阶梯止</td>
            <td >
                <asp:TextBox ID="TextBox_第二阶梯止" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第二阶梯百分比</td>
            <td >
                <asp:TextBox ID="TextBox_第二阶梯百分比" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
                    <td style=" width: 30%;">
                第三阶梯起</td>
            <td >
                <asp:TextBox ID="TextBox_第三阶梯起" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第三阶梯止</td>
            <td >
                <asp:TextBox ID="TextBox_第三阶梯止" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第三梯百分比</td>
            <td >
                <asp:TextBox ID="TextBox_第三梯百分比" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
                    <td style=" width: 30%;">
                第四阶梯起</td>
            <td >
                <asp:TextBox ID="TextBox_第四阶梯起" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第四阶梯止</td>
            <td >
                <asp:TextBox ID="TextBox_第四阶梯止" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style=" width: 30%;">
                第四阶梯百分比</td>
            <td >
                <asp:TextBox ID="TextBox_第四阶梯百分比" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        </table>

        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
        <asp:Button ID="Button_发出更新" runat="server" Text="发出更新" class="btn btn-info btn-fw" OnClick="CustomerUpdate" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />


</div>

</asp:Content>