document.addEventListener('DOMContentLoaded', function () {

    const formsEliminar = document.querySelectorAll('.form-eliminar-proyecto');

    formsEliminar.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); // ⛔ no eliminamos aún

            Swal.fire({
                title: "Advertencia",
                text: "Este proyecto puede tener tareas asociadas. ¿Deseás eliminarlo de todas formas?",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar",
                reverseButtons: true
            }).then(result => {
                if (result.isConfirmed) {
                    // ✅ SOLO si acepta, se envía el form
                    form.submit();
                }
                // ❌ Si cancela, NO pasa absolutamente nada
            });
        });
    });

});
function AlertaProyectoAgregado() {
    Swal.fire({
        title: "Proyecto Agregado",
        text: "El proyecto se agrego correctamente.",
        icon: "success",
        timer: 1800,
        timerProgressBar: true,
        showConfirmButton: false
    });
}
