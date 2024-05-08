jQuery(function ($) {
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
            url: 'WarehouseMasterLocationPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Plant = JSON.parse(pageload[0]);
                // var Zone = JSON.parse(pageload[1]);
                //var warehouse = JSON.parse(pageload[1]);
                var status = JSON.parse(pageload[1]);
                var DETAILS = JSON.parse(pageload[2]);

                $("#ddlplant").empty().append('<option value="">Select an Option</option>');
                $.each(Plant, function () {
                    $("#ddlplantlst").append($("<option></option>").val(this["PLANT"]).html(this["PLANTNAME"]));
                    $('#ddlplantlst').trigger("chosen:updated");
                });


                //$("#ddlwarehouse").empty().append('<option value="">Select an Option</option>');
                //$.each(warehouse, function () {
                //    $("#ddlwarehouse").append($("<option></option>").val(this["WAREHOUSECODE"]).html(this["WAREHOUSENAME"]));
                //    $('#ddlwarehouse').trigger("chosen:updated");
                //});

                $.each(status, function () {
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


                    { className: "text-capitalize", 'data': 'Plant' },
                    // { 'data': 'Employee Code' },
                    { className: "text-capitalize", 'data': 'Warehouse' },
                    { className: "text-capitalize", 'data': 'Location Code' },
                    { className: "text-capitalize", 'data': 'Location Name' },
                    { className: "text-capitalize", 'data': 'Record Status' },
                    { className: "text-capitalize", 'data': 'Remarks' },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class=" typcn typcn-edit" aria-hidden="true"></i></button><span style = "visibility: hidden;" >' + data + '</span >'


                        }

                    }
                   

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Warehouse Location Master',
                        text: '<img src="../../Images/excel.png" title ="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title ="PDF"style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Warehouse Location Master'


                    }, {
                        extend: 'print',
                        title: 'Warehouse Location Master',
                        text: '<img src="../../Images/print.png" title ="Print" style="height: 25px;">',
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

    $("#ddlplant").change(function () {
        $.ajax({
            url: 'GetWarehouseLocationMasterpartcodebyID',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'partcode': $('#ddlplant').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var plant = JSON.parse(Res[0]);
                $('#ddlwarehouse').val(plant[0].Warhouse);

            }

        });

    });


    $('#WarehouseLocationMaster-form').parsley().on('field:validated', function () {
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

            var Warehouse = $('#ddlwarehouse').val();
            var split = Warehouse.split('-');
            var dates3 = split[0] 
            var formData = new FormData();
           
            formData.append('PLANT', $('#ddlplant').val());
            formData.append('WAREHOUSE', dates3);
            formData.append('LOCATIONCODE', $('#txtlocationcode').val());
            formData.append('LOCATION', $('#txtlocation').val());
            formData.append('RECORDSTATUS', $('#ddlstatus').val());
            formData.append('REMARKS', $('#txtRemarks').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertWarehouseLocationMaster',
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
        var autoid = $(this).closest('tr').find('td:eq(6) > span').html();
        var warehouse = $(this).closest('tr').find('td:eq(1)').html();
        GetWarehouseDetailsByID(autoid)
    });

    function GetWarehouseDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditWarehouseLocationMaster',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'usercode': autoid }),

            success: function (data) {

                //  $.each(resJ, function (i, item) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $("#ddlplant").val(resJ[0].PLANT).trigger('change');
                // $('#txtwhcode').val(resJ[0].WHCODE);
                $('#txtwhname').val(resJ[0].WAREHOUSENAME);
                $('#txtlocationcode').val(resJ[0].LOCATIONCODE);
                $('#txtlocation').val(resJ[0].LOCATION);
                              //setTimeout(function () {
                //    $("#ddlState").val(resJ[0].STATE).trigger('change');
                //}, 300);
                //setTimeout(function () {
                //    $("#ddlCity").val(resJ[0].CITY).trigger('change');
                //}, 300);

                $('#ddlstatus').val(resJ[0].RECORDSTATUS).trigger('change');
                $('#txtRemarks').val(resJ[0].REMARKS)
                // $('#txttransittime').val(resJ[0].TRANSITTIME);
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }
    $('#btnprint').click(function () {
        var $row = $(this).closest("tr");    // Find the row
        var $text = $row.find(".sorting_1").text(); // Find the text
        $('#printmodalview').modal('show');
        callprintpopup($text);
    });
    function callprintpopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'WarehouseLocationMasterPrintView',
            dataType: 'json',
            type: 'POST',
            data: { "locationcode": $text },

            success: function (data) {
                debugger;
                var Tables = data.split('|');
                var DetailsTable = JSON.parse(Tables[0]);
                debugger;
                BindprintpopupTab(DetailsTable, '1');
                $("html, body").animate({ scrollTop: 0 }, "slow");

            }
        });
        $(".loader").hide("slow");
    }

    $('#btntemplateimport').click(function () {
        window.location.href = "Downloads/?file=WAREHOUSELOCATIONMASTER.xlsx";
    });
    $("#btnimport").click(function () {
        $('.loadingGIF').show();
        var uploadFile = new FormData();
        var files = $("#hf").get(0).files;
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);

            $.ajax({
                url: 'MoldMastersUploads',
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

    function BindprintpopupTab(ResData, type) {

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
        var rcount = $("#dynamic-printtableDetails> tbody > tr").length
        if (rcount > 0) {
            $('#dynamic-printtableDetails').dataTable().fnDestroy();
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

            var dyTable = $('#dynamic-printtableDetails');

            $('#dynamic-printtableDetails').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bPaginate": false,
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
                columns: cols1DATA,
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Warehouse Location Master',
                        text: '<img src="../../Images/excel.png" title ="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title ="PDF"style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Warehouse Location Master'


                    }, {
                        extend: 'print',
                        title: 'Warehouse Location Master',
                        text: '<img src="../../Images/print.png" title ="Print" style="height: 25px;">',
                        footer: false
                    }
                ]
            });
        }
        else {
            $('#dynamic-printtableDetails tbody').remove();
            $('#dynamic-printtableDetails thead').remove();
            $('#dynamic-printtableDetails').dataTable({
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
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": false,
                "searching": true
            });


        }
        $(".loader").hide("slow");
    }

    
    $('#checkall').change(function () {
        if ($(this).is(":checked")) {
            $('.isselect').attr("checked", true);
        } else {
            $('.isselect').attr("checked", false);
        }

    });

    $("#btnprintview").click(function () {
        var locationcode = "", locationname = "", locationdescription = "";
        jQuery("table.checkprint tbody > tr").each(function () {

            // sno += jQuery('td:eq(0)', this).text() + "^";
            if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                debugger;
                locationcode += jQuery('td:eq(3)', this).text() + "^";
                locationname += jQuery('td:eq(4)', this).text() + "^";
                locationdescription += jQuery('td:eq(2)', this).text() + "^";
            }

        });
        $(".loader").show("slow");
        $.ajax({
            url: 'PrintBarcodeByLocation',
            dataType: 'json',
            type: 'POST',
            data: {
                "locationcode": locationcode = locationcode.substring(0, locationcode.length - 1),
                "locationname": locationname = locationname.substring(0, locationname.length - 1),
                "locationdescription": locationdescription = locationdescription.substring(0, locationdescription.length - 1)
            },

            success: function (data) {
                var result = data.split("|");
                var tb = (result[0]);
                $("#printable").html(tb);

                var locationcodearr = locationcode.split("^");
                var COUNT = 0;
                for (var i = 0; i < locationcodearr.length; i++) {
                    var ID = locationcodearr[i];

                    QRCODEPRINT(COUNT, ID);
                    COUNT++;

                }

                setTimeout(function () {
                    printableinner();
                }, 20);

            }
        });
        $(".loader").hide("slow");

    });

    function QRCODEPRINT(COUNT, ID) {
        //   $("#qrcode").empty();

        var qrcode = new QRCode(document.getElementById("locationid" + COUNT), {
            width: 130,
            height: 130
        });
        function makeCode() {
            var elText = ID;
            qrcode.makeCode(elText);
        }
        makeCode();
    }

    function printableinner() {

        var disp_setting = "toolbar=no,location=no,";
        disp_setting += "directories=no,menubar=no,";
        disp_setting += "scrollbars=yes";
        var content_vlue = document.getElementById("printable").innerHTML;
        var docprint = window.open("", "", disp_setting);
        docprint.document.open();
        docprint.document.write('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"');
        docprint.document.write('"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">');
        docprint.document.write('<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">');
        docprint.document.write('<head>');
        docprint.document.write('<style type="text/css">body{ border:0px solid #000;margin:0px;font-family:verdana,Arial;color:#000;font-family:Verdana, Geneva, sans-serif; font-size:8px;}a{color:#000;text-decoration:none;}*{font-size:8px;} small{font-size:30% !important;}table {border-collapse: collapse;}table, tr, td, th {border: 0px solid black;padding:0.5rem 2px;}@page {size: 4in 2in;margin: 1mm;}div#printable {width: 4in;height: 2in;} .qr-panel img{width:50px;}</style> ');
        docprint.document.write('</head><body onLoad="self.print()">');
        docprint.document.write(content_vlue);
        docprint.document.close();
        docprint.focus();

    }

});