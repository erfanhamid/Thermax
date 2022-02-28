function EnterPress() {


    //$(document).on("keydown", "#clmDescription", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        $("#Refno").focus();
    //    }
    //});

    $(document).on("keydown", "#Refno", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#GroupID").focus();
        }
    });
    
    $('#select2-GroupID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#GroupID').on('select2:select', function (e) {
        $("#ItemID").focus();
    });

    $('#select2-ItemID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#ItemID').on('select2:select', function (e) {
        $("#Qty").focus();
    });
    
    $(document).on("keydown", "#Qty", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").focus();
        }
    });

    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#addToGrid").click();
        }
    });

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

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 80) {
            e.preventDefault();
            $("#print").click();
            return false;
        }
    });

    $(document).on("keydown", "#IRMID", function (e) {
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
}