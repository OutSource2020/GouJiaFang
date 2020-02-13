<%@ Page Title="商户统计充值手续费" Language="C#" MasterPageFile="~/WebsiteAgent/L2/SiteTemplateAgentL2.Master" AutoEventWireup="true" CodeBehind="商户统计充值手续费.aspx.cs" Inherits="web1.WebsiteAgent.L2.DetailedData.商户统计充值手续费" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div id="筛选类"  class="auto-style1" >

<div id="筛选时间">
         <table class="auto-style1">
            <tr>
                <td>
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
                    <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click" Text="查找" />
                </td>
            </tr>

        </table>

    </div>


</div>


        <asp:GridView ID="GridView1" runat="server" class="auto-style1" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>

                    <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
                    <asp:BoundField DataField="max(B.商户名称)" HeaderText="商户名称" />
                    <asp:BoundField DataField="count(A.商户ID)" HeaderText="充值笔数" />
                    <asp:BoundField DataField="sum(A.充值金额)" HeaderText="充值金额" />
                    <asp:BoundField DataField="sum(A.产生手续费)" HeaderText="产生手续费" />


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
