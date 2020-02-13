<%@ Page Title="代理首页L2" Language="C#" MasterPageFile="~/WebsiteAgent/L2/SiteTemplateAgentL2.Master" AutoEventWireup="true" CodeBehind="代理首页L2.aspx.cs" Inherits="web1.WebsiteAgent.L2.Overview.代理首页L2" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1">

<h3>代理首页L2</h3>

<table class="auto-style1">
    <tr>
        <td colspan="3">
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



</asp:Content>