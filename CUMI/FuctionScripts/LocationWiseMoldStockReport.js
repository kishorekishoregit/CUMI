jQuery(function ($) {
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
            url: 'LocationWiseMoldStockReportPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var location = JSON.parse(pageload[0]);
                var moldlocationreport = JSON.parse(pageload[1]);
                $("#ddllocation").empty().append('<option value="">Select an Option</option>');
                $.each(location, function () {
                    $("#ddllocation").append($("<option></option>").val(this["LOCATIONCODE"]).html(this["LOCATIONNAME"]));
                    $('#ddllocation').trigger("chosen:updated");
                });

                BindTab(moldlocationreport, '1');

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
                    { className: "text-capitalize", 'data': 'Location' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Description' },
                    { className: "text-capitalize", 'data': 'Mould Type' },
                    {
                        className: "text-capitalize", 'data': 'Supplier Name',
                        //'render': function (data, type, full, meta) {

                        //    return '<div><span style="visibility: hidden;">' + data + '</span></div>';

                        //}
                    },

                ],

                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Mold Status Report',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Mold Status Report'


                    }, {
                        extend: 'print',
                        title: 'Mold Status Report',
                        text: '<img src="../../Images/print.png" style="height: 25px;">',
                        footer: false
                    }
                ],
         
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

    $('#Location-form').parsley().on('field:validated', function () {
        var ok = $('.parsley-error').length === 0;
        $('.bs-callout-info').toggleClass('hidden', !ok);
        $('.bs-callout-warning').toggleClass('hidden', ok);
    })
        .on('form:submit', function () {

            GenerateStock();;
            return false;
        });
    //$("#btnsubmit1").click(function () {
    //    GenerateStock();;
    //});
    function GenerateStock() {

        var formData = new FormData();
        formData.append('Fromdate', $('#txtfromdate').val());
        formData.append('Todate', $('#txttodate').val());
        formData.append('Location', $('#ddllocation').val());

        $.ajax({
            url: 'LocationWiseMoldStockReportGeneratere',
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