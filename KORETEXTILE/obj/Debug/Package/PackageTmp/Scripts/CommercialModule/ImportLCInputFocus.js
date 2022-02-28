function ImportLCEnterPress() {
    $("#SupplierId").focus();
    //$('#ILCId').on('keydown', function (e) {
    //    if (e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#SupplierId").focus();
    //    }
    //});
    $('#ILCId').on('keydown', function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#ILCIDSearch").focus();
        }
    });
    $("#SupplierId").on('select2:select', function (e) {
        $("#PI").focus();

    });
    $("#PI").on('select2:select', function (e) {
        $("#ILCNO").focus();

    });
    $('#ILCNO').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#IALCNO").focus();
        }
    });
    $('#IALCNO').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PShipment").focus();
        }
    });
    $("#PShipment").on('select2:select', function (e) {
        $("#ILCDate").focus();
    });
   
    $('#ILCDate').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ShipDate").focus();
        }
    }); 
    $('#ShipDate').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MiscellaneousCost").focus();
        }
    }); 
    $('#MiscellaneousCost').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    //$('#ILCId').on('keydown', function (e) {
    //    if (e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#SupplierId").focus();
    //    }
    //});
    $('#save').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 83) {
            e.preventDefault();
            $("#save").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 82) {
            e.preventDefault();
            $("#refresh").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 85) {
            e.preventDefault();
            $("#update").click();
            return false;
        }
    });
}