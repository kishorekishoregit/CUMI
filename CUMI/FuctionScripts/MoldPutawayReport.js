jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value

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
        $("#txtfromdate").val(today);
        $("#txttodate").val(today);

    }
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
            url: 'MoldPutawayreportPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result;

                var MoldInwarddts = JSON.parse(pageload);
                BindTab(MoldInwarddts, '1');
            }
        });
        $(".loader").hide("slow");
    }
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);
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

        var rcount = $("#dynamic-table > tbody > tr").length
        if (rcount > 0) {
            $('#dynamic-table').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }

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

                    { className: "text-capitalize", 'data': 'Mould ItemCode' },
                    { className: "text-capitalize", 'data': 'Mould Description' },
                    { className: "text-capitalize", 'data': 'Location' },
                    { className: "text-capitalize", 'data': 'RFID Number' },
                    { className: "text-capitalize", 'data': 'Putaway Date' },
                 
                    /*  { className: "text-capitalize", 'data': 'FG Item Name' },*/
                    //{ className: "text-capitalize", 'data': 'Child Item Name' },
                    //{ className: "text-capitalize", 'data': 'Quantity' },


                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Mould PutAway Report',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mould PutAway Report'


                    }, {
                        extend: 'print',
                        title: 'Mould PutAway Report',
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
                    "emptyTable": "Mould PutAway Report No records found.."
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
    $('#btnsubmit1').click(function () {

        GenerateMoldInwardInterlinkReport();
    });

    function GenerateMoldInwardInterlinkReport() {

        $(".loader").show("slow");
        var formData = new FormData();
        formData.append('Fromdate', $('#txtfromdate').val());
        formData.append('Todate', $('#txttodate').val());
        $.ajax({
            url: 'MoldGeneratereport',
            contentType: false, // Not to set any content header  
            processData: false,
            // contentType: "application/json; charset=utf-8",
            dataType: 'json',
            type: 'POST',
            data: formData,

            success: function (data) {
                var resJ = JSON.parse(data);
                if (click == 0) {
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