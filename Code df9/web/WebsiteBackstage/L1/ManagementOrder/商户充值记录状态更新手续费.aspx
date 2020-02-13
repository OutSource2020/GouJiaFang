<%@ Page Title="商户充值记录状态更新手续费" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户充值记录状态更新手续费.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementOrder.商户充值记录状态更新手续费" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

     <table class="auto-style1">
        <tr>
            <td style="width: 30%">
                <asp:Label Text="订单号" runat="server" />
            </td>
            <td>
                <asp:Label ID="Label_订单号" runat="server" Text="Label_订单号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户ID</td>
            <td>
                <asp:Label ID="Label_商户ID" runat="server" Text="Label_交易方"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户银行卡卡号</td>
            <td>
                <asp:Label ID="Label_商户银行卡卡号" runat="server" Text="Label_交易方卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户充值目标卡号</td>
            <td>
                 <asp:Label ID="Label_商户充值目标卡号" runat="server" Text="Label_商户充值目标卡号"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">充值类型</td>
            <td>
                <asp:Label ID="Label_充值类型" runat="server" Text="Label_交易方银行"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">充值金额</td>
            <td>
                <asp:Label ID="Label_充值金额" runat="server" Text="Label_金额"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                备注后台
            </td>
            <td>
                <asp:TextBox ID="TextBox_备注后台" runat="server"  Width="160px" MaxLength="30" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                收款银行卡
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_选择银行卡" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                设置状态
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
            <td colspan="2">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_变更状态" runat="server" Text="变更状态" class="btn btn-info btn-fw" OnClick="Button_变更状态_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
        
    </table>


</div>

</asp:Content>