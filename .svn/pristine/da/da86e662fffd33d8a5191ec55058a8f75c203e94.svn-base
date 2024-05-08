jQuery(function ($) {
    var EmpDatatable = $('#dynamic-table');
    var table = document.getElementById("dynamic-table");

    $('[data-rel=tooltip]').tooltip();
    $('.select2').css('width', '200px').select2({ allowClear: true })
    .on('change', function () {
        $(this).closest('form').validate().element($(this));
    });

    var $validation = true;
    $('#fuelux-wizard-container')
    .ace_wizard({
        //step: 2 //optional argument. wizard will jump to step "2" at first
        //buttons: '.wizard-actions:eq(0)'
    })
    .on('actionclicked.fu.wizard', function (e, info) {

        if (!$('#myform').valid()) e.preventDefault();
        else {
        }
    })
    //.on('changed.fu.wizard', function() {
    //})
  .on('stepclick.fu.wizard', function (e) {
      //e.preventDefault();//this will prevent clicking and selecting steps
  });


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



    //documentation : http://docs.jquery.com/Plugins/Validation/validate


    $.mask.definitions['~'] = '[+-]';
    $('#phone').mask('(999) 999-9999');

    jQuery.validator.addMethod("phone", function (value, element) {
        return this.optional(element) || /^\(\d{3}\) \d{3}\-\d{4}( x\d{1,6})?$/.test(value);
    }, "Enter a valid phone number.");

    $('#myform').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: true,
        ignore: "",
        rules: {
            vendorcode: {
                required: true
            },
            partno: {
                required: true
            },
            partname: {
                required: true
            },
            vendorname: {
                required: true
            }
        },

        messages: {
            email: {
                required: "Please provide a valid email.",
                email: "Please provide a valid email."
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
    $('#modal-wizard-container').ace_wizard();
    $('#modal-wizard .wizard-actions .btn[data-dismiss=modal]').removeAttr('disabled');

    $(document).one('ajaxloadstart.page', function (e) {
        //in ajax mode, remove remaining elements before leaving page
        $('[class*=select2]').remove();
    });

    BindPageLoad();


    function BindPageLoad() {

        $.ajax({
            url: 'TaxMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (Result) {
                var pageloag = Result.split('|');
                var taxcode = JSON.parse(pageloag[0]);
                var taxname = JSON.parse(pageloag[1]);
                var taxagency= JSON.parse(pageloag[2]);
                var resJ = JSON.parse(pageloag[3]);
                // DATABIND
                $("#taxcode").empty().append('<option value="">All</option>');
                $.each(taxcode, function () {
                    $("#taxcode").append($("<option></option>").val(this["TAXCODE"]).html(this["TAXCODE"]));
                    $('#taxcode').trigger("chosen:updated");
                });

                $("#taxname").empty().append('<option value="">All</option>');
                $.each(taxname, function () {
                    $("#taxname").append($("<option></option>").val(this["TAXNAME"]).html(this["TAXNAME"]));
                    $('#taxname').trigger("chosen:updated");
                });

                $("#taxagency").empty().append('<option value="">All</option>');
                $.each(taxagency, function () {
                    $("#taxagency").append($("<option></option>").val(this["TAXAGENCY"]).html(this["TAXAGENCY"]));
                    $('#taxagency').trigger("chosen:updated");
                });
                
                BindTab(resJ, "1");     // "1" for Find out page load ,  "0" for Search By Parameters
            }
        });
    }

    $('#btnSearch').click(function () {
        $.ajax({
            url: 'TaxMasterSearch',
            dataType: "json",
            type: "POST",
            data: $("#myform").serialize(),
            success: function (data) {
                var exampleRecord = JSON.parse(data);
                BindTab(exampleRecord, "0");
            }
        });
    });

    function BindTab(Emp, type) {
       
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = Emp[0];
        // TABLE BIND     
        if (type == '0') {
            $('#dynamic-table').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        //get keys in object. This will only work if your statement remains true that all objects have identical keys
        var keys = Object.keys(exampleRecord);

        //for each key, add a column definition
        keys.forEach(function (k) {
            cols.push({
                title: k,
                data: k,
                targets: k

                //optionally do some type detection here for render function
            });
        });

        $.each(cols, function (index, item) {

            item.title, item.targets

            cols1.push({
                title: item.title,
                targets: index

            });

            cols1DATA.push({
                data: item.title,
            });
           

        });


        $('#dynamic-table').dataTable({
            data: Emp,
            "bAutoWidth": false,
            "scrollX": true,
            "aColumns": [
      finalcolsresult,
      { "bSortable": false }
            ],
            "aSorting": [],
            'columnDefs': [{
                'targets': 0,
                'searchable': true,
                'orderable': true,
                'className': 'dt-body-center'
            }],

            columnDefs: cols1,
            columns: cols1DATA

        });
        var tableTools = new $.fn.dataTable.TableTools(EmpDatatable, {
            'aButtons': [
                {
                    'sExtends': 'copy',
                    'sButtonText': "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>"
                },
                {
                    'sExtends': 'csv',
                    'sButtonText': "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                    'bFooter': false
                },
                {
                    'sExtends': 'xls',
                    'sButtonText': "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                    'sFileName': 'Data.xls'

                },
                 {
                     'sExtends': 'pdf',
                     'sButtonText': "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                     'bFooter': false

                 },
                {
                    'sExtends': 'print',
                    'sButtonText': "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                    'bShowAll': true
                }

            ],
            'sSwfPath': 'http://180.179.42.237/Scripts/NewFolder1/copy_csv_xls_pdf.swf'
        });
        if (type == '1') {
            $(tableTools.fnContainer()).insertBefore('#dynamic-table_wrapper');
        }

    }

    $('#btnClear').click(function () {
        location.reload();
    });
});