function SearchCustomer()
{
    $(document).on('click', "#searchCustomer", function () {
        var param = { depotId: $("#DepotId").val(), custId: $("#CustomerID").val() };
        if (param.depotId
            != "") {
            $.ajax({
                url: '/Sales/GetCustomerById/',
                contentType: "application/json;charset=utf-8",
                data: param,
                type: 'GET',
                dataType: 'json',
                success: function (datas) {
                    if (datas != 0) {
                        if (typeof ($("#CustName").val()) != 'undefined' && $("#CustName").val() != null) {
                            $("#CustName").val(datas.Name);
                        }
                        if (typeof ($("#ZoneId").val()) != 'undefined' && $("#ZoneId").val() != null) {
                            $("#ZoneId ").val(datas.Zone);
                        }
                        if (typeof ($("#Area").val()) != 'undefined' && $("#Area").val() != null) {
                            $("#Area").val(datas.Area);
                        }
                        if (typeof ($("#CustMobileNo").val()) != 'undefined' && $("#CustMobileNo").val() != null) {
                            $("#CustMobileNo").val(datas.MobileNo);
                        }
                        if (typeof ($("#CreditLimit").val()) != 'undefined' && $("#CreditLimit").val() != null) {
                            $("#CreditLimit").val(datas.Credit);
                        }
                        if (typeof ($("#Balance").val()) != 'undefined' && $("#Balance").val() != null) {
                            $("#Balance").val(datas.Due);
                        }
                        if (typeof ($("#Due").val()) != 'undefined' && $("#Due").val() != null) {
                            $("#Due").val(datas.Due);
                        }
                        if (typeof ($("#CreditDays").val()) != 'undefined' && $("#CreditDays").val() != null) {
                            $("#CreditDays").val(datas.CreditDays);
                        }
                        if (typeof ($("#SRId").val()) != 'undefined' && $("#SRId").val() != null) {
                            $("#SRId").val(datas.TSOId);
                        }
                        if (typeof ($("#SrName").val()) != 'undefined' && $("#SrName").val() != null) {
                            $("#SrName").val(datas.TSOName);
                        }
                        if (typeof ($("#SrDesignation").val()) != 'undefined' && $("#SrDesignation").val() != null) {
                            $("#SrDesignation").val(datas.Designation);
                        }
                    }
                    else {
                        if (typeof ($("#CustName").val()) != 'undefined' && $("#CustName").val() != null) {
                            $("#CustName").val("");
                        }
                        if (typeof ($("#ZoneId").val()) != 'undefined' && $("#ZoneId").val() != null) {
                            $("#ZoneId ").val("");
                        }
                        if (typeof ($("#Area").val()) != 'undefined' && $("#Area").val() != null) {
                            $("#Area").val("");
                        }
                        if (typeof ($("#CustMobileNo").val()) != 'undefined' && $("#CustMobileNo").val() != null) {
                            $("#CustMobileNo").val("");
                        }
                        if (typeof ($("#CreditLimit").val()) != 'undefined' && $("#CreditLimit").val() != null) {
                            $("#CreditLimit").val(0);
                        }
                        if (typeof ($("#Balance").val()) != 'undefined' && $("#Balance").val() != null) {
                            $("#Balance").val(0);
                        }
                        if (typeof ($("#Due").val()) != 'undefined' && $("#Due").val() != null) {
                            $("#Due").val(0);
                        }
                        if (typeof ($("#CreditDays").val()) != 'undefined' && $("#CreditDays").val() != null) {
                            $("#CreditDays").val(0);
                        }
                        if (typeof ($("#SRId").val()) != 'undefined' && $("#SRId").val() != null) {
                            $("#SRId").val(0);
                        }
                        if (typeof ($("#SrName").val()) != 'undefined' && $("#SrName").val() != null) {
                            $("#SrName").val(0);
                        }
                        if (typeof ($("#SrDesignation").val()) != 'undefined' && $("#SrDesignation").val() != null) {
                            $("#SrDesignation").val(datas.TSOName);
                        }
                        alert("May be customer doesn't exist or customer doesn't not exist on this Depot.");

                    }
                }
            });
        }
        else {
            alert("Depot is required field for search customer.");
        }

    });
}