<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="WebApplication1.signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
            background-image: url("logbg.jpg");
            background-size: cover;
            background-repeat: no-repeat;
        }

        .user {
            font-size: 20px;
            padding: 8px;
            border-radius: 10px;
        }

        .user:hover {
            background-color: darkgrey;
            transition: 0.2s ease;
        }
        
        .user:not(:hover) {
            transition: 0.2s ease;
        }

        .login {
            font-size: 25px;
            padding-left: 20px;
            padding-right: 20px;
            padding-top: 5px;
            padding-bottom: 5px;
            margin-top: 20px;
            border-radius: 5px;
            cursor: pointer;
            border: none;
        }

        .login:hover {
            background-color: darkgrey;
            transition: 0.2s ease;
        }
        
        .login:not(:hover) {
            transition: 0.2s ease;
        }

        .log {
            margin-top: 20px;
            color: white;
        }

        .top {
            font-size: 40px;
        }

        .gender {
            font-size: 25px;
            margin-top: 0px;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div class="log">
                <p class="top">SIGN UP</p>

                <asp:TextBox ID="txt1" runat="server" CssClass="user" placeholder="Username" Height="30px" Width="264px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is Required" ControlToValidate="txt1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
                <br />

                <asp:TextBox ID="mail" runat="server" CssClass="user" placeholder="Email" Height="30px" Width="264px"></asp:TextBox>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="mail" ErrorMessage="Invalid Email ID" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <br />
                <br />
                <br />

                <asp:TextBox ID="pass" runat="server" TextMode="Password" CssClass="user" placeholder="Password" Height="30px" Width="264px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is Required" ControlToValidate="pass" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
                <br />

                <asp:TextBox ID="conpass" runat="server" TextMode="Password" CssClass="user" placeholder="Confirm Password" Height="30px" Width="264px"></asp:TextBox>
            
                <br />
            
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="conpass" ControlToValidate="pass" ErrorMessage="Password doesn't match" ForeColor="Red"></asp:CompareValidator>
                <br />
                <br />
            
                <br />
                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="gender" Text="Male" CssClass="gender" />
&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="gender" Text="Female" CssClass="gender" />
&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="gender" Text="Other" CssClass="gender"/>
                <br />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" CssClass="login" Text="Create Account" />
            
                <br />
                <br />
                <asp:Label ID="lbl1" runat="server" ForeColor="Red" Text=" "></asp:Label>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ForeColor="white" Font-Size="20px">Login into an existing account</asp:LinkButton>
                <br />
                <asp:Label ID="msg" runat="server" Text=" "></asp:Label>
            
            </div>
        </center>
    </form>
</body>
</html>
