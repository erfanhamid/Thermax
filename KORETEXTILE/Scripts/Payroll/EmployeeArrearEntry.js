function EnterPress() {
    $(document).on("keydown", "#ARAmount", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ARRefNO").focus();
        }
    });
    $(document).on("keydown", "#ARRefNO", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ARDescription").focus();
        }
    });
    $(document).on("keydown", "#ARDescription", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    $(document).on('keydown', "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
}