<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gracias.aspx.cs" Inherits="Gracias" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Gracias</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc:Navbar ID="Navbar1" runat="server" />

        <div class="container">
            <h1 style="text-align: center; height: calc(100vh - 181px);">Gracias por su compra!!
            </h1>
        </div>

        <f:Footer ID="footer" runat="server" />
    </form>
</body>
</html>
