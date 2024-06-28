<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SideBar.ascx.cs" Inherits="SideBar" %>
<link href="Estilos/SideBar.css" rel="stylesheet" />

<div class="sidebar">
    <ul>
        <li><a href="#" onclick="setMenu(1)">Bitácora</a></li>
        <li><a href="#" onclick="setMenu(2)">Usuarios</a></li>
        <li><a href="#" onclick="setMenu(3)">Productos</a></li>
        <li><a href="#" onclick="setMenu(4)">Ventas</a></li>
        <li><a href="#" onclick="setMenu(5)">Backup</a></li>
    </ul>
</div>

<script src="Scripts/SideBar.js"></script>
