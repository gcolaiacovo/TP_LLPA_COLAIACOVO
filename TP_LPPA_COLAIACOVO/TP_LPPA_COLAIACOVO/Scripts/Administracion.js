﻿const MENU = {
    Bitacora: 1,
    Usuarios: 2,
    Productos: 3,
    Ventas: 4,
}

var menuSeleccionado = MENU.Bitacora;

window.onload = function () {
    document.getElementById("tablaProductos").classList.add("hidden");
    document.getElementById("tablaUsuarios").classList.add("hidden");
}

function setMenu(menu) {
    menuSeleccionado = menu;
    mostrarOcultarTablas();
}

function mostrarOcultarTablas() {
    if (!document.getElementById("tablaProductos").classList.contains("hidden")) {
        document.getElementById("tablaProductos").classList.add("hidden");
    }
    if (!document.getElementById("tablaBitacora").classList.contains("hidden")) {
        document.getElementById("tablaBitacora").classList.add("hidden");
    }
    if (!document.getElementById("tablaUsuarios").classList.contains("hidden")) {
        document.getElementById("tablaUsuarios").classList.add("hidden");
    }
    //if (!document.getElementById("tablaVentas").classList.contains("hidden")) {
    //    document.getElementById("tablaVentas").classList.add("hidden");
    //}

    if (menuSeleccionado == MENU.Bitacora) {
        document.getElementById("tablaBitacora").classList.remove("hidden");
    }
    else if (menuSeleccionado == MENU.Usuarios) {
        document.getElementById("tablaUsuarios").classList.remove("hidden");
    }
    else if (menuSeleccionado == MENU.Productos) {
        document.getElementById("tablaProductos").classList.remove("hidden");
    }
    //else if (menuSeleccionado == MENU.Productos) {
    //    document.getElementById("tablaVentas").classList.remove("hidden");
    //}
}