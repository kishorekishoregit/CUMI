jQuery(function ($) {
    //Grid Bind Value
    BindTable();
    var usercode = "";

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
            url: 'FabricateRackBookStockReportPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Plant = JSON.parse(pageload[0]);
                var Details = JSON.parse(pageload[1]);

                $("#ddlplant").empty().append('<option value="">Select an Option</option>');
                $.each(Plant, function () {
                    $("#ddlplant").append($("<option></option>").val(this["PLANTNAME"]).html(this["PLANTNAME"]));
                    $('#ddlplant').trigger("chosen:updated");
                });

                BindTab(Details, '1');

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
        //if (type == '0') {
        //    $('#dynamic-table').dataTable().fnDestroy();
        //    cols.length = 0;
        //    cols1.length = 0;
        //}

        var rcount = $("#dynamic-table > tbody > tr").length

        // TABLE BIND     
        //if (type == '0') {
        //    $('#dynamic-table').dataTable().fnDestroy();
        //    cols.length = 0;
        //    cols1.length = 0;
        //}

        if (rcount > 0) {
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
                    { className: "text-capitalize", 'data': 'Scrap Out Date' },
                    { className: "text-capitalize", 'data': 'Scrap Out Quantity' },
                    { className: "text-capitalize", 'data': 'Scrap Out Remark' },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Fabrication Rack Book Report',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Fabrication Rack Book Report'


                    }, {
                        extend: 'print',
                        title: 'Fabrication Rack Book Report',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ],
                // }
            });


        }
        else {
            $('#dynamic-table tbody').remove();
            $('#dynamic-table thead').remove();
            $('#dynamic-table').dataTable({
                "language": {
                    "emptyTable": " No records found.."
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

    $('#Inventory-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);
    })
        .on('form:submit', function () {

            GenerateStock();
            return false;
        });
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    function GenerateStock() {
        var formData = new FormData();
        formData.append('PLANT', $('#ddlplant').val());
        $.ajax({
            url: 'GetFabricateRackBookStockGenerate',
            contentType: false,
            processData: false,

            dataType: 'json',
            type: 'POST',
            data: formData,
            success: function (data) {
                var resJ = JSON.parse(data);

                //   BindTab(resJ, '1');

                if (resJ.length > 0) {
                    BindTab(resJ, '1');
                }
                else {
                    BindTab(resJ, '0');
                }


            }

        });

        $(".loader").hide("slow");
    }
});