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
            url: 'MoldIssueConfirmationPageload',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Productionuser = JSON.parse(pageload[0]);
                var MoldDetails = JSON.parse(pageload[1]);

                $("#ddlproductionuser").empty().append('<option value="">Select an Option</option>');
                $.each(Productionuser, function () {
                    $("#ddlproductionuser").append($("<option></option>").val(this["USERNAME"]).html(this["USERNAME"]));
                    $('#ddlproductionuser').trigger("chosen:updated");
                });

                BindchildTabPopup(MoldDetails, '1');

                var checkbox = document.getElementById("checkall");

                // Uncheck the checkbox
                checkbox.checked = false;
            }
        });
        $(".loader").hide("slow");
    }
    function BindchildTabPopup(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var idinc = 0;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-table2 tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-table2').dataTable().fnDestroy();
            $('#dynamic-table2').DataTable().destroy(); $('#dynamic-table2').html('');
            //$('#dynamic-tablePropDetails').dataTable().fnDestroy();
            cols.length = 0;
            cols1.length = 0;
        }
        if (exampleRecordUpload) {
            //get keys in object. This will only work if your statement remains true that all objects have identical keys
            var keys = Object.keys(exampleRecordUpload);

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

            $('#dynamic-table2').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bFilter": false,
                "bInfo": false,
                "bPaginate": false,
                "bLengthChange": false,
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
                columnDefs: cols1,
                columns: [



                    { className: "text-capitalize", 'data': 'Select' },
                    { className: "text-capitalize", 'data': 'Production Order No' },
                    { className: "text-capitalize", 'data': 'Production Order Date' },
                    { className: "text-capitalize", 'data': 'FG Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Code' },
                    { className: "text-capitalize", 'data': 'Mould Item Description' },
                    { className: "text-capitalize", 'data': 'Production Qty' },


                ],
                // dom: 'Brtip',


            });
        }
        else {
            $('#dynamic-table2 tbody').remove();
            $('#dynamic-table2 thead').remove();
            $('#dynamic-table2').dataTable({
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
                "info": false,
                "paging": false,
                "searching": false,
                "ordering": false,
            });


        }

    }
    $('#checkall').click(function (event) {
        if (this.checked) {
            // Iterate each checkbox
            $(':checkbox').each(function () {
                this.checked = true;
            });
        } else {
            $(':checkbox').each(function () {
                this.checked = false;
            });
        }
    });
    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";

        if ($('#ddlproductionuser').val() == "") {
            errormessage += "Please Select Production User.</br>";
        }
        if (!$('.checkbox').is(':checked')) {
            errormessage += ("At least one checkbox must be checked.</br>");
            //return false; // Prevent form submission
        }
        if (errormessage.length == 0) {
            var intcount = 0;
            var productionno = "";
            var productiondate = "";
            var FGItemcode = "";
            var Molditemcode = "";
            var Molditemname = "";
            var ProductQty = "";

            $("#dynamic-table2 tbody > tr").each(function () {
                intcount++;
                debugger;
                if (jQuery('td:eq(0)', this).children('input.checkbox').is(':checked')) {
                    productionno += jQuery('td:eq(1)', this).html() + "^";
                    productiondate += jQuery('td:eq(2)', this).html() + "^";
                    FGItemcode += jQuery('td:eq(3)', this).html() + "^";
                    Molditemcode += jQuery('td:eq(4)', this).html() + "^";
                    Molditemname += jQuery('td:eq(5)', this).text() + "^";
                    ProductQty += jQuery('td:eq(6)', this).html() + "^";
                }
            });
            if (intcount > 0) {
                debugger;
                productionno = productionno.substring(0, productionno.length - 1);
                productiondate = productiondate.substring(0, productiondate.length - 1);
                FGItemcode = FGItemcode.substring(0, FGItemcode.length - 1);
                Molditemcode = Molditemcode.substring(0, Molditemcode.length - 1);
                Molditemname = Molditemname.substring(0, Molditemname.length - 1);
                ProductQty = ProductQty.substring(0, ProductQty.length - 1);

            }

            $.ajax({
                url: 'MoldIssueConfirmationAssign',
                dataType: "json",
                type: "POST",


                data: $.param({ 'ProductionUser': $('#ddlproductionuser').val() })
                    + '&' + $.param({ 'productionno': productionno })
                    + '&' + $.param({ 'productiondate': productiondate })
                    + '&' + $.param({ 'FGitemcode': FGItemcode })
                    + '&' + $.param({ 'Molditemcode': Molditemcode })
                    + '&' + $.param({ 'Molditemname': Molditemname })
                    + '&' + $.param({ 'Productionqty': ProductQty }),
                  

                success: function (data) {
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {

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
});