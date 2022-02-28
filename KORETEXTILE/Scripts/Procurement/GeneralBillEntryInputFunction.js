function GeneralBillEnterPress() {
    $("#DepotId").focus();
    $('#DepotId').on('select2:select', function (e) {
        $("#RefNo").focus();
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#SGId").focus();
        }
    }); 
    $('#SGId').on('select2:select', function (e) {
        $("#SupplierId").focus();
    });
    $('#SupplierId').on('select2:select', function (e) {
        $("#GBEDate").focus();
    });
    $(document).on("keydown", "#GBEDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#DebitAccId").focus();
        }
    });
    $('#DebitAccId').on('select2:select', function (e) {
        $("#CGId").focus();
    });
    $('#CGId').on('select2:select', function (e) {
        $("#Amount").focus();
    });
    
    $(document).on("keydown", "#Amount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Descriptions").focus();
        }
    }); 
    $(document).on("keydown", "#Descriptions", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#DebitAccId").focus();
        }
    });

    //Amount
    $(document).on("keydown", "#Discountamt", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VAT").focus();
        }
    }); $(document).on("keydown", "#VAT", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#VDS").focus();
        }
    }); $(document).on("keydown", "#VDS", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AIT").focus();
        }
    }); $(document).on("keydown", "#AIT", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });


    $(document).on("keydown", "#GBENo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#GBENo").focus();
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