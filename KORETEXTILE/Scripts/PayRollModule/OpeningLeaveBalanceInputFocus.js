function EnterPress() {
    $("#CasualLeave").focus();
    $("#CasualLeave").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SickLeave").focus();
        }
    });
    $("#SickLeave").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#EarnLeave").focus();
        }
    });
    $("#EarnLeave").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
            $("#save").click();
        }
    });
}