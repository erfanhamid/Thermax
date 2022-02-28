function EnterPress() {
    
    $('#select2-SupplierID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#SupplierID').on('select2:select', function (e) {
        $("#AdjustmentAmnt").focus();
    });

    $(document).on("keydown", "#AdjustmentAmnt", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });

    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Descriptions").focus();
        }
    });

    $(document).on("keydown", "#Descriptions", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });

    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });

    $(document).on("keydown", "#EmployeeID", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").focus();
        }
    });

    $(document).on("keydown", "#go", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").click();
            $("#SupplierID").focus();
        }
    });

    $(document).on("keydown", "#MRPANo", function (e) {
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