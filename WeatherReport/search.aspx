<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="WebApplication1.search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            padding: 0;
            margin: 0;
            background-image: url("searchbg.jpg");
            background-size: cover;
            background-repeat: no-repeat;
        }

        .top {
            margin-top: 20px;
        }

        .search {
            font-size: 15px;
            border-radius: 5px;
            padding-left: 10px;
        }

        .result {
           margin-top: 70px;
        }

        .weather-info {
            border: 2px solid black;
            text-align: center;
            align-items: center;
            padding: 20px;
            margin-right: 60px;
            margin-bottom: 30px;
            background-image: linear-gradient(rgba(255,255,255,.3), rgba(255,255,255,.3)), url("infobg.jpg");
            background-repeat: no-repeat;
            font-family: 'Barlow Condensed', sans-serif;
            font-size: 20px;
            font-weight: bold;
            width: 30%;
        }

        .search:hover {
            background-color: darkgrey;
            transition: 0.2s ease;
        }
        
        .search:not(:hover) {
            transition: 0.2s ease;
        }

        .srch {
            font-size: 20px;
            font-weight: bold;
            cursor: pointer;
            margin-top: 10px;
        }

        .srch:hover {
            color: white;
            font-weight: bold;
            background-color: black;
            transition: 0.3s ease;
        }

        .srch:not(:hover) {
            transition: 0.3s ease;
        }

        .back {
            margin-right: 60px;
            font-size: 20px;
            font-weight: bold;
            cursor: pointer;
            margin-top: 10px;
            padding: 10px;
        }

        .srch:hover {
            background-color: darkgray;
            transition: 0.3s ease;
        }

        .srch:not(:hover) {
            transition: 0.3s ease;
        }


    </style>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Barlow+Condensed:wght@300&family=Inter:wght@500&family=Josefin+Sans&display=swap" rel="stylesheet">

    <script src="https://kit.fontawesome.com/078b828167.js" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="top">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text="<- Back" OnClick="Button2_Click" CssClass="back" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txt1" runat="server" Height="39px" Width="472px" CssClass="search" placeholder="Search for a City.."></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Height="40px" OnClick="Button1_Click" Text="Search" CssClass="srch" Width="107px" />
            <br />
        </div>

        <center>
            &nbsp;
            <div class="result" id="tblweather" runat="server">
            </div>
            <p>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" UseSubmitBehavior="false"></asp:LinkButton>
            </p>
        </center>

    </form>
</body>
</html>
