<%@ Page Title="账户余额交易明细" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="账户余额交易明细.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.账户余额交易明细" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">


<div id="筛选类"  class="auto-style1" >

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
                <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="_设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间设置_CheckedChanged"  />
                开始时间<asp:TextBox ID="TextBox_开始时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                结束时间<asp:TextBox ID="TextBox_结束时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                (格式: 年-月-日)
            </td>
        </tr>
        <tr>
            <td>
                类型筛选:
                <asp:RadioButton ID="RadioButton_类型全部" runat="server" GroupName="DingDuaLeiHin" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_类型全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_类型充值提款手续费" runat="server" GroupName="DingDuaLeiHin" Text="充值手续费" type="radio" value="充值手续费" AutoPostBack="True" OnCheckedChanged="RadioButton_类型充值提款手续费_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型充值提款余额" runat="server" GroupName="DingDuaLeiHin" Text="充值余额" type="radio" value="充值余额" AutoPostBack="True" OnCheckedChanged="RadioButton_类型充值提款余额_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型提款" runat="server" GroupName="DingDuaLeiHin" Text="提款" type="radio" value="提款" AutoPostBack="True" OnCheckedChanged="RadioButton_类型提款_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型冲正" runat="server" GroupName="DingDuaLeiHin" Text="冲正" type="radio" value="冲正" AutoPostBack="True" OnCheckedChanged="RadioButton_类型冲正_CheckedChanged" />
            </td>
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                     <asp:ListItem Value="未选择">未选择</asp:ListItem>
                     <asp:ListItem Value="订单号">订单号</asp:ListItem>
                     <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                </asp:DropDownList>

                <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click1" Text="查找" />
            </td>
            <td>

            </td>
        </tr>
    </table>

</div>



</div>




        <asp:GridView ID="GridView1" runat="server"  class="auto-style1"  AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>
            <asp:BoundField DataField="订单号" HeaderText="订单号" />
            <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
            <asp:BoundField DataField="交易前账户余额" HeaderText="交易前账户余额" />
            <asp:BoundField DataField="交易金额" HeaderText="交易金额" />
            <asp:BoundField DataField="交易后账户余额" HeaderText="交易后账户余额" />
            <asp:BoundField DataField="类型" HeaderText="类型" />
            <asp:BoundField DataField="时间创建" HeaderText="时间" />
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



<div id="分页">
    <asp:TextBox ID="TextBox_分页页数" runat="server" Width="60px">1</asp:TextBox>
    <asp:Button ID="Button_分页" runat="server" Text="转页" OnClick="Button_分页_Click" />
    <asp:Label ID="Label_现在是第几页" runat="server" Text="."></asp:Label>
</div>


</asp:Content>
