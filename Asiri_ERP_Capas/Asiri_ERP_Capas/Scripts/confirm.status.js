// Se necesitan las referencias jquery-confirm.css y jquery-confirm.js
// Los enlaces para habilitar y deshabilitar deben tener las clases stEnable y stDisable respectivamente
// Ambos enlaces deben tener como ruta datah-ref en lugar de href
// Deben tener la clase status

// Desabilitar
$(".status.stEnable").click(function (event) {
    var href = $(this).attr("data-href");
    $.confirm({
        title: 'Deshabilitar',
        content: '¿Está seguro que desea deshabilitar el item seleccionado?',
        autoClose: 'cancel|10000',
        buttons: {
            confirm: {
                text: 'Confirmar',
                action: function () {
                    //$.alert('Deshabilitado!');
                    window.location.href = href;
                }
            }
            ,
            cancel: {
                text: 'Cancelar',
                action: function () {
                    $.alert('Cancelado!');
                }
            }
        }
    })
});

// Habilitar
$(".status.stDisable").click(function () {
    var href = $(this).attr("data-href");
    $.confirm({
        title: 'Habilitar',
        content: '¿Está seguro que desea Habilitar el item seleccionado?',
        autoClose: 'cancel|10000',
        buttons: {
            confirm: {
                text: 'Confirmar',
                action: function () {
                    window.location.href = href;
                    //$.alert('Habilitado!');
                }
            },
            cancel: {
                text: 'Cancelar',
                action: function () {
                    $.alert('Cancelado!');
                }
            }
        }
        //draggable: true
    })
});
