function EnterPress() {

    $('#select2-Store').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#Store').on('select2:select', function (e) {
        $("#TransectionType").focus();
    });

    $('#select2-TransectionType').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#TransectionType').on('select2:select', function (e) {
        $("#Description").focus();
    });

    $(document).on("keydown", "#Description", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#GroupId").focus();
        }
    });

    $('#select2-GroupId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#GroupId').on('select2:select', function (e) {
        $("#ItemId").focus();
    });

    $('#select2-ItemId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#ItemId').on('select2:select', function (e) {
        $("#Qty").focus();
    });

    $(document).on("keydown", "#Qty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
            $("#addToGrid").click();
            $("#ItemId").focus();
        }
    });

    $(document).on("keydown", "#SANo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
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