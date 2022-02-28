function MRVFieldEnterPress() {
    $("#EmployeeId").focus();
    $(document).on("keydown", "#EmployeeId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").click();
            $("#go").focus();
        }
    });
    $("#go").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Date").focus();
        }
    });
    $("#Date").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PaymentAcId").focus();
        }
    });
    $('#PaymentAcId').on('select2:select', function (e) {
        $("#Amount").focus();
    });
    $("#Amount").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });
    $("#RefNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });
    $("#Description").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });  
    $("#save").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $("#MRVId").on("keydown", function (e) {
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
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 82) {
            e.preventDefault();
            $("#refresh").click();
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 85) {
            e.preventDefault();
            $("#update").click();
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 68) {
            e.preventDefault();
            $("#delete").click();
        }
    });

}
