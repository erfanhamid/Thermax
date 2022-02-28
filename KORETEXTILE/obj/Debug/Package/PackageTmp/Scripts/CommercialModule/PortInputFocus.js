function ImportPortFieldEnterPress() {
    $("#PortName").focus();
    $('#PortName').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    $("#save").on('select2:select', function (e) {
        $("#save").focus();
    });
}