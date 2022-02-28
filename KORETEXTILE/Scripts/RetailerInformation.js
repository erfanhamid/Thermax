function EnterPress() {
    $(document).on("keydown", "#RetailerId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $('#search').click();
        }
    });

    $('#select2-DepotId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#DepotId').on('select2:select', function (e) {
        $("#CustomerID").focus();
        e.preventDefault();
    });

    $('#select2-CustomerID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#CustomerID').on('select2:select', function (e) {
        $("#RetailerName").focus();
        e.preventDefault();
    });

    $(document).on("keydown", "#RetailerName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#IsDealerYes").focus();
        }
    });

    $(document).on("keydown", "#IsDealerYes", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#IsDealerNo").focus();
        }
    });
    $(document).on("keydown", "#IsDealerNo", function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            $("#IsDealerNo").prop("checked", true);
            $("#OB").focus();
        } else if (e.keyCode == 9) {
            e.preventDefault();
            $("#OB").focus();
        }
    });

    $(document).on("keydown", "#OB", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ContactPerson").focus();
        }
    });

    $(document).on("keydown", "#ContactPerson", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Mobile").focus();
        }
    });
    $(document).on("keydown", "#Mobile", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Phone").focus();
        }
    });
    $(document).on("keydown", "#Phone", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Email").focus();
        }
    });
    $(document).on("keydown", "#Email", function (e) {
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
}