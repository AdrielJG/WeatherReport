<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="letsee.aspx.cs" Inherits="WebApplication1.letsee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
       .data {
           margin-top: 50px;
           display: grid;
           grid-template-columns: auto auto auto;
           align-content: center;
           align-items: center;
           padding-left: 50px;
       }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="data" id="aweather" runat="server">
              <asp:Timer ID="weatherUpdateTimer" runat="server" Interval="3000" OnTick="weatherUpdateTimer_Tick"></asp:Timer>
              <asp:UpdatePanel ID="weatherUpdatePanel" runat="server">
                    <ContentTemplate>
            <!-- Place your weather information here -->
                    </ContentTemplate>
                                <Triggers>
        <asp:AsyncPostBackTrigger ControlID="weatherUpdateTimer" EventName="Tick" />
    </Triggers>
    </asp:UpdatePanel>
</div>
    </form>
</body>
</html>
