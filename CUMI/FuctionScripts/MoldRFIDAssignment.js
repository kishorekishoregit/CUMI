jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();
    getmonth();
    function getmonth() {
        var today = new Date();

        var dd = today.getDate() > 9 ? today.getDate() : '0' + today.getDate();
        var mm = today.getMonth(); //January is 0!
        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        var yyyy = today.getFullYear();

        var today = dd + '/' + months[mm] + '/' + yyyy;
        // $("#txtorderdate").val(today);
        $("#txtpreviousmrdate").val(today);
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
            url: 'GetMoldRFIDAssignmentPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var recordstatus = JSON.parse(pageload[0]);
                var uom = JSON.parse(pageload[1]);
                var Plant = JSON.parse(pageload[2]);
                var mold = JSON.parse(pageload[3]);
                var resJ = JSON.parse(pageload[4]);

               // $("#ddlstatus").empty().append('<option value="">Select an Option</option>');
                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METADATADESCRIPTION"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                $("#ddluom").empty().append('<option value="">Select an Option</option>');
                $.each(uom, function () {
                    $("#ddluom").append($("<option></option>").val(this["METADATADESCRIPTION"]).html(this["METADATADESCRIPTION"]));
                    $('#ddluom').trigger("chosen:updated");
                });

                $("#ddlplant").empty().append('<option value="">Select an Option</option>');
                $.each(Plant, function () {
                    $("#ddlplantlst").append($("<option></option>").val(this["PLANT"]).html(this["PLANTNAME"]));
                    $('#ddlplantlst').trigger("chosen:updated");
                });

                $("#ddlmold").empty().append('<option value="">Select an Option</option>');
                $.each(mold, function () {
                    $("#ddlmoldlst").append($("<option></option>").val(this["MOLDITEMCODE"]).html(this["DESCRIPTION"]));
                    $('#ddlmoldlst').trigger("chosen:updated");
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
                "info": false,
                "ascrollX": "100%",
                "language": { "Search": "Search:" },
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
                    {
                        className: "text-capitalize", 'data': 'Plant',
                        //'render': function (data, type, full, meta) {

                        //    var pakarr = data.split('_');

                        //    return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                        //}
                    },

                    { className: "text-capitalize", 'data': 'Location' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Varient Code' },
                    { className: "text-capitalize", 'data': 'Mould Open Count' },
                    { className: "text-capitalize", 'data': 'Mould Press Count' },
                    { className: "text-capitalize", 'data': 'MR Alert Count' },
                    { className: "text-capitalize", 'data': 'Mould Life Count' },
                    { className: "text-capitalize", 'data': 'RFID Number' },
                    { className: "text-capitalize", 'data': 'Record Status' },
                    { className: "text-capitalize", 'data': 'Remarks' },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {
                            var pakarr = data;
                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="typcn typcn-edit" aria-hidden="true"></i></button></div><span style = "visibility:hidden; font-size: 1px;">' + pakarr + '</span>'
                            //return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },
                    //{
                    //    className: "text-capitalize", 'data': 'View',
                    //    'render': function (data, type, full, meta) {
                    //        //return '<button type="button" class="btn btn-success btn-fab searchdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="fa fa-search" aria-hidden="true"></i></button>'
                    //        return '<button type="button"  class="btn-az-secondary  searchdetails " id="print" data-toggle="tooltip" data-placement="top" title="View"><i class="icon ion-md-eye" aria-hidden="true"></i></button>'

                    //    }
                    //},

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Mold RFID Assignment',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF"style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mold RFID Assignment'


                    }, {
                        extend: 'print',
                        title: 'Mold RFID Assignment',
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
                    "emptyTable": "Mold RFID Assignment No records found.."
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
                  //  { sWidth: "10%", bSearchable: false, bSortable: false },
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
            url: 'GetMoldRFIDAssignmentpartcode',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'partcode': $('#ddlplant').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var plant = JSON.parse(Res[0]);
                $('#txtlocation').val(plant[0].Location);
            }
        });

    });

    $("#ddlmold").change(function () {
        $.ajax({
            url: 'GetVarientcodefetch',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'Moldcode': $('#ddlmold').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var plant = JSON.parse(Res[0]);
                $('#txtvarientcode').val(plant[0].VARIANTCODE);
                $('#txtmoulditemdescription').val(plant[0].DESCRIPTION);
                $('#txtuom').val(plant[0].UOM);
            }
        });

    });

    $('#Employee-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);

    })
        .on('form:submit', function () {
            save();
            return false;
        });
    function save() {
        var location = $('#txtlocation').val();
        var split = location.split('-');
        var dates3 = split[0]
        var formData = new FormData();
        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('PLANTCODE', $('#ddlplant').val());
            formData.append('LOCATION', dates3);
            formData.append('MOLDITEMCODE', $('#ddlmold').val());
            formData.append('VARIENTCODE', $('#txtvarientcode').val());
            formData.append('PONUMBER', $('#txtponumber').val());
            formData.append('BATCHNUMBER', $('#txtbatchnumber').val());
            formData.append('MOLDOPENCOUNT', $('#txtmoldopencount').val());
            formData.append('PREVIOUSMRCOUNT', $('#txtpreviousmrcount').val());
            formData.append('PREVIUOSMRDATE', $('#txtpreviousmrdate').val());
            formData.append('MRCOUNT', $('#txtmrcount').val());
            formData.append('MRALERTCOUNT', $('#txtmralertcount').val());
            formData.append('MOLDLIFECOUNT', $('#txtmoldlifecount').val());
            formData.append('RFIDNUMBER', $('#txtrfidnumber').val());
            formData.append('RECORDSTATUS', $('#ddlstatus').val());
            formData.append('REMARKS', $('#txtremarks').val());
            formData.append('SUPPLIERID', $('#txtsupplierid').val());
            formData.append('MOLDITEMNAME', $('#txtmoulditemdescription').val());
            formData.append('UOM', $('#txtuom').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertMoldRFIDAssignment',
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
                        $('#txtplantcode').val("");
                        $("#txtlocation").val("");
                        $('#ddluom').val("")
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
        //var autoid = $(this).closest('tr').find('td:eq(7) > div > span').html();
        var autoid = $(this).closest('tr').find('td:eq(11) > span').html();
        //var MOLDITEMCODE = $(this).closest('tr').find('td:eq(3)').html();
        GetinterlinkpartDetailsByID(autoid)
    });

    $("#dynamic-table").on("click", ".searchdetails", function () {
        var $text = $(this).closest('tr').find('td:eq(3)').html();
        // var $text1 = $(this).closest('tr').find('td:eq(1)').text();
        $('#modalview').modal('show');
        callpropertypopup($text);
    });

    function callpropertypopup($text) {
        $(".loader").show("slow");
        $.ajax({
            url: 'InterlinkingChilddetialsView',
            dataType: 'json',
            type: 'POST',
            data: { "MOLDITEMCODE": $text },

            success: function (data) {
                //debugger;
                var Tables = data.split('|');
                var CasePropertyDetails = JSON.parse(Tables[0]);
                //debugger;
                BindchildTab(CasePropertyDetails, '1');
                //   $("html, body").animate({ scrollTop: 0 }, "slow");

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



                    { 'data': 'Childitemcode' },
                    // { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Uom' },
                    { className: "text-capitalize", 'data': 'Quantity' },

                    {
                        className: "text-capitalize", 'data': 'Remove',
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

    $("#dynamic-table1").on("click", ".remove", function () {
        var CHILDITEMCODE = $(this).closest('tr').find('td:eq(0)').html();

        $.ajax({
            url: 'DeleteInterlinkingchildRow',
            dataType: "json",
            type: "POST",
            data: $.param({ 'CHILDITEMCODE': CHILDITEMCODE }),

            success: function (result) {

                BindchildPopupTab(JSON.parse(result), '1');

            }
        });

    });

    $('#btnadd').click(function (event) {
        //if (!$('#validation-form').valid()) e.preventDefault(); {
        var errormessage = "";
        if ($('#ddlchild').val() == "") {
            errormessage += "Please  Select Child Itemcode</br>";
        }
        if ($('#ddluom').val() == "") {
            errormessage += "Please Enter Uom</br>";
        }
        if ($('#txtquantity').val() == "") {
            errormessage += "Please Enter Quantity</br>";
        }

        if (errormessage.length == 0) {
            var action = $('#actiontypeadd').html();
            $(".loader").show("slow");
            if (window.FormData !== undefined) {
                var formData = new FormData();
                formData.append('CHILDITEMCODE', $('#ddlchild').val());
                formData.append('UOM', $('#ddluom').val());
                formData.append('QUANTITY', $('#txtquantity').val());
                formData.append('action', action);
                $.ajax({
                    url: 'AddInterlinkingchilddetails',
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
                    },
                    error: function (err) {
                        //alert(err.statusText);
                    }
                });

            }
        }
        else {
            bootbox.alert({
                message: '<span class="text-danger"><i class="ace-icon fa fa-exclamation-triangle fa-4x"></i><br>' + errormessage + '</span>',
                size: 'small'
            });
        }

    });

    function GetinterlinkpartDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditMoldRFIDAssignment',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'AUTOID': autoid }),

            success: function (result) {
                // var resJ = JSON.parse(data);
                var Tables = result.split("|");
                var Header = JSON.parse(Tables[0]);
               // var Details = JSON.parse(Tables[1]);
                //  $.each(resJ, function (i, item) {

                $("#hdautoid").val(Header[0].AUTOID);
                // $("#ddlplantlst").val(resJ[0].PLANTCODE);
                $("#ddlplant").val(Header[0].PLANTCODE).trigger('change');
                $("#txtlocation").val(Header[0].LOCATION);
                $('#ddlmold').val(Header[0].MOLDITEMCODE);
                $('#txtvarientcode').val(Header[0].VARIENTCODE);
                $('#txtvarientcode').val(Header[0].VARIENTCODE);
                $('#txtponumber').val(Header[0].PONUMBER);
                $('#txtbatchnumber').val(Header[0].BATCHNUMBER);
                $('#txtmoldopencount').val(Header[0].MOLDOPENCOUNT);
                $('#txtpreviousmrcount').val(Header[0].PREVIOUSMRCOUNT);
                $('#txtpreviousmrdate').val(Header[0].PREVIOUSMRDATE);
                $('#txtmrcount').val(Header[0].MRCOUNT);
                $('#txtmralertcount').val(Header[0].MRALERTCOUNT);
                $('#txtmoldlifecount').val(Header[0].MOLDLIFECOUNT);
                $('#txtrfidnumber').val(Header[0].RFIDNUMBER);
                $('#ddlstatus').val(Header[0].RECORDSTATUS).trigger('change');
                $('#txtremarks').val(Header[0].REMARKS);
                $('#txtsupplierid').val(Header[0].SUPPLIERID);
                $('#txtmoulditemdescription').val(Header[0].MOULDITEMNAME);
                $('#txtuom').val(Header[0].UOM);
                $('#actiontype').html("Update");
              //  BindchildPopupTab(Details, '1');
                //$('#ddlmold').prop('disabled', true)
         
            }

        });

        $(".loader").hide("slow");
    }

    $('#btnitemclear').click(function () {
        reset();
    });
    function reset() {
        $('#ddlchild').val("").trigger('change');
        $('#ddluom').val("").trigger('change');
        $('#txtquantity').val("");
        //$('#txttotalparts').val("");

    }


    $("#txtmailid").change(function () {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddress = $("#txtmailid").val();
        if (!emailReg.test(emailaddress)) {
            alert("Please Enter the Correct  EmailId");
            $('#txtmailid').val("");
            $('#txtmailid').focus();
        }
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
                columnDefs: cols1,
                columns: [



                    { className: "text-capitalize", 'data': 'Child Item Code' },
                    { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Quantity' },
                    // { className: "text-capitalize", 'data': 'Uom' },

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

    $("#txtnumber").change(function () {

        var mobileNum = $("#txtnumber").val();
        var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
        if (validateMobNum.test(mobileNum) && mobileNum.length == 10) {
            //alert("Valid Mobile Number");
        }
        else {
            alert("Invalid Mobile Number");
            $("#txtnumber").val("");
        }


    });
});