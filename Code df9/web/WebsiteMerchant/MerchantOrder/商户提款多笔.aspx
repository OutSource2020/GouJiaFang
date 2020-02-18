<%@ Page Title="商户提款多笔" Language="C#" MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" AutoEventWireup="true" CodeBehind="商户提款多笔.aspx.cs" Inherits="web1.WebsiteMerchant.商户订单.商户提款多笔" %>

<%@ Import NameSpace="System.Data.SqlClient" %>
<%@ Import NameSpace="System.Data" %>

<%@ Import NameSpace="web1" %>
<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>

<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">


<div  class="auto-style1" >


    <div>
        <asp:gridview ID="Gridview1" runat="server" ShowFooter="true" AutoGenerateColumns="false">
            <Columns>
            <asp:BoundField DataField="RowNumber" HeaderText="" />
            <asp:TemplateField HeaderText="交易方卡号">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"  Width="160px" MaxLength="30" OnTextChanged="TextBox_交易方卡号_TextChanged" AutoPostBack="True" onKeyPress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="交易方姓名">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="交易方银行">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server"  Width="160px" MaxLength="30" Enabled="False"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="交易金额">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server"  Width="160px" MaxLength="30" OnTextChanged="TextBox_交易金额_TextChanged" AutoPostBack="True" onkeypress="return validateFloatKeyPress(this,event);" ></asp:TextBox>
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
        </asp:gridview>
    </div>


        <div id="余额和比率">
            <table class="auto-style1">
                <tr>
                    <td colspan="5">
                        <ul>
                            <li>注意 "提款余额"和"手续费余额"不足以支付时将不会成功发出提款</li>
                            <li>注意 每笔不能小于"最低提款金额"或者大于"最高提款金额"</li>
                            <li>注意 提款订单数较多时每次提交不超出10单</li>
                        </ul>
                    </td>
                </tr>
                <tr>
                               <p></p>
  <asp:fileupload ID="UploadExcel" runat="server"></asp:fileupload>
         <asp:Button ID="Upload_提款订单文档" runat="server" Text="导入提款订单文档" class="btn btn-info btn-fw" OnClick="Button_提款订单文档导入_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
        <br>
                    <p></p>
  <asp:fileupload ID="UploadTxt" runat="server"></asp:fileupload>
         <asp:Button ID="Upload_提款订单文本" runat="server" Text="导入提款订单文本" class="btn btn-info btn-fw" OnClick="Button_提款订单文本导入_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />

        
                    </tr>
                <tr>
                    <td style="width: 20%">&nbsp;</td>
                    <td style="width: 20%">金额</td>
                    <td>手续费</td>
                    <td>限制最低提款金额</td>
                    <td>限制最高提款金额</td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        账户信息</td>
                    <td style="width: 20%">
                        账户提款余额:
                        <asp:Label ID="Label_提款余额" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        手续费余额: 
                        <asp:Label ID="Label_手续费余额" runat="server" Text="0"></asp:Label> 
                        单笔手续费: 
                        <asp:Label ID="Label_单笔手续费" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        最低提款金额:
                        <asp:Label ID="Label_账户最低提款金额" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        最高提款金额:
                        <asp:Label ID="Label_账户最高提款金额" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        预估信息 
                    </td>
                    <td style="width: 20%">
                        预估订单金额: 
                        <asp:Label ID="Label_预计提款金额" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        预估手续费金额: 
                        <asp:Label ID="Label_预计提款手续费" runat="server" Text="0"></asp:Label>
                    </td>
                    <td style="width: 20%">

                    </td>
                    <td style="width: 20%">

                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="Button_刷新预估" runat="server" Text="刷新预估(检查)" class="btn btn-info btn-fw" OnClick="Button_刷新预估_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />
                    </td>
                </tr>

                <%
                  string Cookie_UserName=null;
                  //檢測cookie
                  if (System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"] != null)
                      Cookie_UserName = ClassLibrary1.ClassAccount.cookie解密(System.Web.HttpContext.Current.Request.Cookies["PPusernameMerchant"]["username"]);
                  if(Cookie_UserName!=null)
                      using (var db = (new DBClient()).GetClient())
                      {
                          var data = db.Queryable<Sugar.Enties.table_商户账号>().Where(it => it.商户ID == Cookie_UserName).First();
                          if(data.二步验证状态 == true){ 
                 %>
                 <tr>
                    <td colspan="5">
                        输入google验证密码 :
                        <asp:TextBox ID="TextGoogleValidate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                <%
                          }

                      }


                  %>

                <tr>
                    <td colspan="5">
                        输入支付密码 :
                        <asp:TextBox ID="TextBox_输入支付密码" runat="server"></asp:TextBox>
                        <asp:Button ID="Button_批量发起提款订单" runat="server" Text="批量发起提款订单" class="btn btn-info btn-fw" OnClick="Button_批量发起提款订单_Click" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='处理中...';" />

                    </td>
                </tr>

            </table>

        </div>


</div>
     <div>

    </div>
    
    <div class="__web-inspector-hide-shortcut__"><p hidden>
    <asp:TextBox ID="创建方式_Text"  runat="server" Text="手动"></asp:TextBox></p>
     </div>
        
</asp:Content>


