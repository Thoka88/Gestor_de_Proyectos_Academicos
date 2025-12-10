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
document.addEventListener('DOMContentLoaded', function () {
    const formsEliminar = document.querySelectorAll('.form-eliminar-estudiante');

    formsEliminar.forEach(form => {
        form.addEventListener('submit', function (e) {
            e.preventDefault(); // detenemos el submit normal

            Swal.fire({
                title: "¿Eliminar este estudiante?",
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
function AlertaEstudianteAsignado() {
    Swal.fire({
        title: "Estudiante Asignado",
        text: "El estudiante se agrego correctamente.",
        icon: "success",
        timer: 1800,
        timerProgressBar: true,
        showConfirmButton: false
    });
}
