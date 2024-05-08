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
        $(".loadingGIF").show("slow");
        $.ajax({
            url: 'MoldReceipt_RDCPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var RDCNO = JSON.parse(pageload[0]);
                var MoldDetails = JSON.parse(pageload[1]);


              

                $("#ddlrdcno").empty().append('<option value="">Select an Option</option>');
                $.each(RDCNO, function () {
                    $("#ddlrdcno").append($("<option></option>").val(this["RDCNO"]).html(this["RDCNO"]));
                    $('#ddlrdcno').trigger("chosen:updated");
                });
              

                BindTab(MoldDetails, '1');
            }
        });
        $(".loadingGIF").hide("slow");
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


                   
                    { className: "text-capitalize", 'data': 'RDC No' },
                    { className: "text-capitalize", 'data': 'RFID No' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Name' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Mould Receipt-RDC',
                        text: '<img src="../../Images/excel.png" title = "Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title = "PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mould Receipt-RDC'


                    }, {
                        extend: 'print',
                        title: 'Mould Receipt-RDC',
                        text: '<img src="../../Images/print.png" title = "Print" style="height: 25px;">',
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
                    "emptyTable": "Mold Receipt-RDC No records found.."
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

    $("#ddlrdcno").change(function () {

        $.ajax({
            url: 'MoldReceipt_RDCFetchDetailsByRDCNO',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'RDCNO': $('#ddlrdcno').val().trim() }),
            success: function (data) {
                var Res = data;
                var result = JSON.parse(Res);

                // var productiondetails = JSON.parse(Res[2]);
                // if (result.toUpperCase() == "TRUE") {
                BindchildTab(result, '1');



            }

        });
    });
    function BindchildTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var idinc = 0;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-table2 tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-table2').dataTable().fnDestroy();
            $('#dynamic-table2').DataTable().destroy(); $('#dynamic-table2').html('');
            //$('#dynamic-tablePropDetails').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecordUpload) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecordUpload);

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

            $('#dynamic-table2').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bFilter": false,
                "bInfo": false,
                "bPaginate": false,
                "bLengthChange": false,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                // columnDefs: cols1,
                columns: [

                    { className: "text-capitalize", 'data': 'Select' },
                    { className: "text-capitalize", 'data': 'RFID No' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Name' },
                   // { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                   

                ],


            });
        }
        else {
            $('#dynamic-table2 tbody').remove();
            $('#dynamic-table2 thead').remove();
            $('#dynamic-table2').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false }
                ],
                "scrollCollapse": false,
                "info": false,
                "paging": false,
                "searching": false,
                "ordering": false,
            });


        }

    }
    //$("#txtscan").change(function () {
    //    var RFID = '';
    //    jQuery("table.checkupload tbody > tr").each(function () {
    //        var RFIDText = jQuery('td:eq(1)', this).text().trim(); 

            
    //        if (RFIDText !== '') {
    //            RFID += RFIDText;

    //            var scanvalue = $("#txtscan").val();

    //            if (RFIDText == scanvalue) {
    //                jQuery('input[type="checkbox"]', this).prop('checked', true);
    //                jQuery('td:eq(1)', this).css('color', 'red');
    //                jQuery('td:eq(2)', this).css('color', 'red');
    //                jQuery('td:eq(3)', this).css('color', 'red');
    //                jQuery('td:eq(4)', this).css('color', 'red');
    //                jQuery('td:eq(5)', this).css('color', 'red');
    //                jQuery('td:eq(6)', this).css('color', 'red');
    //            } else {
    //                bootbox.alert({
    //                    /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
    //                    message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Invalid RFID No." + '</span>',
    //                    size: 'small',
    //                    callback: function () {
    //                        Clear();
    //                    }
    //                });
    //                $('.loadingGIF').hide();
    //            }
    //        }
    //    });
    //});

    //$("#txtscan").change(function () {
    //    var scanvalue = $(this).val().trim();
       
    //    if (scanvalue !== '') {
    //        // Loop through each row in the table
    //        $("table.checkupload tbody > tr").each(function () {
    //            // Get the RFID value for this row
    //            var RFIDText = $(this).find('td:eq(1)').text().trim();

    //            // Check if RFID value is not empty
    //            if (RFIDText !== '') {
    //                // Check if RFID value matches the scan value
    //                if (RFIDText === scanvalue) {
    //                    // If match, check the checkbox and color the row
    //                    var checkbox = $(this).find('input[type="checkbox"]');
    //                    if (!checkbox.prop('checked')) {
    //                        checkbox.prop('checked', true);
    //                    }
    //                    $(this).find('td:eq(1), td:eq(2), td:eq(3), td:eq(4), td:eq(5), td:eq(6)').css('color', 'red');
    //                }

    //            }
    //        });
    //    } else {
    //        bootbox.alert({
    //            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger" style="margin-left: 100px;font-size: 50px;"></i><br>' + "Invalid RFID No." + '</span>',
    //            size: 'small',
    //            callback: function () {
    //                Clear();
    //            }
    //        });
    //        $('.loadingGIF').hide();
    //    }

    //    // Clear the textbox value
    //    $(this).val('');
    //});
    $("#txtscan").change(function () {
        var scanvalue = $(this).val().trim();
        var foundMatch = false;
        //var scannedval = scanvalue.split('\t');
        //scanvalue = scannedval[0];
        if (scanvalue !== '') {
            $("table.checkupload tbody > tr").each(function () {
                var RFIDText = $(this).find('td:eq(1)').text().trim();
               

                if (RFIDText !== "") {
                    if (RFIDText === scanvalue) {
                        foundMatch = true;
                        var checkbox = $(this).find('input[type="checkbox"]');
                        if (!checkbox.prop('checked')) {
                            checkbox.prop('checked', true);
                        }
                        $(this).find('td:eq(1), td:eq(2), td:eq(3), td:eq(4), td:eq(5), td:eq(6)').css('color', 'red');
                    }
                }
            });

            if (!foundMatch) {
                bootbox.alert({
                    message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger" style="margin-left: 100px;font-size: 50px;"></i><br>' + "Invalid RFID No." + '</span>',
                    size: 'small',
                   
                });
                $('.loadingGIF').hide();
            }
        } else {
            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger" style="margin-left: 100px;font-size: 50px;"></i><br>' + "Invalid RFID No." + '</span>',
                size: 'small',
                callback: function () {
                    Clear();
                }
            });
            $('.loadingGIF').hide();
        }

        $(this).val('');
    });
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $("#btnsubmit").click(function () {
        $(".loadingGIF").show("slow");
        var errormessage = "";

        if ($('#ddlrdcno').val() == "") {
            errormessage += "Please Select RDC No.</br>";
        }
        if (!$('.checkbox').is(':checked')) {
            errormessage += ("At least one checkbox must be checked.</br>");
            //return false; // Prevent form submission
        }
        if (errormessage.length == 0) {
            var RDCNO = "",RFIDNO = '', itemcode = "", itemname = "", uom = "", quantity = "", Supplier = "";
            var intcount = 0;
            jQuery("table.checkupload tbody > tr").each(function () {
                debugger;
                if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                    intcount++;
                    RFIDNO += jQuery('td:eq(1)', this).text() + "^";
                    itemcode += jQuery('td:eq(2)', this).text() + "^";
                    itemname += jQuery('td:eq(3)', this).text() + "^";
                    //uom += jQuery('td:eq(4)', this).text() + "^";
                    quantity += jQuery('td:eq(4)', this).text() + "^";
                    Supplier += jQuery('td:eq(5)', this).text() + "^";
                }

            });
            if (intcount > 0) {
                RFIDNO = RFIDNO.substring(0, RFIDNO.length - 1);
                itemcode = itemcode.substring(0, itemcode.length - 1);
                itemname = itemname.substring(0, itemname.length - 1);
               // uom = uom.substring(0, uom.length - 1);
                quantity = quantity.substring(0, quantity.length - 1);
                Supplier = Supplier.substring(0, Supplier.length - 1);
            }


            if (window.FormData !== undefined) {
                //$('.loadingGIF').show();
                // $(".loader").show("slow");
                $.ajax({
                    url: 'MoldReceiptRDCInsert',
                    dataType: "json",
                    type: "POST",


                    data: $.param({ 'RDCNO': $('#ddlrdcno').val() })
                        + '&' + $.param({ 'RFIDNO': RFIDNO })
                        + '&' + $.param({ 'ITEMCODE': itemcode })
                        + '&' + $.param({ 'ITEMNAME': itemname })
                       // + '&' + $.param({ 'UOM': uom })
                        + '&' + $.param({ 'QUANTITY': quantity })
                        + '&' + $.param({ 'SUPPLIER': Supplier }),

                    //  data:formData,
                    success: function (data) {
                        var Res = data.split('|');
                        var result = Res[0];
                        var msg = Res[1];
                        if (result.toUpperCase() == "TRUE") {
                            //$("form").trigger("reset");
                            //$("#actiontype").text("Save")

                            bootbox.alert({

                                message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                                size: 'small',
                                callback: function () {
                                    Clear();
                                }
                            });

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
        } else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();

        }
    });
});