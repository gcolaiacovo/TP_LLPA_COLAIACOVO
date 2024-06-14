<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductoDetail.aspx.cs" Inherits="ProductoDetail" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Detalles del producto</title>
    <link href="Estilos/ProductoDetail.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <uc:Navbar ID="Navbar1" runat="server" />
        <div class="container">
            <div class="product-details">
                <asp:LinkButton runat="server" PostBackUrl="Default.aspx"><asp:Label ID="Label1" runat="server" CssClass="product-brand">< Atrás</asp:Label></asp:LinkButton>
                <div class="product-image">
                    <asp:Image ID="imgProducto" runat="server" CssClass="img-producto" />
                </div>
                <div class="product-info">
                    <h2>
                        <asp:Label ID="lblNombre" runat="server" CssClass="product-name"></asp:Label>
                    </h2>
                    <p>
                        <asp:Label ID="lblMarca" runat="server" CssClass="product-brand"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblPrecio" runat="server" CssClass="product-price"></asp:Label>
                    </p>
                    <p>
                        <asp:Label ID="lblStock" runat="server" CssClass="product-stock"></asp:Label>
                    </p>
                    <asp:Button ID="Button1" runat="server" Text="Agregar al carrito" CssClass="btn-add-to-cart" OnClick="btnAgregarCarrito_Click" />
                </div>
            </div>
            <div class="product-description">
                <h3>Descripción del Producto</h3>
                <p>
                    <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                </p>
            </div>
        </div>
    </form>
    <f:Footer ID="footer" runat="server" />
</body>
</html>
