function SupplierGroupEnterpress() {
    $("#SgroupName").focus();
    $(document).on("keydown", "#SgroupName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SgroupCode").focus();
        }
    });
    $('#SgroupCode').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });

}