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
            url: 'GetCityMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var Zone = JSON.parse(pageload[0]);
                var Status = JSON.parse(pageload[1]);
                var DETAILS = JSON.parse(pageload[2]);



                $("#ddlZone").empty().append('<option value="">Select an Option</option>');
                $.each(Zone, function () {
                    $("#ddlZone").append($("<option></option>").val(this["ZONE"]).html(this["ZONE"]));
                    $('#ddlZone').trigger("chosen:updated");
                });


                $.each(Status, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
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


                    {
                        className: "text-capitalize",'data': 'Zone',
                        'render': function (data, type, full, meta) {

                            var pakarr = data.split('_');

                            return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                        }
                    },
                    // { 'data': 'Employee Code' },
                    { className: "text-capitalize",'data': 'State' },
                    { className: "text-capitalize",'data': 'City' },
                    { className: "text-capitalize",'data': 'Status' },
                    {

                        className: "text-capitalize",'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'City Master',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'City Master'


                    }, {
                        extend: 'print',
                        title: 'City Master',
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
                    "emptyTable": "City Master No records found.."
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
                "info": true,
                "paging": true,
                "searching": true
            });


        }
        $(".loader").hide("slow");
    }

    $("#ddlZone").change(function () {
        $.ajax({
            url: 'GetZONECityMaster',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'ZONE': $('#ddlZone').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var State = JSON.parse(Res[0]);

                $("#ddlState").empty().append('<option value="">Select an Option</option>');
                $.each(State, function () {
                    $("#ddlState").append($("<option></option>").val(this["STATE"]).html(this["STATE"]));
                    $('#ddlState').trigger("chosen:updated");
                });
                if (state != "") {
                    $('#ddlState').val(state).trigger('change');
                }
            }

        });
    });


    $('#City-form').parsley().on('field:validated', function () {
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
            formData.append('ZONE', $('#ddlZone').val());
            formData.append('STATE', $('#ddlState').val());
            formData.append('CITY', $('#txtCity').val());
            formData.append('STATUS', $('#ddlstatus').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertCityMaster',
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

                        $('#ddlZone').val("");
                        $('#txtstate').val("")
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
        var autoid = $(this).closest('tr').find('td:eq(0) > div > span').html();
        GetEmployeeDetailsByID(autoid)
    });

    function GetEmployeeDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'GetCityMasterEDIT',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'ID': autoid }),

            success: function (data) {

                //  $.each(resJ, function (i, item) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $("#ddlZone").val(resJ[0].ZONE).trigger('change');
                $("#txtCity").val(resJ[0].CITY);
                state = resJ[0].STATE;
                $('#ddlstatus').val(resJ[0].STATUS).trigger('change');
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }


});