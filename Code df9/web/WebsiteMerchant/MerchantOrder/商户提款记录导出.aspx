<%@ Page Title="商户提款记录导出" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户提款记录导出.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户提款记录导出" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">


<script type = "text/javascript">

<!--
//function Check_Click(objRef)
//{
//    //Get the Row based on checkbox
//    var row = objRef.parentNode.parentNode;
    
//    //Get the reference of GridView
//    var GridView = row.parentNode;
    
//    //Get all input elements in Gridview
//    var inputList = GridView.getElementsByTagName("input");
    
//    for (var i=0;i<inputList.length;i++)
//    {
//        //The First element is the Header Checkbox
//        var headerCheckBox = inputList[0];
        
//        //Based on all or none checkboxes
//        //are checked check/uncheck Header Checkbox
//        var checked = true;
//        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox)
//        {
//            if(!inputList[i].checked)
//            {
//                checked = false;
//                break;
//            }
//        }
//    }
//    headerCheckBox.checked = checked;
    
//}


//function checkAll(objRef)
//{
//    var GridView = objRef.parentNode.parentNode.parentNode;
//    var inputList = GridView.getElementsByTagName("input");
//    for (var i=0;i<inputList.length;i++)
//    {
//        var row = inputList[i].parentNode.parentNode;
//        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
//        {
//            if (objRef.checked)
//            {
//                inputList[i].checked=true;
//            }
//            else
//            {
//                if(row.rowIndex % 2 == 0)
//                {
//                   row.style.backgroundColor = "#C2D69B";
//                }
//                else
//                {
//                   row.style.backgroundColor = "white";
//                }
//                inputList[i].checked=false;
//            }
//        }
//    }
//}
//-->


</script>


    <script type = "text/javascript">
    function checkAll(objRef)
    {
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i=0;i<inputList.length;i++)
    {
        var row = inputList[i].parentNode.parentNode;
        if(inputList[i].type == "checkbox"  && objRef != inputList[i])
        {
            inputList[i].checked = objRef.checked;
        }
    }
    }

    function Check_Click(objRef)
    {
    //Get the Row based on checkbox
    var row = objRef.parentNode.parentNode;
    
    //Get the reference of GridView
    var GridView = row.parentNode;
    
    //Get all input elements in Gridview
    var inputList = GridView.getElementsByTagName("input");
    
    for (var i=0;i<inputList.length;i++)
    {
        //The First element is the Header Checkbox
        var headerCheckBox = inputList[0];
        
        //Based on all or none checkboxes
        //are checked check/uncheck Header Checkbox
        var checked = true;
        if(inputList[i].type == "checkbox" && inputList[i] != headerCheckBox)
        {
            if(!inputList[i].checked)
            {
                checked = false;
                break;
            }
        }
    }
    headerCheckBox.checked = checked;
    
    }
    </script> 









<div id="筛选类"  >

