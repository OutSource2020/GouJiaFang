<%@ Page Title="后台账号列表新增" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="后台账号列表新增.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBackstage.后台账号列表新增" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

            <table style="width: 100%">
                <tr>
                    <td style="width: 20%">账号信息</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">后台账号ID</td>
                    <td><asp:TextBox ID="TextBox_账号信息_后台账号ID" runat="server" Width="160px" MaxLength="10" Enabled="False"></asp:TextBox>
                        <asp:Button ID="Button_随机生成后台账号ID" runat="server" class="btn btn-info btn-fw" OnClick="Button_随机生成后台账号ID_Click" Text="随机生成后台账号ID" />
                        <asp:CheckBox ID="CheckBox_手动设置后台账号ID" runat="server" Text="手动设置" OnCheckedChanged="CheckBox_手动设置后台账号ID_CheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">后台账号密码</td>
                    <td>
                        <asp:TextBox ID="TextBox_后台账号密码" runat="server" Width="160px" MaxLength="10" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">账号名称</td>
                    <td>
                        <asp:TextBox ID="TextBox_账号名称" runat="server" Width="160px" MaxLength="10" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">状态</td>
                    <td>
                        <asp:DropDownList ID="DropDownList_状态" runat="server">
                        <asp:ListItem Text="未选择" Value="未选择" />
                        <asp:ListItem Text="启用" Value="启用" />
                        <asp:ListItem Text="停用" Value="停用" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:  20%">
                        验证密匙
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_验证密匙" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                </table>

        
    <asp:Button ID="Button_返回" runat="server" Text="取消" class="btn btn-info btn-fw" OnClick="Button_返回_Click" /> 
    <asp:Button ID="Button_新增后台账号" runat="server" Text="新增后台账号" class="btn btn-info btn-fw" OnClick="Button_新增后台账号_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
        
</div>


</asp:Content>
