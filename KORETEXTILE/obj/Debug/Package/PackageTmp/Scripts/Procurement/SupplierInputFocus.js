function SupplierEnterpress() {
    $("#GroupID").focus();
    $(document).on("keydown", "#Id", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#SNoSearch").click();
        }
    });
    //$(document).on("keydown", "#Id", function (e) {
    //    if (e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#GroupID").focus();
    //    }
    //});
    $('#GroupID').on('select2:select', function (e) {
        $("#SupplierName").focus();
    });

    $(document).on("keydown", "#SupplierName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#OB").focus();
        }
    });
    //$(document).on("keydown", "#BankAccountNumber", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#SCode").focus();
    //    }
    //});
    $(document).on("keydown", "#OB", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SCode").focus();
        }
    });
    //$(document).on("keydown", "#AsDate", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#SCode").focus();
    //    }
    //});
    $(document).on("keydown", "#SCode", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BankAccountNumber").focus();
        }
    });
    $(document).on("keydown", "#BankAccountNumber", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BankName").focus();
        }
    });
    $(document).on("keydown", "#BankName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BankAddress").focus();
        }
    });
    $(document).on("keydown", "#BankAddress", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ContactPerson").focus();
        }
    });
    $(document).on("keydown", "#ContactPerson", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MobileNo").focus();
        }
    });
    $(document).on("keydown", "#MobileNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#TelephoneNo").focus();
        }
    });
    $(document).on("keydown", "#TelephoneNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#EmailId").focus();
        }
    }); 
    $(document).on("keydown", "#EmailId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Address").focus();
        }
    });
    $('#Address').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });

}