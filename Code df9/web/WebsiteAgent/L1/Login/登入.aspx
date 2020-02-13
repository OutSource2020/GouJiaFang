<%@ Page Language="C#" AutoEventWireup="true" Title="登入代理L1" CodeBehind="登入.aspx.cs" Inherits="web1.WebsiteAgent.L1.Login.登入" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>




<%--https://codepen.io/colorlib/pen/rxddKy--%>
<style>

/*@import url(https://fonts.googleapis.com/css?family=Roboto:300);*/

.login-page {
  width: 360px;
  padding: 2% 0 0;
  margin: auto;
}
.form {
  position: relative;
  z-index: 1;
  background: #FFFFFF;
  max-width: 360px;
  margin: 0 auto 100px;
  padding: 45px;
  text-align: center;
  box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
}
.form input {
  font-family: "Roboto", sans-serif;
  outline: 0;
  background: #f2f2f2;
  width: 100%;
  border: 0;
  margin: 0 0 15px;
  padding: 15px;
  box-sizing: border-box;
  font-size: 14px;
}
.form button {
  font-family: "Roboto", sans-serif;
  text-transform: uppercase;
  outline: 0;
  background: #4CAF50;
  width: 100%;
  border: 0;
  padding: 15px;
  color: #FFFFFF;
  font-size: 14px;
  -webkit-transition: all 0.3 ease;
  transition: all 0.3 ease;
  cursor: pointer;
}
.form button:hover,.form button:active,.form button:focus {
  background: #43A047;
}
.form .message {
  margin: 15px 0 0;
  color: #b3b3b3;
  font-size: 12px;
}
.form .message a {
  color: #4CAF50;
  text-decoration: none;
}
.form .register-form {
  display: none;
}
.container {
  position: relative;
  z-index: 1;
  max-width: 500px;
  margin: 0 auto;
}
.container:before, .container:after {
  content: "";
  display: block;
  clear: both;
}
.container .info {
  margin: 50px auto;
  text-align: center;
}
.container .info h1 {
  margin: 0 0 15px;
  padding: 0;
  font-size: 36px;
  font-weight: 300;
  color: #1a1a1a;
}
.container .info span {
  color: #4d4d4d;
  font-size: 12px;
}
.container .info span a {
  color: #000000;
  text-decoration: none;
}
.container .info span .fa {
  color: #EF3B3A;
}
body {
  background: #FFFFF2; /* fallback for old browsers */
  background: -webkit-linear-gradient(right, #FFFFF2, #88dba3);
  background: -moz-linear-gradient(right, #FFFFF2, #88dba3);
  background: -o-linear-gradient(right, #FFFFF2, #88dba3);
  background: linear-gradient(to left, #FFFFF2, #88dba3);
  font-family: "Roboto", sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;      
}

</style>


<%--https://www.w3schools.com/howto/tryit.asp?filename=tryhow_css_register_form--%>
<style>
body {
  font-family: Arial, Helvetica, sans-serif;
  /*background-color: black;*/
}

* {
  box-sizing: border-box;
}

/* Add padding to containers */
.container {
  padding: 16px;
  background-color: white;
}

/* Full-width input fields */
input[type=text], input[type=password] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
}

input[type=text]:focus, input[type=password]:focus {
  background-color: #ddd;
  outline: none;
}

/* Overwrite default styles of hr */
hr {
  border: 1px solid #f1f1f1;
  margin-bottom: 25px;
}

/* Set a style for the submit button */
.registerbtn {
  background-color: #4CAF50;
  color: white;
  padding: 16px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

.registerbtn:hover {
  opacity: 1;
}

/* Add a blue text color to links */
a {
  color: dodgerblue;
}

/* Set a grey background color and center the text of the "sign in" section */
.signin {
  background-color: #f1f1f1;
  text-align: center;
}
</style>





    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>


<%--<div class="login-page">
  <div class="form">
    <form class="register-form">
      <input type="text" placeholder="name"/>
      <input type="password" placeholder="password"/>
      <input type="text" placeholder="email address"/>
      <button>create</button>
      <p class="message">Already registered? <a href="#">Sign In</a></p>
    </form>
    <form class="login-form">
      <input type="text" placeholder="username"/>
      <input type="password" placeholder="password"/>
      <button>login</button>
      <p class="message">Not registered? <a href="#">Create an account</a></p>
    </form>
  </div>
</div>--%>



<%--<div class="container">
  <form action="/action_page.php">
    <label for="fname">First Name</label>
    <input type="text" id="fname" name="firstname" placeholder="Your name..">

    <label for="lname">Last Name</label>
    <input type="text" id="lname" name="lastname" placeholder="Your last name..">

    <label for="country">Country</label>
    <select id="country" name="country">
      <option value="australia">Australia</option>
      <option value="canada">Canada</option>
      <option value="usa">USA</option>
    </select>

    <label for="subject">Subject</label>
    <textarea id="subject" name="subject" placeholder="Write something.." style="height:200px"></textarea>

    <input type="submit" value="Submit">
  </form>
</div>--%>


<div class="login-page">
  <div>
    

    <div class="container">
    <h1>登入</h1>
    <%--<p>Please fill in this form to create an account.</p>--%>
    <hr>

    <%--<label for="email"><b>账号</b></label>--%>
    <%--<input type="text" name="email" required>--%>
    <asp:TextBox ID="TextBox_账号" runat="server" placeholder="账号" MaxLength="30"></asp:TextBox>

    <%--<label for="psw"><b>密码</b></label>--%>
    <%--<input type="password" placeholder="Enter Password" name="psw" required>--%>
    <asp:TextBox ID="TextBox_密码" runat="server" placeholder="密码" MaxLength="30" TextMode="Password"></asp:TextBox>

    <%--<label for="psw-repeat"><b>KEY</b></label>--%>
    <%--<input type="password" placeholder="Repeat Password" name="psw-repeat" required>--%>
    <asp:TextBox ID="TextBox_回答" runat="server" placeholder="KEY" MaxLength="30"></asp:TextBox>

    <hr>

    <%--<p>By creating an account you agree to our <a href="#">Terms & Privacy</a>.</p>--%>

    <asp:CheckBox ID="CheckBox_保存验证" runat="server" Text="保存验证" />
    <asp:Button ID="Button_登入" runat="server" OnClick="Button_登入_Click" Text="登入" class="registerbtn" UseSubmitBehavior="false" OnClientClick="this.disabled=true;this.value='正在登入...';" />
  </div>



  </div>
</div>






        </div>
    </form>
</body>
</html>