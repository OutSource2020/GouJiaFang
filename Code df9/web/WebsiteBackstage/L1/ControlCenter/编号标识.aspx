<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteBackstage/L1/SiteTemplateBackstageL1.Master" AutoEventWireup="true" CodeBehind="编号标识.aspx.cs" Inherits="web1.WebsiteBackstage.L1.ControlCenter.编号标识" %>
<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

    <div>

        <table class="auto-style1">
            <tr>
                <td colspan="4">管理端 Backstage / Management / Administrator</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台日志管理</td>
                <td style="width: 33%">后台端登入日志编号</td>
                <td style="width: 33%">Backstage Login Log </td>
                <td>   BLL</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台白名单管理</td>
                <td style="width: 33%">后台白名单管理</td>
                <td style="width: 33%">Backstage Whitelist IP Address</td>
                <td>BWIPA</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台出款银行卡管理</td>
                <td style="width: 33%">后台出款银行卡管理</td>
                <td style="width: 33%">Backstage Out Paragraph Bank Card</td>
                <td>BOPBC</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台出款银行卡流水</td>
                <td style="width: 33%">卡对卡-出</td>
                <td style="width: 33%">Backstage Card To Card Out</td>
                <td>BCTCO</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台出款银行卡流水</td>
                <td style="width: 33%">卡对卡-入</td>
                <td style="width: 33%">Backstage Card To Card Enter</td>
                <td>BCTCE</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台出款银行卡流水</td>
                <td style="width: 33%">出款卡充值</td>
                <td style="width: 33%">Backstage Out Paragraph Bank Card Recharge</td>
                <td>BOPBCR</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台收款银行卡管理</td>
                <td style="width: 33%">后台收款银行卡管理 卡编号</td>
                <td style="width: 33%">Backstage Receive Paragraph Bank Card</td>
                <td>BRPBC</td>
            </tr>
            <tr>
                <td style="width: 33%">table_后台收款银行卡流水</td>
                <td style="width: 33%">后台出款银行卡付款</td>
                <td style="width: 33%">Backstage Out Paragraph Bank Card Payment</td>
                <td>BOPBCP</td>
            </tr>
            <tr>
                <td style="width: 33%">&nbsp;</td>
                <td style="width: 33%">&nbsp;</td>
                <td style="width: 33%">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 33%">table_商户明细余额</td>
                <td style="width: 33%">订单出款退款</td>
                <td style="width: 33%">Merchant Out Paragraph Order Refund</td>
                <td>MOPOR</td>
            </tr>
            <tr>
                <td style="width: 33%">table_商户明细手续费</td>
                <td style="width: 33%">手续费订单退款</td>
                <td style="width: 33%">Merchant Handling Fee Order Refund</td>
                <td>MHFOR</td>
            </tr>
            <tr>
                <td colspan="4">商户端 Merchant</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户日志管理</td>
                <td style="width: 33%">   商户端登入日志编号</td>
                <td style="width: 33%">   Merchant Login Log</td>
                <td>   MLL</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户白名单管理</td>
                <td style="width: 33%">   商户白名单管理 编号</td>
                <td style="width: 33%">   Merchant Whitelist IP Address</td>
                <td>   MWIPA</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户明细充值</td>
                <td style="width: 33%">   充值订单号-余额</td>
                <td style="width: 33%">   Merchant Recharge Order Number Balance</td>
                <td>   MRONB</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户明细充值</td>
                <td style="width: 33%">   充值订单号-手续费</td>
                <td style="width: 33%">   Merchant Recharge Order Number Handling Fee</td>
                <td>   MRONHF</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户明细提款</td>
                <td style="width: 33%">   提款订单号</td>
                <td style="width: 33%">   Merchant Mention Paragraph Order Number</td>
                <td>   MMPON</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户明细余额</td>
                <td style="width: 33%">   余额订单号</td>
                <td style="width: 33%">   Merchant Balance Order Number /Numbering</td>
                <td>   MBON</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户明细手续费</td>
                <td style="width: 33%">   手续费订单号</td>
                <td style="width: 33%">   Merchant Handling Fee Order Number</td>
                <td>   MHFON</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_商户银行卡</td>
                <td style="width: 33%">   商户银行卡 编号</td>
                <td style="width: 33%">   Merchant Bank Card Number</td>
                <td>   MBCN</td>
            </tr>
            <tr>
                <td colspan="4">   代理端 Agent</td>
            </tr>
            <tr>
                <td style="width: 33%">   table_代理日志管理等级1</td>
                <td style="width: 33%">   代理端登入日志编号</td>
                <td style="width: 33%">   Agent Login Log</td>
                <td>   ALL</td>
            </tr>
            <tr>
                <td style="width: 33%">   &nbsp;</td>
                <td style="width: 33%">   &nbsp;</td>
                <td style="width: 33%">   &nbsp;</td>
                <td>   &nbsp;</td>
            </tr>
        </table>

    </div>

</asp:Content>
