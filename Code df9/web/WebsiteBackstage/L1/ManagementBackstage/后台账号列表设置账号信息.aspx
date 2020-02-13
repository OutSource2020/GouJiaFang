<%@ Page Title="后台账号列表设置账号信息" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="后台账号列表设置账号信息.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBackstage.后台账号列表设置账号信息" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

     <table class="auto-style1">
        <tr>
            <td colspan="2">
                <h3>设置后台账号信息</h3>
            </td>
        </tr>
        <tr>
            <td style="width:  30%">
                后台ID</td>
            <td>
                <asp:Label ID="Label_后台ID" runat="server" Text="Label_后台ID"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:  30%">
                后台账号名称</td>
            <td>
                <asp:TextBox ID="TextBox_后台账号名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:  30%">
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
            <td style="width:  30%">
                时间最后登入</td>
            <td>
                <asp:Label ID="Label_时间最后登入" runat="server" Text="Label_时间最后登入"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:  30%">
                时间注册</td>
            <td>
                <asp:Label ID="Label_时间注册" runat="server" Text="Label_时间注册"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="width:  30%">
                验证密匙
            </td>
            <td>
                <asp:TextBox ID="TextBox_验证密匙" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>

        </table>

        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
        <asp:Button ID="Button_发出更新" runat="server" Text="发出更新" class="btn btn-info btn-fw"  OnClick="CustomerUpdate" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />



</div>

</asp:Content>