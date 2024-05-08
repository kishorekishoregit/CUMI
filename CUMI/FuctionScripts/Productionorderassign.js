jQuery(function ($) {

    BindTable();
    var assemblyid = '';
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
            url: 'ProductionOrderAssignPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var warehouse = JSON.parse(pageload[0]);
                var location = JSON.parse(pageload[1]);
                var table = JSON.parse(pageload[2]);
                var chiltable = JSON.parse(pageload[3]);
                assemblyid = JSON.parse(pageload[4]);

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
                BindTab(table, '1');
                BindchildTab(chiltable, '1');

                var checkbox = document.getElementById("checkall");

                // Uncheck the checkbox
                checkbox.checked = false;
            }
        });
        $(".loader").hide("slow");
    }

    $("#txtproductionno").change(function () {

        $.ajax({
            url: 'GetProductionordernumberdetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'PRODUCTIONORDER': $('#txtproductionno').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var result = Res[0];
                var msg = Res[1];
                var productiondetails = JSON.parse(Res[2]);                      
                if (result.toUpperCase() == "TRUE") {
                    BindchildTab(productiondetails, '1');
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


                    //{
                    //    className: "text-capitalize", 'data': 'Plant',
                    //    'render': function (data, type, full, meta) {

                    //        var pakarr = data.split('_');

                    //        return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                    //    }
                    //},
                    { className: "text-capitalize", 'data': 'Production OrderNo' },
                    { className: "text-capitalize", 'data': 'Date' },
                    { className: "text-capitalize", 'data': 'Warehouse Picker' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Production Qty' },
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
                        title: 'Mold Master',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mold Master'


                    }, {
                        extend: 'print',
                        title: 'Mold Master',
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
                    "emptyTable": "Mould Production Order No records found.."
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
        var $text = $(this).closest('tr').find('td:eq(0)').html();
        // var $text1 = $(this).closest('tr').find('td:eq(1)').text();
        $('#modalview').modal('show');
        callpropertypopup($text);
    });

    function callpropertypopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'ProductionorderdetialsView',
            dataType: 'json',
            type: 'POST',
            data: { "PRODUCTIONORDER": $text },

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



                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Name' },
                    { className: "text-capitalize", 'data': 'Production Qty' },

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
               // columnDefs: cols1,
                columns: [

                    { className: "text-capitalize", 'data': 'Select' },
                    { className: "text-capitalize", 'data': 'Production Order No' },
                    { className: "text-capitalize", 'data': 'Production Order Date' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Variant Code' },
                    { className: "text-capitalize", 'data': 'Production Qty' },
                    
                    {
                        className: "text-capitalize",
                        title: "Assembly ID",
                        "data": "assemblyid",
                        render: function (data, type, row) {
                            var $select = $('<select></select>', {
                                "id": row[0] + "assemblyid",
                                "value": data,
                                "style": "height: 30px; width: 150px;" // Added styles for height and width
                            });

                            var $option = $("<option/>", {
                                text: "Select",
                                value: ""
                            });
                            $select.append($option);
                            $.each(assemblyid, function (k, v) {
                                var $option = $("<option/>", {
                                    text: this["ASSEMBLYID"],
                                    value: this["ASSEMBLYID"]
                                });
                                if (data === this["ASSEMBLYID"]) {
                                    $option.attr("selected", "selected")
                                }

                                $select.append($option);
                            });

                            return $select.prop("outerHTML");
                        }
                    },
                    {
                        className: "text-capitalize", title: "Assembly Qty ", "data": "Assembly Qty",
                        render: function (data, type, row) {
                            var $remarks = '';
                            $remarks = '<input class="form-control text-wrap" style=Width:95px; id="ASSEMBLYQTY"   text="ASSEMBLYQTY">';
                            return $remarks

                        }
                    },
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

    $("#btnassign").click(function () {
        $(".loader").show("slow");
        var errormessage = "";
       
        if ($('#ddlwarehousepicker').val() == "") {
            errormessage += "Please Select Warehouse Picker.</br>";
        }
        if (!$('.checkbox').is(':checked')) {
            errormessage += ("At least one checkbox must be checked.</br>");
            //return false; // Prevent form submission
        }
        if (errormessage.length == 0) {
            var intcount = 0;
            var productionno = "";
            var productiondate = "";
            var variant = "";
            var FGItemcode = "";
            var ProductQty = "";
            var Assemblyid = "";
            var Assemblyqty = "";


            $("#dynamic-table2 tbody > tr").each(function () {
                intcount++;
                debugger;
                if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                    productionno += jQuery('td:eq(1)', this).html() + "^";
                    productiondate += jQuery('td:eq(2)', this).html() + "^";
                    FGItemcode += jQuery('td:eq(3)', this).html() + "^";
                    variant += jQuery('td:eq(4)', this).html() + "^";
                    ProductQty += jQuery('td:eq(5)', this).html() + "^";
                    Assemblyid += $('td:eq(6) :selected', this).val() + "^";
                    Assemblyqty += $(this).closest('tr').find('td:eq(7) input').val() + "^";
                   
                    
                }
            });
            if (intcount > 0) {
                debugger;
                productionno = productionno.substring(0, productionno.length - 1);
                productiondate = productiondate.substring(0, productiondate.length - 1);
                FGItemcode = FGItemcode.substring(0, FGItemcode.length - 1);
                variant = variant.substring(0, variant.length - 1);
                ProductQty = ProductQty.substring(0, ProductQty.length - 1);
                Assemblyid = Assemblyid.substring(0, Assemblyid.length - 1);
                Assemblyqty = Assemblyqty.substring(0, Assemblyqty.length - 1);
               

            }


            $.ajax({
                url: 'ProductionOrderAssigndts',
                dataType: "json",
                type: "POST",


                data: $.param ({ 'Warehousepicker': $('#ddlwarehousepicker').val() })
                    + '&' + $.param({ 'PRODUCTIONORDERNO': productionno })
                    + '&' + $.param({ 'Fgitemcode': FGItemcode })
                    + '&' + $.param({ 'AssemblyID': Assemblyid })
                    + '&' + $.param({ 'VariantCode': variant })
                    + '&' + $.param({ 'Productionqty': Assemblyqty }),
                    

                success: function (data) {
                    var Res = data;
                    var Assembydts = JSON.parse(Res);

                    BindAssesmblydts(Assembydts, '1');


                    $("#dynamic-table2 tbody > tr").each(function () {
                       // Assemblyid += $('td:eq(6) :selected', this).val().trigger('change');
                        Assemblyqty += $(this).closest('tr').find('td:eq(7) input').val('');
                    });
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
    function BindAssesmblydts(ResData, type) {

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
        var len = $('#dynamic-tableassemblydts tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableassemblydts').dataTable().fnDestroy();
            $('#dynamic-tableassemblydts').DataTable().destroy(); $('#dynamic-tableassemblydts').html('');
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

            $('#dynamic-tableassemblydts').dataTable({
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




                    { className: "text-capitalize", title: 'FGITEMCODE', 'data': 'FGITEMCODE' },
                    { className: "text-capitalize", title: 'MOLDITEMCODE', 'data': 'MOLDITEMCODE' },
                    { className: "text-capitalize", title: 'MOLDITEMNAME', 'data': 'MOLDITEMNAME' },
                    { className: "text-capitalize", title: 'VARIANT', 'data': 'VARIANT' },
                    { className: "text-capitalize", title: 'QUANTITY', 'data': 'QUANTITY' },
                 

                ],
                // dom: 'Brtip',


            });
        }
        else {
            $('#dynamic-tableassemblydts tbody').remove();
            $('#dynamic-tableassemblydts thead').remove();
            $('#dynamic-tableassemblydts').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
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



    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";

        if ($('#ddlwarehousepicker').val() == "") {
            errormessage += "Please Select Warehouse Picker.</br>";
        }
        if (!$('.checkbox').is(':checked')) {
            errormessage += ("At least one checkbox must be checked.</br>");
            //return false; // Prevent form submission
        }
        if (errormessage.length == 0) {
            var intcount = 0;
            var intcount1 = 0;
            var productionno = "";
            var productiondate = "";
            var variant = "";
            var FGItemcode = "";
            var ProductQty = "";
            var Assemblyid = "";
            var Assemblyqty = "";
            var Molditemcode = "";
            var MoldItemname = "";
            var Moldvariant = "";
            var Totalassemblyqty = "";
            var FGitemcodedts = "";

            $("#dynamic-table2 tbody > tr").each(function () {
                intcount++;
                debugger;
                if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                    productionno += jQuery('td:eq(1)', this).html() + "^";
                    productiondate += jQuery('td:eq(2)', this).html() + "^";
                    FGItemcode += jQuery('td:eq(3)', this).html() + "^";
                    variant += jQuery('td:eq(4)', this).html() + "^";
                    ProductQty += jQuery('td:eq(5)', this).html() + "^";
                    Assemblyid += $('td:eq(6) :selected', this).val() + "^";
                    Assemblyqty += $(this).closest('tr').find('td:eq(7) input').val() + "^";


                }
            });
            if (intcount > 0) {
                debugger;
                productionno = productionno.substring(0, productionno.length - 1);
                productiondate = productiondate.substring(0, productiondate.length - 1);
                FGItemcode = FGItemcode.substring(0, FGItemcode.length - 1);
                variant = variant.substring(0, variant.length - 1);
                ProductQty = ProductQty.substring(0, ProductQty.length - 1);
                Assemblyid = Assemblyid.substring(0, Assemblyid.length - 1);
                Assemblyqty = Assemblyqty.substring(0, Assemblyqty.length - 1);


            }
            $("#dynamic-tableassemblydts tbody > tr").each(function () {
                intcount1++;
                debugger;
                FGitemcodedts += jQuery('td:eq(0)', this).html() + "^";
                Molditemcode += jQuery('td:eq(1)', this).html() + "^";
                MoldItemname += jQuery('td:eq(2)', this).html() + "^";
                Moldvariant += jQuery('td:eq(3)', this).html() + "^";
                Totalassemblyqty += jQuery('td:eq(4)', this).html() + "^";
            });
            if (intcount1 > 0) {
                debugger;
                FGitemcodedts = FGitemcodedts.substring(0, FGitemcodedts.length - 1);
                Molditemcode = Molditemcode.substring(0, Molditemcode.length - 1);
                MoldItemname = MoldItemname.substring(0, MoldItemname.length - 1);
                Moldvariant = Moldvariant.substring(0, Moldvariant.length - 1);
                Totalassemblyqty = Totalassemblyqty.substring(0, Totalassemblyqty.length - 1);
            }


            $.ajax({
                url: 'ProductionOrderAssignInsert',
                dataType: "json",
                type: "POST",


                data: $.param({ 'Warehousepicker': $('#ddlwarehousepicker').val() })
                    + '&' + $.param({ 'PONO': productionno })
                    + '&' + $.param({ 'PODATE': productiondate })
                    + '&' + $.param({ 'FGItemCode': FGItemcode })
                    + '&' + $.param({ 'FGVariant': variant })
                    + '&' + $.param({ 'ProductionQty': ProductQty })
                    + '&' + $.param({ 'Assemblyid': Assemblyid })

                    + '&' + $.param({ 'FGitemcodedts': FGitemcodedts })
                    + '&' + $.param({ 'MoldItemcode': Molditemcode })
                    + '&' + $.param({ 'MoldItemname': MoldItemname })
                    + '&' + $.param({ 'Moldvariant': Moldvariant })
                    + '&' + $.param({ 'Totalassemblyqty': Totalassemblyqty }),


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
        else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();

        }
    });
});