


var iniIndexComp = function (urlCitasPorCobrar, urlDetalle, urlAnular) {

    $("body").on("click",".btn-reporte", function () {
        var id = $(this).data("id");
        window.open("/Comprobante/ReporteComprobante/" + id, "popupWindow", "width=600,height=800,scrollbars=yes");
    });

    $("#btn-nuevo").on("click", function () {
        $("#modal-body-citas-por-cobrar").load(urlCitasPorCobrar)
    });

    $(".btn-dtl").on("click", function () {
        $("#modal-body-detalle-comp").load(urlDetalle, { idComp: $(this).data("id") });
    });

    $(".btn-anular").on("click", function () {
        $("#modal-content-anular-comp").load(urlAnular, { idComp: $(this).data("id") });
    });

}