document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('loginForm');
    if (!form) return;

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        Swal.fire({
            title: "Iniciando sesión...",
            text: "Por favor esperá un momento",
            icon: "info",
            showConfirmButton: false,
            allowOutsideClick: false,
            timer: 1000
        }).then(() => {
            form.submit();
        });
    });
});

// 🟢 Exitoso
function AlertaSesionExitosa(redir) {
    Swal.fire({
        title: "¡Sesión iniciada!",
        text: "Bienvenido/a al sistema",
        icon: "success",
        timer: 1800, 
        showConfirmButton: true  
    }).then(() => {
        window.location.href = redir;
    });
}


// 🔴 Fallido
function AlertaSesionFallida(mensaje) {
    Swal.fire({
        title: "Error al iniciar sesión",
        text: mensaje || "Credenciales incorrectas.",
        icon: "error",
        confirmButtonText: "Aceptar"
    });
}


