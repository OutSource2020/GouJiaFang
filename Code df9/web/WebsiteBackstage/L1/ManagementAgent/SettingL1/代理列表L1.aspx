<%@ Page Title="代理列表L1" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="代理列表L1.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementAgent.SettingL1.代理列表L1" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<h3>代理列表L1</h3>

<div id="筛选类" class="auto-style1">

    <table class="auto-style1">
        <tr>
            <td colspan="2">
                选择时间筛选
                <asp:RadioButton ID="RadioButton_时间今天" runat="server" GroupName="DenXiGan" Text="今天" type="radio" value="今天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间今天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间昨天" runat="server" GroupName="DenXiGan" Text="昨天" type="radio" value="昨天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间昨天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间7天" runat="server" GroupName="DenXiGan" Text="7天" type="radio" value="7天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间7天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间本周" runat="server" GroupName="DenXiGan" Text="本周" type="radio" value="本周" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本周_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间本月" runat="server" GroupName="DenXiGan" Text="本月" type="radio" value="本月" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本月_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="_设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间设置_CheckedChanged" Checked="True" />
                开始时间<asp:TextBox ID="TextBox_开始时间" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                结束时间<asp:TextBox ID="TextBox_结束时间" runat="server"  Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                (格式: 年-月-日)
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                账号状态筛选:
                <asp:RadioButton ID="RadioButton_状态全部" runat="server" GroupName="DenHinTai" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_状态全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_状态启用" runat="server" GroupName="DenHinTai" Text="启用" type="radio" value="启用" AutoPostBack="True" OnCheckedChanged="RadioButton_状态启用_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_状态停用" runat="server" GroupName="DenHinTai" Text="停用" type="radio" value="停用" AutoPostBack="True" OnCheckedChanged="RadioButton_状态停用_CheckedChanged" />
            </td>
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True">
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="代理ID">代理ID</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click" Text="查找" />
                <asp:Button ID="Button2" runat="server" Text="新增代理"   class="btn btn-info btn-fw" OnClick="Button1_Click" />
            </td>
            <td>

            </td>
        </tr>
    </table>

</div>



<div>
<asp:GridView ID="GridView1" runat="server" class="auto-style1" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>

                    <asp:BoundField DataField="代理ID" HeaderText="代理ID" />
                    <asp:BoundField DataField="代理名称" HeaderText="代理名称" />
                    <asp:BoundField DataField="时间最后登入" HeaderText="最后登入时间" />
                    <asp:BoundField DataField="时间注册" HeaderText="时间注册" />
                    <asp:BoundField DataField="状态" HeaderText="状态" />
                    
        <asp:HyperLinkField Text="操作" DataNavigateUrlFields="代理ID" DataNavigateUrlFormatString="代理列表L1设置.aspx?Bianhao={0}" />
    </Columns>
                <EmptyDataTemplate>No Record Available 沒有可用記錄</EmptyDataTemplate>


                <PagerTemplate>
                                    当前第:
                                     <%--//((GridView)Container.NamingContainer)就是为了得到当前的控件--%>
                                    <asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex + 1 %>"></asp:Label>
                                    页/共:
                                    <%--//得到分页页面的总数--%>
                                    <asp:Label ID="LabelPageCount" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
                                    页
                                    <%--//如果该分页是首分页，那么该连接就不会显示了.同时对应了自带识别的命令参数CommandArgument--%>
                                    <asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First" CommandName="Page"
                                        Visible='<%#((GridView)Container.NamingContainer).PageIndex != 0 %>'>首页</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                                        CommandName="Page" Visible='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>'>上一页</asp:LinkButton>
                                    <%--//如果该分页是尾页，那么该连接就不会显示了--%>
                                    <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                                        Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>下一页</asp:LinkButton>
                                    <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                                        Visible='<%# ((GridView)Container.NamingContainer).PageIndex != ((GridView)Container.NamingContainer).PageCount - 1 %>'>尾页</asp:LinkButton>
                                    转到第
                                    <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />页
                                    <%--//这里将CommandArgument即使点击该按钮e.newIndex 值为3 --%>
                                    <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                                        CommandName="Page" Text="GO" />
                                </PagerTemplate>


</asp:GridView>

<div id="选择每页行数">
    <asp:DropDownList ID="DropDownList_选择每页行数" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_选择每页行数_SelectedIndexChanged" Enabled="False">
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
</div>

</div>


</asp:Content>
