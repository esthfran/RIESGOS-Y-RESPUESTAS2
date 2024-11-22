// Función para alternar el modo noche
const nightModeButton = document.getElementById("night-mode-button");
const body = document.body;

// Verificar si el modo noche ya está activado
if (localStorage.getItem("night-mode") === "enabled") {
    body.classList.add("night-mode");
    nightModeButton.classList.add("night-active");
}

// Función para alternar el modo noche
function toggleNightMode() {
    body.classList.toggle("night-mode");

    if (body.classList.contains("night-mode")) {
        // Cambia el icono a sol
        nightModeButton.innerHTML = '<i class="fas fa-sun"></i>';
        // Activa la clase del botón
        nightModeButton.classList.add("night-active");
        localStorage.setItem("night-mode", "enabled");
    } else {
        // Cambia el icono a luna
        nightModeButton.innerHTML = '<i class="fas fa-moon"></i>';
        // Remueve la clase del botón
        nightModeButton.classList.remove("night-active");
        localStorage.setItem("night-mode", "disabled");
    }
}

// Agregar el evento de clic al botón
nightModeButton.addEventListener("click", toggleNightMode);

// Desactivar transiciones de CSS para modo noche
body.classList.add("no-transition");
setTimeout(() => {
    body.classList.remove("no-transition");
}, 50); // ajuste de tiempo si es necesario

// Esperar 3 segundos y luego ocultar la alerta con una transición suave
setTimeout(() => {
    const alert = document.getElementById("successAlert");
    if (alert) {
        alert.classList.add('fade-out'); // Agregar clase para la transición
        setTimeout(() => {
            alert.style.display = 'none'; // Ocultar la alerta después de la transición
        }, 500); // Esperar a que termine la transición de 0.5s
    }
}, 3000); // 3000 milisegundos = 3 segundos

// Agregar clase de animación al cargar la página
window.onload = () => {
    const titleElement = document.getElementById("pageTitle"); // Asumiendo que tu título tiene este ID
    if (titleElement) {
        titleElement.classList.add('slide-in'); // Agregar clase para la animación
    }
};