<%@ Page Title="商户充值记录" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户充值记录.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementOrder.商户充值记录" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<h3>商户充值记录</h3>

<div id="筛选类" class="auto-style1" >

<div>

    <table class="auto-style1">
        <tr>
            <td colspan="2">
                选择时间筛选
                <asp:RadioButton ID="RadioButton_时间今天" runat="server" GroupName="DenXiGan" Text="今天" type="radio" value="今天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间今天_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_时间昨天" runat="server" GroupName="DenXiGan" Text="昨天" type="radio" value="昨天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间昨天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间7天" runat="server" GroupName="DenXiGan" Text="7天" type="radio" value="7天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间7天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间本周" runat="server" GroupName="DenXiGan" Text="本周" type="radio" value="本周" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本周_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间本月" runat="server" GroupName="DenXiGan" Text="本月" type="radio" value="本月" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本月_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="_设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间设置_CheckedChanged" />
                开始时间<asp:TextBox ID="TextBox_开始时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                结束时间<asp:TextBox ID="TextBox_结束时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                (格式: 年-月-日)
            </td>
        </tr>
        <tr>
            <td>
                类型筛选:
                <asp:RadioButton ID="RadioButton_类型全部" runat="server" GroupName="DingDuaLeiHin" Text="全部" type="radio" value="全部" AutoPostBack="True"  Checked="True" OnCheckedChanged="RadioButton_类型全部_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型充值提款手续费" runat="server" GroupName="DingDuaLeiHin" Text="充值提款手续费" type="radio" value="充值提款手续费" AutoPostBack="True" OnCheckedChanged="RadioButton_类型充值提款手续费_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型充值提款余额" runat="server" GroupName="DingDuaLeiHin" Text="充值提款余额" type="radio" value="充值提款余额" AutoPostBack="True" OnCheckedChanged="RadioButton_类型充值提款余额_CheckedChanged" />
            </td>
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True">
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                按金额区间筛选:
                <asp:TextBox ID="TextBox_区间金额起始" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">0</asp:TextBox>
                -
                <asp:TextBox ID="TextBox_区间金额结束" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">50000</asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click" Text="查找" />
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



</div>



<%--=========要刷新的部分 开始=========--%>
<div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<asp:Label ID="Label_刷新时间" runat="server" Text=".."></asp:Label>

    <div>
        <table style="width: 100%" class="table table-bordered">
            <tr>
                <td>
                    充值提款余额笔数(成功)
                </td>
                <td>
                    充值手续费笔数(成功)
                </td>
                <td>
                    充值提款余额总数(成功)
                </td>
                <td>
                    充值手续费金额总数(成功)
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label_充值提款余额笔数" runat="server" Text="充值提款余额笔数"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label_充值手续费笔数" runat="server" Text="充值手续费笔数"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label_充值提款余额总数" runat="server" Text="充值提款余额总数"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label_充值手续费金额总数" runat="server" Text="充值手续费金额总数"></asp:Label>
                </td>
            </tr>
        </table>

    <br />



    </div>

        <asp:GridView ID="GridView1" runat="server" class="auto-style1" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>

                    <asp:BoundField DataField="订单号" HeaderText="订单号" />
                    <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
                    <asp:BoundField DataField="商户银行卡卡号" HeaderText="银行卡卡号" />
                    <asp:BoundField DataField="商户充值目标卡号" HeaderText="充值到卡号" />
                    <asp:BoundField DataField="充值类型" HeaderText="充值类型" />
                    <asp:BoundField DataField="充值金额" HeaderText="充值金额" />
                    <asp:BoundField DataField="备注后台" HeaderText="备注" />
                    <asp:BoundField DataField="状态" HeaderText="状态" />
                    <asp:BoundField DataField="时间创建" HeaderText="创建时间" />

                    <asp:HyperLinkField Text="操作" DataNavigateUrlFields="订单号" DataNavigateUrlFormatString="商户充值记录状态更新判定.aspx?Bianhao={0}" />
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


<%--//得到分页页面的总数--%>
            <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%--=========要刷新的部分 结束=========--%>


<div id="选择每页行数">
    <asp:DropDownList ID="DropDownList_选择每页行数" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_选择每页行数_SelectedIndexChanged" >
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
</div>


<div id="分页">
    <asp:TextBox ID="TextBox_分页页数" runat="server" Width="60px">1</asp:TextBox>
    <asp:Button ID="Button_分页" runat="server" Text="转页" OnClick="Button_分页_Click" />
    <asp:Label ID="Label_现在是第几页" runat="server" Text="."></asp:Label>
</div>


</asp:Content>