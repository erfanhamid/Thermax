

function DepotChange(storeId) {
    var id = { id: $("#DepotId").val() };
    if (id.id != "") {
        $.ajax({
            url: '/Sales/GetStoreByDepotId',
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(id),
            type: 'POST',
            dataType: 'json',
            success: function (datas) {

                $("#StoreId").empty();
                $("#StoreId").prop("disabled", false);
                $.each(datas, function (index, value) {
                    $("#StoreId").append($('<option />', {
                        value: value.Value,
                        text: value.Text
                    })).trigger('change');
                });
                if (storeId != "") {
                    $("#StoreId").val(storeId).change();
                }
            }
        });
    }
    else {
        $("#StoreId").empty();
        $("#StoreId").prop("disabled", true);
    }
}