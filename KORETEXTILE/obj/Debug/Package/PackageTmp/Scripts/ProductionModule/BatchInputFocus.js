function BatchFieldEnterPress() {
    $(document).on("keydown", "#batchid", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BatchIdSearch").click();
            
        }
    });
   
    $("#BatchNo").focus();
    $(document).on("keydown", "#BatchNo", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BatchDate").focus();
        }
    });
    $('#BatchDate').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#GroupId").focus();
        }
    });
    $('#GroupId').on('select2:select', function (e) {
        $("#ItemID").focus();
    });

    $('#ItemID').on('select2:select', function (e) {
        $("#Qty").focus();
    });
    $('#Qty').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#BatchDesc").focus();
        }
    });
    $('#BatchDesc').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#add").focus();
        }
    });
    $('#add').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $('#add').click();
            $("#ItemID").focus();
         
        }
    });
   
}