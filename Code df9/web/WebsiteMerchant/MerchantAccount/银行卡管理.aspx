<%@ Page Title="银行卡管理" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="银行卡管理.aspx.cs" Inherits="web1.WebsiteMerchant.商户账号.银行卡管理" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div  class="auto-style1" >

        <div>
            <asp:Button ID="Button1" runat="server" Text="新增银行卡" class="btn btn-info btn-fw" OnClick="Button1_Click" />
        </div>

                <div id="刷新选择">         
                    <asp:CheckBox ID="CheckBox_刷新自动勾选" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_刷新自动勾选_CheckedChanged" />
                    <asp:TextBox ID="TextBox_刷新秒数" runat="server" Width="20px" Enabled="False">20</asp:TextBox>秒自动刷新
                </div>


<%--=========要刷新的部分 开始=========--%>
<div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<asp:Label ID="Label_刷新时间" runat="server" Text=".."></asp:Label>


            <asp:GridView ID="GridView1" runat="server"  class="auto-style1"  AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>
                    <asp:BoundField DataField="编号" HeaderText="编号" />
                    <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
                    <asp:BoundField DataField="商户银行卡卡号" HeaderText="银行卡卡号" />
                    <asp:BoundField DataField="商户银行名称" HeaderText="银行名称" />
                    <asp:BoundField DataField="商户银行卡主姓名" HeaderText="银行卡主姓名" />
                    <asp:BoundField DataField="状态" HeaderText="状态" />
                    
                    <asp:HyperLinkField Text="删除" DataNavigateUrlFields="编号" DataNavigateUrlFormatString="银行卡管理详情.aspx?Bianhao={0}" />
        <%--<asp:HyperLinkField Text="操作" DataNavigateUrlFields="编号" DataNavigateUrlFormatString="银行卡管理更新.aspx?Bianhao={0}" />--%>
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



<%--//得到分页页面的总数--%>
            <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%--=========要刷新的部分 结束=========--%>




</div>

</asp:Content>
