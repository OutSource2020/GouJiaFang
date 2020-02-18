<%@ Page Title="商户通知设置" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户通知设置.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户通知设置" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div  class="auto-style1" >
    <table style="width: 100%">
        <tr>
            <td style="width:  30%">短信通知</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="Button1" runat="server" class="btn btn-info btn-fw" OnClick="Button1_Click" Text="启用" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button2" runat="server" class="btn btn-info btn-fw" OnClick="Button2_Click" Text="关闭" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
        <tr>
            <td style="width:  30%">提款验证码验证</td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <asp:Button ID="Button3" runat="server" class="btn btn-info btn-fw" OnClick="Button3_Click" Text="启用" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button4" runat="server" class="btn btn-info btn-fw" OnClick="Button4_Click" Text="关闭" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

</div>

</asp:Content>
