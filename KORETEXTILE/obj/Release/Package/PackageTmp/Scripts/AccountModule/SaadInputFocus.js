function SaadFieldEnterPress() {
    $("#Date").focus();
    $("#Date").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LedgerAcId").focus();
        }
    });

    $('#LedgerAcId').on('select2:select', function (e) {
        $("#SupplierId").focus();
    });
    $('#SupplierId').on('select2:select', function (e) {
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
            $("#save").click();
        }
    });
    $("#SAADNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Search").focus();
            $("#Search").click();
        }
    });
    
}