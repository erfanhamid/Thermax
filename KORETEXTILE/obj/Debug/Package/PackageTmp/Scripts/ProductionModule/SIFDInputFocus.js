function EnterPress() {

    $('#select2-NewStoreID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#NewStoreID').on('select2:select', function (e) {
        $("#Description").focus();
        e.preventDefault();
    });

    $(document).on("keydown", "#Description", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ItemGrpID").focus();
        }
    });

    //$(document).on("keydown", "#Ref", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#ItemGrpID").focus();
    //    }
    //});

    $('#select2-ItemGrpID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ItemGrpID').on('select2:select', function (e) {
        $("#ItemID").focus();
    });

    $('#select2-ItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ItemID').on('select2:select', function (e) {
        $("#ShiftPCsQty").focus();
    });

    $(document).on("keydown", "#ShiftPCsQty", function (e) {
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

    $(document).on("keydown", "#SIFDNo", function (e) {
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
}