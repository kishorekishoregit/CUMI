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
            url: 'PlantMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');

                var recordstatus = JSON.parse(pageload[0]);
                var resJ = JSON.parse(pageload[1]);

                $("#ddlRecordStatus").empty();
                $.each(recordstatus, function () {
                    $("#ddlRecordStatus").append($("<option></option>").val(this["METADATADESCRIPTION"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlRecordStatus').trigger("chosen:updated");
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
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "info": false,
                "bFilter": true,
                "ascrollX": "100%",
                "language": { "search": "Search:" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],

                'columnDefs': [
                    { "width": "1%", "targets": 5 }, {
                        'targets': 0,
                        'searchable': true,
                        'orderable': true,
                        'className': 'dt-body-center'
                    }
                ],

                columnDefs: cols1,
                //columns: cols1DATA,

                columns: [
                    { className: "text-capitalize",'data': 'Plant Code'},
                    { className: "text-capitalize",'data': 'Plant Name' },
                    { className: "text-capitalize",'data': 'Record Status' },
                    { className: "text-capitalize",'data': 'Remarks' },
                    {

                        className: "text-capitalize",'data': 'Edit',
                        'render': function (data, type, full, meta) {
                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class=" typcn typcn-edit" aria-hidden="true"></i></button>' + '<span style = "visibility: hidden;" >' + data + '</span >'
                                
                        }
                    },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Plant Master',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Plant Master'


                    }, {
                        extend: 'print',
                        title: 'Plant Master',
                        text: '<img src="../../Images/print.png" title="Print" style="height: 25px;">',
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
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
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
    $('#Plant-form').parsley().on('field:validated', function () {
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
            formData.append('PlantCode', $('#txtPlantCode').val());
            formData.append('PlantName', $('#txtPlantName').val());
            //formData.append('EmailID', $('#txtEmailID').val());
            //formData.append('MobileNo', $('#txtMobileNo').val());
            //formData.append('ShipToAddress', $('#txtShipToAddress').val());
            formData.append('Status', $('#ddlRecordStatus').val());
            formData.append('Remarks', $('#txtRemarks').val());
            formData.append('autoId', $('#hdautoid').val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertPlantMaster',
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

                        $('#txtPlantCode').val("");
                        $("#txtPlantName").val("");
                        //$('#txtEmailID').val("")
                        //$("#txtMobileNo").val("");
                        //$("#txtShipToAddress").val("");
                        $("#ddlRecordStatus").val("");
                        //$("#ddllocation").val("");
                        //$('#ddlstatus').val("MDSUB_001_0001");
                        $('.loadingGIF').hide();
                    }
                    else {
                        bootbox.alert({
                            /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i></i><br>' + msg + '</span>',
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
    }


    //-------------------------------------------------------------------------------------------Table Selected Index Change
    //on Click table Row
    //$(document).ready(function () {
    $("#dynamic-table").on("click", ".editdetails", function () {

        //var usercode = $(this).closest('tr').find('td:eq(4) > div > span').html();
        var usercode = $(this).closest('tr').find('td:eq(4) > span').html();
        GetUserCreationDetailsByID(usercode)
    });
    // });
    //-------------------------------------------------------------------------------------------Assign values table into TextBox (Controls)   
    //get the Role Creation Details by ID
    function GetUserCreationDetailsByID(usercode) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GetPlantMasterByID',
            dataType: 'json',
            type: 'POST',
            data: { "usercode": usercode },

            success: function (data) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                //  $('#ddlemployeeid').val(resJ[0].EMPLOYEECODE).trigger('change');
                $("#txtPlantCode").val(resJ[0].PLANTCODE);
                $("#txtPlantName").val(resJ[0].PLANTNAME);
                //$("#txtEmailID").val(resJ[0].EMAILID);
                //$("#txtMobileNo").val(resJ[0].MOBILENO);
                //$("#txtShipToAddress").val(resJ[0].SHIPTOADDRESS);
                $('#ddlRecordStatus').val(resJ[0].RECORDSTATUS).trigger('change');
                $("#txtRemarks").val(resJ[0].REMARKS);
                $("#actiontype").text("Update");
                //$("#addtab").addClass("active");
                //$("#detailstab").removeClass("active");

                //$("#adduser").addClass("show active");
                //$("#adduserDetails").removeClass("show active");


            }

        });

        $(".loader").hide("slow");
    }

    //-------------------------------------------------------------------------------------------Clear
    $('#btnclear').click(function () {
        location.reload();
    });

    $('#btntemplateimport').click(function () {
        window.location.href = "Downloads/?file=PLANTMASTER.xlsx";
    });

    $("#btnimport").click(function () {
        $('.loadingGIF').show();
        var uploadFile = new FormData();
        var files = $("#hf").get(0).files;
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);

            $.ajax({
                url: 'PlantMastersUploads',
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function (result) {
                    $('.loadingGIF').hide();
                    $("#hf").val(null);
                    if (result == 'true') {
                        bootbox.alert({
                            message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Uploaded Successfully." + '</span>',
                            size: 'small',
                            callback: function () {
                                Clear();
                            }
                        });
                    } else if (result == 'false') {
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Plant Code Cannot be left empty." + '</span>',
                            size: 'small',
                        });
                    }
                    else {
                        $("#hf").val(null);
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Already uploaded" + '</span>',
                            size: 'small',
                        });
                    }
                },
                error: function () {
                    $('.loadingGIF').hide();
                    $("#hf").val(null);
                    bootbox.alert({
                        message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "An error occurred while uploading." + '</span>',
                        size: 'small',
                    });
                }
            });
        } else {
            $('.loadingGIF').hide();
            $("#hf").val(null);
            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Choose Your file." + '</span>',
                size: 'small',
            });
        }
    });

    $("#txtEmailID").change(function () {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddress = $("#txtEmailID").val();
        if (!emailReg.test(emailaddress)) {
            alert("Please Enter the Correct  EmailId");
            $('#txtEmailID').val("");
            $('#txtEmailID').focus();
        }
    });

    $("#txtMobileNo").change(function () {

        var mobileNum = $("#txtMobileNo").val();
        var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
        if (validateMobNum.test(mobileNum) && mobileNum.length == 10) {
            //alert("Valid Mobile Number");
        }
        else {
            alert("Invalid Mobile Number");
        }


    });

});