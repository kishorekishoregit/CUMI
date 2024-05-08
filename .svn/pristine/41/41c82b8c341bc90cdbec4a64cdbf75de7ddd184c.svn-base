/// <reference path="sweetalert.min.js" />



jQuery(function ($) {



    //var VendorDataTable = $('#dynamic-table');
    //var table = document.getElementById("dynamic-table");

    //$('[data-rel=tooltip]').tooltip();
    //$('.select2').css('width', '200px').select2({ allowClear: true })
    //.on('change', function () {
    //    $(this).closest('form').validate().element($(this));
    //});

    //  var $validation = true;
    //  $('#login-box')
    //  //.ace_wizard({
    //  //    //step: 2 //optional argument. wizard will jump to step "2" at first
    //  //    //buttons: '.wizard-actions:eq(0)'
    //  //})
    //  .on('actionclicked.fu.wizard', function (e, info) {

    //      if (!$('frm_login').valid()) e.preventDefault();
    //      else {

    //      }
    //  })
    //  //.on('changed.fu.wizard', function() {
    //  //})
    //.on('stepclick.fu.wizard', function (e) {
    //    //e.preventDefault();//this will prevent clicking and selecting steps
    //});


    //jump to a step
    /**
    var wizard = $('#fuelux-wizard-container').data('fu.wizard')
    wizard.currentStep = 3;
    wizard.setState();
    */

    //determine selected step
    //wizard.selectedItem().step



    //hide or show the other form which requires validation
    //this is for demo only, you usullay want just one form in your application


    //// F12 PRESS AVOID BY USER
    //$(document).keydown(function (e) {
    //    if (e.which === 123) {
    //        return false;
    //    }
    //});
    //$(document).keydown(function (e) {
    //    if (e.ctrlKey || e.shiftKey || e.which === 73)
    //        return false;
    //});
    //// DISABLE MOUSE RIGHT CLICK
    //$(document).ready(function () {
    //    ////Disable cut copy paste
    //    //$('body').bind('cut copy paste', function (e) {
    //    //    e.preventDefault();
    //    //});


    //    ////Disable mouse right click
    //    //$("body").on("contextmenu", function (e) {
    //    //    alert("hai");
    //    //    return false;
    //    //});

    //});
   


    $('#skip-validation').removeAttr('checked').on('click', function () {
        $validation = this.checked;
        if (this.checked) {
            $('#validation-form').addClass('hide');
            $('#myform').show();
        }
        else {
            $('#sample-form').hide();
            $('#myform').removeClass('hide');
        }
    })


    $('#btnlogin').click(function (e) {
        if (!$('#frm_login').valid()) e.preventDefault();
            //document.frm_login.submit();

        else if (this) {
            loginPageLoad();
            document.frm_login.submit();
        }
        else {
            alert('Please enter the correct password.')
        }
    });


    //documentation : http://docs.jquery.com/Plugins/Validation/validate


    $.mask.definitions['~'] = '[+-]';
    $('#phone').mask('(999) 999-9999');

    jQuery.validator.addMethod("phone", function (value, element) {
        return this.optional(element) || /^\(\d{3}\) \d{3}\-\d{4}( x\d{1,6})?$/.test(value);
    }, "Enter a valid phone number.");

    $('#frm_login').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: true,
        ignore: "",
        rules: {
            Username: {
                required: true,
                errorClass: "invalid"
            },
            password: {
                required: true,
                errorClass: "invalid"
            }
        },

        messages: {
            Username: {
                required: "Please provide a Username.",
                Username: "Please provide a valid Username."
            },
            password: {
                required: "Please specify a password.",
                minlength: "Please specify a secure password."
            },
            state: "Please choose state",
            subscription: "Please choose at least one option",
            gender: "Please choose gender",
            agree: "Please accept our policy"
        },


        highlight: function (e) {
            $(e).closest('.form-group').removeClass('has-info').addClass('has-error');
        },

        success: function (e) {
            $(e).closest('.form-group').removeClass('has-error');//.addClass('has-info');
            $(e).remove();
        },

        errorPlacement: function (error, element) {
            if (element.is('input[type=checkbox]') || element.is('input[type=radio]')) {
                var controls = element.closest('div[class*="col-"]');
                if (controls.find(':checkbox,:radio').length > 1) controls.append(error);
                else error.insertAfter(element.nextAll('.lbl:eq(0)').eq(0));
            }
            else if (element.is('.select2')) {
                error.insertAfter(element.siblings('[class*="select2-container"]:eq(0)'));
            }
            else if (element.is('.chosen-select')) {
                error.insertAfter(element.siblings('[class*="chosen-container"]:eq(0)'));
            }
            else error.insertAfter(element.parent());
        },

        submitHandler: function (form) {
        },
        invalidHandler: function (form) {
        }


    });
    function loginPageLoad() {
        $.ajax({
            url: 'Index',
            dataType: "json",
            type: "POST",
            data: $("#frm_login").serialize(),
            success: function (data) {
               
                var resJ = JSON.parse(data);
                if (resJ = "true") {
                    //document.frm_login.submit();
                }
                else {
                    alert("Please enter the correct password.");
                }  
            }
        });
    }

    //$('#modal-wizard-container').ace_wizard();
    //$('#modal-wizard .wizard-actions .btn[data-dismiss=modal]').removeAttr('disabled');

    $(document).one('ajaxloadstart.page', function (e) {
        //in ajax mode, remove remaining elements before leaving page
        $('[class*=select2]').remove();
    });

   
    url = window.location.href;
    var array = url.split('?');
    if (array[1] == 'IncorrectPassword') {
        //sweetAlert('Please enter the correct password');    

        swal({
            title: "Incorrect ?",
            text: "Please Enter the Valid User Id, Password",
            type: "warning",
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'OK!',
            closeOnConfirm: false,            
        }, function () {
            window.location = "Login";
        });
    }

    $('#btnlogin').click(function () {
        var loginusername = $('#username').val();
        $('#loginuser').text(loginusername);
    });


});


