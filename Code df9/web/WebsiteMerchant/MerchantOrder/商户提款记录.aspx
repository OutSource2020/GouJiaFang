<%@ Page Title="商户提款记录" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户提款记录.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户提款记录" %>

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
                订单状态筛选:
                <asp:RadioButton ID="RadioButton_状态全部" runat="server" GroupName="DenHinTai" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_状态全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_状态待处理" runat="server" GroupName="DenHinTai" Text="待处理" type="radio" value="待处理" AutoPostBack="True" OnCheckedChanged="RadioButton_状态待处理_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_状态成功" runat="server" GroupName="DenHinTai" Text="成功" type="radio" value="成功" AutoPostBack="True" OnCheckedChanged="RadioButton_状态成功_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_状态失败" runat="server" GroupName="DenHinTai" Text="失败" type="radio" value="失败" AutoPostBack="True" OnCheckedChanged="RadioButton_状态失败_CheckedChanged" />
            </td>
            <td>
                类型筛选:
                <asp:RadioButton ID="RadioButton_类型全部" runat="server" GroupName="DingDuaLeiHin" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_类型全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_类型出账" runat="server" GroupName="DingDuaLeiHin" Text="出账" type="radio" value="出账" AutoPostBack="True" OnCheckedChanged="RadioButton_类型出账_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_类型冲正" runat="server" GroupName="DingDuaLeiHin" Text="冲正" type="radio" value="冲正" AutoPostBack="True" OnCheckedChanged="RadioButton_类型冲正_CheckedChanged" />
            </td>
        </tr>
        <tr>
            
            <td >
                创建方式筛选:
                <asp:RadioButton ID="RadioButton_创建方式全部" runat="server" GroupName="LoDenXi" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_创建方式手动" runat="server" GroupName="LoDenXi" Text="手动" type="radio" value="手动" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式手动_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_创建方式API" runat="server" GroupName="LoDenXi" Text="API" type="radio" value="API" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式API_CheckedChanged" />
               <asp:RadioButton ID="RadioButton_创建方式文档" runat="server" GroupName="LoDenXi" Text="文档导入" type="radio" value="文档导入" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式文档_CheckedChanged" />
            <asp:RadioButton ID="RadioButton_创建方式文本" runat="server" GroupName="LoDenXi" Text="文本导入" type="radio" value="文本导入" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式文本_CheckedChanged" />
            
                </td>
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                <asp:ListItem Value="未选择">未选择</asp:ListItem>
                <asp:ListItem Value="订单号">订单号</asp:ListItem>
                <asp:ListItem Value="交易方姓名">交易方</asp:ListItem>
                <asp:ListItem Value="商户API订单号">商户API订单号</asp:ListItem>
                </asp:DropDownList>

                 <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="查找" class="btn btn-info btn-fw" OnClick="Button_查找_Click" />
                 <asp:Button ID="Button2" runat="server" Text="发起提款" class="btn btn-info btn-fw" OnClick="Button_提款_Click" />
            </td>
            <td>

                <div id="刷新选择">
                    <asp:CheckBox ID="CheckBox_刷新自动勾选" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_刷新自动勾选_CheckedChanged" />
                    <asp:TextBox ID="TextBox_刷新秒数" runat="server" Width="20px" Enabled="False">20</asp:TextBox>秒自动刷新
                </div>

            </td>
        </tr>
    </table>

