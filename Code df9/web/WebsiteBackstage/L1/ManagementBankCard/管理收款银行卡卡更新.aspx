<%@ Page Title="管理收款银行卡卡更新" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理收款银行卡卡更新.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理收款银行卡卡更新" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >
    <table class="auto-style1">
        <tr>
            <td style="width: 30%">
                编号</td>
            <td>
                <asp:Label ID="lblCustomerID" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款收款银行卡名称</td>
            <td>
                <asp:Label ID="Label_收款银行卡名称" runat="server" Text="Label_收款收款银行卡名称"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行卡卡号</td>
            <td>
                <asp:Label ID="Label_收款银行卡卡号" runat="server" Text="Label_收款银行卡卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行名称</td>
            <td>
                <asp:TextBox ID="TextBox_收款银行名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:Button ID="Button_识别收款银行名称" runat="server" Text="识别收款银行名称" OnClick="Button_识别收款银行名称_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行卡余额</td>
            <td>
                <asp:Label ID="Label_收款银行卡余额" runat="server" Text="Label_收款银行卡余额"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行卡主姓名
            </td>
            <td>
                <asp:TextBox ID="TextBox_收款银行卡主姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行卡主电话
            </td>
            <td>
                <asp:TextBox ID="TextBox_收款银行卡主电话" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                显示标记(在商户端充值页面显示)
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_显示标记" runat="server">
                    <asp:ListItem Text="未选择" Value="未选择" />
                    <asp:ListItem Text="启用" Value="启用" />
                    <asp:ListItem Text="停用" Value="停用" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                状态</td>
            <td>
                <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                    <asp:ListItem Text="未选择" Value="未选择" />
                    <asp:ListItem Text="启用" Value="启用" />
                    <asp:ListItem Text="停用" Value="停用" />
                </asp:DropDownList>
            </td>
        </tr>
                        <tr>
            <td style="width: 30%">用作手续卡</td>
            <td>
                            <asp:DropDownList ID="DropDownList_手续卡" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
        </tr>
                        <tr>
            <td style="width: 30%">用作金额卡</td>
            <td>
                            <asp:DropDownList ID="DropDownList_金额卡" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_更新作业" runat="server" Text="更新作业" class="btn btn-info btn-fw" OnClick="Button_更新作业_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                &nbsp;</td>
        </tr>
    </table>
</div>

</asp:Content>
