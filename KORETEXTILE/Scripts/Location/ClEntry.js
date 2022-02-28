function EnterPress() {
    $("#CLNo").focus();
    $(document).on("keydown", "#CLNo", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#search").focus();
            $("#search").click();
            $("#TsoID").focus();
        }
    });

    $('#TsoID').on('select2:select', function (e) {
        $("#CustomerID").focus();
    });
    $('#CustomerID').on('select2:select', function (e) {
        $("#Longitude").focus();
    });

    $(document).on("click", '[name="editRow"]', function (e) {
        $("#TsoID").focus();
    });
    $(document).on("keydown", "#Latitude", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });
    $(document).on("keydown", "#Longitude", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#Latitude").focus();
        }
    });
    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#save").focus();
        }
    });



    $(document).on("#save", "keydown", function (e) {
        if (e.altKey && e.keyCode === 83) {
            e.preventDefault();
            $("#save").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 82) {
            e.preventDefault();
            $("#refresh").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 85) {
            e.preventDefault();
            $("#update").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 80) {
            e.preventDefault();
            $("#print").click();
            return false;
        }
    });

}