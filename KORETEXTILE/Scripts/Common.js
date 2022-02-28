function DisableStoreId(isDisabled) {
    if (isDisabled == true) {
        $("#StoreId").prop("disabled", true);
    }
    else {
        $("#StoreId").prop("disabled", false);
    }
}

function setError(id, message) {
    var span = $("span[data-valmsg-for=\"" + id + "\"]");
    if (span && span.length > 0) {
        $(span).html(message);
        if (message && message != "") {
            $(span).removeClass("field-validation-valid");
            $(span).addClass("field-validation-no-valid");
        } else {
            $(span).removeClass("field-validation-no-valid");
            $(span).addClass("field-validation-valid");
        }
    }
}

function RemoveError() {
    $(".field-validation-error").text("");
    $(".field-validation-no-valid").text("");
   
}

$(document).on('click', "#clear", function () {
    location.reload();
});

//$.fn.function SelectFirstValue (selectId){
//    $("#" + selectId).val("# " + selectId +" option:first-child").trigger('change')

//}