<%@ Page Title="商户安全白名单新增" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户安全白名单新增.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户安全白名单新增" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1">

            <table class="auto-style1">
                <tr>
                    <td style="width: 30%">商户ID</td>
                    <td>
                        <asp:DropDownList ID="DropDownList_商户ID" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">商户白名单IP</td>
                    <td><asp:TextBox ID="TextBox_商户白名单IP" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                        (必须一致,注意检查是否有多处符号或者空格)</td>
                </tr>
                <tr>
                    <td style="width: 30%">商户白名单备注</td>
                    <td><asp:TextBox ID="TextBox_商户白名单备注" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">状态</td>
                    <td>
            <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                    <asp:ListItem Text="未选择" Value="未选择" />
                    <asp:ListItem Text="启用" Value="启用" />
                    <asp:ListItem Text="停用" Value="停用" />
                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                        <asp:Button ID="Button_创建提交" runat="server" Text="创建提交" class="btn btn-info btn-fw" OnClick="Button_创建提交_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />


                    </td>
                </tr>
            </table>


</div>

</asp:Content>
