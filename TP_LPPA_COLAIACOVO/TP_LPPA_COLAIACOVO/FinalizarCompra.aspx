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
                                <th></th>
                            </tr>
                        </thead>
                        <tbody id="productosCarritoPlaceholder" runat="server">
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="separador" style="border-top: 1px solid gray; margin-bottom: 30px"></div>

            <div id="formPago" class="form-container" runat="server">
                <h2>Formulario de Pago</h2>

                <asp:Label ID="lblPaymentMethod" runat="server" Text="Medio de Pago"></asp:Label>
                <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" CssClass="select-payment-method">
                    <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                    <asp:ListItem Value="3">Transferencia</asp:ListItem>
                    <asp:ListItem Value="1">Efectivo</asp:ListItem>
                    <asp:ListItem Value="2">Tarjeta</asp:ListItem>
                </asp:DropDownList>

                <!-- Detalles para Transferencia -->
                <div id="transferDetails" class="payment-details" runat="server">
                    <h3>Datos para Transferencia</h3>
                    <p><strong>CBU:</strong> 1234567890123456789012</p>
                    <p><strong>Alias:</strong> ZAPATO.PERCHA.ESPADA</p>
                    <p><strong>Dueño de la cuenta:</strong> Gino Colaiacovo</p>
                    <p><strong>TRANSFERIR DENTRO DE LAS 48HS. Y ENVIAR EL COMPROBANTE DE PAGO A pagos@gymfit.com O LA COMPRA SE CANCELA</strong></p>
                </div>

                <!-- Detalles para Efectivo -->
                <div id="cashDetails" class="payment-details" runat="server">
                    <h3>Pagar en efectivo</h3>
                    <p>Debe realizar el pago en el local antes de las 48 horas o la compra se cancela.</p>
                    <p>Horarios de atención: L a V de 08 a 17hs, Sábados de 08 a 12hs.</p>
                    <p><strong>Dirección del local:</strong> Calderón de la Barca 1625</p>
                </div>

                <!-- Detalles para Tarjeta -->
                <div id="cardDetails" class="payment-details" runat="server">
                    <h3>Datos de la Tarjeta</h3>
                    <asp:Label ID="lblCardNumber" runat="server" Text="Número de Tarjeta"></asp:Label>
                    <asp:TextBox ID="txtCardNumber" runat="server" MaxLength="16" Placeholder="1234 5678 9101 1121"></asp:TextBox>

                    <asp:Label ID="lblCardHolder" runat="server" Text="Titular de la Tarjeta"></asp:Label>
                    <asp:TextBox ID="txtCardHolder" runat="server" Placeholder="Nombre del Titular"></asp:TextBox>

                    <asp:Label ID="lblCardExpiry" runat="server" Text="Fecha de Expiración (MM/AA)"></asp:Label>
                    <asp:TextBox ID="txtCardExpiry" oninput="applyDateMask(this)"  runat="server" MaxLength="5" Placeholder="MM/AA"></asp:TextBox>

                    <asp:Label ID="lblCardCVV" runat="server" Text="CVV"></asp:Label>
                    <asp:TextBox ID="txtCardCVV" runat="server" MaxLength="3" Placeholder="123"></asp:TextBox>
                </div>

                <asp:Button ID="btnFinalizarCompra" runat="server" Text="Finalizar Compra" OnClick="btnFinalizarCompra_Click" CssClass="btn-lindo" />
            </div>
        </div>

        <f:Footer ID="footer" runat="server" />
    </form>

    <script type="text/javascript">
        function applyDateMask(input) {
            // Elimina cualquier carácter que no sea un número
            var value = input.value.replace(/\D/g, '');

            // Aplica la máscara (ejemplo: MM/YY)
            if (value.length >= 2) {
                value = value.substring(0, 2) + '/' + value.substring(2, 4); // Añade la barra después del mes
            }

            // Limita el tamaño a 7 caracteres (MM/YY)
            if (value.length > 5) {
                value = value.substring(0, 5);
            }

            // Actualiza el valor del input
            input.value = value;
        }
    </script>
</body>
</html>
