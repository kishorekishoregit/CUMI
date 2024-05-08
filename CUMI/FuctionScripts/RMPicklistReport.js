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
            url: 'RMPicklistReportpageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result;
                var Picklistno = JSON.parse(pageload);
              
                $("#ddlmoldpicklistno").empty().append('<option value="">Select an Option</option>');
                $.each(Picklistno, function () {
                    $("#ddlmoldpicklistno").append($("<option></option>").val(this["PICKLISTNO"]).html(this["PICKLISTNO"]));
                    $('#ddlmoldpicklistno').trigger("chosen:updated");
                });

            }
        });
        $(".loader").hide("slow");
    }
    $('#btnsubmit').click(function () {
        $('.loadingGIF').show()
        if ($('#ddlmoldpicklistno').val() != '') {

            $.ajax({
                url: 'RMPicklistReportGenerate',
                dataType: 'json',
                type: 'POST',
                data: $.param({ 'Picklistno': $('#ddlmoldpicklistno').val() }),
                success: function (data) {
                    var pageload = data.split('|');
                    var Header = JSON.parse(pageload[0]);
                    var Details = JSON.parse(pageload[1]);
                    generatereport(Header, Details);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Status: " + textStatus); alert("Error: " + errorThrown);
                }
            });
        } else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Select Picklist No." + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();
        }
    });
    function generatereport(Header, Details) {
        debugger;
        $("#ptnpicklistno").text(Header[0].PICKLISTNO);
        $("#ptnPicklistDate").text(Header[0].PICKLISTDATE);
        $("#ptnwarehousepicker").text(Header[0].WAREHOUSEPICKER);
        $("#ptnproductionorderno").text(Header[0].PRODUCTIONORDERNO);
        //$("#rptZone").text(Header[0].ZONE);
        debugger;
        $("#rptgrid").empty();
        for (var i = 0; i < Details.length; i++) {
            $('#rptgrid').append(' <tr style="height:35px;"><td style="text-align: center;">' + Details[i].SNO +
                '</td> <td style="text-align: center;">' + Details[i].ITEMCODE +
                '</td> <td style="text-align: center;">' + Details[i].BARCODE +
                '</td> <td style="text-align: center;">' + Details[i].QUANTITY +
                '</td><td style="text-align: center;">' + Details[i].LOCATIONCODE + '</td></tr>');

        }
        printrow = Details.length;

    }
    var printrow = '';
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $("#btnPrint").click(function () {

        if (printrow > 0) {
            var contents = $("#dvContents").html();
            var frame1 = $('<iframe />');
            frame1[0].name = "frame1";
            frame1.css({ "position": "absolute", "top": "-1000000px" });
            $("body").append(frame1);
            var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
            frameDoc.document.open();
            //Create a new HTML document.
            //frameDoc.document.write('<html><head><meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><title>Quality Inspection Report</title>');
            //frameDoc.document.write('</head><body>');
            //Append the external CSS file.
            //frameDoc.document.write('<style>body{font-family: "Century Gothic", CenturyGothic, AppleGothic, sans-serif;font-size:12px !important;}table {border-collapse: collapse !important;width:100% !important;}table thead th{background:#bbb !important;}table, td, th {border: 1px solid #e7e7e7 !important;}thead {display: table-header-group;}@media print { thead {display: table-header-group;}table thead th{background:#bbb !important;}} td{font-size: 12px !important;vertical-align:middle !important;padding:2px !important;}.qualityparameterreport td, .qualityparameterreport th{text-align:center !important;vertical-align:middle !important;font-size: 12px !important; padding:2px !important; }.qualityparameterreport td{padding:2px !important;text-align:center !important; vertical-align:middle !important;} .table thead tr {background: #eaeaea !important;} table, td, th {border: 1px solid #dddddd !important;} tr.header{background: #eaeaea !important;} h2{font-size:16px; font-weight:900; margin: 0px !important;} .header-table td p{text-align:left;white-space:normal !important;} .table thead tr.top-header *, thead .sub-header tr * {background:#FFF !important;}td.text-left p{text-align:left !important; font-size:12px;}.header-table td p{text-align:left;white-space:normal !important;}.sub-header .text-left *{text-align:left !important;}.sub-header td:nth-child(3)>p {width:50px;}.qualityparameterreport>caption>* th, .qualityparameterreport>caption>* td{padding:0px !important;}.text-1 p{padding-left:8px !important;}.lpa-table td:nth-child(3)>p{width:100% !important;}.text-1 p {padding-left: 8px !important;}.img-responsive, .thumbnail a>img, .thumbnail>img {display: block;max-width:100%;height: auto;}table.whattocheck td:nth-child(2){width:500px !important;}table { page-break-inside:auto }tr{ page-break-inside:avoid; page-break-after:auto }thead { display:table-header-group }tfoot { display:table-footer-group }@media print{table {page-break-after:always}}@media print {@page { margin:5px; } #myTable thead tr th {background: #eaeaea !important;}  #myTable thead tr {background: #eaeaea !important;}}  #myTable thead tr {background: #eaeaea !important;}  #myTable thead tr th {background: #eaeaea !important;}.StrReplace span.new_class {color:#000;font-weight:900 !important;}text{font-size: 12px !important;font-weight:bold !important;fill:#000 !important;}</style>');
            //Append the DIV contents.
            frameDoc.document.write(contents);

            //frameDoc.document.write('</body></html>');
            frameDoc.document.close();
            setTimeout(function () {
                window.frames["frame1"].focus();
                window.frames["frame1"].print();
                frame1.remove();
            }, 500);
        } else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Generate Picklist." + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();
        }

    });
});