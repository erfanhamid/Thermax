function EnterPress() {

    $(document).on("keydown", "#Name", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Type").focus();
        }
    });
    
    $('#select2-Type').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#Type').on('select2:select', function (e) {
        $("#save").focus();
    });

}