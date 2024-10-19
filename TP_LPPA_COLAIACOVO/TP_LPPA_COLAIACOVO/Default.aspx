<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/NavBar.ascx" TagPrefix="uc" TagName="Navbar" %>
<%@ Register Src="~/Productos.ascx" TagPrefix="up" TagName="Productos" %>
<%@ Register Src="~/Footer.ascx" TagPrefix="f" TagName="Footer" %>
<%@ Register Src="~/About.ascx" TagPrefix="a" TagName="About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gymfit - Tus complementos para entrenar</title>
    <link href="Estilos/Default.css" rel="stylesheet" />
</head>
<body>
    <form runat="server">
        <uc:Navbar ID="Navbar1" runat="server" />

        <div class="video-container">
            <video autoplay loop muted disablepictureinpicture src="Resources/background-video.mp4" class="video-bg">
            </video>
            <div class="content">
                <div class="left-content">
                    <h1>Suplementos deportivos</h1>
                    <ul>
                        <li>Mejora el rendimiento deportivo</li>
                        <li>Aumenta la energía y la resistencia</li>
                        <li>Ayuda en la recuperación muscular</li>
                        <li>Facilita la pérdida de peso y la definición muscular</li>
                        <li>Proporciona nutrientes esenciales para una óptima salud</li>
                    </ul>
                </div>
                <div class="right-content">
                    <h1>Entrenamiento personalizado</h1>
                    <ul>
                        <li>Entrenamiento adaptado a las necesidades individuales</li>
                        <li>Planes de entrenamiento personalizados según objetivos</li>
                        <li>Asesoramiento nutricional y dietético</li>
                        <li>Monitoreo y seguimiento del progreso</li>
                        <li>Motivación y apoyo continuo</li>
                    </ul>
                </div>
            </div>
        </div>

        <div class="container">

            <div class="separador grande">
                <br />
            </div>
            <up:Productos ID="Productos" runat="server" />

            <div class="separador grande">
                <br />
            </div>

            <a:About ID="About" runat="server" />

            <div class="separador grande">
                <br />
            </div>

        </div>
        <f:Footer ID="footer" runat="server" />
    </form>
</body>
</html>
