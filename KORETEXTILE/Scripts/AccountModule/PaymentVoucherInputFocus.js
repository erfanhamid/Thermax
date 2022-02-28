function EnterPress() {

    $('#select2-SalesCenterID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#SalesCenterID').on('select2:select', function (e) {
        $("#CashBankID").focus();
    });

    $('#select2-CashBankID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#CashBankID').on('select2:select', function (e) {
        $("#ReceiverName").focus();
    });

    $(document).on("keydown", "#ReceiverName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PVDate").focus();
        }
    });

    $(document).on("keydown", "#PVDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });
    
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Discription").focus();
        }
    });
    $('#Discription').on('select2:select', function (e) {
        $("#BillType").focus();
    });


    $('#select2-BillType').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            $("#BillNo").focus();
        }
    });

   

    $('#BillType').on('select2:select', function (e) {
        $("#BillNo").focus();
    });

    $('#select2-CostGroupID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#CostGroupID').on('select2:select', function (e) {
        $("#RVAmount").focus();
    });

    $(document).on("keydown", "#RVAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });

    $(document).on("keydown", "#Description", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#DebitAccID").focus();
        }
    });

    $('#select2-SubLedgerID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#SubLedgerID').on('select2:select', function (e) {
        $("#Note").focus();
    });

    $(document).on("keydown", "#Note", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PVAmount").focus();
        }
    });

    $(document).on("keydown", "#PVAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#subAddToGrid").focus();
        }
    });

    $(document).on("keydown", "#subAddToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#subAddToGrid").click();
            $("#SubLedgerID").focus();
        }
    });

    $(document).on("keydown", "#PVNO", function (e) {
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