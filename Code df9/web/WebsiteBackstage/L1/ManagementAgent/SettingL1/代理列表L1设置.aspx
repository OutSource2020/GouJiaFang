<%@ Page Title="代理列表L1设置" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="代理列表L1设置.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementAgent.SettingL1.代理列表L1设置" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                </td>
            </tr>
        </table>

        <table id="设置信息" class="auto-style1" >
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    <asp:Button ID="Button_设置代理信息" runat="server" Text="设置代理信息" class="btn btn-info btn-fw" OnClick="Button_设置代理信息_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">代理ID</td>
                <td>
                    <asp:Label ID="Label_代理ID" runat="server" Text="Label_代理ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">代理名称</td>
                <td>
                    <asp:Label ID="Label_代理名称" runat="server" Text="Label_代理名称"></asp:Label>
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
                <td style="width: 30%">时间注册</td>
                <td>
                    <asp:Label ID="Label_时间注册" runat="server" Text="Label_时间注册"></asp:Label>
                </td>
            </tr>
                        
        </table>

    </div>

    <table id="设置安全" class="auto-style1" >
            <tr>
                <td style="width: 30%">安全设置</td>
                <td>
                    <asp:Button ID="Button_设置代理账户安全" runat="server" Text="设置代理账户安全" class="btn btn-info btn-fw" OnClick="Button_设置代理账户安全_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定邮箱</td>
                <td>
                    <asp:Label ID="Label_绑定邮箱" runat="server" Text="Label_绑定邮箱"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定手机</td>
                <td>
                    <asp:Label ID="Label_绑定手机" runat="server" Text="Label_绑定手机"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">登入错误累计</td>
                <td>
                    <asp:Label ID="Label_登入错误累计" runat="server" Text="Label_登入错误累计"></asp:Label>
                </td>
            </tr>
            </table>

</div>

</asp:Content>
