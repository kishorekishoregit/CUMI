jQuery(function ($) {

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
            url: 'CustomerStockReportPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var CUSTOMER = JSON.parse(pageload[0]);
                var CustomerDetails = JSON.parse(pageload[1]);
                $("#ddlCustomer").empty().append('<option value="">Select an Option</option>');
                $.each(CUSTOMER, function () {
                    $("#ddlCustomer").append($("<option></option>").val(this["CUSTOMERID"]).html(this["CUSTOMERNAME"]));
                    $('#ddlCustomer').trigger("chosen:updated");
                });

                BindTab(CustomerDetails, '1');

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
                    { className: "text-capitalize", 'data': 'Customer' },
                    { className: "text-capitalize", 'data': 'Plant' },
                    { className: "text-capitalize", 'data': 'Rack Type' },
                    { className: "text-capitalize", 'data': 'Rack Color' },
                    { className: "text-capitalize", 'data': 'A Frame' },
                    { className: "text-capitalize", 'data': 'Arm' },
                    { className: "text-capitalize", 'data': 'Stopper' },
                    { className: "text-capitalize", 'data': 'Channel' },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Customer Stock Report',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Customer Stock Report'


                    }, {
                        extend: 'print',
                        title: 'Customer Stock Report',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ],
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api(), data;

                    // converting to interger to find total
                    var intVal = function (i) {
                        return typeof i === 'string' ?
                            i.replace(/[\$,]/g, '') * 1 :
                            typeof i === 'number' ?
                                i : 0;
                    };

                    // computing column Total of the complete result 
                    var monTotal = api
                        .column(4)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var tueTotal = api
                        .column(5)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var wedTotal = api
                        .column(6)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    var thuTotal = api
                        .column(7)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    
                    // Update footer by showing the total with the reference of the column index 
                    $(api.column(3).footer()).html('Total');
                    $(api.column(4).footer()).html(monTotal);
                    $(api.column(5).footer()).html(tueTotal);
                    $(api.column(6).footer()).html(wedTotal);
                    $(api.column(7).footer()).html(thuTotal);
                },
                // }
                "rowCallback": function (row, data, index) {
                    var result = data["Rack Color"];
                    var StoppageType = result;
                    if (StoppageType == "Blue") {
                        $('td', row).css('background-color', '#87CEFA');
                    }
                    else if (StoppageType == "Green") {
                        $('td', row).css('background-color', '#90EE90');
                    }
                    else if (StoppageType == "Others") {
                        $('td', row).css('background-color', '');
                    }


                },
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


    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $('#CustomerReport-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);
    })
        .on('form:submit', function () {

            GenerateStock();;
            return false;
        });

    function GenerateStock() {

        var formData = new FormData();
        formData.append('CUSTOMER', $('#ddlCustomer').val());

        $.ajax({
            url: 'GetCustomerStockReport',
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