<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Productos.ascx.cs" Inherits="Productos" %>

<%@ Register Src="~/UserReview.ascx" TagPrefix="ur" TagName="UserReview" %>
<%@ Register Src="~/CardProducto.ascx" TagPrefix="cp" TagName="CardProducto" %>

<link href="Estilos/Productos.css" rel="stylesheet" />

<h1 id="tituloProductos" style="text-align: center; margin-bottom: 50px">Productos</h1>
<div class="carousel" id="productCarousel">
    <div class="carousel-inner">
        <asp:Repeater ID="repeaterProductos" runat="server">
            <ItemTemplate>
                <div class="carousel-item">
                    <cp:CardProducto UrlImagen='<%# Eval("UrlImagen") %>' runat="server" Nombre='<%# Eval("Nombre") %>' Precio='<%# Eval("Precio") %>' IdProducto='<%# Eval("Id") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <!-- Agrega más productos según sea necesario -->
</div>

<div class="separador grande">
    <br />
</div>

<h1 id="tituloServicios" style="text-align: center; margin-bottom: 50px">Servicios</h1>
<div class="carousel" id="serviciosCarousel">
    <div class="carousel-inner">
        <asp:Repeater ID="repeaterServicios" runat="server">
            <ItemTemplate>
                <div class="carousel-item">
                    <cp:CardProducto UrlImagen='<%# Eval("UrlImagen") %>' runat="server" Nombre='<%# Eval("Nombre") %>' Precio='<%# Eval("Precio") %>' IdProducto='<%# Eval("Id") %>' />
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <!-- Agrega más productos según sea necesario -->
</div>

<div class="separador grande">
    <br />
</div>

<div class="reviews-container">
    <p class="text-center">
        Que dicen nuestros clientes
    </p>

    <div class="review">
        <ur:UserReview ID="ur1" UrlImagen="https://i.pravatar.cc/80?img=1" runat="server" Texto="Sus productos son de la mejor calidad! Volveria a comprar!!" Nombre="Pedro Gimenez" CacheBuster='<%= DateTime.Now.Ticks %>' />
        <ur:UserReview ID="ur2" UrlImagen="https://i.pravatar.cc/80?img=2" runat="server" Texto="Compre proteinas y siempre vinieron en forma y estado, además contrate el servicio de entrenamiento personalizado y fue lo mejor!" Nombre="Pablo Lopez" />
        <ur:UserReview ID="ur3" UrlImagen="https://i.pravatar.cc/80?img=4" runat="server" Texto="" Nombre="Leandro Peralta" />
        <ur:UserReview ID="ur4" UrlImagen="https://i.pravatar.cc/80?img=5" runat="server" Texto="La mejor compra que hice" Nombre="Sol Invierno" />
        <ur:UserReview ID="ur5" UrlImagen="https://i.pravatar.cc/80?img=6" runat="server" Texto="Muchas gracias por la atencion dada en su showroom, y por las recomendaciones" Nombre="Manuel Belgrano" />
    </div>
</div>

<script src="Scripts/Productos.js"></script>
