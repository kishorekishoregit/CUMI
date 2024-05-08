jQuery(function ($) {

    $("#txtcity").keypress(function (e) {
        var keyCode = e.which;

        // Check if the pressed key is a valid character (alphanumeric)
        if (!(keyCode >= 65 && keyCode <= 90) && // A-Z
            !(keyCode === 32) &&  // space
            !(keyCode >= 97 && keyCode <= 122)) { // a-z
            e.preventDefault();
        }
    });
    $("#txtstate").keypress(function (e) {
        var keyCode = e.which;

        // Check if the pressed key is a valid character (alphanumeric)
        if (!(keyCode >= 65 && keyCode <= 90) && // A-Z
            !(keyCode >= 97 && keyCode <= 122)) { // a-z
            e.preventDefault();
        }
    });
    
    
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();
    var plant = "";

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
            url: 'WarehouseMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Plant = JSON.parse(pageload[0]);
               // var Zone = JSON.parse(pageload[1]);
                var Status = JSON.parse(pageload[1]);
                var DETAILS = JSON.parse(pageload[2]);

                $("#ddlplant").empty().append('<option value="">Select an Option</option>');
                $.each(Plant, function () {
                    $("#ddlplantlst").append($("<option></option>").val(this["PLANTCODE"]).html(this["PLANTNAME"]));
                    $('#ddlplantlst').trigger("chosen:updated");
                });


                //$("#ddlplant").empty().append('<option value="">Select an Option</option>');
                //$.each(Plant, function () {
                //    $("#ddlplant").append($("<option></option>").val(this["PLANTNAME"]).html(this["PLANTNAME"]));
                //    $('#ddlplant').trigger("chosen:updated");
                //});            

                $.each(Status, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METADATADESCRIPTION"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                BindTab(DETAILS, '1');
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
        var rcount = $("#dynamic-table> tbody > tr").length
        if (rcount > 0) {
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
                "language": { "search": "Search:" },
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


                    { className: "text-capitalize", 'data': 'Warehouse Code' },
                    { className: "text-capitalize", 'data': 'Warehouse Name' },
                    { className: "text-capitalize", 'data': 'Plant' },
                    { className: "text-capitalize", 'data': 'Supervisor' },
                    { className: "text-capitalize", 'data': 'Address1' },
                    { className: "text-capitalize", 'data': 'Address2' },
                    { className: "text-capitalize", 'data': 'Address3' },
                    { className: "text-capitalize", 'data': 'City' },
                    { className: "text-capitalize", 'data': 'State' },
                    { className: "text-capitalize", 'data': 'Pincode' },
                    { className: "text-capitalize", 'data': 'Remarks' },
                    { className: "text-capitalize", 'data': 'Record Status' },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class=" typcn typcn-edit" aria-hidden="true"></i></button><span style = "visibility: hidden;" >' + data + '</span >'
                                

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Warehouse Master',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png"title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Warehouse Master'


                    }, {
                        extend: 'print',
                        title: 'Warehouse Master',
                        text: '<img src="../../Images/print.png"title="Print" style="height: 25px;">',
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

    //$("#ddlplant").change(function () {
    //    $.ajax({
    //        url: 'GetWarehouseMasterpartcodebyID',
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: $.param({ 'partcode': $('#ddlplant').val().trim() }),
    //        success: function (data) {
    //            var Res = data.split('|');
    //            var plant = JSON.parse(Res[0]);

    //            BindTab(plant, '1');
    //        }

    //    });

    //});


    $('#WarehouseMaster-form').parsley().on('field:validated', function () {
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
            formData.append('WAREHOUSECODE', $('#txtwhcode').val());
            formData.append('WAREHOUSENAME', $('#txtwhname').val());
            formData.append('PLANTCODE', $('#ddlplant').val());
            formData.append('SUPERVISOR', $('#txtsupervisor').val());
            formData.append('ADDRESS1', $('#txtaddress1').val());
            formData.append('ADDRESS2', $('#txtaddress2').val());
            formData.append('ADDRESS3', $('#txtaddress3').val());
            formData.append('CITY', $('#txtcity').val());
            formData.append('STATE', $('#txtstate').val());
            formData.append('PINCODE', $('#txtpincode').val());
            formData.append('REMARKS', $('#txtremarks').val());
            formData.append('RECORDSTATUS', $('#ddlstatus').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertWarehouseMaster',
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

                        $('#ddlplant').val("");
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
        var autoid = $(this).closest('tr').find('td:eq(12) > span').html();
        GetWarehouseDetailsByID(autoid)
    });

    function GetWarehouseDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditWarehouseMaster',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'usercode': autoid }),

            success: function (data) {

                //  $.each(resJ, function (i, item) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $('#txtwhcode').val(resJ[0].WHCODE);
                $('#txtwhname').val(resJ[0].WHNAME);
                $("#ddlplant").val(resJ[0].PLANT).trigger('change');
                $('#txtsupervisor').val(resJ[0].SUPERVISOR);
                $('#txtaddress1').val(resJ[0].ADDRESS1);
                $('#txtaddress2').val(resJ[0].ADDRESS2);
                $('#txtaddress3').val(resJ[0].ADDRESS3);
                $('#txtcity').val(resJ[0].CITY);
                $('#txtstate').val(resJ[0].STATE);
                $('#txtpincode').val(resJ[0].PINCODE);
                $('#txtremarks').val(resJ[0].REMARKS);
                $('#ddlstatus').val(resJ[0].RECORDSTATUS).trigger('change');
               // $('#txttransittime').val(resJ[0].TRANSITTIME);
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }
    $('#btntemplateimport').click(function () {
        window.location.href = "Downloads/?file=WAREHOUSEMASTER.xlsx";
    });

    $("#btnimport").click(function () {
        $('.loadingGIF').show();
        var uploadFile = new FormData();
        var files = $("#hf").get(0).files;
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);

            $.ajax({
                url: 'WarehouseMastersUploads',
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
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Excel Uploading Failer" + '</span>',
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


});

