function SalaryKeypress() {
    $(document).on("keydown", "#Basic", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            SetSalaryKeyUp();
            $("#HouseRent").focus();
            if ($("#HouseRent").val() <= 0) {
                $("#HouseRent").val("");
            }
            $('#HouseRent').setCursorToTextEnd();
        }
    });
    $(document).on("keydown", "#HouseRent", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            SetSalaryKeyUp();
            $("#MedicalAllowance").focus();
            if ($("#MedicalAllowance").val() <= 0) {
                $("#MedicalAllowance").val("");
            }
            $('#MedicalAllowance').setCursorToTextEnd();
        }
    });
    $(document).on("keydown", "#MedicalAllowance", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {

            e.preventDefault();
            SetSalaryKeyUp();
            $("#ConveyanceAllowance").focus();
            if ($("#ConveyanceAllowance").val() <= 0) {
                $("#ConveyanceAllowance").val("");
            }
            $('#ConveyanceAllowance').setCursorToTextEnd();
        }
    });
    $(document).on("keydown", "#ConveyanceAllowance", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#DearnessAllowance").focus();
            if ($("#DearnessAllowance").val() <= 0) {
                $("#DearnessAllowance").val("");
            }
            $('#DearnessAllowance').setCursorToTextEnd();
        }
    });
    $(document).on("keydown", "#DearnessAllowance", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            if ($("#Check").val() == 1) {
                e.preventDefault();
                SetSalaryKeyUp();
                $("#update").focus();
            } else {
                e.preventDefault();
                SetSalaryKeyUp();
                $("#save").focus();
            }
           
        }
    });
    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            SetSalaryKeyUp();
            $("#save").click();
        }
    });
    $(document).on("keydown", "#update", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            SetSalaryKeyUp();
            $("#update").click();
        }
    });
}

function SetSalaryKeyUp() {
    a = parseFloat($("#Basic").val());
    b = parseFloat($("#HouseRent").val());
    c = parseFloat($("#MedicalAllowance").val());
    d = parseFloat($("#ConveyanceAllowance").val());
    e = parseFloat($("#DearnessAllowance").val());
    calculate(a, b, c, d, e);
}
//function SetCaretAtEnd(elem) {
//    var elemLen = elem.length;
//    // For IE Only
//    if (document.selection) {
//        // Set focus
//        elem.focus();
//        // Use IE Ranges
//        var oSel = document.selection.createRange();
//        // Reset position to 0 & then set at end
//        oSel.moveStart('character', -elemLen);
//        oSel.moveStart('character', elemLen);
//        oSel.moveEnd('character', 0);
//        oSel.select();
//    }
//    else if (elem.selectionStart || elem.selectionStart == '0') {
//        // Firefox/Chrome
//        elem.selectionStart = elemLen;
//        elem.selectionEnd = elemLen;
//        elem.focus();
//    } // if
//} //
(function ($) {
    $.fn.setCursorToTextEnd = function () {
        $initialVal = this.val();
        this.val($initialVal + ' ');
        this.val($initialVal);
    };
})(jQuery);
