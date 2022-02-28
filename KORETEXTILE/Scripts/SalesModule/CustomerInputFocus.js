function SalesModuleCustomerPage() {
    
    $(document).on("keydown", "#Id", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#custSearch").focus();
        }
    });

    $(document).on("keydown", "#custSearch", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#custSearch").click();
        }
    });

    $(document).on("keydown", "#Id", function (e) {
        if (e.keyCode == 9) {
            e.preventDefault();
            $("#TSOId").focus();
        }
    });

    $('#select2-TSOId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });
    $('#TSOId').on('select2:select', function (e) {
        $("#CustomerName").focus();
    });

    $(document).on("keydown", "#CustomerName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Credit").focus();
        }
    });

    //$('#select2-DepotId').on('keyup', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});
    //$('#DepotId').on('select2:select', function (e) {
    //    $("#Credit").focus();
    //});

    $(document).on("keydown", "#Credit", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#OB").focus();
        }
    });



    $(document).on("keydown", "#OB", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ZoneId").focus();
        }
    });

    $('#select2-ZoneId').on('keyup', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#ZoneId').on('select2:select', function (e) {
        $("#ConPerson").focus();
    });

 
    $(document).on("keydown", "#ConPerson", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MobileNo").focus();
        }
    });

    $(document).on("keydown", "#MobileNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#TelephoneNo").focus();
        }
    });

    $(document).on("keydown", "#TelephoneNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#EmailId").focus();
        }
    });

    $(document).on("keydown", "#EmailId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BillTo").focus();
        }
    });

    $(document).on("keydown", "#BillTo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ShipTo").focus();
        }
    });

    $(document).on("keydown", "#ShipTo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });

    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
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