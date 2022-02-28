function EnterPress() {

    
    $('#Batch').on('select2:select', function (e) {
        $("#Descriptions").focus();
    });
    
    $(document).on("keydown", "#Descriptions", function (e) {
        if (e.keyCode == 13 || e.key == "Tab" || e.keyCode == 9) {
            $("#RefNo").focus();
        }
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.key == "Tab" || e.keyCode == 9) {
            $("#GroupID").focus();
        }
    });
    $('#GroupID').on('select2:select', function (e) {
        $("#ItemID").focus();
    });
    $('#Item').on('select2:select', function (e) {
        $("#Qty").focus();
    });
    
    $(document).on("keydown", "#Qty", function (e) {
        if (e.keyCode == 13 || e.key == "Tab" || e.keyCode == 9) {
            if ($("#Qty").val() == "" || $("#Qty").val() <= 0) {

            }
            else {
               
                $("#add").click();
                $("#ItemID").focus();
            }
           
        }
    });
    $(document).on("keydown", "#add", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#ItemID").focus();
        }
    });
    $(document).on("keydown", "#FGPNo", function (e) {
        if (e.keyCode == 13 ||  e.keyCode == 9) {
            $("#FGPNoSearch").focus();
        }
    });
    $(document).on("keyup", "#FGPNoSearch", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            $("#FGPNoSearch").click();
        }
    });
}