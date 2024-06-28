<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administracion.aspx.cs" Inherits="Administracion" %>

<%@ Register Src="~/SideBar.ascx" TagPrefix="s" TagName="SideBar" %>
<%@ Register Src="~/NavBar.ascx" TagPrefix="n" TagName="NavBar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Estilos/Estilos.css" rel="stylesheet" />
    <link href="Estilos/Administracion.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">

        <div class="container">
            <n:NavBar ID="navBar" runat="server" />
            <s:SideBar ID="sideBar" runat="server" />

            <div class="main-container">
                <div class="table-container">
                    <table id="tablaBitacora">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Usuario</th>
                                <th>Descripcion</th>
                                <th>Fecha</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var bitacora in ((List<LPPA_Colaiacovo_Entidades.Clases.Bitacora>)ViewState["Bitacoras"]))
                                { %>
                            <tr>
                                <td>
                                    <%= bitacora.Id %> 
                                </td>
                                <td>
                                    <%= bitacora.Usuario.Nombre %> <%= bitacora.Usuario.Apellido %>
                                </td>
                                <td>
                                    <%= bitacora.Descripcion %> 
                                </td>
                                <td>
                                    <%= bitacora.FechaCreado %> 
                                </td>
                            </tr>
                            <%}%>
                        </tbody>
                    </table>

                    <table id="tablaUsuarios">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Nombre</th>
                                <th>Apellido</th>
                                <th>Email</th>
                                <th>Rol</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var bitacora in ((List<LPPA_Colaiacovo_Entidades.Clases.Usuario>)ViewState["Usuarios"]))
                                { %>
                            <tr>
                                <td>
                                    <%= bitacora.Id %> 
                                </td>
                                <td class="truncated">
                                    <%= bitacora.Nombre %>
                                </td>
                                <td class="truncated">
                                    <%= bitacora.Apellido %> 
                                </td>
                                <td class="truncated" title="<%= bitacora.Email %>">
                                    <%= bitacora.Email %> 
                                </td>
                                <td>
                                    <%= bitacora.Rol %> 
                                </td>
                            </tr>
                            <%}%>
                        </tbody>
                    </table>

                    <table id="tablaProductos">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Nombre</th>
                                <th>Descripción</th>
                                <th>Marca</th>
                                <th>Categoría</th>
                                <th>Precio</th>
                                <th>Stock</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var bitacora in ((List<LPPA_Colaiacovo_Entidades.Clases.Producto>)ViewState["Productos"]))
                                { %>
                            <tr>
                                <td>
                                    <%= bitacora.Id %> 
                                </td>
                                <td class="truncated" title="<%= bitacora.Nombre %>">
                                    <%= bitacora.Nombre %>
                                </td>
                                <td class="truncated" title="<%= bitacora.Descripcion %>">
                                    <%= bitacora.Descripcion %> 
                                </td>
                                <td class="truncated" title="<%= bitacora.Marca %>">
                                    <%= bitacora.Marca %> 
                                </td>
                                <td>
                                    <%= bitacora.CategoriaId.ToString() %> 
                                </td>
                                <td>$ <%= bitacora.Precio %> 
                                </td>
                                <td><%= bitacora.Stock %> 
                                </td>
                            </tr>
                            <%}%>
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </form>
    <script src="Scripts/Administracion.js"></script>
</body>
</html>
