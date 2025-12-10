function AlertaEstadoActualizado() {
    Swal.fire({
        title: "Estado actualizado",
        text: "El estado de la tarea se actualizó correctamente.",
        icon: "success",
        timer: 1700,
        timerProgressBar: true,
        showConfirmButton: false
    });
}
document.addEventListener('DOMContentLoaded', function () {
    const formsEliminar = document.querySelectorAll('.form-eliminar-tarea');

    formsEliminar.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); // detenemos el submit normal

            Swal.fire({
                title: "¿Eliminar esta tarea?",
                text: "Esta acción no se puede deshacer.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar",
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {
                    form.submit(); // 👈 ahora sí la eliminamos
                }
                // si cancela, no hacemos nada
            });
        });
    });
});

function AlertaTareaCreada() {
    Swal.fire({
        title: "Tarea creada",
        text: "La tarea se ha creado correctamente.",
        icon: "success",
        timer: 1800,
        timerProgressBar: true,
        showConfirmButton: false
    });
}