function EnterPress() {

    $('#select2-BranchId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#BranchId').on('select2:select', function (e) {
        $("#CustomerId").focus();
    });

    $(document).on("keydown", "#CustomerId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchCustomer").focus();
        }
    });
    
    $(document).on("keydown", "#searchCustomer", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchCustomer").click();
            $("#InvoiceNo").focus();
        }
    });

    $('#select2-InvoiceNo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#InvoiceNo').on('select2:select', function (e) {
        $("#Amount").focus();
    });
    
    $(document).on("keydown", "#Amount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#InvoiceNo").focus();
        }
    });

    $(document).on("keydown", "#CAANo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });

    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
            $("#InvoiceNo").blur();
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