jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();
    getmonth();

    var ISClick = false;
    function getmonth() {
        var today = new Date();

        var dd = today.getDate() > 9 ? today.getDate() : '0' + today.getDate();
        var mm = today.getMonth(); //January is 0!
        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        var yyyy = today.getFullYear();

        var today = dd + '/' + months[mm] + '/' + yyyy;
        // $("#txtorderdate").val(today);
        $("#txtdateofshot").val(today);
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
            url: 'GetMoldShotCountPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var mold = JSON.parse(pageload[0]);
                var resJ = JSON.parse(pageload[1]);

                $("#ddlmolditemcode").empty().append('<option value="">Select an Option</option>');
                $.each(mold, function () {
                    $("#ddlmolditemcode").append($("<option></option>").val(this["MOLDITEMCODE"]).html(this["MOLDITEMCODE"]));
                    $('#ddlmolditemcode').trigger("chosen:updated");
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


                    {
                        className: "text-capitalize", 'data': 'Mould Item Code',
                        //'render': function (data, type, full, meta) {

                        //    var pakarr = data.split('_');

                        //    return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                        //}
                    },
                    // { 'data': 'Employee Code' },
                    { className: "text-capitalize", 'data': 'RFID Number' },
                    { className: "text-capitalize", 'data': 'Mould Description' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Shot Count' },
                    { className: "text-capitalize", 'data': 'Date Of Shot' },
                    {
                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {
                            var pakarr = data;
                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="typcn typcn-edit" aria-hidden="true"></i></button></div><span style = "visibility:hidden; font-size: 1px;">' + pakarr + '</span>'

                           // return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'
                        }
                    },
                    //{
                    //    className: "text-capitalize", 'data': 'Print',
                    //    'render': function (data, type, full, meta) {

                    //        var pakarr = data;
                    //        return '<button type="button" class="btn-az-secondary printdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="View" style="width: 30px;height: 30px;padding: 1px;"><i class="icon ion-ios-print" aria-hidden="true"></i></button></div><span style = "visibility:hidden; font-size: 1px;">' + pakarr + '</span>'

                    //    }
                    //},
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
                        title: 'Mold Shot Count',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF"style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mold Shot Count'


                    }, {
                        extend: 'print',
                        title: 'Mold Shot Count',
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
                    "emptyTable": "Interlinking Master No records found.."
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
    $("#ddlmolditemcode").change(function () {
        
        $.ajax({
            url: 'GetMoldShotCountcodedetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'moldcode': $('#ddlmolditemcode').val() }),
            success: function (data) {
                var Res = data.split('|');
                var molddetails = JSON.parse(Res[0]);
              //  $('#txtfgitemcode').val(molddetails[0].FGITEMCODE);
                $("#dllrfidnumber").empty().append('<option value="">Select an Option</option>');
                $.each(molddetails, function () {
                    $("#dllrfidnumber").append($("<option></option>").val(this["RFIDNUMBER"]).html(this["RFIDNUMBER"]));
                    $('#dllrfidnumber').trigger("chosen:updated");
                });
            }

        });

    });

    $("#dllrfidnumber").change(function () {
        $.ajax({
            url: 'GetMoldShotCountcodeRFIDdetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'moldcode': $('#dllrfidnumber').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var molddetails = JSON.parse(Res[0]);
                $('#txtitemdescription').val(molddetails[0].DESCRIPTION);
                $('#txtfgitemcode').val(molddetails[0].FGITEMCODE);

            }

        });

    });

    //$('#Employee-form').parsley().on('field:validated', function () {
    //    var ok = $('.parsley-error').length === 0;
    //    $('.bs-callout-info').toggleClass('hidden', !ok);
    //    $('.bs-callout-warning').toggleClass('hidden', ok);

    //})
    //    .on('form:submit', function () {
    //        save();
    //        return false;
    //    });

    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";
        if ($('#txtrdcno').val() == "") {
            errormessage += "Please Choose Mold Item Code</br> ";
        }
        if ($('#txtdateofshot').val() == "") {
            errormessage += "Please Enter Date.</br>";
        }
        if ($('#txtshotcount').val() == "") {
            errormessage += "Please Enter Shot Count.</br>";
        }

        if (errormessage.length == 0) {
            var intcount = 0;
            var itemcode = "";
            var itemname = "";
            var qty = '';
            var uom = "";
        
            $.ajax({
                url: 'InsertMoldShotCount',
                dataType: "json",
                type: "POST",


                data: $.param({ 'MOLDITEMCODE': $('#ddlmolditemcode').val() })
                    + '&' + $.param({ 'RFIDNUMBER': $('#dllrfidnumber').val() })
                    + '&' + $.param({ 'MOLDNAME': $('#txtitemdescription').val() })
                    + '&' + $.param({ 'FGITEMCODE': $('#txtfgitemcode').val() })
                    + '&' + $.param({ 'SHOTCOUNT': $('#txtshotcount').val() })
                    + '&' + $.param({ 'DATEOFSHOT': $('#txtdateofshot').val() })
                    + '&' + $.param({ 'actiontype': $("#actiontype").text() })
                    + '&' + $.param({ 'AUTOID': $("#hdautoid").val() }),
                success: function (data) {
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {
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


    $("#dynamic-table").on("click", ".editdetails", function () {
        var autoid = $(this).closest('tr').find('td:eq(6) > span').html();
        //var MOLDITEMCODE = $(this).closest('tr').find('td:eq(5)').html();
        GetmoldDetailsByID(autoid)
    });


    function GetmoldDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditMoldShotCount',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'ID': autoid }),

            success: function (result) {
                // var resJ = JSON.parse(data);
                var Tables = result.split("|");
                var Header = JSON.parse(Tables[0]);
               // var Details = JSON.parse(Tables[1]);
                //  $.each(resJ, function (i, item) {
               $("#hdautoid").val(Header[0].AUTOID);
                // $("#ddlplantlst").val(resJ[0].PLANTCODE);
                $("#ddlmolditemcode").val(Header[0].MOLDITEMCODE).trigger('change').prop('disabled', true);
                setTimeout(function () {
                    $("#dllrfidnumber").val(Header[0].RFIDNUMBER).trigger('change');
                }, 300);

              // $("#dllrfidnumber").val(Header[0].RFIDNUMBER).trigger('change')
                $("#txtitemdescription").val(Header[0].MOLDITEMSESCRIPTION);
                $('#txtfgitemcode').val(Header[0].FGITEMCODE);
                $('#txtshotcount').val(Header[0].SHOTCOUNT);
                $('#txtdateofshot').val(Header[0].DATEOFSHOT);            
                $("#actiontype").text("Update");
                //BindchildPopupTab(Details, '1');
                //$('#ddlmold').prop('disabled', true);


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