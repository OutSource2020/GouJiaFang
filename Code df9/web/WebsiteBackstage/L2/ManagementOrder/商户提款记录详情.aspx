<%@ Page Title="商户提款记录详情" Language="C#" MasterPageFile="~/WebsiteBackstage/L2/SiteTemplateBackstageL2.Master" AutoEventWireup="true" CodeBehind="商户提款记录详情.aspx.cs" Inherits="web1.WebsiteBackstage.L2.ManagementOrder.商户提款记录详情" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

    <div>
        <p>基本信息</p>
        <table style="width: 100%">
                <tr>
                    <td style="width: 20%">订单号</td>
                    <td>
                        <asp:Label ID="Label_订单号" runat="server" Text="Label_订单号"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户ID</td>
                    <td>
                        <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">商户名称</td>
                    <td>
                        <asp:Label ID="Label_商户名称" runat="server" Text="Label_商户名称"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">类型</td>
                    <td>
                        <asp:Label ID="Label_类型" runat="server" Text="Label_类型"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">出款银行卡名称</td>
                    <td>
                        <asp:Label ID="Label_出款银行卡名称" runat="server" Text="Label_出款银行卡名称"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">出款银行卡卡号</td>
                    <td>
                        <asp:Label ID="Label_出款银行卡卡号" runat="server" Text="Label_出款银行卡卡号"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">交易方卡号</td>
                    <td>
                        <asp:Label ID="Label_交易方卡号" runat="server" Text="Label_交易方卡号"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">交易方姓名</td>
                    <td>
                        <asp:Label ID="Label_交易方姓名" runat="server" Text="Label_交易方姓名"></asp:Label>
                    </td>
                </tr>
            </table>

    </div>
    <div>
        <p>收款人信息</p>
        <table style="width: 100%">
                <tr>
                    <td style="width: 20%">交易方银行</td>
                    <td>
                        <asp:Label ID="Label_交易方银行" runat="server" Text="Label_交易方银行"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">交易金额</td>
                    <td>
                        <asp:Label ID="Label_交易金额" runat="server" Text="Label_交易金额"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">手续费</td>
                    <td>
                        <asp:Label ID="Label_手续费" runat="server" Text="Label_手续费"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">创建方式</td>
                    <td>
                        <asp:Label ID="Label_创建方式" runat="server" Text="Label_创建方式"></asp:Label>
                    </td>
                </tr>
            </table>

    </div>
    <div>
        <p>交易信息</p>
        <table style="width: 100%">
                <tr>
                    <td style="width: 20%">备注商户写</td>
                    <td>
                        <asp:Label ID="Label_备注商户写" runat="server" Text="Label_备注商户写"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">备注管理写</td>
                    <td>
                        <asp:Label ID="Label_备注管理写" runat="server" Text="Label_备注管理写"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">状态</td>
                    <td>
                        <asp:Label ID="Label_状态" runat="server" Text="Label_状态"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">时间创建</td>
                    <td>
                        <asp:Label ID="Label_时间创建" runat="server" Text="Label_时间创建"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">时间完成</td>
                    <td>
                        <asp:Label ID="Label_时间完成" runat="server" Text="Label_时间完成"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">时间修改</td>
                    <td>
                        <asp:Label ID="Label_时间修改" runat="server" Text="Label_时间修改"></asp:Label>
                    </td>
                </tr>
            </table>
    </div>

    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />


</div>

</asp:Content>