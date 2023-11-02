// Función para mostrar el mensaje de bienvenida
function mostrarBienvenida() {
    var bienvenida = document.getElementById("bienvenida");
    bienvenida.style.display = "block";
}

// Función para cerrar el mensaje de bienvenida
function cerrarBienvenida() {
    var bienvenida = document.getElementById("bienvenida");
    bienvenida.style.display = "none";
}

// Mostrar el mensaje de bienvenida cuando la página se carga
window.addEventListener("load", mostrarBienvenida);

