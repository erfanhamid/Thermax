function CurrencyFieldEnterPress() {
    $("#CurrencyName").focus();
    $('#CurrencyName').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#ExchangeRate").focus();
        }
    });
    $('#ExchangeRate').on('keydown', function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    $("#save").on('select2:select', function (e) {
        $("#save").focus();
    });
}