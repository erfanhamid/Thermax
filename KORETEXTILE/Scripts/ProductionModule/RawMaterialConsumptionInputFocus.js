function EnterPressForRawMaterialConsumption() {
    //$('#GroupID').focus();
    $('#BatchID').focus();

    $('#BatchID').on('select2:select', function (e) {
        $("#Descriptions").focus();
    });
    $(document).on("keydown", "#Descriptions", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#RMCDate").focus();
        }
    });
    $(document).on("keydown", "#RMCDate", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#RefNo").focus();
        }
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#GroupID").focus();
        }
    });
    //$(document).on("keydown", "#GroupID", function (e) {
    //    if (e.keyCode == 13 || e.key == "Tab") {
    //        $("#ItemID").focus();
    //    }
    //});
    $('#GroupID').on('select2:select', function (e) {
        $("#ItemID").focus();
    });
    $('#ItemID').on('select2:select', function (e) {
        $("#ItemQty").focus();
    });
    $(document).on("keydown", "#ItemQty", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#add").focus();
           
        }
    }); 
    $(document).on("keydown", "#add", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#ItemID").focus();
        }
    }); 
    $(document).on("keydown", "#RMCNo", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#RMCNoSearch").focus();
        }
    });
    
}