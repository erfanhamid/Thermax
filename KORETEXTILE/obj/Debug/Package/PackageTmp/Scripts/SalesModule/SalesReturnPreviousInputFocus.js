function EnterPress() {

    $('#select2-ItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ItemID').on('select2:select', function (e) {
        $("#ReturnQty").focus();
    });

    $(document).on("keydown", "#ReturnQty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Price").focus();
        }
    });

    //$(document).on("keydown", "#ReturnQty", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#SRDate").focus();
    //    }
    //});

    $(document).on("keydown", "#SRDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });

    $(document).on("keydown", "#Description", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ItemID").focus();
        }
    });


    $(document).on("keydown", "#Price", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ClmCostPrice").focus();
        }
    });

    $(document).on("keydown", "#ClmCostPrice", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#ItemID").focus();
        }
    });

    $(document).on("keydown", "#InvoiceNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Store").focus();
        }
    });

    $('#select2-Store').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#Store').on('select2:select', function (e) {
        $("#TSO").focus();
    });

    $('#select2-TSO').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#TSO').on('select2:select', function (e) {
        $("#CustomerId").focus();
    });

    $('#select2-BranchId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#BranchId').on('select2:select', function (e) {
        $("#Store").focus();
    });

    $('#select2-CustomerId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#CustomerId').on('select2:select', function (e) {
        $("#SRDate").focus();
    });
    
    $(document).on("keydown", "#SRNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#srSearch").focus();
        }
    });

    $(document).on("keydown", "#srSearch", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#srSearch").click();
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