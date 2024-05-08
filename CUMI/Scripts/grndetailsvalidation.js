jQuery(function ($) {

    var EmpDatatable = $('#dynamic-table');
    var table = document.getElementById("dynamic-table");

    BindPageLoad();
    $('#grnno').keyup(function () {
       
        var grnno = $('#grnno').val();
        var grnnoresult = "";
        var grnno1;
        $('.error').remove();
        $.ajax({
            url: 'GetGRNDetailsPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                var pageloag = Result.split('|');              
                var grnno = JSON.parse(pageloag[3]);  
                var resJ = grnno;

                if (resJ.length > 0) {
                    for (var i in resJ) {
                        if (resJ[i].GRNNO) {
                            grnno1 = resJ[i].GRNNO;
                            grnnoresult = grnnoresult + grnno1 + ',';
                        }
                    }
                    var s1 = new Array();
                    s1 = grnnoresult.split(",");
                    $("#grnno").autocomplete({
                        min: 2,
                        maxShowItems: 5,
                        source: s1
                    });
                }
                else {
                    $('.error').remove();
                    $("#grnno").after("<span class='error text-danger'>No Result Found !!</span>");
                }
            }
        });
    });

    function BindPageLoad() {
       
        $.ajax({
            url: 'vendorcodechange',
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (Result) {
               
                var pageloag = Result.split('|');
                var customerplant = JSON.parse(pageloag[0]);
                var invoiceno = JSON.parse(pageloag[1]);
                var vendorcode = JSON.parse(pageloag[2]);
                var grnno = JSON.parse(pageloag[3]);
                var resJ = JSON.parse(pageloag[4]);               
                BindTab(resJ, "1");     // "1" for Find out page load ,  "0" for Search By Parameters
            }
        });
    }


    $('#btnSearch').click(function () {
        $.ajax({
            url: 'GetGRNDetailsSearch',
            dataType: "json",
            type: "POST",
            data: $("#myform").serialize(),
            success: function (data) {
                var exampleRecord = data;
                BindTab(exampleRecord, "0");
            }
        });
    });



    function BindTab(Emp, type) {
       
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecord = Emp[0];
        // TABLE BIND     
        if (type == '0') {
            $('#dynamic-table').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
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


        $('#dynamic-table').dataTable({
            data: Emp,
            "bAutoWidth": false,
            "scrollX": true,
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
            columns: cols1DATA

        });
        var tableTools = new $.fn.dataTable.TableTools(EmpDatatable, {
            'aButtons': [
                {
                    'sExtends': 'copy',
                    'sButtonText': "<i class='fa fa-copy bigger-110 pink'></i> <span class='hidden'>Copy to clipboard</span>"
                },
                {
                    'sExtends': 'csv',
                    'sButtonText': "<i class='fa fa-database bigger-110 orange'></i> <span class='hidden'>Export to CSV</span>",
                    'bFooter': false
                },
                {
                    'sExtends': 'xls',
                    'sButtonText': "<i class='fa fa-file-excel-o bigger-110 green'></i> <span class='hidden'>Export to Excel</span>",
                    'sFileName': 'Data.xls'

                },
                 {
                     'sExtends': 'pdf',
                     'sButtonText': "<i class='fa fa-file-pdf-o bigger-110 red'></i> <span class='hidden'>Export to PDF</span>",
                     'bFooter': false

                 },
                {
                    'sExtends': 'print',
                    'sButtonText': "<i class='fa fa-print bigger-110 grey'></i> <span class='hidden'>Print</span>",
                    'bShowAll': true
                }

            ],
            'sSwfPath': 'http://180.179.42.237/Scripts/NewFolder1/copy_csv_xls_pdf.swf'
        });
       
            $(tableTools.fnContainer()).insertBefore('#dynamic-table_wrapper');
      

    }

    $('#btnclear').click(function () {
        location.reload();
    });
});