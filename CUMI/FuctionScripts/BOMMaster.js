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
            url: 'PageloadBOMMaster',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageload = Result.split('|');

                var Variantcode = JSON.parse(pageload[0]);
                var Molditemcode = JSON.parse(pageload[1]);
                var BOMMASTERDTS = JSON.parse(pageload[2]);
                var Detailsvariant = JSON.parse(pageload[3]);
                

                $("#ddlvariant").empty().append('<option value="">Select an Option</option>');;
                $.each(Variantcode, function () {
                    $("#ddlvariant").append($("<option></option>").val(this["VARIANTCODE"]).html(this["VARIANTCODE"]));
                    $('#ddlvariant').trigger("chosen:updated");
                });

                $("#ddlmolditemcode").empty().append('<option value="">Select an Option</option>');;
                $.each(Molditemcode, function () {
                    $("#ddlmolditemcode").append($("<option></option>").val(this["MOLDITEMCODE"]).html(this["MOLDITEMCODE"]));
                    $('#ddlmolditemcode').trigger("chosen:updated");
                });

                $("#ddlvariantdetails").empty().append('<option value="">Select an Option</option>');;
                $.each(Detailsvariant, function () {
                    $("#ddlvariantdetails").append($("<option></option>").val(this["VARIANT"]).html(this["VARIANT"]));
                    $('#ddlvariantdetails').trigger("chosen:updated");
                });

                BindTab(BOMMASTERDTS, '1');

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
                    //{
                    //    className: "text-capitalize", 'data': 'Customer ID',
                    //    'render': function (data, type, full, meta) {

                    //        var pakarr = data.split('_');

                    //        return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                    //    }
                    //},
                    { className: "text-capitalize", 'data': 'Assembly ID' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'FG Item Name' },
                    { className: "text-capitalize", 'data': 'Variant Code' },
                    {

                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },
                    {
                        className: "text-capitalize", 'data': 'View',
                        'render': function (data, type, full, meta) {
                            //return '<button type="button" class="btn btn-success btn-fab searchdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="fa fa-search" aria-hidden="true"></i></button>'
                            return '<button type="button"  class="btn-az-secondary  searchdetails " id="print" data-toggle="tooltip" data-placement="top" title="View"><i class="icon ion-md-eye" aria-hidden="true"></i></button>'

                        }
                    },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'BOM Master',
                        text: '<img src="../../Images/excel.png" style="height: 25px;" title="Excel">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;"title="PDF">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'BOM Master'


                    }, {
                        extend: 'print',
                        title: 'BOM Master',
                        text: '<img src="../../Images/print.png" style="height: 25px;"title="Print">',
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
    $('#btnadd').click(function (event) {
        //if (!$('#validation-form').valid()) e.preventDefault(); {
        var errormessage = "";
        if ($('#ddlmolditemcode').val() == "") {
            errormessage += "Please  Select Mould Itemcode</br>";
        }
        
        if ($('#txtquantity').val() == "") {
            errormessage += "Please Enter Quantity</br>";
        }
        if ($('#ddlvariantdetails').val() == "") {
            errormessage += "Please  Select  Variant</br>";
        }
        if (errormessage.length == 0) {
            var action = $('#actiontypeadd').html();
            $(".loader").show("slow");
            if (window.FormData !== undefined) {
                var formData = new FormData();
                formData.append('MOLDITEMCODE', $('#ddlmolditemcode').val());
                formData.append('MOLDITEMNAME', $('#txtmolditemname').val());
                formData.append('VARIANT', $('#ddlvariantdetails').val());
                formData.append('QUANTITY', $('#txtquantity').val());
                formData.append('action', action);
                $.ajax({
                    url: 'AddBOMmasterdetails',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: formData,
                    success: function (result) {
                        var results = result.split('|');
                        var printload = JSON.parse(results[1]);
                        if (results[0] == 'true') {
                            BindchildPopupTab(printload, '1');
                            reset();
                        }
                        else {
                            bootbox.alert({
                                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + results[1] + '</span>',
                                size: 'small'
                            });
                            $('.loadingGIF').hide();
                        }
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });

            }
            $('#ddlmolditemcode').val('');
            $('#ddlmolditemcode').trigger('change')
            $('#ddlvariantdetails').val('').trigger('change');
           /* $('#ddlvariantdetails').trigger('change')*/
            $('#txtmolditemname').val('');
            $('#txtquantity').val('');

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
    function BindchildPopupTab(ResData, type) {

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




                    { className: "text-capitalize", title: 'Mold Item Code', 'data': 'Mold_Item_Code' },
                    { className: "text-capitalize", title: 'Mold Item Name', 'data': 'Mold_Item_Name' },
                    { className: "text-capitalize", title: 'Variant', 'data': 'Variant' },
                    { className: "text-capitalize", title: 'Quantity', 'data': 'Quantity' },
                    {
                        className: "text-capitalize", title:'Remove', 'data': 'Remove',
                        'render': function (data, type, full, meta) {
                            return '<button type="button" class="btn-danger remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="typcn typcn-archive"></i></button>'
                        }
                    },

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
    $("#ddlmolditemcode").change(function () {
        $.ajax({
            url: 'FetchItemNameBOMMaster',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'MOLDITEMCODE': $('#ddlmolditemcode').val().trim() }),
            success: function (data) {
                var Res = data;
                var plant = JSON.parse(Res);
                $('#txtmolditemname').val(plant[0].MOLDITEMNAME);

            }

        });

    });
    $("#txtfgitemcode").change(function () {
        $.ajax({
            url: 'FetchFGItemnameBOMMaster',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'FGITEMCODE': $('#txtfgitemcode').val().trim() }),
            success: function (data) {
                var Res = data;
                var plant = JSON.parse(Res);
                $('#txtfgitemname').val(plant[0].FGITEMNAME);

            }

        });

    });
    $("#dynamic-table1").on("click", ".remove", function () {
        var MOLDITEMCODE = $(this).closest('tr').find('td:eq(0)').html();

        $.ajax({
            url: 'DeleteBOMmasterdts',
            dataType: "json",
            type: "POST",
            data: $.param({ 'MOLDITEMCODE': MOLDITEMCODE }),

            success: function (result) {

                BindchildPopupTab(JSON.parse(result), '1');

            }
        });

    });
    $('#btnsubmit').click(function () {
     
        var formData = new FormData();

        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('FGITEMCODE', $('#txtfgitemcode').val());
            formData.append('FGITEMNAME', $('#txtfgitemname').val());
            formData.append('ASSEMBLYID', $('#txtassemblyid').val());
            formData.append('VARIANT', $('#ddlvariant').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertBOMMMaster',
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

                     
                        $("#txtfgitemcode").val("");
                        $("#txtfgitemname").val("");
                        $("#txtassemblyid").val("");
                        $('#ddlvariant').val("")
                        $('#ddlvariant').trigger('change');
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


    });

    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);


    $("#dynamic-table").on("click", ".editdetails", function () {
        //  var autoid = $(this).closest('tr').find('td:eq(0) > div > span').html();
        var FGITEMCODE = $(this).closest('tr').find('td:eq(0)').html();
        EditBommasterDetails(FGITEMCODE)
    });


    function EditBommasterDetails(FGITEMCODE) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditBOMMaster',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'FGITEMCODE': FGITEMCODE }),

            success: function (result) {
                var Tables = result.split("|");
                var Header = JSON.parse(Tables[0]);
                var Details = JSON.parse(Tables[1]);

                $("#hdautoid").val(Header[0].AUTOID);
                $("#txtfgitemcode").val(Header[0].FGITEMCODE);
                $("#txtfgitemname").val(Header[0].FGITEMNAME);
                $("#txtassemblyid").val(Header[0].ASSEMBLYID);
                $('#ddlvariant').val(Header[0].VARIANT).trigger('change');
                $('#actiontype').html("Update");
                BindchildPopupTab(Details, '1');


            }

        });

        $(".loader").hide("slow");
    }
    function BindchildPopupTab(ResData, type) {

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



                    { className: "text-capitalize",title:'Mold Item Code', 'data': 'Mold_Item_Code' },
                    { className: "text-capitalize", title: 'Mold Item Name', 'data': 'Mold_Item_Name' },
                    { className: "text-capitalize", title: 'Variant', 'data': 'Variant' },
                    { className: "text-capitalize", title:'Quantity', 'data': 'Quantity' },

                    {
                        className: "text-capitalize", title:'Remove', 'data': 'Remove',
                        'render': function (data, type, full, meta) {
                            return '<button type="button" class="btn-danger remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="typcn typcn-archive"></i></button>'
                        }
                    },

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
    $('#btnitemclear').click(function () {
        reset();
    });
    function reset() {
        $('#ddlmolditemcode').val("").trigger('change');
        $('#txtmolditemname').val("");
        $('#txtquantity').val("");
        //$('#txttotalparts').val("");

    }
    $("#dynamic-table").on("click", ".searchdetails", function () {
        var $text = $(this).closest('tr').find('td:eq(0)').html();
        // var $text1 = $(this).closest('tr').find('td:eq(1)').text();
        $('#modalview').modal('show');
        Bomviewdts($text);
    });
    function Bomviewdts($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'ViewBOMMaster',
            dataType: 'json',
            type: 'POST',
            data: { "FGITEMCODE": $text },

            success: function (data) {
                //debugger;
                var Tables = data;
                var BOMDts = JSON.parse(Tables);
                //debugger;
                BindchildTab(BOMDts, '1');
                //   $("html, body").animate({ scrollTop: 0 }, "slow");

            }
        });
        $(".loader").hide("slow");
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



                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Name' },
                    { className: "text-capitalize", 'data': 'Variant' },
                    { className: "text-capitalize", 'data': 'Quantity' },


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
});