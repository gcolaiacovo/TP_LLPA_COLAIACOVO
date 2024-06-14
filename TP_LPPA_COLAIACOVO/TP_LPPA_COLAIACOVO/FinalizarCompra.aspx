<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalizarCompra.aspx.cs" Inherits="FinalizarCompra" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Finalizar Compra</title>
    <link href="Estilos/FinalizarCompra.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc:Navbar ID="Navbar1" runat="server" />

        <div class="container">
            <h1>Productos en carrito
            </h1>

            <div class="main-container">
                <div class="table-container">
                    <table>
                        <thead>
                            <tr>
                                <th>Nombre</th>
                                <th>Descripcion</th>
                                <th>Marca</th>
                                <th>Cantidad</th>
                                <th>Precio Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            <% foreach (var bitacora in ((List<LPPA_Colaiacovo_Entidades.DTO.ProductoCarritoDTO>)ViewState["ProductosCarrito"]))
                                { %>
                            <tr>
                                <td>
                                    <%= bitacora.Nombre %> 
                                </td>
                                <td>
                                    <%= bitacora.Descripcion %>
                                </td>
                                <td>
                                    <%= bitacora.Marca %> 
                                </td>
                                <td>
                                    <%= bitacora.Cantidad %> 
                                </td>
                                <td>$ <%= bitacora.PrecioTotal %> 
                                </td>
                            </tr>
                            <%}%>
                        </tbody>
                    </table>
                </div>
            </div>

            <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" CssClass="btn-lindo" />
        </div>

        <f:Footer ID="footer" runat="server" />
    </form>
</body>
</html>
