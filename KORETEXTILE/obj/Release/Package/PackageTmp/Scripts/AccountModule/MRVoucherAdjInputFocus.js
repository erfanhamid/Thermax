function MRVAFieldEnterPress() {
    
    $('#Date').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#CostCenterId").focus();
        }
    });

    //$('#select2-CostCenterId').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //    }
    //});
    $('#CostCenterId').on('select2:select', function (e) {
        $("#EmpID").focus();
    });
    
    $('#EmpID').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").focus();
        }
    });
    $('#go').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#go").click();
            $("#DebitAcId").focus();

        }
    });
    $('#DebitAcId').on('select2:select', function (e) {
        $("#CostGroupId").focus();
    });
    $('#CostGroupId').on('select2:select', function (e) {
        $("#MRVAAmount").focus();
    });
    $('#MRVAAmount').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#RefNo").focus();
        }
    });
    
    $('#RefNo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MRVADescription").focus();
        }
    });
    $('#MRVADescription').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#add").focus();
            $("#add").click();

        }
    });


}