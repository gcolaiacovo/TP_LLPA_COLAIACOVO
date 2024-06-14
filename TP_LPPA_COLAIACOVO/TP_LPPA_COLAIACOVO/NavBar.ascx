<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavBar.ascx.cs" Inherits="NavBar" %>
<link href="Estilos/Estilos.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" />
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap" rel="stylesheet">
<link href="Estilos/Navbar.css" rel="stylesheet" />
<%@ Register Src="~/CambiarContraseña.ascx" TagPrefix="cs" TagName="CambiarContraseña" %>

<nav class="navbar">
    <div class="navbar-brand"><a class="nav-link" href="Default.aspx">Gymfit</a></div>
    <ul class="navbar-nav">
        <li class="nav-item">
            <a class="nav-link" href="Default.aspx#tituloProductos" onclick="scrollToSection('tituloProductos')">Productos</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="Default.aspx#tituloServicios">Servicios</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="Default.aspx#tituloAcercaDe">Acerca de</a>
        </li>
    </ul>
    <div class="navbar-icons">
        <a href="#" onclick="toggleUserMenu(event)"><i class="fas fa-user"></i></a>
        <ul class="user-menu" id="userMenu">
            <% if (IsAdmin)
                {
            %>
            <li>
                <a href="Administracion.aspx">Administración</a>
            </li>
            <% } %>
            <% if (Request.Cookies["UsuarioLogueado"] != null)
                {
            %>
            <li>
                <a href="#" onclick="abrirModal()">Cambiar contraseña</a>
            </li>
            <li>
                <asp:LinkButton runat="server" OnClick="btnServerSide_Click">Cerrar sesión</asp:LinkButton>
            </li>
            <% } %>
            <% else
                { %>
            <li><a href="Login.aspx">Iniciar Sesión</a></li>
            <li><a href="Registro.aspx">Registro</a></li>
            <% } %>
        </ul>
        <a href="FinalizarCompra.aspx"><i class="fas fa-shopping-cart"></i></a>
        <span id="cartItemCount" class="cart-item-count">
            <asp:Literal runat="server" ID="itemsCarrito"></asp:Literal>
        </span>
    </div>
</nav>

<cs:CambiarContraseña ID="cs1" runat="server" />

<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/js/fontawesome.min.js"></script>

<script>
    function scrollToSection(sectionId) {
        var section = document.getElementById(sectionId);
        if (section) {
            window.scrollTo({
                top: section.offsetTop - 200,
                behavior: 'smooth' // Desplazamiento suave
            });
        }
    }

    function toggleUserMenu(event) {
        event.preventDefault();
        var userMenu = document.getElementById('userMenu');
        if (userMenu.style.display === 'block') {
            userMenu.style.display = 'none';
        } else {
            userMenu.style.display = 'block';
        }
    }

    function actualizarItemsCarrito() {
        var productosCookie = getCookie("ItemsEnCarrito");
        var cantidad = productosCookie ? JSON.parse(productosCookie).length : 0;
        document.getElementById('cartItemCount').innerText = cantidad.toString();
    }

    function getCookie(name) {
        var cookieName = name + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var cookieArray = decodedCookie.split(';');
        for (var i = 0; i < cookieArray.length; i++) {
            var cookie = cookieArray[i].trim();
            if (cookie.indexOf(cookieName) == 0) {
                return cookie.substring(cookieName.length, cookie.length);
            }
        }
        return null;
    }

    // Cerrar el menú de usuario si se hace clic fuera de él
    window.onclick = function (event) {
        var userMenu = document.getElementById('userMenu');
        if (!event.target.matches('.fas.fa-user') && !event.target.matches('.user-menu')) {
            userMenu.style.display = 'none';
        }

        actualizarItemsCarrito();
    }
</script>
