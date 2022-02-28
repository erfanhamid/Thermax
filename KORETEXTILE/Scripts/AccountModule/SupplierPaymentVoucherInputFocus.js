function EnterPress() {
    //$("#Date").focus();
    $(document).on("keydown", "#Date", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#CreditAdvance").focus();
        }
    });
    $(document).on("keydown", "#Date", function (e) {
        if ( e.keyCode == 9) {
            e.preventDefault();
            $("#CreditAdvance").focus();
        }
    });
    $('#CreditAdvance').on('select2:select', function (e) {
        $("#GroupID").focus();
    });
    $('#GroupID').on('select2:select', function (e) {
        $("#SupplierID").focus();
    });
    $('#SupplierID').on('select2:select', function (e) {
        $("#PaymentAccID").focus();
    });
    $('#PaymentAccID').on('select2:select', function (e) {
        $("#RefNo").focus();
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Discription").focus();
        }
    });

    $(document).on("keydown", "#Discription", function (e) {

        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BillType").focus();
        }
    });
    $('#BillType').on('select2:select', function (e) {
        $("#BillNo").focus();
    });
    $('#BillNo').on('select2:select', function (e) {
        $("#PaidAmount").focus();
    });
    $(document).on("keydown", "#PaidAmount", function (e) {

        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            //$("#addToGrid").click();
            $("#addToGrid").focus();
        }
    });
    $(document).on("keydown", "#addToGrid", function (e) {

        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#BillType").focus();
        }
    });
    //$(document).on("keydown", "#addToGrid", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#save").focus();
    //    }
    //});
    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $(document).on("keydown", "#SPVNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });
}