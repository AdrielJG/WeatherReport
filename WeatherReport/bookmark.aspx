<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookmark.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .bar {
            display: flex;
        }

        .login {
            font-size: 20px;
            margin-top: 30px;
        }

       .title {
           font-size: 100px;
           margin-left: 40px;
       }

       .menu {
           margin-right: 30px;
           margin-left: 10px;
       }

       .data {
           margin-top: 50px;
           display: grid;
           grid-template-columns: auto auto auto;
           align-content: center;
           align-items: center;
           padding-left: 50px;
       }

       .weather-info {
           border: 2px solid black;
           text-align: center;
           align-items: center;
           padding: 20px;
           margin-right: 60px;
           margin-bottom: 30px;
       }

    </style>
    <script src="https://kit.fontawesome.com/078b828167.js" crossorigin="anonymous"></script>
</head>
<body>
    
    <form id="form1" runat="server">
        
    <div class="bar">
        <asp:Label ID="Label1" runat="server" Text="Site Name" CssClass="title" Width="69%"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:Panel ID="LoginPanel" runat="server" Visible="true">
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Button1_Click" CssClass="login" Height="40px" Width="129px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLogup" runat="server" Text="Sign Up" OnClick="Button2_Click" CssClass="login" Height="40px" Width="129px" />
        </asp:Panel>
        <asp:Panel ID="WelcomePanel" runat="server" Visible="false">
            <asp:Label ID="lblWelcome" runat="server"></asp:Label>
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </asp:Panel>
   </div>
        
        
        <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal">
            <StaticMenuItemStyle CssClass="menu" />
            <Items>
                <asp:MenuItem Text="Weather around the World" Value="Popular Cities" NavigateUrl="~/form.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Hottest Cities" Value="Hottest Cities" NavigateUrl="~/hottest.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Coldest Cities" Value="Coldest Cities" NavigateUrl="~/coldest.aspx"></asp:MenuItem>
                <asp:MenuItem Text='<i class="fa-solid fa-magnifying-glass"></i>' Value="Search" NavigateUrl="~/search.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Bookmarked Cities" Value="Bookmarked Cities"></asp:MenuItem>
            </Items>
        </asp:Menu>

        <div class="data" id="tblweather" runat="server">

        </div>
        <asp:Label ID="Label2" runat="server" Text="Cities: "></asp:Label>
    </form>
</body>
</html>