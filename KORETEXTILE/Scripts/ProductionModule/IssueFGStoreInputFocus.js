function EnterPress() {

    $(document).on("keydown", "#clmGRQNO", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#clmRefno").focus();
        }
    });

    $(document).on("keydown", "#clmRefno", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#clmItemGrpID").focus();
        }
    });

    $('#select2-clmItemGrpID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#clmItemGrpID').on('select2:select', function (e) {
        $("#clmItemID").focus();
    });

    $('#select2-clmItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#clmItemID').on('select2:select', function (e) {
        $("#clmQty").focus();
    });

    $(document).on("keydown", "#clmQty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#clmItemID").focus();
        }
    });

    $(document).on("keydown", "#clmIFGSID", function (e) {
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