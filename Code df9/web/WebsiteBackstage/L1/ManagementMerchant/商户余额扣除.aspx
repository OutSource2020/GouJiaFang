<%@ Page Title="商户余额扣除" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户余额扣除.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户余额扣除" %>

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
            <td style="width: 30%">金额</td>
            <td>
                <asp:TextBox ID="TextBox_扣除金额" runat="server" onKeyPress="if((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46 || /\.\d\d$/.test(value))event.returnValue=false"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_操作扣除" runat="server" class="btn btn-info btn-fw" OnClick="Button_操作扣除_Click" Text="操作扣除" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>

</div>

</asp:Content>
