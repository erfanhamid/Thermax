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
    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
            $("#save").focus();
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
            $("#PDate").focus();
        }
    });
    $(document).on("keydown", "#GRNNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });
    $("#Account").on('select2:select', function (e) {
        $("#Amount").focus();

    });
    $(document).on("keydown", "#PDate", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PurRef").focus();
        }
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
            $("#addToGrid").focus();
        }
    });
}