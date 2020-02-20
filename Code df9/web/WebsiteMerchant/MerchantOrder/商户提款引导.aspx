<%@ Page Language="C#" 
       MasterPageFile="~/WebsiteMerchant/SiteTemplateMerchant.Master" 
       AutoEventWireup="true"
    CodeBehind="商户提款引导.aspx.cs" 
    Inherits="web1.WebsiteMerchant.MerchantOrder.商户提款引导" %>

<asp:Content ID="Content_NR1" ContentPlaceHolderID="ContentPlaceHolder_NR1" runat="server">

</asp:Content>


<asp:Content ID="Content_NR2" ContentPlaceHolderID="ContentPlaceHolder_NR2" runat="server">
       <style>
        td {text-align:center}
         table,table tr th, table tr td { border:3px solid #0094ff; }
    </style>
    <div style="margin-right: 10%;margin-left: 10%;">  
        <h1>商户提款引导</h1>
    <table class="table table-striped"   style="text-align:center;width: 100%;">

        <tbody>
            <tr>
                <td>文档导入</td>
                <td>
                    <asp:Button ID="Button_文档导入" runat="server" Text="文档导入" class="btn btn-info btn-fw" OnClick="Button_文档导入_Click" />
                </td>


            </tr>
            <tr>

                <td>文本导入</td>
                <td>
                    <asp:Button ID="Button_文本导入" runat="server" Text="文本导入" class="btn btn-info btn-fw" OnClick="Button_文本导入_Click" />
                </td>

            </tr>

                      <tr>

                <td>手动输入</td>
                <td>
                    <asp:Button ID="Button_手动输入" runat="server" Text="手动输入" class="btn btn-info btn-fw" OnClick="Button_手动输入_Click" />
                </td>

            </tr>
          

        </tbody>
    </table>

        </div>


</asp:Content>


