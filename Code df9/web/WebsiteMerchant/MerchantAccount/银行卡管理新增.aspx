<%@ Page Title="银行卡管理新增" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="银行卡管理新增.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.银行卡管理新增" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1">


             <table class="auto-style1">
                <tr>
                    <td style="width: 30%">银行卡卡号</td>
                    <td><asp:TextBox ID="TextBox_商户银行卡卡号" runat="server"  Width="160px" MaxLength="30" OnTextChanged="TextBox_交易方卡号_TextChanged" AutoPostBack="True"  ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">银行名称</td>
                    <td><asp:TextBox ID="TextBox_商户银行名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">银行卡主姓名</td>
                    <td>
                        <asp:TextBox ID="TextBox_商户银行卡主姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                        <asp:Button ID="Button_银行卡提交" runat="server" Text="银行卡提交" class="btn btn-info btn-fw" OnClick="Button_银行卡提交_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    </td>
                </tr>
                </table>
        </div>

</asp:Content>
