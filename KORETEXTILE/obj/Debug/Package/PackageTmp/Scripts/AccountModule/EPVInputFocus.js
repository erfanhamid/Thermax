function EPVFieldEnterPress()
{
    $('#PayAccId').focus();
    $('#PayAccId').on('select2:select', function (e) {
        $("#Date").focus();
    });
    $("#Date").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });
    $("#RefNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Description").focus();
        }
    });
    $("#Description").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#EmployeeId").focus();
        }
    });
    $("#EmployeeId").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").focus();
            $("#go").click();
            $("#Amount").focus();
        }
    });
    $("#Amount").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#add").focus();
            $("#add").click();
            $("#EmployeeId").focus();

        }
    });
    $("#EPVNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Search").focus();
            $("#Search").click();
        }
    });
}