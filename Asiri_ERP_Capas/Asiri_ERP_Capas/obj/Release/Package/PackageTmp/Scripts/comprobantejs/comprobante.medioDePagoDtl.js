var iniMedioDePagoDtl = function (maxAmount, urlCreate, urlIndex) {

    var items = [];
    var isValid = false;
    var mtoPagar = parseFloat(Cookies.get("mtoTotal"));
    var mtoRestante = mtoPagar;
    var mtoAcumulado = 0;
    var tipoDeCambioService = 1;
    var isoActual = "";
    var iso = [];
    var getIso = function (id) {
        var codIso = "";
        iso.forEach(function (e) {
            if (e.id == id) {
                codIso = e.iso;
                return false;
            }
        });
        return codIso;
    }
    $.ajax({
        url: '/Comprobante/GetMonedaISO',
        type: 'POST',
        success: function (rpta) {
            debugger;
            if (rpta != null)
                iso = $.parseJSON(rpta);
        },
        error: function (e) {
            console.log('error iso')
        },
        complete: function (e) {
            var _idMoneda = Cookies.get("idMoneda");
            isoActual = getIso(_idMoneda);
            console.log(":" + isoActual);
        }
    });

    $("#tipoDeCambio").prop("disabled", true);

    var idMonedaPreComp = parseInt(Cookies.get("idMoneda"));
    var simboloMoneda = $("#value-simbolo-moneda").val();
    var nombreMoneda = $("#value-nombre-moneda").val();

    $("#mtoMedioDePago").numeric({
        allowMinus: false,
        allowThouSep: false,
        allowLeadingSpaces: false,
        maxDecimalPlaces: 2,
        maxPreDecimalPlaces: 12,
        max: maxAmount,
        min: 0
    });

    $("#tipoDeCambio").numeric({
        allowMinus: false,
        allowThouSep: false,
        allowLeadingSpaces: false,
        maxDecimalPlaces: 2,
        maxPreDecimalPlaces: 12,
        max: maxAmount,
        min: 0
    });






    $(function () {
        $(".simbolo-moneda").text(simboloMoneda);
        //$(".error").css("font-size","11px");
        $(".nombre-moneda").text(nombreMoneda);
        $("#mto-a-pagar").text(mtoPagar);
        $("#mto-restante").text(mtoPagar);
        $("#mto-acumulado").text(mtoAcumulado);

        $("#idMoneda").val(idMonedaPreComp);
    });

    //METHODS NUMBERS
    function isInteger(n) {
        return Math.floor(n) == +n && $.isNumeric(n)
    }

    function isNumeric(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

    $("#idMoneda").on("change", function () {

        var idMoneda = $("#idMoneda").val();
        if (idMoneda == idMonedaPreComp) {
            $("#div-tipo-de-cambio").addClass("hidden");
            $("#tipoDeCambio").val("");
            $("#valTipoDeCambio").text("");
        }
        else {
            $("#div-tipo-de-cambio").removeClass("hidden");

            var codIso = getIso($(this).val())
 
            if (codIso != "") {
                $("#btn-add-medioDePagoDtl").prop("disabled", true);
                $.ajax({
                    url: '/Comprobante/GetTipoCambio',
                    type: 'POST',
                    data: { mto: 1, monedaInicial: codIso, monedaFinal: isoActual },
                    success: function (rpta) {
                        //debugger;
                        if (rpta != null) {
                          var  respuesta = $.parseJSON(rpta);
                          $("#tipoDeCambio").val(respuesta.valor_final);
                        }
                    },
                    error: function (e) {
                        $("#tipoDeCambio").val("");
                        console.log('error al traer tipo de cambio')
                    },
                    complete: function () { $("#btn-add-medioDePagoDtl").prop("disabled", false); }
                });
            }
        }
    });



    var isValidItem = function () {

        try {
            //debugger;
            isValid = true;

            $("#valIdMedioDePago, #valMtoMedioDePago, #valIdMoneda, #valTipoDeCambio,#valMedioDePagoDtl").addClass("hidden");
            $("#valIdMedioDePago").text("");
            $("#valMtoMedioDePago").text("");
            $("#valIdMoneda").text("");
            $("#valTipoDeCambio").text("");
            $("#valMedioDePagoDtl").text("");

            var idMedioDePago = $("#idMedioDePago").val();
            var mtoMedioDePago = $("#mtoMedioDePago").val();
            var idMoneda = $("#idMoneda").val();
            var cambio = $("#tipoDeCambio").val();

            if (!isInteger(idMedioDePago)) {
                isValid = false;
                $("#valIdMedioDePago").text("El medio de pago es requerido.");
                $("#valIdMedioDePago").removeClass("hidden");
            }
            if (!isInteger(idMoneda)) {
                isValid = false;
                $("#valIdMoneda").text("La moneda es requerida.");
                $("#valIdMoneda").removeClass("hidden");
            }
            if (!isNumeric(mtoMedioDePago)) {
                isValid = false;
                $("#valMtoMedioDePago").text("Debe ser un número.");
                $("#valMtoMedioDePago").removeClass("hidden");
            }
            else {

                var _auxMtoMedioDePago = parseFloat(mtoMedioDePago);
                if (!(_auxMtoMedioDePago > 0 && _auxMtoMedioDePago <= maxAmount)) {
                    isValid = false;
                    $("#valMtoMedioDePago").text("Debe ser un número mayor a 0 y menor o igual a " + maxAmount + ".");
                    $("#valMtoMedioDePago").removeClass("hidden");
                }
            }


            if (!isNumeric(cambio)) {

                if (!$("#div-tipo-de-cambio").hasClass("hidden")) {
                    isValid = false;
                    $("#valTipoDeCambio").text("Debe ser un número.");
                    $("#valTipoDeCambio").removeClass("hidden");
                }
            }
            else {
                var _auxTipoDeCambio = parseFloat(cambio);
                if (!(_auxTipoDeCambio > 0 && _auxTipoDeCambio <= maxAmount)) {
                    isValid = false;
                    $("#valTipoDeCambio").text("Debe ser un número mayor a 0 y menor o igual a " + maxAmount + ".");
                    $("#valTipoDeCambio").removeClass("hidden");
                }
            }

            // VERIFICAR EL TIPO DE CAMBIO POR MONEDA
            if (isValid) {
                var _idMoneda = parseInt(idMoneda);

                var _tipoDeCambioCandidato = 1;
                if (!$("#div-tipo-de-cambio").hasClass("hidden"))
                    _tipoDeCambioCandidato = parseFloat(cambio);

                var _tipoDeCambio = getTipoDeCambioByMoneda(_idMoneda);

                // EXISTE LA MONEDA Y EL TIPO DE CAMBIO ES DIFERENTE PARA LA MISMA MONEDA
                if (_tipoDeCambio != null && _tipoDeCambio != _tipoDeCambioCandidato) {
                    isValid = false;
                    $("#valMedioDePagoDtl").removeClass("hidden");
                    $("#valMedioDePagoDtl").text("No puede ingresar dos tipos de cambio distintos para la misma moneda (" + simboloMoneda + "" + _tipoDeCambioCandidato + " ≠ " + simboloMoneda + _tipoDeCambio + ").");
                }
            }

            // SI TODO ESTÁ BIEN HASTA AQUÍ, SOLO QUEDA VALIDAR LA CONVERSIÓN
            if (isValid) {
                var _mtoMedioDePago = parseFloat(mtoMedioDePago);

                var _tipoDeCambio = 1;
                if (!$("#div-tipo-de-cambio").hasClass("hidden"))
                    _tipoDeCambio = parseFloat(cambio);
                var _conversion = parseFloat((_mtoMedioDePago * _tipoDeCambio).toFixed(2));
                // EL MONTO RESTANTE ES MENOR A PAGAR
                if (_conversion > mtoRestante) {
                    isValid = false;
                    $("#valMedioDePagoDtl").removeClass("hidden");
                    var msj = mtoRestante == 0 ? "No puede agregar más montos. Ya completó el monto a pagar." :
                        "El monto que intenta agregar debe ser menor o igual al monto restante.";
                    $("#valMedioDePagoDtl").text(msj);
                }
                else {
                    if (!(_conversion > 0 && _conversion <= maxAmount)) {
                        isValid = false;
                        $("#valMedioDePagoDtl").text("Debe ser un número mayor a 0 y menor o igual " + maxAmount + ".");
                        $("#valMedioDePagoDtl").removeClass("hidden");
                    }
                }
            }
        }
        catch (e) {
            isValid = false;
            console.log("Excepción: " + e.message);
        }
        console.log(isValid ? " MEDIO DE PAGO VÁLIDO" : "MEDIO DE PAGO NO VÁLIDO");
        return isValid;
    };



    //UPDATES AMOUNTS
    var updateMounts = function () {
        try {
            var _mtoAcumulado = 0;
            var _mtoRestante = 0;

            $.each(items, function (i, e) {
                _mtoAcumulado += e.conversion;
            });
            _mtoRestante = parseFloat((mtoPagar - parseFloat(_mtoAcumulado.toFixed(2))).toFixed(2));

            mtoAcumulado = parseFloat(_mtoAcumulado.toFixed(2));
            mtoRestante = _mtoRestante;
            $("#mto-acumulado").text(mtoAcumulado);
            $("#mto-restante").text(mtoRestante);
        }
        catch (e) {
            mtoAcumulado = 0;
            mtoRestante = mtoPagar;
            $("#mto-acumulado").text(mtoAcumulado);
            $("#mto-restante").text(mtoRestante);
            items = [];
            renderRows();
            alert("Ocurrió un error no controlado. No se pudo actualizar los cálculos.")
        }
        //Al mommento de ver que está completo activar el boton de PAGAR - cuando está completo no debe pagar (disabled)
    };

    //IS VALID ALL ORDER - PRE SAVE
    var isAllValid = function () {

        var isValid = true;

        if (items.length > 0) {
            $("#valMedioDePagoDtl").addClass("hidden");
            $("#valMedioDePagoDtl").text("")
            if (!(mtoRestante == 0 && parseFloat((mtoRestante + mtoAcumulado).toFixed(2)) == mtoPagar)) {
                isValid = false;
                $("#valMedioDePagoDtl").removeClass("hidden");
                $("#valMedioDePagoDtl").text("Debe terminar de acumular el monto total a pagar.");
            }
        }
        else {
            isValid = false;
            $("#valMedioDePagoDtl").removeClass("hidden");
            $("#valMedioDePagoDtl").text("No ha agregado ningún medio de pago.");
        }

        if (isValid) {
            $("#valMedioDePagoDtl").addClass("hidden");
            $("#valMedioDePagoDtl").text("");
            if (!(mtoPagar <= maxAmount && mtoPagar > 0)) {
                isValid = false;
                $("#valMedioDePagoDtl").removeClass("hidden");
                $("#valMedioDePagoDtl").text("El monto total tiene que tiene que ser un número entre 0 y " + maxAmount);
            }
        }

        return isValid;
    };

    //RENDER ROWS
    var renderRows = function () {
        var rows = "";
        var accion = "<button type='button' class='btn btn-danger btn-sm deleteItem'><i class='glyphicon glyphicon-minus'></i>  Quitar</button>";
        $.each(items, function (i, e) {
            rows += "<tr class='row-item'><td class='text-nowrap'>" + e.nombreMedioDePago
                + "</td><td class='text-right text-nowrap'>" + e.mtoMedioDePago + " " + e.nombreMoneda
                + "</td><td class='text-right text-nowrap'>" + simboloMoneda + " " + e.tipoDeCambio
                + "</td><td class='text-right text-nowrap'>" + simboloMoneda + " " + e.conversion
                + "</td><td class='text-center text-nowrap'>" + accion
                + "</td><td class='hide id-value'>"
                + "<input class='item-id-medio-pago' type='hidden' value='" + e.idMedioDePago + "'/>" +
                + "</td><td class='hide id-value'>"
                + "<input class='item-id-moneda' type='hidden' value='" + e.idMoneda + "'/>" +
                +"</td></tr>";
        });
        $("#body-details-medioDePagoDtl").empty();
        $("#body-details-medioDePagoDtl").append(rows);
    };

    var getItem = function (pIdMedioDePago, pIdMoneda) {
        var item = { value: null, index: -1 };
        $.each(items, function (i, e) {
            if ((e.idMedioDePago == pIdMedioDePago) && (e.idMoneda == pIdMoneda)) {
                item.value = e;
                item.index = i;
                return false;
            }
        });
        return item;
    };

    var getTipoDeCambioByMoneda = function (pIdMoneda) {
        var tipoDeCambio = null;
        $.each(items, function (i, e) {
            if (e.idMoneda == pIdMoneda) {
                tipoDeCambio = parseFloat(e.tipoDeCambio);
                return false;
            }
        });
        return tipoDeCambio;
    };

    //--------EVENTS------------------------------------------------

    //ADD ITEM DETAIL
    $("#btn-add-medioDePagoDtl").on("click", function () {
        if (isValidItem()) {
            //GET VALUES
            var _idMedioDePago = parseInt($("#idMedioDePago").val());
            var _nombreMedioDePago = $("#idMedioDePago option:selected").text();
            var _idMoneda = parseInt($("#idMoneda").val());
            var _nombreMoneda = $("#idMoneda option:selected").text();
            var _mtoMedioDePago = parseFloat($("#mtoMedioDePago").val());

            var _tipoDeCambio = 1;
            //CUANDO LA CONDICIÓN EN FALSA INGRESA - ES DECIR - CUANDO EL TIPO CAMBIO SE VE
            if (!$("#div-tipo-de-cambio").hasClass("hidden"))
                _tipoDeCambio = parseFloat($("#tipoDeCambio").val());

            var _conversion = parseFloat((_mtoMedioDePago * _tipoDeCambio).toFixed(2));

            var item = getItem(_idMedioDePago, _idMoneda);

            if (item.value != null && item.index != -1 && item.value.tipoDeCambio == _tipoDeCambio) {
                item.value.mtoMedioDePago = (item.value.mtoMedioDePago + parseFloat(_mtoMedioDePago.toFixed(2)));
                item.value.conversion = (item.value.conversion + _conversion);
                items[item.index] = item.value;
            }
            else {
                items.push
                    ({
                        idMedioDePago: _idMedioDePago,
                        nombreMedioDePago: _nombreMedioDePago,
                        mtoMedioDePago: _mtoMedioDePago,
                        idMoneda: _idMoneda,
                        nombreMoneda: _nombreMoneda,
                        tipoDeCambio: _tipoDeCambio,
                        conversion: _conversion,
                        activo: true
                    });
            }
            renderRows();
            updateMounts();
            console.log(JSON.stringify(items) + "LEGNTH: " + items.length);
            console.log("mto restante: " + mtoRestante + " | mto acumulado:" + mtoAcumulado);
        }
    });

    //DELETE ITEM
    $("#body-details-medioDePagoDtl").on("click", ".deleteItem", function () {
        //GET KEY
        var itemIdMoneda = $(this).parent().siblings(".id-value").children(".item-id-moneda").val();
        var itemIdMedioDePago = $(this).parent().siblings(".id-value").children(".item-id-medio-pago").val();

        //REMOVE ITEM IN ARRAY AUX
        var itemsAux = $.grep(items, function (item) {
            return !(item.idMedioDePago == itemIdMedioDePago && item.idMoneda == itemIdMoneda);
        });

        //REALLY DELETE?
        if (items.length > itemsAux.length) {
            items = itemsAux;
            renderRows();
            updateMounts();
        }
        console.log(JSON.stringify(items) + "LEGNTH: " + items.length);
        console.log("mto restante: " + mtoRestante + " | mto acumulado:" + mtoAcumulado);
    })

    $("#mtoMedioDePago, #tipoDeCambio").on("keyup", function () {
        isValidItem();
    });

    $("#modal-medioDePagoDtl").on("hidden.bs.modal", function () {
        //ES NECESARIO DESVINCULAR EL SUBMIT DEBIDO
        // A QUE EL MODAL AL CERRAR ACUMULA LOS EVENTOS SUBMIT
        //(PORQUE ESTE BOTÓN SIEMPRE SE MANTIENE EN EL PADRE)
        $("#modal-body-medioDePagoDtl").text("");
        $("#form-create-comp").unbind("submit");
        Cookies.remove('idCita');
        Cookies.remove('idImpto');
        Cookies.remove('idMoneda');
        Cookies.remove('mtoTotal');
        Cookies.remove('mtoSubtotal');
        Cookies.remove('mtoImpto');
        Cookies.remove('porcentajeImpto');
        Cookies.remove('mtoDescuento');
        Cookies.remove('porcentajeDescuento');
        Cookies.remove('ComprobanteDtl');
    });

    var getComprobanteEmitido = function () {

        var comp = {

            idCita: Cookies.get("idCita"),
            mtoTotal: Cookies.get("mtoTotal"),
            mtoSubtotal: Cookies.get("mtoSubtotal"),
            mtoImpto: Cookies.get("mtoImpto"),
            porcentajeImpto: Cookies.get("porcentajeImpto"),
            mtoDescuento: Cookies.get("mtoDescuento"),
            porcentajeDescuento: Cookies.get("porcentajeDescuento"),
            idMoneda: Cookies.get("idMoneda"),
            idImpto: Cookies.get("idImpto"),
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
        //debugger;
        comp.MedioDePagoDtl = items;
        comp.ComprobanteDtl = Cookies.getJSON('ComprobanteDtl');

        return comp;
    };

    //__RequestVerificationToken: $("input[name=__RequestVerificationToken]").val(),
    //contenttype: "application/json; charset=utf-8",

    //SAVE
    $("#form-create-comp").submit(function (e) {
        if (!isAllValid()) {
            console.log("SUBMIT: NO ES VÁLIDO");
            e.preventDefault();
        }
        else {
            var reporte = window.open("", "reporte de comprobante", "width=600,height=800,scrollbars=yes");

            var comp = getComprobanteEmitido();
            $("#btn-insertar-comp").prop("disabled", true);
            $.ajax({
                url: urlCreate,
                type: 'POST', //1
                data: { comp},
                success: function (rpta) {
                    /*
                    AÚN ASÍ EN LA ACCIÓN REDIRECCIONE A UNA VISTA DISTINTA
                    AQUÍ LLEGA AL FINALIZAR EL AJAX
                    AÚN ASÍ NO HAYA UN RETURN FALSE.
                    */
                    //3
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
                complete: function () { $("#btn-insertar-comp").prop("disabled", false); }
            });
        }
        //PREVIENE QUE SE VAYA A OTRA VISTA
        //SI NO ESTUVIERA
        //PROBAR QUE EL CONTROLADOR REGRESE EL VIEW (NO DEBERÍA LLEVARME AL VIEW)
        //DESPUÉS DE ENVIAR LA DATA Y LLEGAR AL CONTROLADOR TERMINA AQUI//2
        return false;
    });

}