<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="WebApplication1.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            padding: 0;
            margin: 0;
        }

        .hero {
            position: relative;
            width: 100%;
            height: 100%;
            overflow: hidden; /* Hide overflow from absolutely positioned elements */
            background: url('background.mp4') no-repeat center center fixed;
            background-size: cover;
        }

        .container {
            top: 0;
            position: relative; /* Ensure the content is positioned relative to the hero div */
            z-index: 1; /* Ensure the content appears on top of the video background */
        }

        .bar {
            display: flex;
        }

        .login {
            font-size: 20px;
            margin-top: 30px;
            font-weight: bold;
            cursor: pointer;
        }

            .login:hover {
                color: white;
                font-weight: bold;
                background-color: black;
                transition: 0.3s ease;
            }

            .login:not(:hover) {
                transition: 0.3s ease;
            }

        .menu {
            margin-right: 30px;
            margin-left: 10px;
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
            position: relative;
        }

        .bgvid {
            position: absolute;
            z-index: -1;
            top: 0;
            left: 0;
            min-height: 100%;
            min-width: 100%;
        }

        .weather-info {
            border: 2px solid black;
            text-align: center;
            align-items: center;
            padding: 20px;
            margin-right: 60px;
            margin-bottom: 30px;
            background-color: rgba(133, 133, 133, 0.3);
            color: white;
            font-family: 'Barlow Condensed', sans-serif;
            font-size: 20px;
            font-weight: bold;
        }

        .bar {
            background-color: #13207b;
            padding-top: 30px;
            padding-bottom: 30px;
        }

        .login {
            font-family: 'Josefin Sans', sans-serif;
            border-radius: 20px;
            border: none;
            margin-top: 0px;
            margin-left: 10px;
        }

        .menudiv {
            background-color: black;
        }

        .menu {
            padding: 10px;
            background-color: black;
            color: white;
            padding-left: 15px;
            padding-right: 15px;
            font-family: 'Inter', sans-serif;
        }

        .search {
            background-color: white;
            color: black;
            height: 100%;
            padding-top: 5px;
            padding-bottom: 5px;
            padding-left: 10px;
            padding-right: 10px;
            border-radius: 20px;
        }

            .search:hover {
                transition: 0.4s ease;
            }

            .search:not(:hover) {
                transition: 0.4s ease;
            }


        #Menu1 a {
            border-radius: 20px;
            padding-left: 15px;
            padding-right: 15px;
        }

            #Menu1 a:hover {
                background-color: white;
                color: black;
                cursor: pointer;
                transition: 0.2s ease;
            }

            #Menu1 a:not(:hover) {
                transition: 0.2s ease;
            }

        .welcome {
            font-family: 'Josefin Sans', sans-serif;
            font-size: 20px;
            color: white;
        }

        #btnLogout {
            padding-left: 15px;
            padding-right: 15px;
            padding-top: 5px;
            padding-bottom: 5px;
            margin-left: 30px;
            margin-top: 10px;
        }

        .logged {
            margin-left: 200px;
        }
    </style>
    <script src="https://kit.fontawesome.com/078b828167.js" crossorigin="anonymous"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="hero">
            <div class="container">

                <div class="bar">
                    <asp:Label ID="Label1" runat="server" Text="Site Name" CssClass="title" Width="69%"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
        <asp:Panel ID="LoginPanel" runat="server" Visible="true">
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Button1_Click" CssClass="login" Height="40px" Width="129px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLogup" runat="server" Text="Sign Up" OnClick="Button2_Click" CssClass="login" Height="40px" Width="129px" />
        </asp:Panel>
                    <asp:Panel ID="WelcomePanel" runat="server" Visible="false" CssClass="logged">
                        <asp:Label ID="lblWelcome" runat="server" CssClass="welcome"></asp:Label>
                        <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="login" OnClick="btnLogout_Click" />
                    </asp:Panel>
                </div>

                <div class="menudiv">
                    <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal">
                        <StaticMenuItemStyle CssClass="menu" />
                        <DynamicMenuItemStyle CssClass="menu-item" />
                        <Items>
                            <asp:MenuItem Text="Weather around the World" Value="Popular Cities" NavigateUrl="~/form.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Hottest Cities" Value="Hottest Cities" NavigateUrl="~/hottest.aspx"></asp:MenuItem>
                            <asp:MenuItem Text="Coldest Cities" Value="Coldest Cities" NavigateUrl="~/coldest.aspx"></asp:MenuItem>
                            <asp:MenuItem Text=' ' Value="Search" NavigateUrl="~/search.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/dashboard.aspx" Text="Bookmarked Cities" Value="Bookmarked Cities"></asp:MenuItem>
                        </Items>
                    </asp:Menu>
                </div>

                <div class="data" id="tblweather" runat="server">
                    <video autoplay loop muted class="bgvid">
                        <source src="background.mp4" type="video/mp4" />
                    </video>

                </div>

                <asp:Label ID="Label2" runat="server" Text=" "></asp:Label>
            </div>
        </div>

    </form>
</body>
</html>
