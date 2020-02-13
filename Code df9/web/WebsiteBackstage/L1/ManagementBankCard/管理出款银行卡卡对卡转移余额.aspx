<%@ Page Title="管理出款银行卡卡对卡转移余额" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理出款银行卡卡对卡转移余额.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理出款银行卡卡对卡转移余额" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >
     <table class="auto-style1">
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td>注意:<ul>
                <li>金额不能大于银行卡A的目前余额</li>
                </ul>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">A 银行卡:</td>
            <td>
                <asp:DropDownList ID="DropDownList_银行卡转移余额" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">B 银行卡:</td>
            <td>
                <asp:DropDownList ID="DropDownList_银行卡转移目标" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">金额:</td>
            <td>
                <asp:TextBox ID="TextBox_金额" runat="server" onKeyPress="if((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46 || /\.\d\d$/.test(value))event.returnValue=false"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td>
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_操作转移开始" runat="server" class="btn btn-info btn-fw" OnClick="Button_操作转移开始_Click" Text="操作转移开始" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

</div>

</asp:Content>
