function EnterPress() {

    $('#select2-AssesmentYear').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#AssesmentYear').on('select2:select', function (e) {
        $("#Location").focus();
    });

    $('#select2-Location').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#Location').on('select2:select', function (e) {
        $("#Corpuration").focus();
    });


    $('#select2-Corpuration').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
        }
    });

    $('#Corpuration').on('select2:select', function (e) {
        $("#search").focus();
    });
    
    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
        }
    });

    $(document).on("keydown", "#ID", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchId").focus();
        }
    });

    $(document).on("keydown", "#searchId", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#searchId").click();
        }
    });
}