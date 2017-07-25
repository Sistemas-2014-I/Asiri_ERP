
var iniCreateComp = function (
    pidCita,
    pidMoneda,
    pidImpto,
    pComprobanteDtl,
    pmtoTotal,
    pmtoSubtotal,
    pmtoImpto,
    pporcentajeImpto,
    pdescMedioDePagoRapido,
    pidMedioDePagoRapido,
    maxAmount,
    urlCreate,
    urlIndex,
    urlMedioDePagoDtl) {

    var isValid = true;
    var idCita = pidCita;
    var idMoneda = pidMoneda;
    var idImpto = pidImpto;
    var ComprobanteDtl = pComprobanteDtl;
    var mtoTotal = pmtoTotal;
    var mtoSubtotal = pmtoSubtotal;
    var mtoImpto = pmtoImpto;
    var porcentajeImpto = pporcentajeImpto;
    var mtoDescuento = 0;
    var porcentajeDescuento = 0;
    var descMedioDePagoRapido = pdescMedioDePagoRapido;
    var idMedioDePagoRapido = pidMedioDePagoRapido;


    //Activa la validación en tabs
    $("#form-create-comp").data("validator").settings.ignore = "";

    // REMOVE THE COOKIE
    $(window).on("unload", function () {
        Cookies.remove('idCita');
        Cookies.remove('idMoneda');
        Cookies.remove('idImpto');
        Cookies.remove('mtoTotal');
        Cookies.remove('mtoSubtotal');
        Cookies.remove('mtoImpto');
        Cookies.remove('porcentajeImpto');
        Cookies.remove('mtoDescuento');
        Cookies.remove('porcentajeDescuento');
        Cookies.remove('ComprobanteDtl');
    });

    //var mtoSubtotal = parseFloat($("#mtoSubtotal").val());

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    $("#mtoDescuento").numeric({
        allowMinus: false,
        allowThouSep: false,
        allowLeadingSpaces: false,
        maxDecimalPlaces: 2,
        maxPreDecimalPlaces: 12,
        max: maxAmount,
        min: 0
    });

    $("#porcentajeDescuento").numeric({
        allowMinus: false,
        allowThouSep: false,
        allowLeadingSpaces: false,
        maxDecimalPlaces: 2,
        maxPreDecimalPlaces: 3,
        max: 100,
        min: 0
    });

    var updateMounts = function (criterio, isValid) {
        try {

            if (criterio == "M") {

                mtoDescuento = isValid ? $("#mtoDescuento").val().trim() == "" ? 0 : parseFloat($("#mtoDescuento").val()) : 0;
                porcentajeDescuento = ((mtoDescuento / mtoSubtotal) * 100).toFixed(2);
                mtoTotal = (mtoSubtotal + mtoImpto - mtoDescuento).toFixed(2);

                $("#porcentajeDescuento").val(porcentajeDescuento == 0 && ($("#mtoDescuento").val().trim() == "" || !isValid) ? "" : porcentajeDescuento);
                $("#mtoTotal").val(mtoTotal);
            }
            else if (criterio == "P") {
                porcentajeDescuento = isValid ? $("#porcentajeDescuento").val().trim() == "" ? 0 : parseFloat($("#porcentajeDescuento").val()) : 0;
                mtoDescuento = (mtoSubtotal * (porcentajeDescuento / 100)).toFixed(2);
                mtoTotal = (mtoSubtotal + mtoImpto - mtoDescuento).toFixed(2);
                //pintar
                $("#mtoDescuento").val(mtoDescuento == 0 && ($("#porcentajeDescuento").val().trim() == "" || !isValid) ? "" : mtoDescuento);
                $("#mtoTotal").val(mtoTotal);
            }
        }
        catch (e) {
            mtoTotal = (mtoSubtotal + mtoImpto).toFixed(2);;
            mtoDescuento = 0;
            porcentajeDescuento = 0;
            $("#mtoTotal").val(mtoTotal);
            $("#mtoSubtotal").val(mtoSubtotal);
            $("#mtoImpto").val(mtoImpto);
            $("#mtoDescuento").val(mtoDescuento);
            $("#porcentajeDescuento").val(porcentajeDescuento);
        }
        console.log("SUB: " + mtoSubtotal);
        console.log("IMTPO: " + mtoImpto);
        console.log("MTO DSCTO: " + mtoDescuento);
        console.log("% DSCTO: " + porcentajeDescuento);
        console.log("TOT: " + mtoTotal);
    };


    var isValidNumber = function (value, criterio) {
        isValid = true;

        $("#valMtoDescuento, #valPorcentajeDescuento").addClass("hidden");
        $("#valMtoDescuento").text("");
        $("#valPorcentajeDescuento").text("");

        if (isNumeric(value)) {
            var num = parseFloat(value);
            if (criterio == "M") {

                if (num > mtoSubtotal) {
                    isValid = false;
                    $("#valMtoDescuento").html("El descuento no puede ser mayor al subtotal. <br>");
                    $("#valMtoDescuento").removeClass("hidden");
                }
                else if (!(num >= 0 && num <= maxAmount)) {
                    isValid = false;
                    $("#valMtoDescuento").html("Debe ser un número entre 0 y " + maxAmount + ". <br>");
                    $("#valMtoDescuento").removeClass("hidden");
                }
            }
            if (criterio == "P") {
                if (!(num >= 0 && num <= 100)) {
                    isValid = false;
                    $("#valPorcentajeDescuento").html("Debe ser un número entre 0% y 100%. <br>")
                    $("#valPorcentajeDescuento").removeClass("hidden");
                }
            }
        }
        else {
            //ACEPTAMOS CÓMO VÁLIDO LOS ""
            if (criterio == "M") {
                if (value.trim() != "") {
                    isValid = false;
                    $("#valMtoDescuento").html("Debe ser un número. <br>");
                    $("#valMtoDescuento").removeClass("hidden");
                }
            }
            else if (criterio == "P") {
                if (value.trim() != "") {
                    isValid = false;
                    $("#valPorcentajeDescuento").html("Debe ser un número. <br>");
                    $("#valPorcentajeDescuento").removeClass("hidden");
                }
            }
        }
        console.log(isValid ? "VÁLIDO" : "NO VÁLIDO");
        return isValid;
    };

    $("#mtoDescuento").on("keyup", function () {
        updateMounts("M", isValidNumber($(this).val(), "M"));
    });

    $("#porcentajeDescuento").on("keyup", function () {
        updateMounts("P", isValidNumber($(this).val(), "P"));
    });


    //$("body").on("click", function () {
    //    alert($("#idSucursal").val());
    //})

    var isAllValid = function () {
        var valido = true;
        $("#div-val-create").addClass("hidden");
        $("#val-create").html("");

        if (!(mtoTotal <= maxAmount && mtoTotal > 0)) {
            valido = false;
            $("#div-val-create").removeClass("hidden");
            $("#val-create").html("El monto total tiene que tiene que ser un número entre 0 y " + maxAmount);
        }
        return valido;
    }

    // MODAL FORMA DE PAGO
    $("#btn-forma-pago").on("click", function (e) {
        //Validacion final pre grabacion

        (isValid && isAllValid() && $('#form-create-comp').data('unobtrusiveValidation').validate()) ? $("#modal-forma-pago").modal("show") : console.log("No pasa la validación");
    });

    //MEDIO DE PAGO
    var getComprobanteEmitido = function () {
        var comp = {
            idCita: idCita,
            mtoTotal: mtoTotal,
            mtoSubtotal: mtoSubtotal,
            mtoImpto: mtoImpto,
            porcentajeImpto: porcentajeImpto,
            mtoDescuento: mtoDescuento,
            porcentajeDescuento: porcentajeDescuento,
            idMoneda: idMoneda,
            idImpto: idImpto,
            idSucursal: $("#idSucursal").val(),
            idTipoComprobante: $("#idTipoComprobante").val(),
            info01: $("#info01").val(),
            info02: $("#info02").val(),
            info03: $("#info03").val(),
            fecha01: $("#fecha01").val(),
            fecha02: $("#fecha02").val(),
            fecha03: $("#fecha03").val(),
            obsvComprobanteEmitido: $("#obsvComprobanteEmitido").val(),
            esAnulado: false,
            ComprobanteDtl: [],
            MedioDePagoDtl: []
        };
        comp.MedioDePagoDtl.push
        ({
            idMedioDePago: idMedioDePagoRapido,
            mtoMedioDePago: mtoTotal,
            idMoneda: idMoneda,
            tipoDeCambio: 1,
            activo: true
        });
        comp.ComprobanteDtl = ComprobanteDtl;

        return comp;
    };

    //LUEGO VER REPORTE DEL COMPROBANTE!!
    $(function () {
        if (descMedioDePagoRapido != "" && idMedioDePagoRapido != 0) {
            var html = "<button id='btn-medio-de-pago-rapido' type='button' class='btn btn-success btn-lg'><i class='fa fa- lg fa-money'></i>" + descMedioDePagoRapido + "</button>";
            $("#content-medio-de-pago-rapido").html(html);
            $("#btn-medio-de-pago-rapido").on("click", function (e) {

                //                     if (!isAllValid())
                //{
                //    console.log("SUBMIT: NO ES VÁLIDO");
                //    e.preventDefault();
                //}
                //else
                //{
                var reporte = window.open("", "reporte de comprobante", "width=600,height=800,scrollbars=yes");

                var comp = getComprobanteEmitido();
                $("#btn-medio-de-pago-rapido").prop("disabled", true);
                $.ajax({
                    url: urlCreate,
                    type: 'POST', //1
                    data: { comp},
                    success: function (rpta) {
                        //debugger;
                        if (rpta.success && rpta.id > 0) {
                            reporte.location.href = "/Comprobante/ReporteComprobante/" + rpta.id;
                            console.log("successful");
                        }
                        else
                            console.log("no se guardó :/");

                        window.location.href = urlIndex;
                    },
                    error: function (e) {
                        console.log(JSON.stringify(e));
                        var success = false;
                        var url = "/Comprobante/Index/?success=" + success;
                        window.location.href = url;
                    },
                    complete: function () { $("#btn-medio-de-pago-rapido").prop("disabled", false); }
                });
                //}
                return false;

            });
        }

    });

    $("#btn-otra-forma-pago").on("click", function () {
        Cookies.set('idCita', idCita);
        Cookies.set('idImpto', idImpto);
        Cookies.set('idMoneda', idMoneda);
        Cookies.set('mtoTotal', mtoTotal);
        Cookies.set('mtoSubtotal', mtoSubtotal);
        Cookies.set('mtoImpto', mtoImpto);
        Cookies.set('porcentajeImpto', porcentajeImpto);
        Cookies.set('mtoDescuento', mtoDescuento);
        Cookies.set('porcentajeDescuento', porcentajeDescuento);
        Cookies.set('ComprobanteDtl', ComprobanteDtl);

        $("#modal-body-medioDePagoDtl").load(urlMedioDePagoDtl)
    });
}
