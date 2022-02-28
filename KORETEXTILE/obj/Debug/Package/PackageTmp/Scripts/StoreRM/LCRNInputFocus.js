function EnterPress() {
    $("#LCRNNo").focus();
    $(document).on("keydown", "#LCRNNo", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#search").focus();
        }
    });
    $(document).on("keydown", "#LCRNNo", function (e) {
        if (e.keyCode == 9) {
            e.preventDefault();
            $("#Date").focus();
        }
    });
    $(document).on("keydown", "#Date", function (e) {
        if (e.keyCode == 9 || e.keyCode == 13) {
            e.preventDefault();
            $("#ChallanNo").focus();
        }
    });
    $(document).on("keydown", "#ChallanNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SupplierId").focus();
        }
    });

    $('#select2-SupplierId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#SupplierId').on('select2:select', function (e) {
        $("#LCId").focus();
    });

    $('#select2-LCId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#LCId').on('select2:select', function (e) {
        $("#RefNo").focus();
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
            $("#StoreId").focus();
        }
    });
    //$('#select2-StoreId').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});
    $('#StoreId').on('select2:select', function (e) {
        $("#ItemId").focus();
    });
    $('#ItemId').on('select2:select', function (e) {
        $("#ReceiveQty").focus();
    });
    $(document).on("keydown", "#ReceiveQty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#ItemId").focus();
        }
    });
    //$(document).on("keydown", "#addToGrid", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#ItemId").focus();
    //    }
    //});
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 67) {
            e.preventDefault();
            $("#clear").click();
            return false;
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

    //$(document).on("keydown", function (e) {
    //    if (e.altKey && e.keyCode == 80) {
    //        e.preventDefault();
    //        $("#print").click();
    //        return false;
    //    }
    //});

    //$(document).on("keydown", "#GRNNo", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#search").focus();
    //    }
    //});

    //$(document).on("keydown", "#search", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#search").click();
    //    }
    //});
}