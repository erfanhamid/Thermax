function SalesModuleValidatePage()
{
    $(document).on("keydown", "#OrderNumber", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            $("#orderSearch").focus();
        }
    });
    $(document).on("keydown", "#orderSearch", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            $("#InvoiceNo").focus();
            return false;
        }
    });
    $(document).on("keydown", "#InvoiceNo", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            $("#CustomerID").focus();
            return false;
        }
    });

    $(document).on("keydown", "#CustomerID", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#searchCustomer").click();
            if ($("#Type").val() == "SalesEntry") {
                $("#SalesDate").focus();
            }
            else {
                $("#StoreId").focus();
            }
            
            return false;
         
        }
    });
   
    $(document).on("keydown", "#SRId", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            $("#searchEmployee").click();
            $("#SalesDate").focus();
            return false;
        }
    });
   
    $(document).on("keydown", "#SalesDate", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            e.preventDefault();
            $("#StoreId").focus();
            return false;
        }
    });
    $('#select2-StoreId').on('keyup', function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            e.preventDefault();
            $("#RefNo").focus();
            return false;
        }
    });
    $(document).on("keydown", "#RefNo", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#Description").focus();
            return false;
        }
    });
    $('#Description').on('keydown', function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {

            e.preventDefault();
            if ($("#InvoiceNo").val() != "") {
                $("#Remarks").focus();
            } else {
                $("#GroupId").focus();
            }
            
            return false;
        }
    });

    $(document).on("keydown", "#Remarks", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#GroupId").focus();
            return false;
        }
       
    });
    $('#StoreId').on('select2:select', function (e) {
        $("#RefNo").focus();
        return false;
    });
   
    $('#Item').on('select2:select', function (e) {
        $("#Carton").focus();
        return false;
    });
    $(document).on("keydown", "#Carton", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#SoldQuantity").focus();
            return false;
        }
    });
    $(document).on("keydown", "#SoldQuantity", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#FreeQty").focus();
            return false;
        }
    });
    //$(document).on("keydown", "#FreeQty", function (e) {
    //    if (e.keyCode == 13 || e.key == "Tab") {
    //        e.preventDefault();
    //        $("#DisPerc").focus();
    //        return false;
    //    }
    //});
    $(document).on("keydown", "#FreeQty", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#Price").focus();
            return false;
        }
    });
    $(document).on("keydown", "#Price", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            e.preventDefault();
            $("#VatPerc").focus();
            return false;
        }
    });
    $(document).on("keydown", "#VatPerc", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            e.preventDefault();
            $("#DisPerc").focus();
            return false;
        }
    });
    $(document).on("keydown", "#DisPerc", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            e.preventDefault();
            $("#addToGrid").focus();
            return false;
        }
    });
    $(document).on("keydown", "#addToGrid", function (e) {
        if (e.keyCode == 13 || e.key=="Tab") {
            e.preventDefault();
            $("#addToGrid").click();
            $("#Item").focus();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 67) {
            e.preventDefault();
            $("#clear").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey  && e.keyCode == 83) {
            e.preventDefault();
            $("#save").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey  && e.keyCode == 82) {
            e.preventDefault();
            $("#refresh").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey  && e.keyCode == 85) {
            e.preventDefault();
            $("#update").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey  && e.keyCode == 65) {
            e.preventDefault();
            $("#approve").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey  && e.keyCode == 80) {
            e.preventDefault();
            $("#print").click();
            return false;
        }
    });

}