function EnterPress() {
    $("#Year").on('select2:select', function (e) {
        $("#Month").focus();
    });
    $("#Month").on('select2:select', function (e) {
        $("#Date").focus();
    });
    $(document).on("keydown", "#Date", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#employee").focus();
        }
    });
    $("#employee").on('select2:select', function (e) {
        $("#Absent").focus();
    });
    $(document).on("keydown", "#Absent", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#employee").focus();
            $("#addToGrid").click();
        }
    });
    //$(document).on("keydown", "#addToGrid", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#addToGrid").click();
    //        $("#save").focus();
    //    }
    //});
    //$(document).on("keydown", "#save", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#save").click();
    //        $("#refresh").focus();
    //    }
    //});
    //$(document).on("keydown", "#search", function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});
    
}