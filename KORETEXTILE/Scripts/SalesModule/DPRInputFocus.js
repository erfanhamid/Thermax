function KeyPress(IsUpdate) {
    $(document).on("keydown", "#RPID", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#search").focus();
        }
    });

    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#search").click();

        }
    });

    $(document).on("keydown", "#CustomerID", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#go").focus();
        }
    });
    $(document).on("keydown", "#go", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#go").click();
            $("#TDate").focus();

        }
    });
    $(document).on("keydown", "#TDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AccountID").focus();
        }
    });
    $('#AccountID').on('select2:select', function (e) {
        $("#ReceiveAmt").focus();
    });
    $(document).on("keydown", "#ReceiveAmt", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BankCharges").focus();
        }
    });
    $(document).on("keydown", "#BankCharges", function (e) {
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
            if (IsUpdate == true) {
                $("#update").focus();
            } else {
                $("#save").focus();
            }
           
        }
    });


    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
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