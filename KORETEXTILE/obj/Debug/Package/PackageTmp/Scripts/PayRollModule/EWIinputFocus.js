function EWIFieldEnterPress() {
    $("#WorkerName").focus();
    $(document).on("keydown", "#WorkerName", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#DesignationID").focus();
        }
    });
    $('#DesignationID').on('select2:select', function (e) {
        $("#WorkerTypeID").focus();
    });
    $('#WorkerTypeID').on('select2:select', function (e) {
        $("#CardNo").focus();
    });
    $("#CardNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#FatherName").focus();
        }
    });
    $("#FatherName").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MotherName").focus();
        }
    });
    $("#MotherName").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#GenderID").focus();
        }
    });
    $('#GenderID').on('select2:select', function (e) {
        $("#ReligionID").focus();
    });
    $('#ReligionID').on('select2:select', function (e) {
        $("#DateOfBirth").focus();
    });
    $("#DateOfBirth").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#NationalID").focus();
        }
    });
    $("#NationalID").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PresentAddress").focus();
        }
    });
    $("#PresentAddress").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#PermanentAddress").focus();
        }
    });
    $("#PermanentAddress").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#MobileNo").focus();
        }
    });
    $("#MobileNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#AltNo").focus();
        }
    });
    $("#AltNo").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#WorkStationID").focus();
        }
    });
    $('#WorkStationID').on('select2:select', function (e) {
        $("#SectionID").focus();
    });
    $('#SectionID').on('select2:select', function (e) {
        $("#JoiningDate").focus();
    });
    $("#JoiningDate").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").focus();
        }
    });
    $("#save").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $("#WorkerID").on("keydown", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").focus();
        }
    });
    $(document).on("keydown", "#search", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#search").click();
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 83) {
            e.preventDefault();
            $("#save").click();
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 82) {
            e.preventDefault();
            $("#refresh").click();
        }
    });

    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 85) {
            e.preventDefault();
            $("#update").click();
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 68) {
            e.preventDefault();
            $("#delete").click();
        }
    });

}