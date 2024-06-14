<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Login</title>
    <link href="Estilos/Login.css" rel="stylesheet" />
    <link href="Estilos/Estilos.css" rel="stylesheet" />
</head>
<body>
    <div class="contenedor">
        <form id="form1" runat="server">
            <div style="margin: auto; width: 500px; margin-top: 100px">
                <h1 style="margin-bottom: 30px; color: darkorange;">Gymfit</h1>
                <h1>Iniciar Sesión</h1>
                Email&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario" ErrorMessage="El usuario es requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                <br />
                <br />
                Contraseña
            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContrasena" ErrorMessage="La contraseña es requerida" ForeColor="Red"></asp:RequiredFieldValidator>
                <p>
                    &nbsp;
                </p>
                <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" />
                <br />
                <br />
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </form>
    </div>
    <f:Footer ID="footer" runat="server" />

    <script type="text/javascript">
        function showAlert() {
            const urlParams = new URLSearchParams(window.location.search);
            if (urlParams.has('showAlert')) {
                alert("Usuario creado con éxito! Inicie sesión");
                removeURLParameter('showAlert'); // Quitar un solo parámetro
            }
        }

        function removeURLParameter(parameter) {
            const url = new URL(window.location.href);
            url.searchParams.delete(parameter);
            window.history.replaceState({}, document.title, url);
        }
        window.onload = showAlert;
    </script>
</body>
</html>
