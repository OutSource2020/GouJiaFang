<%@ Page Title="商户列表设置" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="商户列表设置.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ManagementMerchant.商户列表设置" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                </td>
            </tr>
        </table>

        <table id="设置信息" class="auto-style1" >
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    <asp:Button ID="Button_设置商户信息" runat="server" Text="设置商户信息" class="btn btn-info btn-fw" OnClick="Button_设置商户信息_Click" />
                    <asp:Button ID="Button_删除商户" runat="server" Text="删除商户" class="btn btn-info btn-fw" OnClick="Button_删除商户_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">商户ID</td>
                <td>
                    <asp:Label ID="Label_商户ID" runat="server" Text="Label_商户ID"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">商户名称</td>
                <td>
                    <asp:Label ID="Label_商户名称" runat="server" Text="Label_商户名称"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">状态</td>
                <td>
                    <asp:Label ID="Label_状态" runat="server" Text="Label_状态"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">时间最后登入</td>
                <td>
                    <asp:Label ID="Label_时间最后登入" runat="server" Text="Label_时间最后登入"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">时间注册</td>
                <td>
                    <asp:Label ID="Label_时间注册" runat="server" Text="Label_时间注册"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">所属管理L2</td>
                <td>
                    <asp:Label ID="Label_所属管理L2" runat="server" Text="Label_所属管理L2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">所属代理L1</td>
                <td>
                    <asp:Label ID="Label_所属代理L1" runat="server" Text="Label_所属代理L1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">所属代理L2</td>
                <td>
                    <asp:Label ID="Label_所属代理L2" runat="server" Text="Label_所属代理L2"></asp:Label>
                </td>
            </tr>
                        
        </table>

    </div>

    <div>

         <table class="auto-style1">
            <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    <asp:Button ID="Button_充值余额" runat="server" class="btn btn-info btn-fw" OnClick="Button_充值余额_Click" Text="充值余额" />
                    <asp:Button ID="Button_商户余额扣除" runat="server" class="btn btn-info btn-fw" OnClick="Button_商户余额扣除_Click" Text="商户余额扣除" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">提款余额</td>
                <td>
                    <asp:Label ID="Label_提款余额" runat="server" Text="Label_提款余额"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">手续费余额</td>
                <td>
                    <asp:Label ID="Label_手续费余额" runat="server" Text="Label_手续费余额"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">手续费收款方式</td>
                <td>
                    <asp:Label ID="Label_手续费收款方式" runat="server" Text="Label_手续费收款方式"></asp:Label>
                </td>
            </tr>
        </table>

     </div>

     <div>

          <table id="设置费率" class="auto-style1" >
              <tr>
                <td style="width: 30%">&nbsp;</td>
                <td>
                    <asp:Button ID="Button_设置商户费率" runat="server" Text="设置商户费率" class="btn btn-info btn-fw" OnClick="Button_设置商户费率_Click" />
                </td>
            </tr>
              <tr>
                <td style="width: 30%">提款最低单笔金额</td>
                <td>
                    <asp:Label ID="Label_提款最低单笔金额" runat="server" Text="Label_提款最低单笔金额"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">提款最高单笔金额</td>
                <td>
                    <asp:Label ID="Label_提款最高单笔金额" runat="server" Text="Label_提款最高单笔金额"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">手续费比率</td>
                <td>
                    <asp:Label ID="Label_手续费比率" runat="server" Text="Label_手续费比率"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">单笔手续费</td>
                <td>
                    <asp:Label ID="Label_单笔手续费" runat="server" Text="Label_单笔手续费"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第一阶梯起</td>
                <td>
                    <asp:Label ID="Label_第一阶梯起" runat="server" Text="Label_第一阶梯起"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第一阶梯止</td>
                <td>
                    <asp:Label ID="Label_第一阶梯止" runat="server" Text="Label_第一阶梯止"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第一阶梯百分比</td>
                <td>
                    <asp:Label ID="Label_第一阶梯百分比" runat="server" Text="Label_第一阶梯百分比"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第二阶梯起</td>
                <td>
                    <asp:Label ID="Label_第二阶梯起" runat="server" Text="Label_第二阶梯起"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第二阶梯止</td>
                <td>
                    <asp:Label ID="Label_第二阶梯止" runat="server" Text="Label_第二阶梯止"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第二阶梯百分比</td>
                <td>
                    <asp:Label ID="Label_第二阶梯百分比" runat="server" Text="Label_第二阶梯百分比"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第三阶梯起</td>
                <td>
                    <asp:Label ID="Label_第三阶梯起" runat="server" Text="Label_第三阶梯起"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第三阶梯止</td>
                <td>
                    <asp:Label ID="Label_第三阶梯止" runat="server" Text="Label_第三阶梯止"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第三阶梯百分比</td>
                <td>
                    <asp:Label ID="Label_第三阶梯百分比" runat="server" Text="Label_第三阶梯百分比"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第四阶梯起</td>
                <td>
                    <asp:Label ID="Label_第四阶梯起" runat="server" Text="Label_第四阶梯起"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第四阶梯止</td>
                <td>
                    <asp:Label ID="Label_第四阶梯止" runat="server" Text="Label_第四阶梯止"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">第四阶梯百分比</td>
                <td>
                    <asp:Label ID="Label_第四阶梯百分比" runat="server" Text="Label_第四阶梯百分比"></asp:Label>
                </td>
            </tr>
          </table>

          </div>

<div>

    <table id="设置安全" class="auto-style1" >
            <tr>
                <td style="width: 30%">安全设置</td>
                <td>
                    <asp:Button ID="Button_设置商户账户安全" runat="server" Text="设置商户账户安全" class="btn btn-info btn-fw" OnClick="Button_设置商户账户安全_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定邮箱</td>
                <td>
                    <asp:Label ID="Label_绑定邮箱" runat="server" Text="Label_绑定邮箱"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">绑定手机</td>
                <td>
                    <asp:Label ID="Label_绑定手机" runat="server" Text="Label_绑定手机"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">登入错误累计</td>
                <td>
                    <asp:Label ID="Label_登入错误累计" runat="server" Text="Label_登入错误累计"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%">支付错误累计</td>
                <td>
                    <asp:Label ID="Label_支付错误累计" runat="server" Text="Label_支付错误累计"></asp:Label>
                </td>
            </tr>
            </table>

</div>

</asp:Content>
