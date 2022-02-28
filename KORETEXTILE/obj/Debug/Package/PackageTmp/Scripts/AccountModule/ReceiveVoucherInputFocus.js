function ReceiveVFieldEnterPress() {
    //$("#RVNo").focus();
    //$("#RVDate").focus();
    $("#ReceiveAccountID").focus();
    $(document).on("keydown", "#RVDate", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#RVDate").click();
            $("#RVDate").focus();
        }
    });

    $(document).on("keydown", "#RVDate", function (e) {
        if (e.keyCode == 9) {
            e.preventDefault();
            $("#ReceiveAccountID").focus();
        }
    });

    $('#select2-ReceiveAccountID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ReceiveAccountID').on('select2:select', function (e) {
        $("#BranchID").focus();
    });

    $('#select2-BranchID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#BranchID').on('select2:select', function (e) {
        $("#CostGroupID").focus();
    });

    $('#select2-CostGroupID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#CostGroupID').on('select2:select', function (e) {
        $("#BussinessUnitId").focus();
    });

    $('#select2-BussinessUnitId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#BussinessUnitId').on('select2:select', function (e) {
        $("#CreditAccountID").focus();
    });

    $('#select2-CreditAccountID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#CreditAccountID').on('select2:select', function (e) {
        $("#RVAmount").focus();
    });

    $(document).on("keydown", "#RVAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PayeeName").focus();
        }
    });

    //$('#select2-PayeeName').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});

    //$('#PayeeName').on('select2:select', function (e) {
    //    $("#RefNo").focus();
    //});

    $(document).on("keydown", "#PayeeName", function (e) {
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
            $("#save").focus();
        }
    });
    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $(document).on("keydown", "#RVNo", function (e) {
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
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 80) {
            e.preventDefault();
            $("#print").click();
        }
    });

}