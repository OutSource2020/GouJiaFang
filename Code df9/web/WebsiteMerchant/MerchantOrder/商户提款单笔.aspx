<%@ Page Title="商户提款单笔" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户提款单笔.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户提款单笔" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<div class="auto-style1" >

            <table style="width: 100%">
                <tr>
                    <td>交易方卡号</td>
                    <td>交易方姓名</td>
                    <td>交易方银行</td>
                    <td>交易金额（元）</td>
                    <td>备注</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TextBox_交易方卡号" runat="server"  Width="160px" MaxLength="30" OnTextChanged="TextBox_交易方卡号_TextChanged" AutoPostBack="True" onkeyup="value=value.replace(/[^\d.]/g,'')" onafterpaste="this.value=this.value.replace(/[^\d.]/g,'')" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_交易方姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_交易方银行" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_交易金额" runat="server"  Width="160px" MaxLength="30" onkeyup="value=value.replace(/[^\d.]/g,'')" onafterpaste="this.value=this.value.replace(/[^\d.]/g,'')" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox_备注" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
            <asp:Button ID="Button_识别银行卡所属银行" runat="server" Text="识别银行卡所属银行" class="btn btn-info btn-fw" OnClick="Button_识别银行卡所属银行_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
        

    <div>

        <div id="余额和比率">

                 <table class="auto-style1">
                    <tr>
                        <td style="width: 50%">余额</td>
                        <td>手续费比率</td>
                    </tr>
                    <tr>
                        <td style="width: 50%">
                            <asp:Label ID="Label_提款余额" runat="server" Text="Label_提款余额"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label_提款手续费比率" runat="server" Text="Label_提款手续费比率"></asp:Label>
                        </td>
                    </tr>
                </table>

            </div>

         <table class="auto-style1">
            <tr>
                <td>
                    <ul>
                        <li>注意 提款余额 和手续费余额 不足以支付将不会成功发出提款</li>
                    </ul>
                    
                </td>
            </tr>
            <tr>
                <td>
                    输入支付密码 :
                    <asp:TextBox ID="TextBox_输入支付密码" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button_批量发起提款订单" runat="server" Text="批量发起提款订单" class="btn btn-info btn-fw" OnClick="Button_批量发起提款订单_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />

                </td>
            </tr>
        </table>

    </div>



</div>

</asp:Content>
