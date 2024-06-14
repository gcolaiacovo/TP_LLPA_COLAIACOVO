<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registro.aspx.cs" Inherits="Registro" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Registro</title>
    <link href="Estilos/Registro.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <uc:Navbar ID="Navbar1" runat="server" />
        <div class="container">
            <h2>Formulario de Registro</h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
            <div class="form-group">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio" CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email no es válido" CssClass="text-danger" Display="Dynamic" ValidationExpression="\w+([-+.'']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            </div>
            <div class="form-group">
                <label for="txtPassword">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="La contraseña es obligatoria" CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtFirstName">Nombre</label>
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="El nombre es obligatorio" CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtLastName">Apellido</label>
                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="El apellido es obligatorio" CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtBirthDate">Fecha de Nacimiento</label>
                <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-control" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server" ControlToValidate="txtBirthDate" ErrorMessage="La fecha de nacimiento es obligatoria" CssClass="text-danger" Display="Dynamic" />
            </div>
            <asp:Button runat="server" OnClick="btnRegistro_Click" type="submit" class="btn btn-primary" Text="Registro" />
        </div>
    </form>
    <f:Footer ID="footer" runat="server" />
</body>
</html>
