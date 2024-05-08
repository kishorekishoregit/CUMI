jQuery(function ($) {

    $("#btnconform").click(function () {
        $.ajax({
            url: 'ProfilePasswordupload',
            dataType: "json",
            type: "POST",
            data: $("#validation-form").serialize() + '&' + $.param({ 'actiontype': $("#newpassword").val() }),
            success: function (data) {
                $("form").trigger("reset");
                $("#actiontype").text("Save")
                bootbox.alert({
                    message: '<span class=""><i class="ace-icon fa fa-user"></i>' + data + 'Updated Successfully</span>',
                    size: 'small',
                    callback: function () {
                        location.reload();
                    }

                });


            }
        });
    });


    BindPageLoad();


    function BindPageLoad() {
        $(".loader").show("slow");
        $.ajax({
            url: 'ProfilePageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (Result) {
                var resobj = Result.split('|');
                var resJ = JSON.parse(resobj[0]);
                var itemresJ = JSON.parse(resobj[1]);
                
                $.each(resJ, function (i, item) {

                    $("#vendorcode").text(resJ[i].VENDORCODE);
                    $("#vendorname").text(resJ[i].VENDORNAME);
                    $("#pn").text(resJ[i].PHONENUMBER);
                    $("#address").text(resJ[i].CITY);
                    $("#email").text(resJ[i].EMAIL);
                    $("#postalcode").text(resJ[i].POSTALCODE);
                });
            }
        });
        $(".loader").hide("slow");
    }

    $("#changepassword").change(function () {
       
        var newpassword = $("#newpassword").val();
        var confirm = $("#changepassword").val();
        if(newpassword!=confirm)
        {
            sweetAlert("Error...", "Please enter valid confirmpassword.!", "error");
        }
    });
    


   


})