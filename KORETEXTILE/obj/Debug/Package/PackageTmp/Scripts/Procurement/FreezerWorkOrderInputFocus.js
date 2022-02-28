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

    $('#select2-SupplierID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#SupplierID').on('select2:select', function (e) {
        $("#ItemID").focus();
        e.preventDefault();
    });

    //$('#select2-GroupID').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});

    //$('#GroupID').on('select2:select', function (e) {
    //    $("#ItemID").focus();
    //    e.preventDefault();
    //});

    $('#select2-ItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ItemID').on('select2:select', function (e) {
        $("#ItemQty").focus();
        e.preventDefault();
    });

    $(document).on("keydown", "#ItemQty", function (e) {
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
            $("#VatPer").focus();
        }
    });

    $(document).on("keydown", "#VatPer", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AitPer").focus();
        }
    });

    $(document).on("keydown", "#AitPer", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Specification").focus();
        }
    });

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

    $(document).on("keydown", "#WONo", function (e) {
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