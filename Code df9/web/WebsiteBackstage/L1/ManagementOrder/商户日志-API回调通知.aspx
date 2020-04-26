<%@ Page Title="商户日志-API回调通知" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户日志-API回调通知.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementOrder.商户日志_API回调通知" %>
<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">
</asp:Content>
<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">
    <div id="筛选类">
        <div>
            <table class="auto-style1">
                <tr>
                    <td colspan="2">选择时间筛选
                <asp:RadioButton ID="RadioButton_时间今天" runat="server" GroupName="DenXiGan" Text="今天" type="radio" value="今天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioButton_时间昨天" runat="server" GroupName="DenXiGan" Text="昨天" type="radio" value="昨天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_时间7天" runat="server" GroupName="DenXiGan" Text="7天" type="radio" value="7天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_时间本周" runat="server" GroupName="DenXiGan" Text="本周" type="radio" value="本周" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_时间本月" runat="server" GroupName="DenXiGan" Text="本月" type="radio" value="本月" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="_设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间筛选_CheckedChanged" />
                        开始时间<asp:TextBox ID="TextBox_开始时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                        结束时间<asp:TextBox ID="TextBox_结束时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                        (格式: 年-月-日)
                    </td>
                </tr>
                <tr>
                    <td>订单状态筛选:
                <asp:RadioButton ID="RadioButton_状态全部" runat="server" GroupName="DenHinTai" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_状态筛选_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioButton_状态待处理" runat="server" GroupName="DenHinTai" Text="待处理" type="radio" value="待处理" AutoPostBack="True" OnCheckedChanged="RadioButton_状态筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_状态成功" runat="server" GroupName="DenHinTai" Text="成功" type="radio" value="成功" AutoPostBack="True" OnCheckedChanged="RadioButton_状态筛选_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_状态失败" runat="server" GroupName="DenHinTai" Text="失败" type="radio" value="失败" AutoPostBack="True" OnCheckedChanged="RadioButton_状态筛选_CheckedChanged" />
                    </td>
                    <td>按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                    <asp:ListItem Value="订单号">订单号</asp:ListItem>
                    <asp:ListItem Value="商户API订单号">商户API订单号</asp:ListItem>
                </asp:DropDownList>
                        <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button_查找" runat="server" Text="查找" class="btn btn-info btn-fw" OnClick="Button_查找_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%--=========要刷新的部分 开始=========--%>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Text="载入时间"></asp:Label>
                <div id="gridTable">
                    <asp:GridView
                        ID="GridView1"
                        runat="server"
                        class="auto-style1"
                        AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="true"
                        PageSize="50" OnRowCommand="GridView1_RowCommand"
                        >
                        <Columns>
                            <asp:BoundField DataField="订单号" HeaderText="订单号" />
                            <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
                            <asp:BoundField DataField="商户API订单号" HeaderText="商户API订单号" />
                            <asp:BoundField DataField="状态" HeaderText="订单状态" />
                            <asp:BoundField DataField="回调状态" HeaderText="回调状态" />
                            <asp:BoundField DataField="回调地址" HeaderText="回调地址" />
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:Button Text="修改回调地址" runat="server" CommandName="修改回调地址"  class="btn btn-info btn-fw" CommandArgument="<%# Container.DataItemIndex %>" />
                                    <asp:Button Text="测试发送" runat="server" CommandName="测试发送"  class="btn btn-info btn-fw" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Record Available 沒有可用記錄</EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--=========要刷新的部分 结束=========--%>
    <div id="选择每页行数">
        <asp:DropDownList ID="DropDownList_选择每页行数" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_选择每页行数_SelectedIndexChanged">
            <%--   <asp:ListItem>50</asp:ListItem>--%>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
            <asp:ListItem>200</asp:ListItem>
            <asp:ListItem>400</asp:ListItem>
            <asp:ListItem>1000</asp:ListItem>
        </asp:DropDownList>
    </div>
    <%--//得到分页页面的总数--%>
    <div>
        <asp:GridView ID="GridView_dc" runat="server"></asp:GridView>
    </div>
</asp:Content>
