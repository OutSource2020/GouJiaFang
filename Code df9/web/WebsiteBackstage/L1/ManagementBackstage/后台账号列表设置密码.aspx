<%@ Page Title="后台账号列表设置密码" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="后台账号列表设置密码.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBackstage.后台账号列表设置密码" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

    <table class="auto-style1">
            
        <tr> 
            <td style="width: 30%">后台ID</td>
            <td>
                <asp:Label ID="Label_后台ID" runat="server" Text="Label_后台ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">新密码</td>
            <td>
                <asp:TextBox ID="TextBox_新密码1" runat="server" Width="160px" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">新密码-再次输入</td>
            <td>
                <asp:TextBox ID="TextBox_新密码2" runat="server" Width="160px" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">验证密匙</td>
            <td>
                <asp:TextBox ID="TextBox_key" runat="server" Width="160px" MaxLength="10" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_尝试修改" runat="server" Text="尝试修改" class="btn btn-info btn-fw" OnClick="Button_尝试修改_Click" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>

</div>


</asp:Content>