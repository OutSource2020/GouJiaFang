<%@ Page Title="商户提款导入Excel" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户提款导入Excel.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户提款导入Excel" %>

<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>

<%@ Import Namespace="web1" %>
<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">
</asp:Content>
<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">
    <h1>Merchant Take Money(Import Excel) 商户提款 文档导入</h1>
    <div>
        <table class="auto-style1">
            <tr>
                <td colspan="5">
                    <ul>
                        <li>注意 "提款余额"和"手续费余额"不足以支付时将不会成功发出提款</li>
                        <li>注意 每笔不能小于"最低提款金额"或者大于"最高提款金额"</li>
                        <li>注意 提款订单数较多时每次提交不超出10单</li>
                        <li>警告 请勿操作浏览器页面回退</li>
                        <li>警告 十分钟内相同”交易卡号“”交易方姓名“”交易金额“的订单会被识别为相同订单</li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">&nbsp;</td>
                <td style="width: 20%">金额</td>
                <td>手续费</td>
                <td>限制最低提款金额</td>
                <td>限制最高提款金额</td>
            </tr>
            <tr>
                <td style="width: 20%">账户信息</td>
                <td style="width: 20%">账户提款余额:
                        <asp:Label ID="Label_提款余额" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 20%">手续费余额: 
                        <asp:Label ID="Label_手续费余额" runat="server" Text="0"></asp:Label>
                    单笔手续费: 
                        <asp:Label ID="Label_单笔手续费" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 20%">最低提款金额:
                        <asp:Label ID="Label_账户最低提款金额" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 20%">最高提款金额:
                        <asp:Label ID="Label_账户最高提款金额" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">预估信息 
                </td>
                <td style="width: 20%">预估订单金额: 
                        <asp:Label ID="Label_预计提款金额" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 20%">预估手续费金额: 
                        <asp:Label ID="Label_预计提款手续费" runat="server" Text="0"></asp:Label>
                </td>
                <td style="width: 20%"></td>
                <td style="width: 20%"></td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Button ID="Button_刷新预估" runat="server" Text="刷新预估(检查)" class="btn btn-info btn-fw" OnClick="Button_刷新预估_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="RowNumber" HeaderText="" />
                <asp:TemplateField HeaderText="交易方卡号">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Width="160px" MaxLength="30" AutoPostBack="True" onKeyPress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交易方姓名">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交易方银行">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Width="160px" MaxLength="30" Enabled="False"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="交易金额">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Width="160px" MaxLength="30" OnTextChanged="TextBox_交易金额_TextChanged" AutoPostBack="True" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" />
                    <FooterTemplate>
                        <asp:Button ID="ButtonAdd" runat="server" Text="新增行"
                            class="btn btn-info btn-fw" OnClick="ButtonAdd_Click" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkDel" OnCommand="DeleteRowHandler" Text="删除行"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div id="余额和比率">
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Button ID="Button_返回" runat="server" Text="返回" class="btn btn-info btn-fw" OnClick="Button_返回_Click" UseSubmitBehavior="false" />
                    </td>
                    <td>
                        <asp:Button ID="Button_下载模板" runat="server" Text="下载模板" class="btn btn-info btn-fw" OnClick="Button_下载模板_Click" UseSubmitBehavior="false" />
                        使用Excel导入预备订单 按要列为：交易方卡号，交易方姓名，交易方银行，交易金额，备注
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>1 选择导入文档</p>
                    </td>
                    <td>
                        <asp:FileUpload ID="UploadExcel" runat="server"></asp:FileUpload>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>2 开始导入文档</p>
                    </td>
                    <td>
                        <asp:Button ID="Upload_导入" runat="server" Text="导入" class="btn btn-info btn-fw" OnClick="Button_导入_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>3 检查确认信息</p>
                    </td>
                    <td>
                        <asp:Button ID="Button_识别银行卡名称" runat="server" Text="识别银行卡名称" class="btn btn-info btn-fw" OnClick="Button_识别银行卡名称_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>4 输入支付验证</p>
                    </td>
                    <td>
                        <%
                            string Cookie_UserName = null;
                            //檢測cookie
                            if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
                                Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
                            if (Cookie_UserName != null)
                                using (var db = (new DBClient()).GetClient())
                                {
                                    var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == Cookie_UserName).First();
                                    if (data.二步验证状态 == true)
                                    {
                        %>
                        <p>输入google验证密码 :<asp:TextBox ID="TextGoogleValidate" runat="server"></asp:TextBox></p>
                        <%
                                    }
                                }
                        %>
                        <p>输入支付密码 :<asp:TextBox ID="TextBox_输入支付密码" runat="server"></asp:TextBox></p>
                        <p>输入KEY密码 :<asp:TextBox ID="TextBox_输入KEY密码" runat="server"></asp:TextBox></p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <p>5 确认订单发起</p>
                    </td>
                    <td>
                        <asp:Button ID="Button_确认订单发起" runat="server" Text="确认订单发起" class="btn btn-info btn-fw" OnClick="Button_确认订单发起_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
    </div>
</asp:Content>
