jQuery(function ($) {
    $('#btnupload').click(function (result) {
        var fileUpload = document.getElementById("hf");
        if (fileUpload.value != null) {
            var uploadFile = new FormData();
            var files = $("#hf").get(0).files;
            // Add the uploaded file content to the form data collection
            if (files.length > 0) {
                uploadFile.append("CsvDoc", files[0]);
                $('.loadingGIF').show();
                $.ajax({
                    url: 'MoldPressCountFileUploadView',
                    contentType: false,
                    processData: false,
                    data: uploadFile,
                    type: 'POST',
                    success: function (result) {
                        var results = result.split('^');
                        var errormsg = results[0];
                        if (results[0] == 'True') {
                            if ($('#actiontypeaddd').html() == "Update")
                                $('#actiontypeaddd').html("Add");
                            BindTab(JSON.parse(results[1]), '1');
                        }
                        else {
                            bootbox.alert({
                                message: '<span class="text-danger"><i class="ace-icon fa fa-exclamation-triangle fa-4x"></i><br>' + results[1] + '</span>',
                                size: 'small',
                                callback: function () {
                                    location.reload();
                                }
                            });
                        }

                    }
                });
            }
            else {
                bootbox.alert({
                    message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Choose File." + '</span>',
                    size: 'small',

                    callback: function () {
                        location.reload();
                    }

                });
            }
        }
        //else {
        //    $('.loadingGIF').hide();
        //    sweetAlert('error-message', "Please Choose File.");
        //}
    });
    function BindTab(ResData, type) {
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
                cols1.push
                    ({
                        title: item.title,
                        targets: index
                    });
                cols1DATA.push
                    ({
                        data: item.title,
                    });
                finalcolsresult += 'null' + ',';
            });
            $('#dynamic-tableChild').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "ascrollX": "100%",
                "bPaginate": false,
                "bSort": false,
                "bInfo": false,
                "bFilter": false,
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
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Name' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Shot Count' },
                    { className: "text-capitalize", 'data': 'Date Of Shot' },
                    { className: "text-capitalize", 'data': 'Total Shot Count' },
                    { className: "text-capitalize", 'data': 'RFID No' },
                ],

            });
        }
        else {
            $('#dynamic-tableChild tbody').remove();
            $('#dynamic-tableChild thead').remove();
            $('#dynamic-tableChild').dataTable({
                "language": {
                    "emptyTable": "Mold Press Count File Upload No Records Found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "100%", bSearchable: false, bSortable: false },
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
    $("#btnsubmit").click(function () {
        var fileUpload = document.getElementById("hf");
        var uploadFile = new FormData();
        var files = $("#hf").get(0).files;
        if (files.length > 0) {
            uploadFile.append("CsvDoc", files[0]);
            $('.loadingGIF').show();
            $.ajax({
                url: 'MoldPressCountFileUploadInsert',
                contentType: false,
                processData: false,
                data: uploadFile,
                type: 'POST',
                success: function (result) {
                    var results = result.split('|');
                    if (results[0] == 'True') {
                        bootbox.alert({
                            message: '<span class="text-success"><i class="icon ion-ios-checkmark-circle-outline tx-50 tx-success"style="margin-left: 100px;font-size: 50px;"></i><br>' + "File Upload Successfully" + '</span>',
                            size: 'small',
                            callback: function () {
                                location.reload();
                            }
                        });
                    }
                    else {
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + results[1] + '</span>',
                            size: 'small'

                        });
                    }

                }
            });
        }
        else {

            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Choose File" + '</span>',
                size: 'small',

            });
        }
    })
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);
    $('#btntemplateimport').click(function () {


        window.location.href = "MoldPressCountFileUploadDownload/?file=MouldPressCount.xlsx";
    });
});