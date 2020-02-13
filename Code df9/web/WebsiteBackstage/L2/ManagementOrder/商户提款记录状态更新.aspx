<%@ Page Title="商户提款记录状态更新" Language="C#" MasterPageFile="~/WebsiteBackstage/L2/SiteTemplateBackstageL2.Master" AutoEventWireup="true" CodeBehind="商户提款记录状态更新.aspx.cs" Inherits="web1.WebsiteBackstage.L2.ManagementOrder.商户提款记录状态更新" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >
     <table class="auto-style1">
        <tr>
            <td style="width: 30%">
                商户ID</td>
            <td>
                <asp:Label ID="Label_商户ID" runat="server" Text="Labe_商户ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Label Text="订单号" runat="server" />
            </td>
            <td>
                <asp:Label ID="Label_订单号" runat="server" Text="Label_订单号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">交易方</td>
            <td>
                <asp:Label ID="Label_交易方" runat="server" Text="Label_交易方"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">交易方卡号</td>
            <td>
                <asp:Label ID="Label_交易方卡号" runat="server" Text="Label_交易方卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">交易方银行</td>
            <td>
                <asp:Label ID="Label_交易方银行" runat="server" Text="Label_交易方银行"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">金额</td>
            <td>
                <asp:Label ID="Label_金额" runat="server" Text="Label_金额"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Label Text="状态" runat="server" />
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                    <asp:ListItem Text="未选择" Value="未选择" />
                    <asp:ListItem Text="成功" Value="成功" />
                    <asp:ListItem Text="失败" Value="失败" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                银行卡</td>
            <td>
                <asp:DropDownList ID="DropDownList_选择银行卡" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Label Text="备注" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="TextBox_备注" runat="server" Width="160px" MaxLength="30" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_变更状态" runat="server" Text="变更状态" class="btn btn-info btn-fw" OnClick="Button_变更状态_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button_启动冲正" runat="server" Text="启动冲正" class="btn btn-info btn-fw" OnClick="Button_启动冲正_Click" />
            </td>
        </tr>

    </table>


</div>

</asp:Content>