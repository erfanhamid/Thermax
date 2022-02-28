function keyPress() {
    $("#LTRID").focus();
    $('#LTRID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LTRIDsearch").focus();
            
        }
    });
    $('#LTRIDsearch').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LTRIDsearch").click();
            $('#LTRNO').focus();
        }
    });

    $('#LTRNO').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Date").focus();
           
        }
    });
    $('#Date').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Amount").focus();

        }
    });
    $('#Amount').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();

        }
    });
    $('#RefNo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();

        }
    });
    $('#Description').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LCID").focus();

        }
    });
    $('#LCID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LCIDGo").focus();

        }
    });
    $('#LCIDGo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#LCIDGo").click();
            if (IsSearch == true) {
                $("#update").focus();
            } else {
                $("#save").focus();
            }
            //$('#LCNo').focus();
        }
    });
    //$('#LCNo').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#LCNoGo").focus();

    //    }
    //});
    //$('#LCNoGo').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#LCNoGo").click();
    //        $('#Description').focus();
    //    }
    //});

    //$('#Description').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        if (IsSearch == true) {
    //            $("#update").focus();
    //        } else {
    //            $("#save").focus();
    //        }
            
    //    }
    //});

    $('#save').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
            $('#LTRID').focus();
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