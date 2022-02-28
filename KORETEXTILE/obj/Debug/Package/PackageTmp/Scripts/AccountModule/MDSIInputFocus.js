function EnterPress() {
    $("#DepotID").focus();

    $('#select2-DepotID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#DepotID').on('select2:select', function (e) {
        $("#AccountID").focus();
    });
    $('#select2-AccountID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#AccountID').on('select2:select', function (e) {
        $("#DSIADate").focus();
    });
    $(document).on("keydown", "#MDSIDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });
    $(document).on("keydown", "#Description", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#DealerID").focus();
        }
    });

    $('#select2-DealerID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#DealerID').on('select2:select', function (e) {
        $("#Amount").focus();
    });
    $(document).on("keydown", "#Amount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AddToGrid").focus();
        }
    });
    $(document).on("keydown", "#AddToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AddToGrid").click();
            $("#DealerID").focus();
        }
    });
    $(document).on("keydown", "#MDSINO", function (e) {
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