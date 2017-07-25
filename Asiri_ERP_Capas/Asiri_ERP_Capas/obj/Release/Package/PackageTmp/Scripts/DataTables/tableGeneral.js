
$(document).ready(function () {
    $('#tablegeneral').dataTable({
        language: {
            processing: "En curso...",
            search: "Buscar:",
            lengthMenu: "Visualizar _MENU_ registros",
            info: "_START_ de _END_ registros de un total de _TOTAL_ registros",
            infoEmpty: "0 registros encontrados",
            infoFiltered: "(_MAX_ registros filtrados en total)",
            infoPostFix: "",
            loadingRecords: "Cargando registros...",
            zeroRecords: "No hay registros",
            emptyTable: "Vacío",
            paginate: {
                first: "Primero",
                previous: "Previo  ",
                next: "  Siguiente",
                last: "Último"
            },
            aria: {
                sortAscending: ": Habilitado para ordenar la columna en orden ascendente",
                sortDescending: ": Habilitado para ordenar la columna en orden descendente"
            }
        }
    });
});