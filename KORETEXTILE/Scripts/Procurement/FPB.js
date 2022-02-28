function EnterPress() {
    $(document).on("keydown", "#BillNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchBillNo").focus();
        }
    });
    $(document).on("keydown", "#searchBillNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchBillNo").click();
            $("#update").focus();
        }
    });
    



    //Grid loop

    $(document).on("keydown", "#FRNNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });
    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#PDate").focus();
        }
    });

    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
            $("#clear").focus();
        }
    });
    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
            $("#addToGrid").focus();
        }
    });
    //end loop


    
    $("#Account").on('select2:select', function (e) {
        $("#Amount").focus();

    });
    $(document).on("keydown", "#PDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#DebitAccount").focus();
        }
    });

    $('#DebitAccount').on('select2:select', function (e) {
        $("#PurRef").focus();
    });

    $(document).on("keydown", "#PurRef", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PurDescription").focus();
        }
    });
    $(document).on("keydown", "#PurDescription", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Discount").focus();
        }
    });


    $(document).on("keydown", "#Discount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VatPerc").focus();
        }
    });
    $(document).on("keydown", "#VatPerc", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VatAmount").focus();
        }
    });
    $(document).on("keydown", "#VatAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VDAPerc").focus();
        }
    });
    $(document).on("keydown", "#VDAPerc", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VDAAmount").focus();
        }
    });
    $(document).on("keydown", "#VDAAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AitPerc").focus();
        }
    });

    $(document).on("keydown", "#AitPerc", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AitAmount").focus();
        }
    });
    $(document).on("keydown", "#AitAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
}