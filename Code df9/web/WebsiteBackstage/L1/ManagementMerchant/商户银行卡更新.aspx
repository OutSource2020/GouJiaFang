<%@ Page Title="商户银行卡更新" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户银行卡更新.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户银行卡更新" %>

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
                <td style="width:  30%">商户银行卡卡号</td>
                <td>
                    <asp:Label ID="Label_商户银行卡卡号" runat="server" Text="Label_商户银行卡卡号"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">商户银行名称</td>
                <td>
                    <asp:TextBox ID="TextBox_商户银行名称" runat="server"></asp:TextBox>
                    <asp:Button ID="Button_识别商户银行名称" runat="server" Text="识别商户银行名称" OnClick="Button_识别商户银行名称_Click" />
                </td>
            </tr>
            <tr>
                <td style="width:  30%">商户银行卡主姓名</td>
                <td>
                    <asp:Label ID="Label_商户银行卡主姓名" runat="server" Text="Label_商户银行卡主姓名"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">状态</td>
                <td>
                <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                    <asp:ListItem Text="待审核" Value="待审核" />
                    <asp:ListItem Text="启用" Value="启用" />
                    <asp:ListItem Text="停用" Value="停用" />
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                    <asp:Button ID="Button_操作更新传出" runat="server" Text="操作更新传出" class="btn btn-info btn-fw" OnClick="Button_操作更新传出_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

</div>

</asp:Content>
