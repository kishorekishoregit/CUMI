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
            url: 'RMProductionOrderPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var Zone = JSON.parse(pageload[0]);
                var RMdts = JSON.parse(pageload[1]);
             


                
                $("#ddlwarehousepicker").empty().append('<option value="">Select an Option</option>');
                $.each(Zone, function () {
                    $("#ddlwarehousepickerlst").append($("<option></option>").val(this["EMPLOYEECODE"]).html(this["USERNAME"]));
                    $('#ddlwarehousepickerlst').trigger("chosen:updated");
                });


             

                BindTab(RMdts, '1');
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


                  

                    { className: "text-capitalize", 'data': 'Production Order No' },
                    { className: "text-capitalize", 'data': 'Received date' },
                    { className: "text-capitalize", 'data': 'WareHouse Picker' },
                    {

                        className: "text-capitalize", 'data': 'View',
                        'render': function (data, type, full, meta) {

                            /*return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'*/
                            return '<button type="button"  class="btn-az-secondary  searchdetails " id="view" data-toggle="tooltip" data-placement="top" title="View"><i class="icon ion-md-eye" aria-hidden="true"></i></button>'
                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'RM Production order',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'RM Production order'


                    }, {
                        extend: 'print',
                        title: 'RM Production orderr',
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
                    "emptyTable": "RM Production order No records found.."
                },
                'bSort': false,
                'aoColumns': [
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
    $("#txtproductionno").change(function () {

        $.ajax({
            url: 'RMProductionOrderFetchDtsByProductionno',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'ProductionNo': $('#txtproductionno').val().trim() }),
            success: function (data) {
                var Res = data;
                var result = JSON.parse( Res);
                
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

                    { className: "text-capitalize", 'data': 'ITEMCODE' },
                    { className: "text-capitalize", 'data': 'ITEMNAME' },
                    { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'QUANTITY' },
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
    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";
        if ($('#txtproductionno').val() == "") {
            errormessage += "Please Scan valid Production OrderNo</br> ";
        }
        if ($('#txtreceiveddate').val() == "") {
            errormessage += "Please Enter Date.</br>";
        }
        if ($('#ddlwarehousepicker').val() == "") {
            errormessage += "Please Enter Warehouse Picker.</br>";
        }

      

        if (errormessage.length == 0) {
            var intcount = 0;
            var itemcode = "";
            var itemname = "";
            var Uom = "";
            var Quantity = "";
            $("#dynamic-table2 tbody > tr").each(function () {
                intcount++;
                debugger;
                itemcode += jQuery('td:eq(0)', this).html() + "^";
                itemname += jQuery('td:eq(1)', this).text() + "^";
                Uom += jQuery('td:eq(2)', this).html() + "^";
                Quantity += jQuery('td:eq(3)', this).html() + "^";

            });
            if (intcount > 0) {
                debugger;
                itemcode = itemcode.substring(0, itemcode.length - 1);
                itemname = itemname.substring(0, itemname.length - 1);
                Uom = Uom.substring(0, Uom.length - 1);
                Quantity = Quantity.substring(0, Quantity.length - 1);

            }


            $.ajax({
                url: 'RMProductionOrderInsert',
                dataType: "json",
                type: "POST",


                data: $.param({ 'PONO': $('#txtproductionno').val() })
                    + '&' + $.param({ 'Date': $('#txtreceiveddate').val() })
                    + '&' + $.param({ 'Warehousepicker': $('#ddlwarehousepicker').val() })
                    + '&' + $.param({ 'Itemcode': itemcode })
                    + '&' + $.param({ 'Itemname': itemname })
                    + '&' + $.param({ 'Uom': Uom })
                    + '&' + $.param({ 'Quantity': Quantity }),

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

    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $("#dynamic-table").on("click", ".searchdetails", function () {
        var $text = $(this).closest('tr').find('td:eq(0)').html();
        // var $text1 = $(this).closest('tr').find('td:eq(1)').text();
        $('#modalview').modal('show');
        callpropertypopup($text);
    });

    function callpropertypopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'RMProductionOrderBView',
            dataType: 'json',
            type: 'POST',
            data: { "ProductionNo": $text },

            success: function (data) {
                //debugger;
                var Tables = data;
                var CasePropertyDetails = JSON.parse(Tables);
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
                // columnDefs: cols1,
                columns: [

                    { className: "text-capitalize", 'data': 'Production Order No' },
                    { className: "text-capitalize", 'data': 'Item Code' },
                    { className: "text-capitalize", 'data': 'Item Name' },
                    { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                    

                ],
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
});