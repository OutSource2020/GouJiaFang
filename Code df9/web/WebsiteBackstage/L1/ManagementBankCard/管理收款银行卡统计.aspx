<%@ Page Title="管理收款银行卡统计" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="管理收款银行卡统计.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementBankCard.管理收款银行卡统计" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

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
                <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间设置_CheckedChanged" />
                开始时间<asp:TextBox ID="TextBox_开始时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                结束时间<asp:TextBox ID="TextBox_结束时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                (格式: 年-月-日)
            </td>
        </tr>
        <tr>
            <td>
                筛选类型:
                <asp:DropDownList ID="DropDownList_类型" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_类型_SelectedIndexChanged">
                    <asp:ListItem Value="全部">全部</asp:ListItem>
                    <asp:ListItem Value="充值">充值</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                筛选状态:
                <asp:DropDownList ID="DropDownList_状态" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_状态_SelectedIndexChanged">
                    <asp:ListItem Value="全部">全部</asp:ListItem>
                    <asp:ListItem Value="成功">成功</asp:ListItem>
                    <asp:ListItem Value="失败">失败</asp:ListItem>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                筛选商户名称:
                <asp:DropDownList ID="DropDownList_商户ID" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_商户ID_SelectedIndexChanged" >
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <%--<asp:ListItem Value="商户ID">商户ID</asp:ListItem>--%>
                </asp:DropDownList>

                </td>
            <td>
                筛选收款银行卡:
                <asp:DropDownList ID="DropDownList_银行卡" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_银行卡_SelectedIndexChanged" >    
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <%--<asp:ListItem Value="商户ID">商户ID</asp:ListItem>--%>
                </asp:DropDownList>

            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="查找" class="btn btn-info btn-fw" OnClick="Button_查找_Click" />
            </td>
            <td>

            </td>
        </tr>
    </table>

</div>


    <div id="刷新选择">
        <asp:CheckBox ID="CheckBox_刷新自动勾选" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_刷新自动勾选_CheckedChanged" />
        <asp:TextBox ID="TextBox_刷新秒数" runat="server" Width="20px">10</asp:TextBox>秒刷新
    </div>


</div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <%--=========要刷新的部分 开始=========--%>

<asp:Label ID="Label_刷新时间" runat="server" Text=".."></asp:Label>

<div>



            <asp:GridView ID="GridView1" runat="server" class="auto-style1" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>
                    <asp:BoundField DataField="count(收入)" HeaderText="收入笔数" />
                    <asp:BoundField DataField="count(支出)" HeaderText="支出笔数" />
                    <asp:BoundField DataField="sum(收入)" HeaderText="收入金额" />
                    <asp:BoundField DataField="sum(支出)" HeaderText="支出金额" />
                    <asp:BoundField DataField="收款银行卡卡号" HeaderText="收款银行卡卡号" />
                    <asp:BoundField DataField="收款银行卡名称" HeaderText="收款银行卡名称" />
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

    <%---------- 得到分页页面的总数 ----------%>
<div id="选择每页行数">
    <asp:DropDownList ID="DropDownList_选择每页行数" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_选择每页行数_SelectedIndexChanged" Enabled="False">
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
    </asp:DropDownList>
</div>

</asp:Content>
