<%@ Page Title="商户列表设置安全绑定手机" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表设置安全绑定手机.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表设置安全绑定手机" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">
    
    <div class="auto-style1" >

        <table class="auto-style1">
            <tr>
                <td style="width:  30%">
                商户ID
                </td>
                <td>
                    <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:  30%">
                绑定手机
                </td>
                <td>
                    <asp:TextBox ID="TextBox_绑定手机" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
        </table>

        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
        <asp:Button ID="Button_发出更新" runat="server" Text="发出更新" class="btn btn-info btn-fw"  OnClick="CustomerUpdate" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />

</div>

</asp:Content>