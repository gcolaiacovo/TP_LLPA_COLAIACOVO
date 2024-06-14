<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CardProducto.ascx.cs" Inherits="CardProducto" %>

<div class="card">
    <asp:LinkButton runat="server" ID="linkProducto">
        <asp:Image ID="imgProducto" runat="server"></asp:Image>
        <div class="price-overlay">
            <div class="original-price">
                $
                <asp:Literal ID="txtPrecioSinOferta" runat="server"></asp:Literal>
            </div>
            <div class="offer-price">
                $
                <asp:Literal ID="txtPrecio" runat="server"></asp:Literal>
            </div>
        </div>
        <div runat="server" class="card-name">
            <asp:Literal ID="txtNombre" runat="server"></asp:Literal>
        </div>
    </asp:LinkButton>
</div>