<div>

    <table class="auto-style1">
        <tr>
            <td colspan="2">
                选择时间筛选
                <asp:RadioButton ID="RadioButton_时间今天" runat="server" GroupName="DenXiGan" Text="今天" type="radio" value="今天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间今天_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_时间昨天" runat="server" GroupName="DenXiGan" Text="昨天" type="radio" value="昨天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间昨天_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_时间7天" runat="server" GroupName="DenXiGan" Text="7天" type="radio" value="7天" AutoPostBack="True" OnCheckedChanged="RadioButton_时间7天_CheckedChanged" Enabled="False"/>
                <asp:RadioButton ID="RadioButton_时间本周" runat="server" GroupName="DenXiGan" Text="本周" type="radio" value="本周" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本周_CheckedChanged" Enabled="False"/>
                <asp:RadioButton ID="RadioButton_时间本月" runat="server" GroupName="DenXiGan" Text="本月" type="radio" value="本月" AutoPostBack="True" OnCheckedChanged="RadioButton_时间本月_CheckedChanged" Enabled="False"/>
                <asp:RadioButton ID="RadioButton_时间设置" runat="server" GroupName="DenXiGan" Text="设置时间" type="radio" value="_设置时间" AutoPostBack="True" OnCheckedChanged="RadioButton_时间设置_CheckedChanged"  />
                开始时间<asp:TextBox ID="TextBox_开始时间" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox>
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
                创建方式筛选:
                <asp:RadioButton ID="RadioButton_创建方式全部" runat="server" GroupName="LoDenXi" Text="全部" type="radio" value="全部" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式全部_CheckedChanged" Checked="True" />
                <asp:RadioButton ID="RadioButton_创建方式手动" runat="server" GroupName="LoDenXi" Text="手动" type="radio" value="手动" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式手动_CheckedChanged" />
                <asp:RadioButton ID="RadioButton_创建方式API" runat="server" GroupName="LoDenXi" Text="API" type="radio" value="API" AutoPostBack="True" OnCheckedChanged="RadioButton_创建方式API_CheckedChanged" />
            </td>
        </tr>
        <tr>
            <td>
                按关键词筛选:
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                     <asp:ListItem Value="未选择">未选择</asp:ListItem>
                     <asp:ListItem Value="商户ID">商户ID</asp:ListItem>
                     <asp:ListItem Value="交易方姓名">交易方姓名</asp:ListItem>
                     <asp:ListItem Value="订单号">订单号</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox_筛选关键字" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
            <td>
                按端金额筛选:
                 <asp:DropDownList ID="DropDownList_端金额" runat="server" AutoPostBack="True">  
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
                按金额区间筛选:
                <asp:TextBox ID="TextBox_区间金额起始" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">0</asp:TextBox>
                -
                <asp:TextBox ID="TextBox_区间金额结束" runat="server" Width="60px" MaxLength="30" AutoCompleteType="Disabled">50000</asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="查找" class="btn btn-info btn-fw" OnClick="Button_查找_Click"  UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                <asp:Button ID="Button_全选" runat="server" Text="全选" class="btn btn-info btn-fw" OnClick="Button_全选_Click"  />
                <asp:Button ID="Button_全选取消" runat="server" Text="全选取消" class="btn btn-info btn-fw" OnClick="Button_全选取消_Click"  />
                <asp:Button ID="btnExportExcel" runat="server" Text="导出选中项" class="btn btn-info btn-fw" OnClick="btnExportExcel_Click"  />
                <asp:Button ID="btnExportAll" runat="server" Text="导出全部项" class="btn btn-info btn-fw" OnClick="btnExportAll_Click"  />
            </td>
        </tr>
    </table>

</div>

</div>



<div id="light" class="white_content">
<a href = "javascript:void(0)" onclick = "document.getElementById('light').style.display='none';document.getElementById('fade').style.display='none'">关闭</a>


    <asp:HiddenField ID="hfCount" runat="server" Value = "0" />


</div> 
        <div id="fade" class="black_overlay"></div> 



    <hr />


    <asp:GridView 
        ID="GridView1" 
        runat="server" 
        class="auto-style1" 
        AutoGenerateColumns="False" 
        AllowPaging="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging" 
        ShowHeaderWhenEmpty="true"
        DataKeyNames = "订单号"
        DataKey = "订单号" PageSize="50" OnRowDataBound="GridView1_RowDataBound"
        >
            <%--OnPageIndexChanging = "OnPaging" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"--%>
<Columns>
<asp:TemplateField>
<HeaderTemplate>
  <asp:CheckBox ID="chkAll" runat="server" onclick = "checkAll(this)" Enabled="False" />
</HeaderTemplate> 
<ItemTemplate>
 <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" Enabled='true' />
</ItemTemplate> 
</asp:TemplateField> 
            <asp:BoundField DataField="订单号" HeaderText="订单号" />
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



<%--//得到分页页面的总数--%>




</asp:Content>