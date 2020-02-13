<%@ Page Title="代理列表新增" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="代理列表L2新增.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementAgent.SettingL2.代理列表L2新增" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1">

            <table class="auto-style1">
                <tr>
                    <td style="width: 30%">代理ID</td>
                    <td><asp:TextBox ID="TextBox_代理ID" runat="server"  Width="160px" MaxLength="30" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" Enabled="False" ></asp:TextBox>
                        
                        <asp:Button ID="Button_随机生成代理ID" runat="server" class="btn btn-info btn-fw" OnClick="Button_随机生成代理ID_Click" Text="随机生成代理ID" />
                        <asp:CheckBox ID="CheckBox_手动设置商户ID" runat="server" Text="手动设置(建议随机生成)" OnCheckedChanged="CheckBox_手动设置商户ID_CheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">代理名称</td>
                    <td><asp:TextBox ID="TextBox_代理名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
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
