<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Error</title>
    <link href="Estilos/Error.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="error-layout">

            <div class="img-container">
                <h2>Tenemos problemas con la integridad de la informacion de nuestro sitio.
                </h2>

                <img src="Resources/errorIcon.png" class="logo" />
                <div style="font-family: Roboto; text-align: center">
                    <asp:Literal ID="mensajeError" runat="server" Mode="PassThrough"></asp:Literal>
                </div>

                <% if (IsAdmin)
                    { %>
                <asp:Button ID="btnRestaurar" runat="server" Text="Restaurar" OnClick="btnRestaurar_Click" class="restaurar-button" />
                <% } %>
            </div>
        </div>
    </form>
</body>
</html>
