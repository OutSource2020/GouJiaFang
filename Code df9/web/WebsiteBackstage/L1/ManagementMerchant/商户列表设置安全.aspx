<%@ Page Title="商户列表设置安全" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表设置安全.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表设置安全" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">


    <div>

    <table id="设置安全" class="auto-style1" >
            <tr>
                <td colspan="2">
                    <h3>安全设置</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">设置商户密码</td>
                <td>
                    <asp:Button ID="Button_发送当前账号密码和支付密码" runat="server" class="btn btn-info btn-fw" OnClick="Button_设置账号密码和支付密码_Click" Text="设置账号密码和支付密码" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定邮箱</td>
                <td>
                    <asp:Button ID="Button_设置绑定邮箱" runat="server" Text="设置绑定邮箱" class="btn btn-info btn-fw" OnClick="Button_设置绑定邮箱_Click" />
                    <asp:Label ID="Label_绑定邮箱" runat="server" Text="Label_绑定邮箱"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定手机</td>
                <td>
                    <asp:Button ID="Button_设置绑定手机" runat="server" Text="设置绑定手机" class="btn btn-info btn-fw" OnClick="Button_设置绑定手机_Click" />
                    <asp:Label ID="Label_绑定手机" runat="server" Text="Label_绑定手机"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">登入错误累计</td>
                <td>
                    <asp:Button ID="Button_清空登入错误累计" runat="server" Text="清空登入错误累计" class="btn btn-info btn-fw" OnClick="Button_清空登入错误累计_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    <asp:Label ID="Label_登入错误累计" runat="server" Text="Label_登入错误累计"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">支付错误累计</td>
                <td>
                    <asp:Button ID="Button_清空支付错误累计" runat="server" Text="清空支付错误累计" class="btn btn-info btn-fw" OnClick="Button_清空支付错误累计_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    <asp:Label ID="Label_支付错误累计" runat="server" Text="Label_支付错误累计"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">安全QR码(Google)</td>
                <td>
                    <asp:Button ID="Button_设置安全QR码" runat="server" Text="设置商户安全QR码" class="btn btn-info btn-fw" OnClick="Button_设置安全QR码_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
                    <tr>
                <td style="width: 30%">手动提款状态</td>
                <td>
                    <asp:Button ID="Button_手动提款状态" runat="server" Text="手动提款状态" class="btn btn-info btn-fw" OnClick="Button_手动提款状态_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
                    <tr>
                <td style="width: 30%">二步验证状态</td>
                <td>
                    <asp:Button ID="Button_二步验证状态" runat="server" Text="二步验证状态" class="btn btn-info btn-fw" OnClick="Button_二步验证状态_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
    </table>


        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                </td>
            </tr>
        </table>


</div>




</asp:Content>
