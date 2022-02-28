function EnterPress() {

    $(document).on("keydown", "#Id", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#empSearch").focus();
        }
    });
    $(document).on("keydown", "#empSearch", function (e) {
        if (e.keyCode == 13 || e.keyCode == 9) {
            e.preventDefault();
            $("#empSearch").click();
            $("#Name").focus();
        }
    }); 

    $(document).on("keydown", "#Name", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#OpBalance").focus();
        }
    });

    $(document).on("keydown", "#OpBalance", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#OBDate").focus();
        }
    });

    $(document).on("keydown", "#OBDate", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#EmpGrade").focus();
        }
    });

    $(document).on("keydown", "#EmpGrade", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#FatherName").focus();
        }
    });

    $(document).on("keydown", "#FatherName", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#MotherName").focus();
        }
    });

    $(document).on("keydown", "#MotherName", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#NationalID").focus();
        }
    });

    $(document).on("keydown", "#NationalID", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#BloodGroup").focus();
        }
    });

    $(document).on("keydown", "#BloodGroup", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#NomineeName").focus();
        }
    });

    $(document).on("keydown", "#NomineeName", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#NomineeRelation").focus();
        }
    });

    $(document).on("keydown", "#NomineeRelation", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#MarstausID").focus();
        }
    });

    $('#MarstausID').on('select2:select', function (e) {
        $("#ReligionID").focus();
    });
    
    $('#ReligionID').on('select2:select', function (e) {
        $("#Sex").focus();
    });

    $('#Sex').on('select2:select', function (e) {
        $("#DateOfBirth").focus();
    });

    $(document).on("keydown", "#DateOfBirth", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#PresentAddress").focus();
        }
    });

    $(document).on("keydown", "#PresentAddress", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#PermanentAddress").focus();
        }
    });

    $(document).on("keydown", "#PermanentAddress", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#Mobile").focus();
        }
    });

    $(document).on("keydown", "#Mobile", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#Email").focus();
        }
    });

    $(document).on("keydown", "#Email", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#PersonalEmail").focus();
        }
    });

    $(document).on("keydown", "#PersonalEmail", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#AlternativeContct").focus();
        }
    });

    $(document).on("keydown", "#AlternativeContct", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#DepatmentID").focus();
        }
    });

    $('#DepatmentID').on('select2:select', function (e) {
        $("#Section").focus();
    });

    $('#Section').on('select2:select', function (e) {
        $("#Designation").focus();
    });

    $('#Designation').on('select2:select', function (e) {
        $("#FunctDesignation").focus();
    });

    $('#FunctDesignation').on('select2:select', function (e) {
        $("#StaffType").focus();
    });

    $('#StaffType').on('select2:select', function (e) {
        $("#JoiningDate").focus();
    });

    $(document).on("keydown", "#JoiningDate", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#HighestEducation").focus();
        }
    });

    $('#HighestEducation').on('select2:select', function (e) {
        $("#save").focus();
    });

    $(document).on("keydown", "#save", function (e) {
        if (e.keyCode === 13 || e.keyCode === 9) {
            e.preventDefault();
            $("#save").click();
        }
    });
    
}