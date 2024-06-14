<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CambiarContraseña.ascx.cs" Inherits="CambiarContraseña" %>

<div id="modal" class="modal">
    <div class="modal-content">
        <span class="close" onclick="cerrarModal()">&times;</span>
        <h2>Cambiar Contraseña</h2>
        <label for="currentPassword">Contraseña Actual:</label>
        <asp:TextBox ID="currentPassword" runat="server" TextMode="Password" placeholder="Contraseña Actual" CssClass="form-control" Required="true" />
        <label for="newPassword">Nueva Contraseña:</label>
        <asp:TextBox ID="newPassword" runat="server" TextMode="Password" placeholder="Nueva Contraseña" CssClass="form-control" Required="true" />
        <asp:RegularExpressionValidator ID="revNewPassword" runat="server" ControlToValidate="newPassword" ValidationExpression="^.{8,}$" ErrorMessage="La contraseña debe tener al menos 8 caracteres" Display="Dynamic" CssClass="text-danger" />

        <label for="confirmNewPassword">Confirmar Nueva Contraseña:</label>
        <asp:TextBox ID="confirmNewPassword" runat="server" TextMode="Password" placeholder="Confirmar Nueva Contraseña" CssClass="form-control" Required="true" />
        <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToCompare="newPassword" ControlToValidate="confirmNewPassword" ErrorMessage="Las contraseñas no coinciden" Display="Dynamic" CssClass="text-danger" />
        <asp:Button ID="btnCambiarContraseña" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContraseña_Click" CssClass="btn btn-primary" />

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>

    </div>
</div>

<script type="text/javascript">
    // Función para abrir el modal
    function abrirModal() {
        document.getElementById('modal').style.display = 'block';
    }

    // Función para cerrar el modal
    function cerrarModal() {
        document.getElementById('modal').style.display = 'none';
    }

    // Agregar un listener para cerrar el modal cuando se hace clic fuera de él
    window.onclick = function (event) {
        var modal = document.getElementById('modal');
        if (event.target == modal) {
            modal.style.display = 'none';
        }
    }
</script>

<style>
    /* Estilos para el modal */
    .modal {
        display: none; /* Ocultar el modal por defecto */
        position: fixed; /* Posición fija */
        z-index: 99; /* Por encima de todo */
        left: 0;
        top: 0;
        width: 100%; /* Ancho completo */
        height: 100%; /* Altura completa */
        overflow: auto; /* Desplazamiento si es necesario */
        background-color: rgb(0,0,0); /* Fondo negro con opacidad */
        background-color: rgba(0,0,0,0.4); /* Fondo negro con opacidad */
        overflow: hidden;
    }

    /* Estilos para el contenido del modal */
    .modal-content {
        background-color: #fefefe;
        margin: 15% auto;
        padding: 20px;
        border: 1px solid #888;
        width: 600px;
        display: flex;
        gap: 15px;
        flex-direction: column;
    }

        .modal-content input[type='password'] {
            height: 30px;
        }

        .modal-content input[type='submit'] {
            height: 30px;
        }

    /* Estilos para el botón de cerrar */
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }

        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
        }

    .btn-primary {
        background-color: #007bff;
        color: white;
        border: none;
        padding: 10px 20px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
        border-radius: 4px;
    }

        .btn-primary:hover {
            background-color: #0056b3;
        }

    /* Estilo para el texto de error */
    .text-danger {
        color: #dc3545;
    }
</style>
