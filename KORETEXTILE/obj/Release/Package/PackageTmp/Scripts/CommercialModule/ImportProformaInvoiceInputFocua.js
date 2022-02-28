function ImportProformaInvoiceEnterPress() {
    $("#IPINO").focus();
    $('#IPIId').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#IPIDSearch").click();
        }
    });
    $('#IPINO').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MoPId").focus();
        }
    });
    $("#MoPId").on('select2:select', function (e) {
        $("#IncotermsId").focus();

    });
    $("#IncotermsId").on('select2:select', function (e) {
        $("#PartialShipment").focus();

    });
    $("#PartialShipment").on('select2:select', function (e) {
        $("#Transhipment").focus();
    });
    $("#Transhipment").on('select2:select', function (e) {
        $("#LCType").focus();
    });
    $("#LCType").on('select2:select', function (e) {
        $("#SupplierId").focus();
    });
    $("#SupplierId").on('select2:select', function (e) {
        $("#SupplierAccNo").focus();
    });
    $('#SupplierAccNo').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SupBankName").focus();
        }
    });
    $('#SupBankName').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SupBankAddress").focus();
        }
    });
    $('#SupBankAddress').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#SwiftCode").focus();
        }
    });
    $('#SwiftCode').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#CurrencyId").focus();
        }
    });
    $("#CurrencyId").on('select2:select', function (e) {
        $("#LoadingPortId").focus();
    });
    //$("#LCType").on('select2:select', function (e) {
    //    $("#LoadingPortId").focus();
    //});
    $("#LoadingPortId").on('select2:select', function (e) {
        $("#DischargePortId").focus();
    });
    //$("#BeneficiaryBankId").on('select2:select', function (e) {
    //    $("#SwiftCode").focus();
    //});
    
    $("#DischargePortId").on('select2:select', function (e) {
        $("#IssuingBankId").focus();
    });
    $("#IssuingBankId").on('select2:select', function (e) {
        $("#BeneficiaryBankId").focus();
    });
    $("#BeneficiaryBankId").on('select2:select', function (e) {
        $("#PurchaseOrderNo").focus();
    });
    //$('#IPIDate').on('keydown', function (e) {
    //    if (e.keyCode == 13 || e.keyCode == 9) {
    //        e.preventDefault();
    //        $("#CurrencyId").focus();
    //    }
    //});
    
    
    $("#PurchaseOrderNo").on('select2:select', function (e) {
        $("#GroupId").focus();
    });
    $("#GroupId").on('select2:select', function (e) {
        $("#ItemId").focus();
    });
    $("#ItemId").on('select2:select', function (e) {
        $("#Qty").focus();
    });
    $('#Qty').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#Rate").focus();
        }
    });
    $('#Rate').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#HSCode").focus();
        }
    });
    $('#HSCode').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PackingNote").focus();
        }
    });
    $('#PackingNote').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#add").focus();
        }
    }); 
    $('#PackingNote').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#add").click();
            $("#GroupId").focus();
        }
    });
    $('#Freight').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    $('#save').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 83) {
            e.preventDefault();
            $("#save").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 82) {
            e.preventDefault();
            $("#refresh").click();
            return false;
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 85) {
            e.preventDefault();
            $("#update").click();
            return false;
        }
    });
}