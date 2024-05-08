jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();

    var click = 0;
    getmonth();
    function getmonth() {
        var today = new Date();

        var dd = today.getDate() > 9 ? today.getDate() : '0' + today.getDate();
        var mm = today.getMonth(); //January is 0!
        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        var yyyy = today.getFullYear();

        var today = dd + '/' + months[mm] + '/' + yyyy;

        //  var d = new Date("01");
        var d = "01" + '/' + months[mm] + '/' + yyyy;
        $("#txtgrndate").val(today);
        //$("#txttodate").val(today);

    }
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
            url: 'GRNEntryPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var Itemcode = JSON.parse(pageload[0]);
                var GRNdts = JSON.parse(pageload[1]);
               



                $("#ddlrmitemcode").empty().append('<option value="">Select an Option</option>');
                $.each(Itemcode, function () {
                    $("#ddlrmitemcode").append($("<option></option>").val(this["RMITEMCODE"]).html(this["RMITEMCODE"]));
                    $('#ddlrmitemcode').trigger("chosen:updated");
                });

                
                BindTab(GRNdts, '1');
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

                    { className: "text-capitalize", 'data': 'GRN No' },
                    { className: "text-capitalize", 'data': 'GRN Date' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    { className: "text-capitalize", 'data': 'Po No' },
                    { className: "text-capitalize", 'data': 'Invoice No' },
                    {

                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },
                    {

                        className: "text-capitalize", 'data': 'View',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn btn-success btn-fab searchdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="icon ion-md-eye" aria-hidden="true"></i></button>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'GRN Entry',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'GRN Entry'


                    }, {
                        extend: 'print',
                        title: 'GRN Entry',
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
                    "emptyTable": "GRN Entry No records found.."
                },
                'bSort': false,
                'aoColumns': [
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
    $('#ddlrmitemcode').change(function () {
        
        var optionselected = $(this);
        var OValue = $('#ddlrmitemcode').val();
        $(".loader").show("slow");
        $.ajax({

            url: 'GRNEntryItemDetailsFetch',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: { 'ItemCode': OValue },
            success: function (data) {

                var Res = data;
                var GRNHEADER = JSON.parse(Res);
               
                $('#txtrmitemname').val(GRNHEADER[0].DESCRIPTION);
                $('#txtvariant').val(GRNHEADER[0].VARIANTCODE);
                $('#txtuom').val(GRNHEADER[0].UOM);

            }
        });
        $(".loader").hide("slow");
       
    });

    $('#btnadd').click(function (event) {
        $(".loader").show("slow");
        var errormessage = "";
        if ($('#ddlrmitemcode').val() == "") {
            errormessage += "Please Select RM Item Code.</br>";
        }
      
        if ($('#txtquantity').val() == "") {
            errormessage += "Please Enter Quantity.</br>";
        }

        if (errormessage.length == 0) {
            var action = $('#actiontypeaddd').html();
            if (window.FormData !== undefined) {
                var formData = new FormData();
                //formData.append('PONO', $('#PONO').val());
                //formData.append('Supplier', $('#ddlsupplier').val());
                formData.append('RMITEMCODE', $('#ddlrmitemcode').val());
                formData.append('RMITEMNAME', $('#txtrmitemname').val());
                formData.append('VARIANT', $('#txtvariant').val());
                formData.append('UOM', $('#txtuom').val());
                formData.append('QUANTITY', $('#txtquantity').val());
                formData.append('action', action);
                $.ajax({
                    url: 'AddGRNEntryDetails',
                    type: "POST",
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    data: formData,
                    success: function (result) {
                        var results = result.split('|');
                        if (results[0] == 'true') {
                            if ($('#actiontypeaddd').html() == "Update")
                                $('#actiontypeaddd').html("Add");

                            BindChildTab(JSON.parse(results[1]), '1');
                            reset();
                        }
                        else {
                            bootbox.alert({
                                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + results[1] + '</span>',
                                size: 'small'
                            });
                        }
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });


                // $(".loader").hide("slow");
            }
        } else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small'
            });

            $('#txtquantity').val("");
           
        }

    });
    function BindChildTab(ResData, type) {
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = ResData[0];
        var rcount = $("#dynamic-tableChild > tbody > tr").length
        // TABLE BIND     
        if (rcount > 0) {
            $('#dynamic-tableChild').dataTable().fnDestroy();
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

            $('#dynamic-tableChild').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bPaginate": false,
                "bFilter": false,
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

                    { 'data': 'RMItemCode' },
                    { 'data': 'RMItemName' },
                    { 'data': 'Variant' },
                    { 'data': 'UOM' },
                    { 'data': 'Quantity' },
                    
                    {
                        className: "text-capitalize", 'data': 'Remove',
                        'render': function (data, type, full, meta) {
                            return '<button type="button" class="btn-danger remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="typcn typcn-archive"></i></button>'
                        }
                    },


                ],


            });


           
        }
        else {
            $('#dynamic-tableChild tbody').remove();
            $('#dynamic-tableChild thead').remove();
            $('#dynamic-tableChild').dataTable({
                "language": {
                    "emptyTable": "GRN Entry Details No Records Found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false }
                ],
                "scrollCollapse": false,
                "info": false,
                "paging": false,
                "searching": true
            });

        }


    }
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);
    $('#btnitemclear').click(function () {
        reset();
    });

    function reset() {
      
        $('#ddlrmitemcode').val("");
        $('#ddlrmitemcode').trigger("change");
        $('#txtrmitemname').val("");
        $('#txtvariant').val("");
        $('#txtuom').val("");
        $('#txtquantity').val("");
       

    }

    $("#dynamic-tableChild").on("click", ".remove", function () {
        var Code = $(this).closest('tr').find('td:eq(0)').html();
        var Qty = $(this).closest('tr').find('td:eq(4)').html();
        var Uom = $(this).closest('tr').find('td:eq(3)').html();

        $.ajax({
            url: 'DeleteGRNentryRow',
            dataType: "json",
            type: "POST",
            data: $.param({ 'Code': Code }) + '&' + $.param({ 'Qty': Qty })
                + '&' + $.param({ 'Uom': Uom }),

            success: function (result) {

                BindChildTab(JSON.parse(result), '1');


            }
        });

    });
    $('#btnsubmit').click(function () {

       
        var errormessage = "";

        if ($('#txtgrnno').val() == "") {
            errormessage += "Please Enter GRN No.</br>";
        }
        if ($('#txtgrndate').val() == "") {
            errormessage += "Please Select GRN Date.</br>";
        }
        if ($('#txtsupplier').val() == "") {
            errormessage += "Please Enter Supplier</br>";
        }
        if ($('#txtpono').val() == "") {
            errormessage += "Please Enter PO No.</br>";
        }
        if ($('#txtinvoiceno').val() == "") {
            errormessage += "Please Enter Invoice No.</br>";
        }
        if (errormessage.length == 0) {
            if ($('#dynamic-tableChild >tbody >tr').length > 0) {


                if (window.FormData !== undefined) {
                    $('.loadingGIF').show()
                    var formData = new FormData();
                    formData.append('GRNNO', $('#txtgrnno').val());
                    formData.append('GRNDATE', $('#txtgrndate').val());
                    formData.append('SUPPLIER', $('#txtsupplier').val());
                    formData.append('PONO', $('#txtpono').val());
                    formData.append('INVOICENO', $('#txtinvoiceno').val());
                    formData.append('actiontype', $('#actiontype').text());
                    $(".loader").show("slow");
                    $.ajax({
                        url: 'GRNEntryInsert',
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

            }
            else {
                
                bootbox.alert({
                    /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                    message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please add atleast one  data to Proceed." + '</span>',
                    size: 'small'
                });


                $('.loadingGIF').hide();
            }
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

    $("#dynamic-table").on("click", ".editdetails", function () {

        var GRNNO = $(this).closest('tr').find('td:eq(0)').html();
        GRNEntryEditdts(GRNNO)
    });
    // });
    //-------------------------------------------------------------------------------------------Assign values table into TextBox (Controls)   
    //get the Role Creation Details by ID
    function GRNEntryEditdts(GRNNO) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GRNEntryEdit',
            dataType: 'json',
            type: 'POST',
            data: { "GRNNO": GRNNO },

            success: function (data) {
                var Res = data.split('|');
                var GRNHEADER = JSON.parse(Res[0]);
                var GRNDETAILS = JSON.parse(Res[1]);
               
                $("#txtgrnno").val(GRNHEADER[0].GRNNO);
                $("#txtgrndate").val(GRNHEADER[0].GRNDATE);
                $("#txtsupplier").val(GRNHEADER[0].SUPPLIER);
                $("#txtpono").val(GRNHEADER[0].PONO);
                $("#txtinvoiceno").val(GRNHEADER[0].INVOICENO);
                $("#actiontype").text("Update");

                BindChildTab(GRNDETAILS, '1');

            }

        });

        $(".loader").hide("slow");
    }
    $("#dynamic-table").on("click", ".searchdetails", function () {
        var $text = $(this).closest('tr').find('td:eq(0)').html();
        $('#modalview').modal('show');
        callpopup($text);
    });

    function callpopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GRNEntryView',
            dataType: 'json',
            type: 'POST',
            data: { "GRNNO": $text },

            success: function (data) {
                debugger;
                var Tables = data;
                var GRNItemDetails = JSON.parse(Tables);
                debugger;
                BindPopupTab(GRNItemDetails, '1');
                $("html, body").animate({ scrollTop: 0 }, "slow");

            }
        });
        $(".loader").hide("slow");
    }
    function BindPopupTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetails tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetails').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetails').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
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
                columns: [

                    { className: "text-capitalize", 'data': 'GRN No' },
                    { className: "text-capitalize", 'data': 'Item Code' },
                    { className: "text-capitalize", 'data': 'Item Name' },
                    { className: "text-capitalize", 'data': 'Variant' },
                    { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Quantity' },

                ],


            });
        }
        else {
            $('#dynamic-tableDetails tbody').remove();
            $('#dynamic-tableDetails thead').remove();
            $('#dynamic-tableDetails').dataTable({
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
                "searching": false
            });


        }

    }
});