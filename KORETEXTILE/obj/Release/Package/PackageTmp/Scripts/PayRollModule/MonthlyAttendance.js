function ELREntry() {
    $('#Year').focus();
    $("#Year").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
            $("#EmployeeID").focus();
        }
    });
    
}