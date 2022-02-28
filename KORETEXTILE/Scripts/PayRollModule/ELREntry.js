function ELREntry() {
    $('#ELRNo').focus();
    $("#ELRNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
            $("#EmployeeID").focus();
        }
    });
    $("#EmployeeID").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").focus();
            $("#go").click();
            $("#Date").focus();
        }
    });
    $("#Date").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Name").focus();
        }
    });
    $("#Name").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Designation").focus();
        }
    });
    $("#Designation").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#FD").focus();
        }
    });
    $("#FD").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#WorkStation").focus();
        }
    });
    $("#FD").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#WorkStation").focus();
        }
    });
    $("#WorkStation").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Department").focus();
        }
    });
    $("#Department").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LeaveTypeID").focus();
        }
    });

    $('#LeaveTypeID').on('select2:select', function (e) {
        $("#RefNo").focus();
    });

    $("#RefNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#FromDate").focus();
        }
    });
    $("#FromDate").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#EndDate").focus();
        }
    });
    $("#EndDate").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LeaveDays").focus();
        }
    });
    $("#LeaveDays").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });
    $("#Description").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
            $("#save").click();
        }
    });
}