function ValidateReportSearchParam(items) {
    $("#message").val("false");
    if (items.indexOf("From")>=0)
    {
        var fromDate = $("#From").val();
        if (typeof ($("#From").val()) !== 'undefined' && $("#From").val() != null) {
            if (fromDate == "") {
                setError("From", "From Date Is Required Field");
            }
            else {
                setError("From", "");
            }
        }
    }
    
    if (items.indexOf("To") >= 0) {
        var toDate = $("#To").val();
        if (typeof ($("#To").val()) !== 'undefined' && $("#To").val() != null) {
            if (toDate == "") {
                setError("To", "To Date Is Required Field");
            }
            else {
                setError("To", "");
            }
        }
    }
    if (items.indexOf("StoreId") >= 0) {
        var store = $("#StoreId").val();
        if (typeof ($("#StoreId").val()) !== 'undefined' && $("#StoreId").val() != null) {
            if (store == "") {
                setError("StoreId", "Please select a Store");
            }
            else {
                setError("StoreId", "");
            }
        }
    }
    if (items.indexOf("ItemID") >= 0) {
        var itemid = $("#ItemID").val();
        if (typeof ($("#ItemID").val()) !== 'undefined' && $("#ItemID").val() != null) {
            if (itemid == "") {
                setError("ItemID", "Please select a Item");
            }
            else {
                setError("ItemID", "");
            }
        }
    }
    if (items.indexOf("SupplierID") >= 0) {
        var store = $("#SupplierID").val();
        if (typeof ($("#SupplierID").val()) != 'undefined' && $("#SupplierID").val() != null) {
            if (store == "") {
                setError("SupplierID", "Supplier Is Required Field");
            }
            else {
                setError("SupplierID", "");
            }
        }
    }
    if (items.indexOf("StoreFor") >= 0) {
        var store = $("#StoreFor").val();
        if (typeof ($("#StoreFor").val()) != 'undefined' && $("#StoreFor").val() != null) {
            if (store == "") {
                setError("StoreFor", "Store For Required Field");
            }
            else {
                setError("StoreFor", "");
            }
        }
    }
    if (items.indexOf("RSMId") >= 0) {
        var store = $("#RSMId").val();
        if (typeof ($("#RSMId").val()) != 'undefined' && $("#RSMId").val() != null) {
            if (store == "") {
                setError("RSMId", "RSM is Required Field");
            }
            else {
                setError("RSMId", "");
            }
        }
    } 
    

    if (items.indexOf("LedgerAC") >= 0) {
        var ledgerAc = $("#LedgerAC").val();
        if (typeof ($("#LedgerAC").val()) != 'undefined' && $("#LedgerAC").val() != null) {
            if (ledgerAc == "") {
                setError("LedgerAC", "Ledger Acc Is Required Field");
            }
            else {
                setError("LedgerAC", "");
            }
        }
    }
    
    if (items.indexOf("DepotId") >= 0) {
        var depotId = $("#DepotId").val();
        if (typeof ($("#DepotId").val()) != 'undefined' && $("#DepotId").val() != null) {
            if (depotId == "") {
                setError("DepotId", "Depot Id Is Required Field");
            }
            else {
                setError("DepotId", "");
            }
        }
    }
    if (items.indexOf("Depot") >= 0) {
        var depotId = $("#Depot").val();
        if (typeof ($("#Depot").val()) != 'undefined' && $("#Depot").val() != null) {
            if (depotId == "") {
                setError("Depot", "Please Select a Depot.");
            }
            else {
                setError("Depot", "");
            }
        }
    }
    
    if (items.indexOf("CustomerID") >= 0) {
        var customerId = $("#CustomerID").val();
        if (typeof ($("#CustomerID").val()) != 'undefined' && $("#CustomerID").val() != null) {
            if (customerId == "") {
                setError("CustomerID", "Customer Id Is Required Field");
            }
            else {
                setError("CustomerID", "");
            }
        }
    }
    if (items.indexOf("EmployeeID") >= 0) {
        var customerId = $("#EmployeeID").val();
        if (typeof ($("#EmployeeID").val()) != 'undefined' && $("#EmployeeID").val() != null) {
            if (customerId == "") {
                setError("EmployeeID", "Employee ID Is Required Field");
            }
            else {
                setError("EmployeeID", "");
            }
        }
    }
    if (items.indexOf("ILCID") >= 0) {
        var customerId = $("#ILCID").val();
        if (typeof ($("#ILCID").val()) != 'undefined' && $("#ILCID").val() != null) {
            if (customerId == "") {
                setError("ILCID", "Please enter a ILC Id first");
            }
            else {
                setError("ILCID", "");
            }
        }
    }
    if (items.indexOf("LTRID") >= 0) {
        var customerId = $("#LTRID").val();
        if (typeof ($("#LTRID").val()) != 'undefined' && $("#LTRID").val() != null) {
            if (customerId == "") {
                setError("LTRID", "Please enter a LATR Id first");
            }
            else {
                setError("LTRID", "");
            }
        }
    }
    if (items.indexOf("TSOId") >= 0) {
        var tso = $("#TSOId").val();
        if (typeof ($("#TSOId").val()) != 'undefined' && $("#TSOId").val() != null) {
            if (tso == "") {
                setError("TSOId", "TSO Is Required Field");
            }
            else {
                setError("TSOId", "");
            }
        }
    }


    if (items.indexOf("Group") >= 0) {
        var group = $("#Group").val();
        if (typeof ($("#Group").val()) != 'undefined' && $("#Group").val() != null) {
            if (group == "") {
                setError("Group", "Group Is Required Field");
            }
            else {
                setError("Group", "");
            }
        }
    }
    
    if (items.indexOf("Item") >= 0) {
        var item = $("#Item").val();
        if (typeof ($("#Item").val()) != 'undefined' && $("#Item").val() != null) {
            if (item == "") {
                setError("Item", "Item Required Field");
            }
            else {
                setError("Item", "");
            }
        }
    }

    if (items.indexOf("SalesManId") >= 0) {
        var salesManId = $("#SalesManId").val();
        if (typeof ($("#SalesManId").val()) != 'undefined' && $("#SalesManId").val() != null) {
            if (salesManId == "") {
                setError("SalesManId", "Sales Man Id is Required Field");
            }
            else {
                setError("SalesManId", "");
            }
        }
    }

    if (items.indexOf("SalesManName") >= 0) {
        var salesManName = $("#SalesManName").val();
        if (typeof ($("#SalesManName").val()) != 'undefined' && $("#SalesManName").val() != null) {
            if (salesManName == "") {
                setError("SalesManName", "Sales Man Name is Required Field");
            }
            else {
                setError("SalesManName", "");
            }
        }
    }

    if (items.indexOf("FromR1") >= 0) {
        var fromR1 = $("#FromR1").val();
        if (typeof ($("#FromR1").val()) != 'undefined' && $("#FromR1").val() != null) {
            if (fromR1 == "") {
                setError("FromR1", "From R1 is Required Field");
            }
            else {
                setError("FromR1", "");
            }
        }
    }

    if (items.indexOf("ToR1") >= 0) {
        var toR1 = $("#ToR1").val();
        if (typeof ($("#ToR1").val()) != 'undefined' && $("#ToR1").val() != null) {
            if (toR1 == "") {
                setError("ToR1", "To R1 is Required Field");
            }
            else {
                setError("ToR1", "");
            }
        }
    }

    if (items.indexOf("FromR2") >= 0) {
        var fromR2 = $("#FromR2").val();
        if (typeof ($("#FromR2").val()) != 'undefined' && $("#FromR2").val() != null) {
            if (fromR2 == "") {
                setError("FromR2", "From R2 is Required Field");
            }
            else {
                setError("FromR2", "");
            }
        }
    }

    if (items.indexOf("ToR2") >= 0) {
        var toR2 = $("#ToR2").val();
        if (typeof ($("#ToR2").val()) != 'undefined' && $("#ToR2").val() != null) {
            if (toR2 == "") {
                setError("ToR2", "To R2 is Required Field");
            }
            else {
                setError("ToR2", "");
            }
        }
    }
}
function setError(id, message) {
    var span = $("span[data-valmsg-for=\"" + id + "\"]");
    var isvalid = $("#message").val();
    if (span && span.length > 0) {
        $(span).html(message);
        if (message && message != "") {
            if (isvalid != "true")
            {
                $("#message").val("true");
            }
            $(span).removeClass("field-validation-valid");
            $(span).addClass("field-validation-no-valid");
        } else {
            
            $(span).removeClass("field-validation-no-valid");
            $(span).addClass("field-validation-valid");
        }
    }
}
