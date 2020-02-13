<%@ Page Title="后台账号列表设置QR" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="后台账号列表设置QR.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBackstage.后台账号列表设置QR" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

         <table class="auto-style1">
            <tr>
                <td>
                    <asp:Button ID="Button_重置生成新的" runat="server" Text="重置 生成新的(注意:此操作将重置后台账户的密匙 不可恢复)" class="btn btn-info btn-fw" OnClick="Button_重置生成新的_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    <asp:Button ID="Button_读取账号生成二维码" runat="server" Text="读取账号内密匙生成二维码" class="btn btn-info btn-fw" OnClick="Button_读取账号生成二维码_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
            <tr>
                <td>
                    <%--<strong>Account Secret Key (randomly generated):</strong> <asp:Label runat="server" ID="lblSecretKey"></asp:Label>--%>
                    <strong>设置二维码：</strong><br />
        <asp:Image ID="imgQrCode" runat="server" /><br />
        <br />
        <strong>手动设置代码： </strong> <asp:Label runat="server" ID="lblManualSetupCode"></asp:Label>
        <br />

                </td>
            </tr>
            <tr>
                <td>
                    可以在这测试验证码： 
                    <asp:TextBox runat="server" ID="txtCode" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox> 
                    <asp:Button ID="Button_验证" runat="server" Text="验证代码！" class="btn btn-info btn-fw" OnClick="Button_验证_Click" />
                    <asp:Label runat="server" Font-Bold="true" ID="lblValidationResult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                </td>
            </tr>
        </table>
        
    </div>

</asp:Content>
