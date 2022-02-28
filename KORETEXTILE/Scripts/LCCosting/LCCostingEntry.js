function EnterPress() {
    $("#LCPCNo").focus();
    $(document).on("keydown", "#LCPCNo", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#search").focus();
            $("#search").click();
            $("#ILCID").focus();
        }
    });
    $(document).on("keydown", "#ILCID", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#go").focus();
            $("#go").click();
            $("#LCPCDate").focus();
        }
    });

    $(document).on("click", '[name="editRow1"]', function (e) {
        $("#CostingQty").focus();
    });
    $(document).on("click", '[name="editRow"]', function (e) {
        $("#CostingQty").focus();
    });
    $(document).on("keydown", "#CostingQty", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#Rate").focus();
        }
    });

    $(document).on("keydown", "#Rate", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#AddLCBtn").focus();
        }
    });

    $(document).on("keydown", "#AddLCBtn", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#AddLCBtn").click();
            $("#save").focus();
        }
    });

    $(document).on("#save", "keydown", function (e) {
        if (e.altKey && e.keyCode === 83) {
            e.preventDefault();
            $("#save").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 82) {
            e.preventDefault();
            $("#refresh").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 85) {
            e.preventDefault();
            $("#update").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode === 80) {
            e.preventDefault();
            $("#print").click();
            return false;
        }
    });

}