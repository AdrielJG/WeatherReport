<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication1.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            margin: 0;
            padding: 0;
            background-image: url("logbg.jpg");
            background-size: cover;
            background-repeat: no-repeat;
        }

        .log {
            margin-top: 200px;
            color: white;
        }

        .top {
            font-size: 50px;
        }

        .user {
            font-size: 30px;
            padding: 10px;
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

        .acc:hover {
            font-size: 35px;
            transition: 0.2s ease;
        }

        .acc:not(:hover) {
            transition: 0.2s ease;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div class="log">
                <p class="top">LOGIN</p>
            
                <asp:TextBox ID="txt1" runat="server" CssClass="user" placeholder="Username"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="txt2" runat="server" TextMode="Password" placeholder="Password" CssClass="user"></asp:TextBox>
            
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" CssClass="login" />
            
                <br />
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="acc" ForeColor="white" Font-Size="25px">Create Account</asp:LinkButton>
                <br />
                <asp:Label ID="msg" runat="server" Text=" "></asp:Label>
            
            </div>
        </center>
    </form>
</body>
</html>
