<%@ Page Title="管理出款银行卡卡新增" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理出款银行卡卡新增.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理出款银行卡卡新增" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

<div>

    <h3>新增出款银行卡</h3>

        <table class="auto-style1">
            <tr>
                <td style="width: 30%">出款银行卡名称</td>
                <td>
                            <asp:TextBox ID="TextBox_出款银行卡名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行卡卡号</td>
                <td>
                    <asp:TextBox ID="TextBox_出款银行卡卡号" runat="server"  Width="160px" MaxLength="30" OnTextChanged="TextBox_交易方卡号_TextChanged" AutoPostBack="True" ></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行名称</td>
                <td><asp:TextBox ID="TextBox_出款银行名称" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行卡主姓名</td>
                <td>
                    <asp:TextBox ID="TextBox_出款银行卡主姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行卡主电话</td>
                <td>
                    <asp:TextBox ID="TextBox_出款银行卡主电话" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行卡每日限额</td>
                <td>
                    <asp:TextBox ID="TextBox_出款银行卡每日限额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">出款银行卡最小交易金额</td>
                <td>
                    <asp:TextBox ID="TextBox_出款银行卡最小交易金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">显示标记(在商户端充值页面显示)</td>
                <td>
                            <asp:DropDownList ID="DropDownList_显示标记" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">状态</td>
                <td>
                            <asp:DropDownList ID="DropDownList_出款银行卡状态" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
            </tr>
                            <tr>
            <td style="width: 30%">用作手续卡</td>
            <td>
                            <asp:DropDownList ID="DropDownList_手续卡" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
        </tr>
                        <tr>
            <td style="width: 30%">用作金额卡</td>
            <td>
                            <asp:DropDownList ID="DropDownList_金额卡" runat="server">
                                <asp:ListItem>启用</asp:ListItem>
                                <asp:ListItem>停用</asp:ListItem>
                            </asp:DropDownList>
                        </td>
        </tr>
            <tr>
                <td colspan="2">
                            <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click1" />
                            <asp:Button ID="Button_操作新增出款银行卡" runat="server" Text="操作 新增出款银行卡" class="btn btn-info btn-fw" OnClick="Button_操作新增出款银行卡_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                        </td>
            </tr>
        </table>


    </div>




</div>

</asp:Content>
