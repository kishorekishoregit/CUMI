jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
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
        $(".loader").show("slow");
        $.ajax({
            url: 'GetEmployeeMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var recordstatus = JSON.parse(pageload[0]);
                var department = JSON.parse(pageload[1]);
                var plant = JSON.parse(pageload[2]);
                var resJ = JSON.parse(pageload[3]);


                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                $("#ddlldepartment").empty().append('<option value="">Select an Option</option>');
                $.each(department, function () {
                    $("#ddlldepartment").append($("<option></option>").val(this["DEPARTMENTNAME"]).html(this["DEPARTMENTNAME"]));
                    $('#ddlldepartment').trigger("chosen:updated");
                });
                $("#ddlPlant").empty().append('<option value="">Select an Option</option>');
                $.each(plant, function () {
                    $("#ddlPlant").append($("<option></option>").val(this["PLANTNAME"]).html(this["PLANTNAME"]));
                    $('#ddlPlant').trigger("chosen:updated");
                });


                BindTab(resJ, '1');
            }
        });
        $(".loader").hide("slow");
    }

    //-------------------------------------------------------------------------------------------Bind Table
    function BindTab(ResData, type) {

        var currentDate = new Date()
        var day = currentDate.getDate()
        var month = currentDate.getMonth() + 1
        var year = currentDate.getFullYear()
        var currentdate = day + "/" + month + "/" + year;

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
        if (exampleRecord) {
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
                "bSort": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
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
                columns: [


                    {
                        className: "text-capitalize",'data': 'Employee Code',
                        'render': function (data, type, full, meta) {

                            var pakarr = data.split('_');

                            return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                        }
                    },
                    // { 'data': 'Employee Code' },
                    { className: "text-capitalize",'data': 'Employee Name' },
                    { className: "text-capitalize",'data': 'Department' },
                    { className: "text-capitalize",'data': 'Email Id' },
                    { className: "text-capitalize", 'data': 'Contact No' },
                    { className: "text-capitalize", 'data': 'Plant' },
                    { className: "text-capitalize",'data': 'Employee Status' },
                    {

                        className: "text-capitalize",'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'
                                + '<div>' + '<span style="visibility: hidden;">' + pakarr + '</span></div>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Employee Master',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Employee Master'


                    }, {
                        extend: 'print',
                        title: 'Employee Master',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ]
            });
        }
        else {
            $('#dynamic-table tbody').remove();
            $('#dynamic-table thead').remove();
            $('#dynamic-table').dataTable({
                "language": {
                    "emptyTable": "Employee Master No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": true
            });


        }
        $(".loader").hide("slow");
    }

    $('#Employee-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);

    })
        .on('form:submit', function () {
            save();
            return false;
        });


    function save() {


        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('EMPLOYEECODE', $('#txtemployeecode').val());
            formData.append('EMPLOYEENAME', $('#txtemployeename').val());
            formData.append('DEPARTMENT', $('#ddlldepartment').val());
            formData.append('EMAILID', $('#txtmailid').val());
            formData.append('CONTACTNO', $('#txtnumber').val());
            formData.append('PLANT', $('#ddlPlant').val());
            formData.append('EMPLOYEESTATUS', $('#ddlstatus').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertEmployeeMaster',
                dataType: "json",
                type: "POST",
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  

                data: formData,
                success: function (data) {
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {
                        $("form").trigger("reset");
                        $("#actiontype").text("Save")
                        $('.loadingGIF').hide();
                        bootbox.alert({

                            message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                            size: 'small',
                            callback: function () {
                                Clear();
                            }
                        });

                        $('#txtemployeecode').val("");
                        $("#txtemployeename").val("");
                        $('#ddlldepartment').val("")
                        $('#ddlstatus').val("MDSUB_001_0001");
                        $('.loadingGIF').hide();
                    }
                    else {
                        bootbox.alert({
                            /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                            size: 'small'
                        });
                        $('.loadingGIF').hide();
                    }
                }
            });
        }


    }

    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);


    $("#dynamic-table").on("click", ".editdetails", function () {
        var autoid = $(this).closest('tr').find('td:eq(0) > div > span').html();
        //var EMPLOYEECODE = $(this).closest('tr').find('td:eq(0)').html();
        GetEmployeeDetailsByID(autoid)
    });

    function GetEmployeeDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GeteMPLOYEEMasterID',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'ID': autoid }),

            success: function (data) {
                var resJ = JSON.parse(data);
                //  $.each(resJ, function (i, item) {

                $("#hdautoid").val(resJ[0].AUTOID);

                $("#txtemployeecode").val(resJ[0].EMPLOYEECODE);

                $("#txtemployeename").val(resJ[0].EMPLOYEENAME);
                $("#ddlldepartment").val(resJ[0].DEPARTMENT);
                $("#ddlldepartment").trigger('change');
                $("#txtmailid").val(resJ[0].EMAILID);
                $("#txtnumber").val(resJ[0].CONTACTNO);
                $("#ddlPlant").val(resJ[0].PLANT);
                $('#ddlPlant').trigger('change');
                $('#ddlstatus').val(resJ[0].EMPLOYEESTATUS)
                $('#ddlstatus').trigger('change');
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }


    $("#txtmailid").change(function () {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddress = $("#txtmailid").val();
        if (!emailReg.test(emailaddress)) {
            alert("Please Enter the Correct  EmailId");
            $('#txtmailid').val("");
            $('#txtmailid').focus();
        }
    });

    $("#txtnumber").change(function () {

        var mobileNum = $("#txtnumber").val();
        var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
        if (validateMobNum.test(mobileNum) && mobileNum.length == 10) {
            //alert("Valid Mobile Number");
        }
        else {
            alert("Invalid Mobile Number");
            $("#txtnumber").val("");
        }


    });
});