</div>


    <div>

         <table class="auto-style1">
            <tr>
                <td>账户提款余额:
                    <asp:Label ID="Label_账户提款余额" runat="server" Text="Label_账户提款余额"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>账户手续费余额:
                    <asp:Label ID="Label_账户手续费余额" runat="server" Text="Labe_账户手续费余额"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td> 
                 <asp:Button ID="Button3" runat="server" Text="最新批次正序排列" class="btn btn-info btn-fw" OnClick="Button3_Click"  />
                 <asp:Button ID="Button4" runat="server" Text="最新批次倒序排列" class="btn btn-info btn-fw" OnClick="Button4_Click" />
                    <a href="javascript:void(0)" class="btn btn-info btn-fw" onclick="MakeColor()">相同批次着色</a>
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

                <table style="width: 100%">
                    <tr>
                        <td>提款数据统计</td>
                        <td>待处理</td>
                        <td>成功</td>
                        <td>失败</td>
                        <td>合计</td>
                    </tr>
                    <tr>
                        <td>交易笔数</td>
                        <td>
                            <asp:Label ID="Label_交易笔数_进行中" runat="server" Text="Label_交易笔数_进行中"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_成功" runat="server" Text="Label_交易笔数_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_失败" runat="server" Text="Label_交易笔数_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_合计" runat="server" Text="Label_交易笔数_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>交易总金额</td>
                        <td>
                            <asp:Label ID="Label_交易总金额_进行中" runat="server" Text="Label_交易总金额_进行中"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_成功" runat="server" Text="Label_交易总金额_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_失败" runat="server" Text="Label_交易总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_合计" runat="server" Text="Label_交易总金额_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>手续费总金额</td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_进行中" runat="server" Text="Label_手续费总金额_进行中"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_成功" runat="server" Text="Label_手续费总金额_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_失败" runat="server" Text="Label_手续费总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_合计" runat="server" Text="Label_手续费总金额_合计"></asp:Label>
                        </td>
                    </tr>
                </table>
         <div id="gridTable">   
    <asp:GridView ID="GridView1" runat="server"  class="auto-style1"  AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" ShowHeaderWhenEmpty="true" PageSize="50">
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>

<Columns>
            <asp:BoundField DataField="订单号" HeaderText="订单号" />
            <asp:BoundField DataField="商户API订单号" HeaderText="商户API订单号" />
            <%--<asp:BoundField DataField="商户ID" HeaderText="商户ID" />--%>
            <asp:BoundField DataField="类型" HeaderText="类型" />
            <asp:BoundField DataField="交易方卡号" HeaderText="交易方卡号" />
            <asp:BoundField DataField="交易方姓名" HeaderText="交易方姓名" />
            <asp:BoundField DataField="交易方银行" HeaderText="交易方银行" />
            <asp:BoundField DataField="交易金额" HeaderText="交易金额（元）" />
            <asp:BoundField DataField="手续费" HeaderText="手续费（元）" />
            <asp:BoundField DataField="创建方式" HeaderText="创建方式" />
             <asp:BoundField DataField="状态" HeaderText="状态" />
           <asp:BoundField DataField="时间创建" HeaderText="创建时间" />
            <asp:BoundField DataField="时间完成" HeaderText="时间完成" />
            <asp:BoundField DataField="商户提交批次ID组" HeaderText="商户提交批次ID组" />
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

       </div>
<%--//得到分页页面的总数--%>
            <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%--=========要刷新的部分 结束=========--%>
    

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

        <script>
        // 获取th 的索引("后台处理批次ID组")
        function GetColumnNum(str) {
            let eles = Object.values( document.getElementById("gridTable").getElementsByTagName("th"));
            for (let i = 0; i < eles.length; i++) {
                if (Object.is(eles[i].innerText, str)) {
                    console.log(i);
                    return i;
                }
                   
            }
            
            return -1;
        }
        function MakeColor() {
            let index = GetColumnNum("商户提交批次ID组");

            let flag = "";
            let color = "#2eff99b3";
            let eles = Object.values(document.getElementById("gridTable").getElementsByTagName("tr"));
            for (let i = 0; i < eles.length; i++) {
                if (i == 0)
                    continue;
                if (eles[i].children[index].innerText.trim() != "")
                    if (!Object.is(flag, eles[i].children[index].innerText)) {
                        flag = eles[i].children[index].innerText;
                        if (color != "yellow")
                            color = "yellow";
                        else
                            color = "#2eff99b3";
                        
                        eles[i].children[index].style.background = color;
                    }
                    else {
                    eles[i].children[index].style.background = color;
                    }

                 
               }
            }
        



    </script>
</asp:Content>
