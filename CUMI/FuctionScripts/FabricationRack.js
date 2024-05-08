jQuery(function ($) {



    ////-------------------------------------------------------------------------------------------Bind Pageload 
    var TotalParts1 = "";
    var Aframe1 = "";
    var Arm1 = "";
    var stopper1 = "";
    var ponumbert = "";
    var channel1 = "";
    getmonth();
    function getmonth() {
        var today = new Date();

        var dd = today.getDate() > 9 ? today.getDate() : '0' + today.getDate();
        var mm = today.getMonth(); //January is 0!
        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        var yyyy = today.getFullYear();

        var today = dd + '/' + months[mm] + '/' + yyyy;
        // $("#txtorderdate").val(today);
        $("#txtdate").val(today);
        $("txtdatetimepicker").val(today);
    }
    var autoid;
    BindTable();
    var ID = "";
    var Rfid = "";
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
            url: 'FabricationRackPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var FabricationNo = JSON.parse(pageload[0]);
                var rackColor = JSON.parse(pageload[1]);
                RackType = JSON.parse(pageload[2]);
                var Plant = JSON.parse(pageload[3]);
                var Details = JSON.parse(pageload[4]);
                var Supplier = JSON.parse(pageload[5]);

                $("#txtfabricationno").val(FabricationNo[0].FABRICATIONNO);

                $("#ddlrackcolor").empty().append('<option value="">Select an Option</option>');
                $.each(rackColor, function () {
                    $("#ddlrackcolor").append($("<option></option>").val(this["RACKCOLOR"]).html(this["RACKCOLOR"]));
                    $('#ddlrackcolor').trigger("chosen:updated");
                });
                $("#ddlracktype").empty().append('<option value="">Select an Option</option>');
                $.each(RackType, function () {
                    $("#ddlracktype").append($("<option></option>").val(this["RACKTYPE"]).html(this["RACKTYPE"]));
                    $('#ddlracktype').trigger("chosen:updated");
                });
                $("#txtsupplier").empty().append('<option value="">Select an Option</option>');
                $.each(Supplier, function () {
                    $("#txtsupplier").append($("<option></option>").val(this["SUPPLIERNAME"]).html(this["SUPPLIERNAME"]));
                    $('#txtsupplier').trigger("chosen:updated");
                });
                //$("#ddlplant").empty().append('<option value="">Select an Option</option>');
                //$.each(Plant, function () {
                //    $("#ddlplant").append($("<option></option>").val(this["PLANTNAME"]).html(this["PLANTNAME"]));
                //    $('#ddlplant').trigger("chosen:updated");
                //});
                $('#ddlplant').val(Plant[0].PLANTNAME);
                BindTab(Details, '1');

            }
        });
        $(".loader").hide("slow");
    }
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
            $('#dynamic-table').DataTable().destroy(); $('#dynamic-table').html('');
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
                "ascrollX": "100%",
                "bPaginate": true,
                "sPaginationType": "full_numbers",
                "info": false,
                "bFilter": true,
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [
                    { "width": "1%", "targets": 5 }, {
                    'targets': 0,
                    //'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: [


                    //{ 'data': 'Sl No' },
                    //{
                    //    'data': 'Event Id',
                    //    'render': function (data, type, full, meta) {

                    //        var pakarr = data.split('_');

                    //        return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                    //    }
                    //},
                    { className: "text-capitalize", 'data': 'Rev Date' },
                    {
                        'data': 'Po Number',
                        'render': function (data, type, full, meta) {

                            var pakarr = data.split('_');
                            //ponumbert = pakarr[1];
                            return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + '/' + +pakarr[0] + '</span></div>';

                        }
                    },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    { className: "text-capitalize", 'data': 'Plant' },
                    { className: "text-capitalize", 'data': 'Rev By' },
                    { className: "text-capitalize", 'data': 'Rack Type' },
                    { className: "text-capitalize", 'data': 'Rack Color' },
                    { className: "text-capitalize", 'data': 'A Frame' },
                    { className: "text-capitalize", 'data': 'Arm' },
                    { className: "text-capitalize", 'data': 'Stopper' },
                    { className: "text-capitalize", 'data': 'Channel' },
                    { className: "text-capitalize", 'data': 'Total Parts' },
                    { className: "text-capitalize", 'data': 'Scrap Out Date' },
                    { className: "text-capitalize", 'data': 'Scrap Out Qty' },
                    { className: "text-capitalize", 'data': 'Scarp Remarks' },
                    {
                        className: "text-capitalize",'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button"  class="btn-az-secondary  editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },
                    {
                        className: "text-capitalize", 'data': 'Scrap Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button"  class="btn-az-secondary  searchdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Rack Fabrication',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Rack Fabrication'


                    }, {
                        extend: 'print',
                        title: 'Rack Fabrication',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ]
            });

            $("#dynamic-table").on("click", ".searchdetails", function () {

                var $text = $(this).closest('tr').find('td:eq(1)').text();
                //var $text = $(this).closest('tr').find('td:eq(1) > div > span').html();
                var auto = $text.split('/');
                var test = auto[0];
                var tests = auto[1];
                $('#modalview').modal('show');
                callpopup(tests);
            });
            $("#dynamic-table").on("click", ".editdetails", function () {
                var Ponumbet = $(this).closest('tr').find('td:eq(1)').text();
                //var $text = $(this).closest('tr').find('td:eq(1) > div > span').html();
                var tess = Ponumbet.split('/');
                var tes = tess[0];
                var teses = tess[1];
                GetCategoryDetailsByID(tes)
            });


        }
        else {
            $('#dynamic-table tbody').remove();
            $('#dynamic-table thead').remove();
            $('#dynamic-table').dataTable({
                "language": {
                    "emptyTable": "Fabrication No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
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
    function Clear() {
        location.reload();
    };


    $('#btnclear').click(Clear);

    $("#txtaframe").change(function () {
        Aframe1 = $('#txtaframe').val();
        TotalParts1 = parseInt(Aframe1) + parseInt(Arm1) + parseInt(stopper1) + parseInt(Channel1);
        $('#txttotalparts').val(TotalParts1);
    });
    $("#txtarm").change(function () {
        Arm1 = $('#txtarm').val();
        TotalParts1 = parseInt(Aframe1) + parseInt(Arm1) + parseInt(stopper1) + parseInt(Channel1);
        $('#txttotalparts').val(TotalParts1);
    });
    $("#txtstopper").change(function () {

        stopper1 = $('#txtstopper').val();
        TotalParts1 = parseInt(Aframe1) + parseInt(Arm1) + parseInt(stopper1) + parseInt(Channel1);
        $('#txttotalparts').val(TotalParts1);
    });
    $("#txtchannel").change(function () {

        Channel1 = $('#txtchannel').val();
        TotalParts1 = parseInt(Aframe1) + parseInt(Arm1) + parseInt(stopper1) + parseInt(Channel1);
        $('#txttotalparts').val(TotalParts1);
    });
    $('#btnadd').click(function () {
        $(".loader").show("slow");
        var errormessage = "";
        if ($('#txtponumber').val() == "") {
            errormessage += "Can't be left Empty Po Number.</br>";
        }
        if ($('#txtsupplier').val() == "") {
            errormessage += "Please Select Supplier.</br>";
        }
        if ($('#txtreceivedby').val() == "") {
            errormessage += "Can't be left Empty Received By.</br>";
        }
        if ($('#ddlrackcolor').val() == "") {
            errormessage += "Please Select Rack Color.</br>";
        }

        if ($('#ddlracktype').val()=="") {
            errormessage += "Please Select Rack Type.</br>";
        }
        if ($('#txtnoofrack').val() == "") {
            errormessage += "Can't be left Empty No of Rack.</br>";
        }
        if ($('#txtaframe').val() == "") {
            errormessage += "Can't be left Empty AFrame.</br>";
        }
        if ($('#txtarm').val() == "") {
            errormessage += "Can't be left Empty Arm.</br>";
        }
        if ($('#txtstopper').val() == "") {
            errormessage += "Can't be left Empty Stopper.</br>";
        }
        if ($('#txtchannel').val() == "") {
            errormessage += "Can't be left Empty Channel.</br>";
        }
        if (errormessage.length == 0) {
            var action = $('#actiontypeaddd').html();
            if (window.FormData !== undefined) {
                var formData = new FormData();
                formData.append('PONUMBER', $('#txtponumber').val());
                formData.append('SUPPLIER', $('#txtsupplier').val());
                formData.append('RECEIVEDBY', $('#txtreceivedby').val());
                formData.append('RACKCOLOR', $('#ddlrackcolor').val());
                formData.append('RACKTYPE', $('#ddlracktype').val());
                formData.append('NOOFRACK', $('#txtnoofrack').val());
                formData.append('AFRAME', $('#txtaframe').val());
                formData.append('ARM', $('#txtarm').val());
                formData.append('STOPPER', $('#txtstopper').val());
                formData.append('CHANNEL', $('#txtchannel').val());
                formData.append('TOTALPARTS', $('#txttotalparts').val());
                formData.append('action', action);
                $.ajax({
                    url: 'AddFabricationRack',
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

                                message: '<span class="text-danger"><i class="iicon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + results[1] + '</span>',
                                size: 'small',
 
                            });

                            $('.loadingGIF').hide();
                        }
                    },
                    error: function (err) {

                    }
                });


            }
        }
        else {
            bootbox.alert({

                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small',

            });
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
                "info": false,
                "bFilter": true,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [
                    {
                        "width": "1%",
                        "targets": 5
                    }, {
                    'targets': 0,
                    'searchable': false,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],


                columnDefs: cols1,
                columns: [

                    { className: "text-capitalize",'data': 'PoNumber' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    { className: "text-capitalize", 'data': 'ReceivedBy' },
                    { className: "text-capitalize",'data': 'RackColor' },
                    { className: "text-capitalize",'data': 'RackType' },
                    { className: "text-capitalize",'data': 'NoofRack' },
                    { className: "text-capitalize",'data': 'Arm' },
                    { className: "text-capitalize",'data': 'AFrame' },
                    { className: "text-capitalize",'data': 'Stopper' },
                    { className: "text-capitalize",'data': 'Channel' },
                    { className: "text-capitalize",'data': 'TotalParts' },
                    {
                        className: "text-capitalize",'data': 'Remove',
                        'render': function (data, type, full, meta) {
                            return '<button type="button" class="btn-danger remove" data-toggle="tooltip" data-placement="top" title="Remove"><i class="typcn typcn-archive"></i></button>'
                        }
                    },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {
                            return '<button type="button"  class="btn-az-secondary  edit" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'
                        }
                    },
                ],
            });
            $("#dynamic-tableChild").on("click", ".edit", function () {
                var $row = $(this).closest("tr");    // Find the row
                var $text = $row.find(".sorting_1").text(); // Find the text         
                var Ponumbet = $(this).closest('tr').find('td:eq(0)').html();
                var Supplier = $(this).closest('tr').find('td:eq(1)').html();
                var Receivedby = $(this).closest('tr').find('td:eq(2)').html();
                var RackColor = $(this).closest('tr').find('td:eq(3)').html();
                var RAckType = $(this).closest('tr').find('td:eq(4)').html();
                var Noofrack = $(this).closest('tr').find('td:eq(5)').html();
                var AFrame = $(this).closest('tr').find('td:eq(6)').html();
                var Arm = $(this).closest('tr').find('td:eq(7)').html();
                var Stopper = $(this).closest('tr').find('td:eq(8)').html();
                var Channel = $(this).closest('tr').find('td:eq(9)').html();
                var Totalparts = $(this).closest('tr').find('td:eq(10)').html();

                $('#ddlrackcolor').val(RackColor).trigger('change');
                $('#ddlracktype').val(RAckType).trigger('change');
                $('#txtsupplier').val(Supplier).trigger('change');
                $('#txtponumber').val(Ponumbet);
                $('#txtreceivedby').val(Receivedby);
                $('#txtnoofrack').val(Noofrack);
                $('#txtaframe').val(AFrame);
                $('#txtarm').val(Arm);
                $('#txtstopper').val(Stopper);
                $('#txtchannel').val(Channel);
                $('#txttotalparts').val(Totalparts);
                $('#actiontypeaddd').html("Update");

            });

        }
        else {
            $('#dynamic-tableChild tbody').remove();
            $('#dynamic-tableChild thead').remove();
            $('#dynamic-tableChild').dataTable({
                "language": {
                    "emptyTable": " Fabrication Details No Records Found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false }

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": false,
                "searching": false
            });

        }


    }
    $("#dynamic-tableChild").on("click", ".remove", function () {
        var PoNumber = $(this).closest('tr').find('td:eq(0)').html();
        var RackColor = $(this).closest('tr').find('td:eq(1)').html();
        var RackType = $(this).closest('tr').find('td:eq(2)').html();
        //var SerialNo = $(this).closest('tr').find('td:eq(3)').html();

        $.ajax({
            url: 'DeleteFabricationRackRow',
            dataType: "json",
            type: "POST",
            data: $.param({ 'PoNumber': PoNumber }) + '&' + $.param({ 'RackColor': RackColor }) + '&' + $.param({ 'RackType': RackType }),

            success: function (result) {

                BindChildTab(JSON.parse(result), '1');

            }
        });

    });
    $('#btnitemclear').click(function () {
        reset();
    });
    function reset() {


        $('#ddlrackcolor').val("").trigger('change');
        $('#ddlracktype').val("").trigger('change');
        $('#txtsupplier').val("").trigger('change');
        $('#txtponumber').val("");
        $('#txtreceivedby').val("");
        $('#txtnoofrack').val("");
        $('#txtaframe').val("");
        $('#txtarm').val("");
        $('#txtstopper').val("");
        $('#txtchannel').val("");
        $('#txttotalparts').val("");

    }

    $('#Fabrication-form').parsley().on('field:validated', function () {
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
            formData.append('FABRICATIONNO', $('#txtfabricationno').val());
            formData.append('DATE', $('#txtdate').val());
            formData.append('PLANT', $('#ddlplant').val());
            formData.append('REMARKS', $('#txtremarks').val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertFabricationRack',
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

                            message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-100  tx-success lh-1 mg-t-20 d-inline-block"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                            size: 'small',
                            callback: function () {
                                Clear();
                            }
                        });

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

    function GetCategoryDetailsByID(tes) {
        $(".loader").show("slow");
        $.ajax({
            url: 'editFabricationRack',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'PONUMBER': tes }),

            success: function (result) {
                var Tables = result.split('|');
                var Fabricheader = JSON.parse(Tables[0]);
                var adddetails = JSON.parse(Tables[1]);
                $('#txtfabricationno').val(Fabricheader[0].FABRICATIONNO);
                $('#txtdate').val(Fabricheader[0].DATE);
                $('#ddlplant').val(Fabricheader[0].PLANT).trigger('change');
                $('#txtremarks').val(Fabricheader[0].REMARKS);
                $('#txtfabricationno').prop('disabled', true);
                $('#actiontype').html("Update");
                BindChildTab(adddetails, '1');
            }

        });

        $(".loader").hide("slow");
    }

    function callpopup(tests) {
        $(".loader").show("slow");
        $.ajax({
            url: 'FabricationRackViewDts',
            dataType: 'json',
            type: 'POST',
            data: { "AUTOID": tests },

            success: function (data) {

                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdPOautoid").val(tests);
                $("#txtscrapout").val(resJ[0].SCRAPOUT);
                $("#txtscrapoutremark").val(resJ[0].SCRAPOUTREMARK);

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
        var len = $('#dynamic-tableDetails1 tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetails1').dataTable().fnDestroy();
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

            $('#dynamic-tableDetails1').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bFilter": false,
                "lengthChange": false,
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'columnDefs': [{
                    'targets': 0,
                    //'searchable': false,
                    //'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA

            });
        }
        else {
            $('#dynamic-tableDetails1 tbody').remove();
            $('#dynamic-tableDetails1 thead').remove();
            $('#dynamic-tableDetails1').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                //'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
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
                //"info": false,
                //"paging": false,
                //searching": false
            });


        }

    }


    function Clear() {
        location.reload();
    };
    function BindTab1(ResData, type) {

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
        //if (type == '0') {
        //    $('#dynamic-table').dataTable().fnDestroy();
        //    cols.length = 0;
        //    cols1.length = 0;
        //}

        var rcount = $("#dynamic-table1 > tbody > tr").length

        // TABLE BIND     
        //if (type == '0') {
        //    $('#dynamic-table').dataTable().fnDestroy();
        //    cols.length = 0;
        //    cols1.length = 0;
        //}

        if (rcount > 0) {
            $('#dynamic-table1').dataTable().fnDestroy();
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

            var dyTable = $('#dynamic-table1');

            $('#dynamic-table1').dataTable({
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
                    'searchable': true,
                    'orderable': true,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: [
                    { className: "text-capitalize", 'data': 'Received Date' },
                    { className: "text-capitalize", 'data': 'Po Number' },
                    { className: "text-capitalize", 'data': 'Supplier' },
                    { className: "text-capitalize", 'data': 'Plant' },
                    { className: "text-capitalize", 'data': 'Received By' },
                    { className: "text-capitalize", 'data': 'Rack Type' },
                    { className: "text-capitalize", 'data': 'Rack Color' },
                    { className: "text-capitalize", 'data': 'A Frame' },
                    { className: "text-capitalize", 'data': 'Arm' },
                    { className: "text-capitalize", 'data': 'Stopper' },
                    { className: "text-capitalize", 'data': 'Channel' },
                    { className: "text-capitalize", 'data': 'Total Parts' },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Stock Report',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Stock Report'


                    }, {
                        extend: 'print',
                        title: 'Stock Report',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ],
                // }
            });


        }
        else {
            $('#dynamic-table1 tbody').remove();
            $('#dynamic-table1 thead').remove();
            $('#dynamic-table1').dataTable({
                "language": {
                    "emptyTable": "Fabrication Rack Book Stock Report No records found.."
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
    $('#btnupdate').click(function () {

        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('SCRAPOUT', $('#txtscrapout').val());
            formData.append('SCRAPOUTREMARK', $('#txtscrapoutremark').val());
            formData.append('AUTOID', $('#hdPOautoid').val());
            formData.append('actiontypescrap', $('#actiontypescrap').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'UpdateFabricationRackEdit',
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

                        $('#txtscrapout').val("");
                        $('#txtscrapoutremark').val("")
                        $('#modalview').modal('hide');
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
});