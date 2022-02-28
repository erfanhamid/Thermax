function EnterPress() {
    
    $(document).on("keydown", "#ChallanNo", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#SupplierID").focus();
        }
    });
    
    $('#select2-SupplierID').on('keydown', function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
        }
    });

    $('#SupplierID').on('select2:select', function (e) {
        $("#WONo").focus();
    });

    $('#select2-WONo').on('keydown', function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
        }
    });
    $('#WONo').on('select2:select', function (e) {
        $("#RefNo").focus();
    });

    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#Descriptions").focus();
        }
    });

    $(document).on("keydown", "#Descriptions", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("ReceiveQty").focus();
        }
    });

    $(document).on("keydown", "#ReceiveQty", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });
    
    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#addToGrid").click();
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 67) {
            e.preventDefault();
            $("#clear").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
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

    $(document).on("keydown", "#GRNNo", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });

    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#search").click();
        }
    });
}