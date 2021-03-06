﻿<%@ Page Title="管理控制台" Language="C#" MasterPageFile="~/WebsiteBackstage/L2/SiteTemplateBackstageL2.Master" AutoEventWireup="true" CodeBehind="管理控制台.aspx.cs" Inherits="web1.WebsiteBackstage.L2.ControlCenter.管理控制台" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">
    
    <script type="text/javascript">
   function xalert()
   {
      var audio = new Audio('http://adminx.pppayment.com/MessageAudio/6809.wav');
    audio.loop = false;
    audio.play(); 
    }
</script>

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<h3>Backstage Console - 後台控制台</h3>

<div id="筛选类" class="auto-style1" >

<div>

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
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_查找" runat="server" class="btn btn-info btn-fw" OnClick="Button_查找_Click" Text="查找" />
            </td>
        </tr>

        </table>

<table class="auto-style1">
    <tr>
        <td style="width: 33%">
            <div class="card bg-gradient-info text-white">
                <div class="card-body">
                    <h4 class="font-weight-normal mb-3">商户充值手续费</h4>
                    <p class="card-text">充值手续费金额<asp:Label ID="Label_充值手续费金额" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text">交易手续费笔数<asp:Label ID="Label_交易手续费笔数" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text"> -</p>
                </div>
            </div>
        </td>
        <td style="width: 33%">
            <div class="card bg-gradient-success text-white">
                <div class="card-body">
                    <h4 class="font-weight-normal mb-3">商户充值余额</h4>
                    <p class="card-text">充值余额金额<asp:Label ID="Label_充值余额金额" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text">交易余额笔数<asp:Label ID="Label_交易余额笔数" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text">充值余额产生手续费<asp:Label ID="Label_充值余额产生手续费" runat="server" Text=" "></asp:Label></p>
                </div>
            </div>
        </td>
        <td style="width: 33%">
            <div class="card bg-gradient-warning text-white">
                <div class="card-body">
                    <h4 class="font-weight-normal mb-3">商户提款数据</h4>
                    <p class="card-text">提款金额<asp:Label ID="Label_商户提款提款金额" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text">交易笔数<asp:Label ID="Label_商户提款交易笔数" runat="server" Text=" "></asp:Label></p>
                    <p class="card-text">手续费金额<asp:Label ID="Label_商户提款手续费金额" runat="server" Text=" "></asp:Label></p>
                </div>
		  </div>
        </td>
    </tr>
</table>

</div>


<div id="刷新选择">       
    <asp:CheckBox ID="CheckBox_刷新自动勾选" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_刷新自动勾选_CheckedChanged" />  
    <asp:TextBox ID="TextBox_刷新秒数" runat="server" Width="20px">20</asp:TextBox>秒自动刷新
</div>

</div>
    <asp:CheckBox ID="CheckBox_提示音开关" runat="server" Text="提示音开关 (注意: 必须保持本页才有提示音) 兼容:chrome/firefox" />
<hr />

<%--=========要刷新的部分 开始=========--%>
<div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePaneL2" runat="server">
        <ContentTemplate>

<asp:Label ID="Label_刷新时间" runat="server" Text=".."></asp:Label>


    <table class="auto-style1">
        <tr>
            <td colspan="2">
                <strong>待处理事件</strong>

            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户充值订单</td>
            <td>
                <asp:Label ID="Label_充值订单" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户提款订单</td>
            <td>
                <asp:Label ID="Label_提款订单" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">商户银行卡待审核</td>
            <td>
                <asp:Label ID="Label_银行卡待审核" runat="server" Text="0"></asp:Label>
            </td>
        </tr>

    </table>



<%--//得到分页页面的总数--%>
            <asp:Timer ID="Timer_自动刷新" runat="server" OnTick="TimerTick" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<%--=========要刷新的部分 结束=========--%>


<p>Version Number DT2019-07-19 V1.0.0.129</p>


</asp:Content>
