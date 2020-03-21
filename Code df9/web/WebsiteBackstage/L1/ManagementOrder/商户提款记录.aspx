<%@ Page Title="商户提款记录" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户提款记录.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementOrder.商户提款记录" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">
</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

    <%--弹窗--%>
    <style>
        .black_overlay {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 100%;
            background-color: black;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .80;
            filter: alpha(opacity=88);
        }

        .white_content {
            display: none;
            position: absolute;
            top: 20%;
            left: 20%;
            width: 50%;
            height: 50%;
            padding: 20px;
            border: 10px solid orange;
            background-color: white;
            z-index: 1002;
            overflow: auto;
        }
    </style>



    <script type="text/javascript">

</script>
    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GridView1.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("没有选中记录");
                return false;
            }
            else {
                return confirm("你选择了 " + count + " 个记录");
            }
        }
    </script>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    inputList[i].checked = objRef.checked;
                }
            }
        }

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>

    <div id="筛选类">

        <div>

            <table class="auto-style1">
                <tr>
                    <td colspan="2">选择时间筛选
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
                    <td>订单状态筛选:
                <asp:RadioButton ID="RadioButton_状态全部" runat="server" GroupName="DenHinTai" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_状态全部_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioButton_状态待处理" runat="server" GroupName="DenHinTai" Text="待处理" type="radio" value="待处理" AutoPostBack="True" OnCheckedChanged="RadioButton_状态待处理_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_状态成功" runat="server" GroupName="DenHinTai" Text="成功" type="radio" value="成功" AutoPostBack="True" OnCheckedChanged="RadioButton_状态成功_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_状态失败" runat="server" GroupName="DenHinTai" Text="失败" type="radio" value="失败" AutoPostBack="True" OnCheckedChanged="RadioButton_状态失败_CheckedChanged" />
                    </td>
                    <td>类型筛选:
                <asp:RadioButton ID="RadioButton_类型全部" runat="server" GroupName="DingDuaLeiHin" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_类型全部_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioButton_类型出账" runat="server" GroupName="DingDuaLeiHin" Text="订单出账" type="radio" value="订单出账" AutoPostBack="True" OnCheckedChanged="RadioButton_类型出账_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_类型订单提款冲正" runat="server" GroupName="DingDuaLeiHin" Text="订单提款冲正" type="radio" value="订单提款冲正" AutoPostBack="True" OnCheckedChanged="RadioButton_类型订单提款冲正_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td>创建方式筛选:
                <asp:RadioButton ID="RadioButton_创建方式全部" runat="server" GroupName="LoDenXi" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式全部_CheckedChanged" Checked="True" />
                        <asp:RadioButton ID="RadioButton_创建方式手动" runat="server" GroupName="LoDenXi" Text="手动" type="radio" value="手动" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式手动_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_创建方式API" runat="server" GroupName="LoDenXi" Text="API" type="radio" value="API" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式API_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_创建方式文本导入" runat="server" GroupName="LoDenXi" Text="文本导入" type="radio" value="文本导入" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式文本导入_CheckedChanged" />
                        <asp:RadioButton ID="RadioButton_创建方式文档导入" runat="server" GroupName="LoDenXi" Text="文档导入" type="radio" value="文档导入" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式文档导入_CheckedChanged" />


                    </td>
                    <td>按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                    <asp:ListItem Value="未选择">未选择</asp:ListItem>
                    <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                    <asp:ListItem Value="交易方姓名">交易方姓名</asp:ListItem>
                    <asp:ListItem Value="订单号">订单号</asp:ListItem>
                    <asp:ListItem Value="商户API订单号">商户API订单号</asp:ListItem>
                </asp:DropDownList>
                        <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>按端金额筛选:
                 <asp:DropDownList ID="DropDownList_端金额" runat="server" AutoPostBack="True">
                     <asp:ListItem Value="未选择">未选择</asp:ListItem>
                     <asp:ListItem Value="金额小于">金额小于</asp:ListItem>
                     <asp:ListItem Value="金额等于">金额等于</asp:ListItem>
                     <asp:ListItem Value="金额大于">金额大于</asp:ListItem>
                 </asp:DropDownList>
                        <asp:TextBox ID="TextBox_筛选端金额" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="查找" class="btn btn-info btn-fw" OnClick="Button_查找_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                        <asp:Button ID="Button_全选" runat="server" Text="全选" class="btn btn-info btn-fw" OnClick="Button_全选_Click" />
                        <asp:Button ID="Button_全选取消" runat="server" Text="全选取消" class="btn btn-info btn-fw" OnClick="Button_全选取消_Click" />
                    </td>
                </tr>
                <tr>
                    <td>按金额区间筛选:
                <asp:TextBox ID="TextBox_区间金额起始" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">0</asp:TextBox>
                        -
                <asp:TextBox ID="TextBox_区间金额结束" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">50000</asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button_统计" runat="server" Text="统计数量和金额" class="btn btn-info btn-fw" OnClick="Button_统计_Click" />
                        <asp:Button ID="Button_检查重复" runat="server" Text="检查重复" class="btn btn-info btn-fw" OnClick="Button_检查重复_Click" />
                    </td>
                </tr>
                <tr>
                    <td>导出模板:
                        <asp:RadioButton ID="RadioButton_默认模板" runat="server" GroupName="导出模板" Text="默认模板" type="radio" value="默认" Checked="True" />
                        <asp:RadioButton ID="RadioButton_招商银行" runat="server" GroupName="导出模板" Text="招商银行模板" type="radio" value="招商银行" />
                        <asp:RadioButton ID="RadioButton_光大银行" runat="server" GroupName="导出模板" Text="光大银行模板" type="radio" value="光大银行" />
                        <asp:RadioButton ID="RadioButton_平安银行" runat="server" GroupName="导出模板" Text="平安银行模板" type="radio" value="平安银行" />
                    </td>
                    <td>
                        <asp:Button ID="btnExportExcel" runat="server" Text="导出选中项" class="btn btn-info btn-fw" OnClick="btnExportExcel_Click" />
                        <asp:Button ID="btnExportAll" runat="server" Text="导出全部项" class="btn btn-info btn-fw" OnClick="btnExportAll_Click" />
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
                        <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='block';document.getElementById('fade').style.display='block'">批量操作</a>
                        &nbsp&nbsp&nbsp&nbsp
                        <a href="javascript:void(0)" onclick=" MakeColor()">同一批次着色处理</a>(请排好批次再使用此功能)
                        <asp:Button ID="Button2" runat="server" Text="批次排序" class="btn btn-info btn-fw" OnClick="Button2_Click" />
                        <asp:Button ID="Button3" runat="server" Text="完成时间排序)" class="btn btn-info btn-fw" OnClick="Button3_Click" />
                    </td>
                </tr>
                <tr>
                    <td>最新后台处理批次ID导出模板:
                        <asp:RadioButton ID="RadioButton_批次默认模板" runat="server" GroupName="批次导出模板" Text="默认模板" type="radio" value="默认" Checked="True" />
                        <asp:RadioButton ID="RadioButton_批次招商银行" runat="server" GroupName="批次导出模板" Text="招商银行模板" type="radio" value="招商银行" />
                        <asp:RadioButton ID="RadioButton_批次光大银行" runat="server" GroupName="批次导出模板" Text="光大银行模板" type="radio" value="光大银行" />
                        <asp:RadioButton ID="RadioButton_批次平安银行" runat="server" GroupName="批次导出模板" Text="平安银行模板" type="radio" value="平安银行" />
                    </td>
                    <td>
                        <asp:Button ID="Button_导出最新后台处理批次ID组" runat="server" Text="导出最新后台处理批次ID组" class="btn btn-info btn-fw" OnClick="Button_导出最新后台处理批次ID组_Click" />
                    </td>
                </tr>
                <tr>
                    <td>异步回调条件筛选:
                 <asp:DropDownList ID="DropDownList_回调" runat="server">
                     <asp:ListItem Value="未选择">未选择</asp:ListItem>
                     <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                     <asp:ListItem Value="商户API订单号">商户API订单号</asp:ListItem>
                 </asp:DropDownList>
                        <asp:TextBox ID="TextBox_回调" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="Button_查询回调" runat="server" Text="按筛选条件查询需要回调订单" class="btn btn-info btn-fw" OnClick="Button_查询回调_Click" />
                        <asp:Button ID="Button_发送回调" runat="server" Text="按筛选条件给商户发送回调" class="btn btn-info btn-fw" OnClick="Button_发送回调_Click" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button_发送回调全部完成" runat="server" Text="发送所有未发送的订单回调" class="btn btn-info btn-fw" OnClick="Button_发送未发送回调_Click" />
                        <asp:Button ID="Button_发送近三天订单回调" runat="server" Text="发送近三天所有订单回调" class="btn btn-info btn-fw" OnClick="Button_发送近三天订单回调_Click" />
                    </td>
                </tr>
            </table>
        </div>

    </div>



    <div id="light" class="white_content">
        <a href="javascript:void(0)" onclick="document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">关闭</a>


        <asp:HiddenField ID="hfCount" runat="server" Value="0" />
        <table class="auto-style1">
            <tr>
                <td>银行卡
                <asp:DropDownList ID="DropDownList_选择银行卡" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>状态
                <asp:DropDownList ID="DropDownList_下拉框1" runat="server">
                    <asp:ListItem Text="未选择" Value="未选择" />
                    <asp:ListItem Text="成功" Value="成功" />
                    <asp:ListItem Text="失败" Value="失败" />
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>备注
                <asp:TextBox ID="TextBox_备注" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnDelete" runat="server" Text="批量操作更新" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />


    </div>
    <div id="fade" class="black_overlay"></div>

    <%--=========要刷新的部分 开始=========--%>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Label ID="Label1" runat="server" Text="载入时间"></asp:Label>


                <table style="width: 100%">
                    <tr>
                        <td>&nbsp;</td>
                        <td>成功</td>
                        <td>失败</td>
                        <td>待处理</td>
                        <td>冲正</td>
                        <td>合计</td>
                    </tr>
                    <tr>
                        <td>交易笔数</td>
                        <td>
                            <asp:Label ID="Label_交易笔数_成功" runat="server" Text="Label_交易笔数_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_失败" runat="server" Text="Label_交易笔数_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_待处理" runat="server" Text="Label_交易笔数_待处理"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_冲正" runat="server" Text="Label_交易笔数_冲正"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_合计" runat="server" Text="Label_交易笔数_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>交易总金额</td>
                        <td>
                            <asp:Label ID="Label_交易总金额_成功" runat="server" Text="Label_交易总金额_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_失败" runat="server" Text="Label_交易总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_待处理" runat="server" Text="Label_交易总金额_待处理"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_冲正" runat="server" Text="Label_交易总金额_冲正"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_合计" runat="server" Text="Label_交易总金额_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>手续费总金额</td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_成功" runat="server" Text="Label_手续费总金额_成功"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_失败" runat="server" Text="Label_手续费总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_待处理" runat="server" Text="Label_手续费总金额_待处理"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_冲正" runat="server" Text="Label_手续费总金额_冲正"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_合计" runat="server" Text="Label_手续费总金额_合计"></asp:Label>
                        </td>
                    </tr>
                </table>

                <hr />

                <div id="gridTable">
                    <asp:GridView
                        ID="GridView1"
                        runat="server"
                        class="auto-style1"
                        AutoGenerateColumns="False"
                        AllowPaging="True"
                        OnPageIndexChanging="GridView1_PageIndexChanging"
                        ShowHeaderWhenEmpty="true"
                        DataKeyNames="订单号"
                        DataKey="订单号" PageSize="50" OnRowDataBound="GridView1_RowDataBound">
                        <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this)" Enabled="False" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" Enabled='<%# Eval("状态").Equals("待处理") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="类型" HeaderText="类型" />--%>
                            <asp:BoundField DataField="订单号" HeaderText="订单号" />
                            <asp:BoundField DataField="商户ID" HeaderText="商户ID" />
                            <asp:BoundField DataField="出款银行卡名称" HeaderText="出款银行卡名称" />
                            <asp:BoundField DataField="出款银行卡卡号" HeaderText="出款银行卡卡号" />
                            <asp:BoundField DataField="交易金额" HeaderText="交易金额" />
                            <asp:BoundField DataField="交易方卡号" HeaderText="交易方卡号" />
                            <asp:BoundField DataField="交易方姓名" HeaderText="交易方姓名" />
                            <asp:BoundField DataField="交易方银行" HeaderText="交易方银行" />
                            <asp:BoundField DataField="时间创建" HeaderText="创建时间" />
                            <asp:BoundField DataField="时间完成" HeaderText="完成时间" />
                            <asp:BoundField DataField="创建方式" HeaderText="创建方式" />
                            <asp:BoundField DataField="状态" HeaderText="订单状态" />
                            <asp:BoundField DataField="后台处理批次ID组" HeaderText="后台处理批次ID组" />
                            <asp:BoundField DataField="操作员" HeaderText="操作员" />
                            <asp:BoundField DataField="商户API订单号" HeaderText="商户API订单号" />
                            <asp:BoundField DataField="API回调次数" HeaderText="商户API回调次数" />
                            <asp:BoundField DataField="最后一次回调返回的状态" HeaderText="最后一次回调返回的状态" />
                            <asp:TemplateField HeaderText="行号" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:HyperLinkField Text="详情" DataNavigateUrlFields="订单号" DataNavigateUrlFormatString="商户提款记录详情.aspx?Bianhao={0}" />

                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("订单号", "商户提款记录状态更新.aspx?Bianhao={0}") %>' 
            Text='<%# Eval("状态") %>' Enabled='<%# Eval("状态").Equals("待处理") %>'></asp:HyperLink>--%>
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("订单号", "商户提款记录状态更新.aspx?Bianhao={0}") %>'
                                        Text='操作' Visible='<%# Eval("状态").ToString()=="待处理" %>'></asp:HyperLink>
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("订单号", "商户提款记录状态更新冲正.aspx?Bianhao={0}") %>'
                                        Text='冲正' Visible='<%# Eval("状态").ToString()=="成功" %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>

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
                <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
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



    <div id="分页">
        <asp:TextBox ID="TextBox_分页页数" runat="server" Width="60px">1</asp:TextBox>
        <asp:Button ID="Button_分页" runat="server" Text="转页" OnClick="Button_分页_Click" />
        <asp:Label ID="Label_现在是第几页" runat="server" Text="."></asp:Label>
    </div>



    <%--//得到分页页面的总数--%>



    <div>
        <asp:GridView ID="GridView_dc" runat="server"></asp:GridView>
    </div>

    <script>
        // 获取th 的索引("后台处理批次ID组")
        function GetColumnNum(str) {
            let eles = Object.values(document.getElementById("gridTable").getElementsByTagName("th"));
            for (let i = 0; i < eles.length; i++) {
                if (Object.is(eles[i].innerText, str))
                    return i;
            }
            return -1;
        }
        function MakeColor() {
            let index = GetColumnNum("后台处理批次ID组");

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

        window.οnlοad = function () {
            alert("页面加载完成！");
            MakeColor()
        }


    </script>
</asp:Content>
