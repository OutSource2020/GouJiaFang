<%@ Page Title="后台账号列表设置" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="后台账号列表设置.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBackstage.后台账号列表设置" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >


        <table id="设置信息" class="auto-style1" >
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 30%">后台ID</td>
                <td>
                    <asp:Label ID="Label_后台ID" runat="server" Text="Label_后台ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">后台密码</td>
                <td>
                    <asp:Button ID="Button_设置密码" runat="server" Text="设置密码" class="btn btn-info btn-fw" OnClick="Button_设置密码_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">设置验证器key</td>
                <td>
                    <asp:Button ID="Button_设置验证器" runat="server" Text="设置验证器" class="btn btn-info btn-fw" OnClick="Button_设置验证器_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    <asp:Button ID="Button_设置账号信息" runat="server" Text="设置账号信息" class="btn btn-info btn-fw" OnClick="Button_设置账号信息_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">后台名称</td>
                <td>
                    <asp:Label ID="Label_后台名称" runat="server" Text="Label_后台名称"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">状态</td>
                <td>
                    <asp:Label ID="Label_状态" runat="server" Text="Label_状态"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">时间最后登入</td>
                <td>
                    <asp:Label ID="Label_时间最后登入" runat="server" Text="Label_时间最后登入"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">时间注册
                </td>
                <td>
                    <asp:Label ID="Label_时间注册" runat="server" Text="Label_时间注册"></asp:Label>
                </td>
            </tr>
                        
        </table>

    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />

    </div>


</asp:Content>
