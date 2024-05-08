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
            url: 'PageloadSupplierMaster',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');

                var recordstatus = JSON.parse(pageload[0]);
                var Supplierdts = JSON.parse(pageload[1]);


                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METADATADESCRIPTION"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                BindTab(Supplierdts, '1');
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
                        className: "text-capitalize", 'data': 'Supplier Code',
                        'render': function (data, type, full, meta) {

                            var pakarr = data.split('_');

                            return '<div>' + pakarr[1] + '<span style="visibility: hidden;">' + pakarr[0] + '</span></div>';

                        }
                    },
                
                   /* { className: "text-capitalize", 'data': 'Supplier Code' },*/
                    { className: "text-capitalize", 'data': 'Supplier Name' },
                    { className: "text-capitalize", 'data': 'Address' },
                    { className: "text-capitalize", 'data': 'Record Status' },
                    {

                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {

                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class="typcn typcn-edit" aria-hidden="true"></i></button>'

                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Supplier Master',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Supplier Master'


                    }, {
                        extend: 'print',
                        title: 'Supplier Master',
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
                    "emptyTable": "Supplier Master No records found.."
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
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);


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
        
        var formData = new FormData();

        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('SupplierCode', $('#txtsuppliercode').val());
            formData.append('SupplierName', $('#txtsuppliername').val());
            formData.append('Address', $('#txtAddress').val());
            formData.append('RecordStatus', $('#ddlstatus').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertSupplierMaster',
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

                        $('#txtsuppliercode').val("");
                        $("#txtsuppliername").val("");
                        $('#txtAddress').val("")
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

    $("#dynamic-table").on("click", ".editdetails", function () {

        var ID = $(this).closest('tr').find('td:eq(0) > div > span').html();

        GetUserCreationDetailsByID(ID)
    });
    
    function GetUserCreationDetailsByID(ID) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditSupplierMaster',
            dataType: 'json',
            type: 'POST',
            data: { "ID": ID },

            success: function (data) {
                var Res = data.split('|');
                var resJ = JSON.parse(Res[0]);
                $("#hdautoid").val(resJ[0].AUTOID);
                $("#txtsuppliercode").val(resJ[0].SUPPLIERCODE);
                $("#txtsuppliername").val(resJ[0].SUPPLIERNAME);
                $("#txtAddress").val(resJ[0].ADDRESS);
                $("#ddlstatus").val(resJ[0].RECORDSTATUS).trigger('change');
                $("#actiontype").text("Update");
            }

        });

        $(".loader").hide("slow");
    }
    $('#btntemplateimport').click(function () {
        window.location.href = "Downloads/?file=SUPPLIERMASTER.xlsx";
    });

    $("#btnimport").click(function () {
        $('.loadingGIF').show();
        var uploadFile = new FormData();
        var files = $("#hf").get(0).files;
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);

            $.ajax({
                url: 'SupplierMastersUploads',
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function (result) {
                    $('.loadingGIF').hide();
                    $("#hf").val(null);
                    if (result == 'true') {
                        bootbox.alert({
                            message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Uploaded Successfully." + '</span>',
                            size: 'small',
                            callback: function () {
                                Clear();
                            }
                        });
                    } else if (result == 'false') {
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Supplier Code Cannot be left empty." + '</span>',
                            size: 'small',
                        });
                    }
                    else {
                        $("#hf").val(null);
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Already uploaded" + '</span>',
                            size: 'small',
                        });
                    }
                },
                error: function () {
                    $('.loadingGIF').hide();
                    $("#hf").val(null);
                    bootbox.alert({
                        message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "An error occurred while uploading." + '</span>',
                        size: 'small',
                    });
                }
            });
        } else {
            $('.loadingGIF').hide();
            $("#hf").val(null);
            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Choose Your file." + '</span>',
                size: 'small',
            });
        }
    });


});