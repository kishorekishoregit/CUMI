jQuery(function ($) {

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
            url: 'RDCPicklistAssignPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var warehouse = JSON.parse(pageload[0]);
                var location = JSON.parse(pageload[1]);
                var supplier = JSON.parse(pageload[2]);
                var table = JSON.parse(pageload[3]);
                var ERPdts = JSON.parse(pageload[4]);

                $("#ddlwarehousepicker").empty().append('<option value="">Select an Option</option>');
                $.each(warehouse, function () {
                    $("#ddlwarehousepickerlst").append($("<option></option>").val(this["EMPLOYEECODE"]).html(this["EMPLOYEENAME"]));
                    $('#ddlwarehousepickerlst').trigger("chosen:updated");
                });

                $("#ddllocation").empty().append('<option value="">Select an Option</option>');
                $.each(location, function () {
                    $("#ddllocationlst").append($("<option></option>").val(this["LOCATIONCODE"]).html(this["LOCATIONNAME"]));
                    $('#ddllocationlst').trigger("chosen:updated");
                });

                $("#ddlsupplier").empty().append('<option value="">Select an Option</option>');
                $.each(supplier, function () {
                    $("#ddlsupplierlst").append($("<option></option>").val(this["SUPPLIERCODE"]).html(this["SUPPLIERNAME"]));
                    $('#ddlsupplierlst').trigger("chosen:updated");
                });

                BindTab(table, '1');
                BindchildTab(ERPdts, '1');

                var checkbox = document.getElementById("checkall");

                // Uncheck the checkbox
                checkbox.checked = false;
            }
        });
        $(".loader").hide("slow");
    }

    $("#txtrdcno").change(function () {
        $.ajax({
            url: 'GetRDCNumberpicklistassigndetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'RDCNO': $('#txtrdcno').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var result = Res[0];
                var msg = Res[1];
                var rdcdetails = JSON.parse(Res[2]);
                if (result.toUpperCase() == "TRUE")
                {
                    BindchildTab(rdcdetails, '1');
                }
                else {
                    bootbox.alert({
                        /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                        message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                        size: 'small',
                        callback: function () {
                            Clear();
                        }
                    });
                    $('.loadingGIF').hide();
                }
                
            }

        });
    });


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
                'columnDefs': [{ "width": "1%", "targets": 5 }, {
                    'targets': 0,
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: [


                    //{
                    //    className: "text-capitalize", 'data': 'Plant',
                    //    'render': function (data, type, full, meta) {

                    //        var pakarr = data.split('_');

                    //        return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                    //    }
                    //},
                    { className: "text-capitalize", 'data': 'Warehouse Picker' },
                    { className: "text-capitalize", 'data': 'RDC No' },
                    { className: "text-capitalize", 'data': 'Date' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Description' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                    // { className: "text-capitalize", 'data': 'Remarks' },
                    {

                        className: "text-capitalize", 'data': 'View',
                        'render': function (data, type, full, meta) {
                            //return '<button type="button" class="btn btn-success btn-fab searchdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="fa fa-search" aria-hidden="true"></i></button>'
                            return '<button type="button"  class="btn-az-secondary  searchdetails " id="view" data-toggle="tooltip" data-placement="top" title="View"><i class="icon ion-md-eye" aria-hidden="true"></i></button>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'RDC Picklist Assign',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'RDC Picklist Assign'


                    }, {
                        extend: 'print',
                        title: 'RDC Picklist Assign',
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

    $("#dynamic-table").on("click", ".searchdetails", function () {
        var $text = $(this).closest('tr').find('td:eq(1)').html();
        // var $text1 = $(this).closest('tr').find('td:eq(1)').text();
        $('#modalview').modal('show');
        callpropertypopup($text);
    });

    function callpropertypopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'RDCPicklistassigndetialsView',
            dataType: 'json',
            type: 'POST',
            data: { "RDCNO": $text },

            success: function (data) {
                //debugger;
                var Tables = data.split('|');
                var CasePropertyDetails = JSON.parse(Tables[0]);
                //debugger;
                BindchildTabPopup(CasePropertyDetails, '1');
                //   $("html, body").animate({ scrollTop: 0 }, "slow");

            }
        });
        $(".loader").hide("slow");
    }

    function BindchildTabPopup(ResData, type) {

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
        var len = $('#dynamic-table1 tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-table1').dataTable().fnDestroy();
            $('#dynamic-table1').DataTable().destroy(); $('#dynamic-table1').html('');
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

            $('#dynamic-table1').dataTable({
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
                columnDefs: cols1,
                columns: [



                    { className: "text-capitalize", 'data': 'Item Code' },
                    { className: "text-capitalize", 'data': 'Item Name' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                   

                    //{
                    //    className: "text-capitalize", 'data': 'Remove',
                    //    'render': function (data, type, full, meta) {
                    //        return '<button type="button" class="btn-danger remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="typcn typcn-archive"></i></button>'
                    //    }
                    //},

                ],
                // dom: 'Brtip',


            });
        }
        else {
            $('#dynamic-table1 tbody').remove();
            $('#dynamic-table1 thead').remove();
            $('#dynamic-table1').dataTable({
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
                columnDefs: cols1,
                columns: [

                    { className: "text-capitalize", 'data': 'Select' },
                    { className: "text-capitalize", 'data': 'RDC No' },
                    { className: "text-capitalize", 'data': 'Date' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Description' },
                    { className: "text-capitalize", 'data': 'RFID No' },
                    { className: "text-capitalize", 'data': 'Supplier Code' },
                    { className: "text-capitalize", 'data': 'Remark' },
                    { className: "text-capitalize", 'data': 'Qty' },
                   
                ],
                // dom: 'Brtip',


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
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
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

    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";
       
        if ($('#ddlwarehousepicker').val() == "") {
            errormessage += "Please Enter Warehouse Picker.</br>";
        }

        if (!$('.checkbox').is(':checked')) {
            errormessage += ("At least one checkbox must be checked.</br>");
            //return false; // Prevent form submission
        }
      
        if (errormessage.length == 0) {
            var intcount = 0;
            var itemcode = "";
            var supplier = "";
            var remarks = "";
            var itemname = "";
            var RDCNo = "";
            var date = "";
            var qty = '';
            var RFID = "";
            $("#dynamic-table2 tbody > tr").each(function () {
                intcount++;
                debugger;
                if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {

                    RDCNo += jQuery('td:eq(1)', this).html() + "^";
                    date += jQuery('td:eq(2)', this).text() + "^";
                    itemcode += jQuery('td:eq(3)', this).html() + "^";
                    itemname += jQuery('td:eq(4)', this).html() + "^";
                    RFID += jQuery('td:eq(5)', this).html() + "^";
                    supplier += jQuery('td:eq(6)', this).html() + "^";
                    remarks += jQuery('td:eq(7)', this).html() + "^";
                    qty += jQuery('td:eq(8)', this).html() + "^";
                }


            });
            if (intcount > 0) {
                debugger;
                RDCNo = RDCNo.substring(0, RDCNo.length - 1);
                date = date.substring(0, date.length - 1);
                itemcode = itemcode.substring(0, itemcode.length - 1);
                itemname = itemname.substring(0, itemname.length - 1);
                RFID = RFID.substring(0, RFID.length - 1);
                supplier = supplier.substring(0, supplier.length - 1);
                remarks = remarks.substring(0, remarks.length - 1);
                qty = qty.substring(0, qty.length - 1);

            }


            $.ajax({
                url: 'RDCPicklistAssignInsert',
                dataType: "json",
                type: "POST",


                data: $.param({ 'Warehousepicker': $('#ddlwarehousepicker').val() })
                    + '&' + $.param({ 'RDCNO': RDCNo })
                    + '&' + $.param({ 'Date': date })
                    + '&' + $.param({ 'Itemcode': itemcode })
                    + '&' + $.param({ 'Itemname': itemname })
                    + '&' + $.param({ 'RFIDNo': RFID })
                    + '&' + $.param({ 'supplier': supplier })
                    + '&' + $.param({ 'Remarks': remarks })
                    + '&' + $.param({ 'Quantity': qty }) ,

                success: function (data) {
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {

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
                            size: 'small',
                            callback: function () {
                                Clear();
                            }
                        });

                        $('.loadingGIF').hide();
                    }
                }
            });
        }
        else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();

        }
    });
    $('#checkall').click(function (event) {
        if (this.checked) {
            // Iterate each checkbox
            $(':checkbox').each(function () {
                this.checked = true;
            });
        } else {
            $(':checkbox').each(function () {
                this.checked = false;
            });
        }
    });
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

});