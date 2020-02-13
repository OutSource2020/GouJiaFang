<%@ Page Title="商户充值" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户充值.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户充值" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">

<h3>商户充值</h3>

<div  class="auto-style1" >
        <table style="width: 100%">
        <tr>
            <td style="width: 20%">发起卡号</td>
            <td>
                <asp:DropDownList ID="DropDownList_发起卡号" runat="server">
                    <asp:ListItem>未选择</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">按类型填入要转到的目标姓名</td>
            <td>
                <asp:TextBox ID="TextBox_目标姓名" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">按类型填入要转到的目标卡号</td>
            <td>
                <asp:TextBox ID="TextBox_目标卡号" runat="server" Width="160px" MaxLength="30" onKeyPress="if((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46 || /\.\d\d$/.test(value))event.returnValue=false" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">按类型填入要转到的目标银行名称</td>
            <td>
                <asp:TextBox ID="TextBox_目标银行名称" runat="server"  Width="160px" MaxLength="30" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">金额</td>
            <td>
                <asp:TextBox ID="TextBox_金额" runat="server"  Width="160px" MaxLength="30" onKeyPress="if((event.keyCode<48 || event.keyCode>57) && event.keyCode!=46 || /\.\d\d$/.test(value))event.returnValue=false" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">类型</td>
            <td>
                <asp:RadioButton ID="RadioButton_充值提款手续费" runat="server" GroupName="DenXiGan" Text="充值提款手续费" type="radio" value="充值提款手续费" AutoPostBack="True" />
                <asp:RadioButton ID="RadioButton_充值提款余额" runat="server" GroupName="DenXiGan" Text="充值提款余额" type="radio" value="充值提款余额" AutoPostBack="True" Checked="True" />
                 </td>
        </tr>
        <tr>
            <td style="width: 20%">备注</td>
            <td>
                <asp:TextBox ID="TextBox_备注" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" />
                <asp:Button ID="Button_充值" runat="server" Text="充值" class="btn btn-info btn-fw" OnClick="Button_充值_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
            </td>
        </tr>
    </table>

<div>
    <table class="auto-style1">
        <tr>
            <td>
手续费收款卡
    <asp:GridView ID="GridView_收手续费卡" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="收款银行名称" HeaderText="银行名称" />
            <asp:BoundField DataField="收款银行卡主姓名" HeaderText="卡主姓名" />
            <asp:BoundField DataField="收款银行卡卡号" HeaderText="目标充值手续费卡" />        
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
金额卡收款卡
    <asp:GridView ID="GridView_收金额卡" runat="server" AutoGenerateColumns="False">
            <Columns>
            <asp:BoundField DataField="出款银行名称" HeaderText="银行名称" />
            <asp:BoundField DataField="出款银行卡主姓名" HeaderText="卡主姓名" />
            <asp:BoundField DataField="出款银行卡卡号" HeaderText="目标充值金额卡" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
    </table>
</div>


</div>



</asp:Content>
