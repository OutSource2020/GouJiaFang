<%@ Page Title="管理出款银行卡" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理出款银行卡.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理出款银行卡" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">


<div>

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
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList_关键字" runat="server" Width="100px" AutoPostBack="True"> 
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="出款银行卡名称">出款银行卡名称</asp:ListItem>
                    <asp:ListItem Value="出款银行卡卡号">出款银行卡卡号</asp:ListItem>
                    <asp:ListItem Value="出款银行名称">出款银行名称</asp:ListItem>
                    <asp:ListItem Value="出款银行卡主姓名">出款银行卡主姓名</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                按端金额筛选:
                <asp:DropDownList ID="DropDownList_端金额" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_端金额_SelectedIndexChanged">
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="金额小于">金额小于</asp:ListItem>
                    <asp:ListItem Value="金额等于">金额等于</asp:ListItem>
                    <asp:ListItem Value="金额大于">金额大于</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox_筛选端金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click" Text="查找" />
            </td>
            <td>
                <asp:Button ID="Button_新增银行卡" runat="server" Text="新增银行卡" class="btn btn-info btn-fw" OnClick="Button_新增银行卡_Click" />
                <asp:Button ID="Button_卡对卡余额转移" runat="server" Text="卡对卡余额转移" class="btn btn-info btn-fw" OnClick="Button_卡对卡余额转移_Click"  />
            </td>
        </tr>

        <tr>
            <td>

                <div id="刷新选择">
                    <asp:CheckBox ID="CheckBox_刷新自动勾选" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_刷新自动勾选_CheckedChanged" />          
                    <asp:TextBox ID="TextBox_刷新秒数" runat="server" Width="20px">20</asp:TextBox>秒自动刷新
                </div>

            </td>
            <td>

            </td>
        </tr>

    </table>


</div>



<%--=========要刷新的部分 开始=========--%>
<div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<asp:Label ID="Label_刷新时间" runat="server" Text=".."></asp:Label>


        <div>

             <table class="auto-style1">
                <tr>
                    <td>银行卡总数</td>
                    <td>启用银行卡数</td>
                    <td>启用银行卡总余额</td>
                    <td>启用银行卡总限额</td>
                    <td>启用银行卡可充总金额</td>
                </tr>
                <tr>
                    <td><asp:Label ID="Label_银行卡总数" runat="server" Text="银行卡总数"></asp:Label></td>
                    <td><asp:Label ID="Label_启用银行卡数" runat="server" Text="启用银行卡数"></asp:Label></td>
                    <td><asp:Label ID="Label_启用银行卡总余额" runat="server" Text="出款银行卡余额"></asp:Label></td>
                    <td><asp:Label ID="Label_启用银行卡总限额" runat="server" Text="启用银行卡总限额"></asp:Label></td>
                    <td><asp:Label ID="Label_启用银行卡可充总金额" runat="server" Text="启用银行卡可充总金额"></asp:Label></td>
                </tr>
            </table>
        </div>

    <div>
            <br />
            <asp:GridView ID="GridView1" runat="server" class="auto-style1" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>
                    <asp:BoundField DataField="编号" HeaderText="编号" />
                    <asp:BoundField DataField="出款银行卡名称" HeaderText="出款银行卡名称" />
                    <asp:BoundField DataField="出款银行卡卡号" HeaderText="出款银行卡卡号" />
                    <asp:BoundField DataField="出款银行名称" HeaderText="出款银行名称" />
                    <asp:BoundField DataField="出款银行卡余额" HeaderText="出款银行卡余额" />
                    <asp:BoundField DataField="出款银行卡主姓名" HeaderText="出款银行卡主姓名" />
                    <asp:BoundField DataField="出款银行卡主电话" HeaderText="出款银行卡主电话" />
                    <asp:BoundField DataField="出款银行卡每日限额" HeaderText="出款银行卡每日限额" />
                    <asp:BoundField DataField="出款银行卡最小交易金额" HeaderText="出款银行卡最小交易金额" />
                    <asp:BoundField DataField="显示标记" HeaderText="显示标记" />
                    <asp:BoundField DataField="状态" HeaderText="状态" />
                    <asp:BoundField DataField="时间创建" HeaderText="创建时间" />
        <asp:BoundField DataField="手续卡" HeaderText="手续卡" />
    <asp:BoundField DataField="金额卡" HeaderText="金额卡" />
                    <%----%>
        <asp:HyperLinkField Text="操作" DataNavigateUrlFields="出款银行卡卡号" DataNavigateUrlFormatString="管理出款银行卡卡更新.aspx?Bianhao={0}" />
        <asp:HyperLinkField Text="充值余额" DataNavigateUrlFields="出款银行卡卡号" DataNavigateUrlFormatString="管理出款银行卡卡充值.aspx?Bianhao={0}" />
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

            <%--=========要刷新的部分 结束=========--%>
            <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>


<div id="选择每页行数">
    <asp:DropDownList ID="DropDownList_选择每页行数" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_选择每页行数_SelectedIndexChanged" Enabled="False">
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
</div>

        </div>


</asp:Content>
