function EnterPress() {

    $('#select2-SupplierGroup').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#SupplierGroup').on('select2:select', function (e) {
        $("#SupplierID").focus();
        e.preventDefault();
    });
    //$('#PODate').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#ReferenceNo").focus();
    //    }
    //});
    $(document).on("keydown", "#PODate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ReferenceNo").focus();
        }
    });
    $('#ReferenceNo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PoSubject").focus();
        }
    });
    $('#PoSubject').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Others").focus();
        }
    });
    $('#Others').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SupplierGroup").focus();
        }
    });

    $('#select2-SupplierID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#SupplierID').on('select2:select', function (e) {
        $("#GroupID").focus();
        e.preventDefault();
    });

    $('#select2-GroupID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#GroupID').on('select2:select', function (e) {
        $("#ItemID").focus();
        e.preventDefault();
    });

    $('#select2-ItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ItemID').on('select2:select', function (e) {
        $("#Cartoon").focus();
        e.preventDefault();
    });
    $(document).on("keydown", "#Cartoon", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ItemQty").focus();
        }
    });
    $(document).on("keydown", "#ItemQty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LiterKg").focus();
        }
    });
    $(document).on("keydown", "#LiterKg", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#UnitCost").prop('disabled', false);
            $("#VatPer").prop('disabled', false);
            $("#AitPer").prop('disabled', false);
            $("#UnitCost").focus();
        }
    });

    $(document).on("keydown", "#UnitCost", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VatPerc").focus();
        }
    });

    $(document).on("keydown", "#VatPerc", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Specification").focus();
        }
    });

    //$(document).on("keydown", "#AitPer", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#Specification").focus();
    //    }
    //});

    $(document).on("keydown", "#Specification", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#ItemID").focus();
        }
    });

    $(document).on("keydown", "#PONo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });

    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
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