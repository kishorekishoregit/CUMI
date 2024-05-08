jQuery(function ($) {
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

        if (!$('#validation-form').valid()) e.preventDefault();
        else {
            $.ajax({
                url: 'RoleCreation',
                dataType: "json",
                type: "POST",
                data: $("#validation-form").serialize() + '&' + $.param({ 'actiontype': $("#actiontype").text() }),
                success: function (data) {
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {
                        $("form").trigger("reset");
                        $("#actiontype").text("Save")
                        bootbox.alert({
                            message: '<span class=""><i class="ace-icon fa fa-user"></i>' + msg + '</span>',
                            size: 'small',
                            callback: function () {
                                location.reload();
                            }
                        });
                    }
                    else {
                        bootbox.alert({
                            message: '<span class=""><i class="ace-icon fa fa-user"></i>' + msg + '</span>',
                            size: 'small'
                        });
                    }
                }
            });
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
            $('#sample-form').show();
        }
        else {
            $('#sample-form').hide();
            $('#validation-form').removeClass('hide');
        }
    })



    //documentation : http://docs.jquery.com/Plugins/Validation/validate


    $.mask.definitions['~'] = '[+-]';
    $('#phone').mask('(999) 999-9999');

    jQuery.validator.addMethod("phone", function (value, element) {
        return this.optional(element) || /^\(\d{3}\) \d{3}\-\d{4}( x\d{1,6})?$/.test(value);
    }, "Enter a valid phone number.");

    $('#validation-form').validate({
        errorElement: 'div',
        errorClass: 'help-block',
        focusInvalid: true,
        ignore: "",
        rules: {
            recordstatus: {
                required: true
            },
            roledescription: {
                required: true
            },
            userrole: {
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


    /**
    $('#date').datepicker({autoclose:true}).on('changeDate', function(ev) {
        $(this).closest('form').validate().element($(this));
    });

    $('#mychosen').chosen().on('change', function(ev) {
        $(this).closest('form').validate().element($(this));
    });
    */


    $(document).one('ajaxloadstart.page', function (e) {
        //in ajax mode, remove remaining elements before leaving page
        $('[class*=select2]').remove();
    });


    //Grid Bind Value
    BindTable();

    function BindTable() {
       
        var table = document.getElementById("dynamic-table");
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;

        var RoleDatatable = $('#dynamic-table');
        $.ajax({
            url: 'GetRoleDetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');
                var Recordstatus = JSON.parse(pageload[0]);
                var resJ = JSON.parse(pageload[1]);
                var exampleRecord = resJ[0];

                $("#recordstatus").empty().append('<option value="">Select</option>');
                $.each(Recordstatus, function () {
                    $("#recordstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#recordstatus').trigger("chosen:updated");
                });

                BindTab(resJ, '1');
            }
        });
    }

    function BindTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = ResData[0];
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
            finalcolsresult += 'null' + ',';

        });

        var dyTable = $('#dynamic-table');

        $('#dynamic-table').dataTable({
            data: ResData,
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
        var tableTools = new $.fn.dataTable.TableTools(dyTable, {
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
        // if (type == '1') {
        $(tableTools.fnContainer()).insertBefore('#dynamic-table_wrapper');
        //}

    }

    //on Click table Row
    //$(document).ready(function () {
    $("#dynamic-table").delegate("tr", "click", function (data) {
        var rolecode = $(this).find('td:eq(1)').html();
        GetRoleCreationDetailsByID(rolecode)
    });
    // });

    //get the Role Creation Details by ID
    function GetRoleCreationDetailsByID(Rolecode) {
        $.ajax({
            url: 'GetRoleDetailsByID',
            dataType: 'json',
            type: 'POST',
            data: { "Rolecode": Rolecode },
            success: function (data) {
                var resJ = JSON.parse(data);
                $.each(resJ, function (i, item) {
                    $("#userrole").val(resJ[i].USERROLE);
                    $("#roledescription").val(resJ[i].ROLEDESCRIPTION);
                    $("#recordstatus").val(resJ[i].METASUBCODE);
                    $("#hrecordtimestamp").val(resJ[i].RECORDACCESSEDTIME);

                    $("#userrole").prop('readonly', true);
                });

                $("#actiontype").text("Update");
                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        });
    }


    $('#btnsearch').click(function () {
        $.ajax({
            url: 'SearchRoldeDetails',
            dataType: 'json',
            type: 'POST',
            data: $("#validation-form").serialize(),
            success: function (data) {
                BindTab(JSON.parse(data), '0');
            }
        });
    });

    $('#btnclear').click(function () {
        //$('.help-block').closest('.form-group').removeClass('has-error');//.addClass('has-info');
        //$('.help-block').remove();
        //$("form").trigger("reset");
        //$("#actiontype").text("Save");     

        $("#userrole").prop('readonly', false);
        location.reload();
        //$('input[type=search]').val('');
    });

});