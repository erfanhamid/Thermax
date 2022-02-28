function EnterPress() {

    $('#parentId').on('select2:select', function (e) {
        $("#type").focus();
        return false;
    });
    $('#type').on('select2:select', function (e) {
        if ($(this).val() == "G") {
            $("#Name").focus();
        }
        else {
            $("#ItemCode").focus();
        }
        
    });
    $(document).on("keydown", "#ItemCode", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#Name").focus();
        }
    });

    $(document).on("keydown", "#Name", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
           
                $("#ShortDesc").focus();
          
           
        }
    });
    $(document).on("keydown", "#ShortDesc", function (e) {
        if ($("#type").val() == "G") {
            
            if (e.keyCode == 13 || e.key == "Tab") {
                $("#save").focus();
            }
        }
        else {
            if (e.keyCode == 13 || e.key == "Tab") {
                $("#UoMID").focus();
            }
        }
        
    });
    $('#UoMID').on('select2:select', function (e) {
        $("#PacSize").focus();
    });

    $(document).on("keydown", "#PacSize", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#clmCartonCapacity").focus();
        }
    });
    $(document).on("keydown", "#clmCartonCapacity", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#clmStandardCost").focus();
        }
    });
    $(document).on("keydown", "#clmStandardCost", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#RetailPrice").focus();
        }
    });
    $(document).on("keydown", "#RetailPrice", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#DisPerc").focus();
        }
    });
    $(document).on("keydown", "#DisPerc", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#VatPerc").focus();
        }
    });
    $(document).on("keydown", "#VatPerc", function (e) {
        if (e.keyCode == 13 || e.key == "Tab") {
            $("#CountryId").focus();
        }
    });

    $('#CountryId').on('select2:select', function (e) {
      
        $("#save").focus();
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
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 65) {
            e.preventDefault();
            $("#approve").click();
            return false;
        }
    });
    $(document).on("keydown", function (e) {
        if (e.altKey && e.keyCode == 80) {
            e.preventDefault();
            $("#print").click();
            return false;
        }
    });


}