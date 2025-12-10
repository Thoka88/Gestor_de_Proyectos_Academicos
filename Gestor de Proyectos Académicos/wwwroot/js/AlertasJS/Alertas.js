// RF20: Al crear una tarea, mensaje de confirmación
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

// RF21: Al marcar una tarea como completada, notificación de éxito
function AlertaTareaCompletada() {
    Swal.fire({
        title: "Tarea completada",
        text: "La tarea ha sido marcada como completada.",
        icon: "success",
        timer: 1800,
        timerProgressBar: true,
        showConfirmButton: false
    });
}

// RF22: Advertencia al intentar eliminar un proyecto con tareas asociadas
// idForm = id del <form> o botón que hace la eliminación real
function ConfirmarEliminarProyectoConTareas(idForm) {
    Swal.fire({
        title: "Proyecto con tareas asociadas",
        text: "Este proyecto aún tiene tareas registradas. ¿Deseás eliminarlo de todas formas?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar",
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            // Aquí hacés la acción real: enviar form o llamar al backend
            const form = document.getElementById(idForm);
            if (form) {
                form.submit();
            }
        }
    });
}
