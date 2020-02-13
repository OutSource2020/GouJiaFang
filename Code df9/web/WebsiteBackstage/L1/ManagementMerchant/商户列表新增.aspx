<%@ Page Title="商户列表新增" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表新增.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表新增" %>

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
                    <td style="width: 20%">商户ID</td>
                    <td><asp:TextBox ID="TextBox_账号信息_商户ID" runat="server" Width="160px" MaxLength="10" Enabled="False"></asp:TextBox>
                        <asp:Button ID="Button_随机生成商户ID" runat="server" class="btn btn-info btn-fw" OnClick="Button_随机生成商户ID_Click" Text="随机生成商户ID" />
                        <asp:CheckBox ID="CheckBox_手动设置商户ID" runat="server" Text="手动设置(建议随机生成)" OnCheckedChanged="CheckBox_手动设置商户ID_CheckedChanged" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">手续费收款方式</td>
                    <td>
                        <asp:RadioButton ID="RadioButton_手续费收款方式_预充值" runat="server" GroupName="danxuan1hao" Text="预充值" type="radio" value="预充值" Checked="True" />
                        <asp:RadioButton ID="RadioButton_手续费收款方式_后充值" runat="server" GroupName="danxuan1hao" Text="后充值" type="radio" value="后充值"/>

                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">状态</td>
                    <td>
                        <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                        <asp:ListItem Text="未选择" Value="未选择" />
                        <asp:ListItem Text="启用" Value="启用" />
                        <asp:ListItem Text="停用" Value="停用" />
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>

             <table class="auto-style1">
                <tr>
                    <td style="width: 20%">联系人信息</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">姓名(中英文)</td>
                    <td>
                        <asp:TextBox ID="TextBox_联系人信息_姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">联系电话</td>
                    <td>
                        <asp:TextBox ID="TextBox_联系人信息_联系电话" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">邮箱(建议使用 Google Gmail)</td>
                    <td>
                        <asp:TextBox ID="TextBox_联系人信息_邮箱" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
            </table>

             <table class="auto-style1">
                <tr>
                    <td style="width: 20%">费率设置</td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%">商户手续费比率</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_手续费比率" runat="server" Width="160px" MaxLength="30">0.1</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">单笔手续费</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_单笔手续费" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户充值-充值最低手续费</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_充值最低手续费" runat="server"  Width="160px" MaxLength="30">500</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户充值-充值最低余额</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_充值最低余额" runat="server"  Width="160px" MaxLength="30">10000</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户提款-提款最低单笔金额</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_提款最低单笔金额" runat="server" Width="160px" MaxLength="30">100</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户提款-提款最高单笔金额</td>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox_提款最高单笔金额" runat="server" Width="160px" MaxLength="30">50000</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">第一阶梯</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="TextBox_第一阶梯起" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>-
                        <asp:TextBox ID="TextBox_第一阶梯止" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_第一阶梯百分比" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">第二阶梯</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="TextBox_第二阶梯起" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>-
                        <asp:TextBox ID="TextBox_第二阶梯止" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_第二阶梯百分比" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">第三阶梯</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="TextBox_第三阶梯起" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>-
                        <asp:TextBox ID="TextBox_第三阶梯止" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_第三阶梯百分比" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">第四阶梯</td>
                    <td style="width: 30%">
                        <asp:TextBox ID="TextBox_第四阶梯起" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>-
                        <asp:TextBox ID="TextBox_第四阶梯止" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_第四阶梯百分比" runat="server"  Width="160px" MaxLength="30">0</asp:TextBox>
                    </td>
                </tr>
            </table>

        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
            <asp:Button ID="Button_新增商户" runat="server" Text="新增商户" class="btn btn-info btn-fw" OnClick="Button_新增商户_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
        </div>


</asp:Content>
