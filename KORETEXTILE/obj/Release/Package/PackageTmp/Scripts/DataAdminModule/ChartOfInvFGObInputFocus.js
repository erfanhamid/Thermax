function EnterPress() {
    $(document).on("keydown", "#FGOBNo", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#fgOpenSearch").click();
            $("#StoreId").focus();
        }
    });

    $('#DepotId').on('select2:select', function (e) {
        $("#StoreId").focus();
        return false;
    });

    $('#StoreId').on('select2:select', function (e) {
        $("#GroupId").focus();
        return false;
    });
    $('#GroupId').on('select2:select', function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#ItemId").focus();
        }
    });

    $('#Item').on('select2:select', function (e) {
        if ($(this).val() != "") {
            $("#Quantity").focus();
        }
    });

    $(document).on("keydown", "#Quantity", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#add").focus();
        }
    });
    $(document).on("keydown", "#add", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#GroupId").focus();
        }
    });

    $(document).on("keydown", "#SalesDate", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#StoreId").focus();
            return false;
        }
    });
    $('#select2-StoreId').on('keyup', function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#GroupId").focus();
            return false;
        }
    });
    $('#StoreId').on('select2:select', function (e) {
        $("#GroupId").focus();
        return false;
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
        if (e.altKey && e.keyCode == 65) {
            e.preventDefault();
            $("#approve").click();
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


}