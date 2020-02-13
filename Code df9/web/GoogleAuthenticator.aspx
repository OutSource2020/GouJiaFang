<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleAuthenticator.aspx.cs" Inherits="web1.GoogleAuthenticator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TextBox ID="TextBox1" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" class="btn btn-info btn-fw" OnClick="Button2_Click" Text="生成密匙" />
            <asp:Button ID="Button1" runat="server" Text="生成二维码" class="btn btn-info btn-fw" OnClick="Button1_Click" />
            <hr />
            <strong>Account Secret Key (randomly generated):</strong>
            <asp:Label runat="server" ID="lblSecretKey"></asp:Label>
            <hr />
            <strong>Setup QR Code:</strong><br />
            <asp:Image ID="imgQrCode" runat="server" /><br />
            <br />
            <strong>Manual Setup Code: </strong>
            <asp:Label runat="server" ID="lblManualSetupCode"></asp:Label>
            <hr />
            Validate Code:
            <asp:TextBox runat="server" ID="txtCode" Width="160px" MaxLength="30" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:Button runat="server" ID="btnValidate" Text="Validate My Code!" OnClick="btnValidate_Click" /><br />
            <asp:Label runat="server" Font-Bold="true" ID="lblValidationResult"></asp:Label>


        </div>
    </form>
</body>
</html>
