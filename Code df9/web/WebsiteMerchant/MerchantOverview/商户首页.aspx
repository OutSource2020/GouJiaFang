<%@ Page Title="商户首页" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户首页.aspx.cs" Inherits="web1.WebsiteMerchant.商户首页.商户首页" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div id="shoye" class="auto-style1">



<table class="auto-style1">
    <tr>
        <td colspan="3">
            <h2 style="text-align: center"><strong>欢迎 </strong>
                        <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
        </td>
    </tr>
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
                    <h3 class="font-weight-normal mb-3"> 记录充值 </h3>
                        <p class="card-text">充值笔数今天:<asp:Label ID="Label_记录充值_充值笔数今天" runat="server" Text="Label_记录充值_充值笔数今天"></asp:Label></p>
                        <p class="card-text">充值笔数今天(成功) <asp:Label ID="Label_记录充值_充值笔数今天成功" runat="server" Text="Label_记录充值_充值笔数今天成功"></asp:Label></p>
                        <p class="card-text">充值金额成功: <asp:Label ID="Label_记录充值_充值金额成功" runat="server" Text="Label_记录充值_充值金额成功"></asp:Label></p>
                        <p class="card-text">充值金额7天(成功): <asp:Label ID="Label_记录充值_充值金额成功7天" runat="server" Text="Label_记录充值_充值金额成功7天"></asp:Label></p>
                </div>
            </div>
        </td>
        <td style="width: 33%">
            <div class="card bg-gradient-warning text-white">
                <div class="card-body">
                    <h3 class="font-weight-normal mb-3"> 记录提款 </h3>
                        <p class="card-text">提款笔数今天:<asp:Label ID="Label_记录提款_提款笔数今天" runat="server" Text="Label_记录提款_提款笔数今天"></asp:Label></p>
                        <p class="card-text">提款笔数今天(成功)<asp:Label ID="Label_记录提款_提款笔数今天成功" runat="server" Text="Label_记录提款_提款笔数今天成功"></asp:Label></p>
                        <p class="card-text">提款金额成功<asp:Label ID="Label_记录提款_提款金额成功" runat="server" Text="Label_记录提款_提款金额成功"></asp:Label></p>
                        <p class="card-text">提款金额7天(成功)<asp:Label ID="Label_记录提款_提款金额成功7天" runat="server" Text="Label_记录提款_提款金额成功7天"></asp:Label></p>
                </div>
		  </div>
        </td>
        <td style="width: 33%">
            <div class="card bg-gradient-success text-white">
                <div class="card-body">
                    <h3 class="font-weight-normal mb-3"> 账户余额 </h3>
                        <p class="card-text">账户余额:<asp:Label ID="Label_今日提款详情_账户余额" runat="server" Text="Label_今日提款详情_账户余额"></asp:Label></p>
                        <p class="card-text">手续费余额:<asp:Label ID="Label_今日提款详情_手续费余额" runat="server" Text="Label_今日提款详情_手续费余额"></asp:Label></p>
                        <p class="card-text">_ </p>
                        <p class="card-text">_ </p>
                </div>
		  </div>
        </td>
    </tr>
</table>




    <div>
    <table style="width: 100%">
                    <tr>
                        <td style="width: 20%">数据统计</td>
                        <td style="width: 20%">待处理</td>
                        <td style="width: 20%">成功</td>
                        <td style="width: 20%">失败</td>
                        <td>合计</td>
                    </tr>
                    <tr>
                        <td style="width: 20%">交易笔数</td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易笔数_待处理" runat="server" Text="Label_交易笔数_待处理"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易笔数_成功" runat="server" Text="Label_交易笔数_成功"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易笔数_失败" runat="server" Text="Label_交易笔数_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易笔数_合计" runat="server" Text="Label_交易笔数_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">交易总金额</td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易总金额_待处理" runat="server" Text="Label_交易总金额_待处理"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易总金额_成功" runat="server" Text="Label_交易总金额_成功"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_交易总金额_失败" runat="server" Text="Label_交易总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_交易总金额_合计" runat="server" Text="Label_交易总金额_合计"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">手续费总金额</td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_手续费总金额_待处理" runat="server" Text="Label_手续费总金额_待处理"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_手续费总金额_成功" runat="server" Text="Label_手续费总金额_成功"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <asp:Label ID="Label_手续费总金额_失败" runat="server" Text="Label_手续费总金额_失败"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_手续费总金额_合计" runat="server" Text="Label_手续费总金额_合计"></asp:Label>
                        </td>
                    </tr>
                </table>
    </div>

</div>

</asp:Content>
