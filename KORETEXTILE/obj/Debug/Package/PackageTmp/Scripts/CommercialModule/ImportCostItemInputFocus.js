function ImportCostItemFieldEnterPress() {
    $("#CostItem").focus();
    $('#CostItem').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AccountHead").focus();
        }
    });
    $("#AccountHead").on('select2:select', function (e) {
        $("#save").focus();

    });
}