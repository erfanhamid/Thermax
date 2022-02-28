function EnterPress() {

    $('#select2-BranchId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#BranchId').on('select2:select', function (e) {
        $("#CostGroupId").focus();
    });

    $('#select2-CostGroupId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    
    $('#CostGroupId').on('select2:select', function (e) {
        $("#AccountHeadID").focus();
    });

    $('#select2-AccountHeadID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#AccountHeadID').on('select2:select', function (e) {
        $("#DebOrCre").focus();
    });

    $('#select2-DebOrCre').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#DebOrCre').on('select2:select', function (e) {
        $("#JVAmount").focus();
    });

    $(document).on("keydown", "#JVAmount", function (e) {
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
            $("#AccountHeadID").focus();
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
            $("#SubJVAmount").focus();
        }
    });

    $(document).on("keydown", "#SubJVAmount", function (e) {
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
    
    $(document).on("keydown", "#JVNO", function (e) {
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