jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();

    getmonth();
    function getmonth() {
        var today = new Date();

        var dd = today.getDate() > 9 ? today.getDate() : '0' + today.getDate();
        var mm = today.getMonth(); //January is 0!
        var months = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];
        var yyyy = today.getFullYear();

        var today = dd + '/' + months[mm] + '/' + yyyy;
        // $("#txtorderdate").val(today);
        $("#txtreceiveddate").val(today);
    }
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
            url: 'ReceiptatPlantPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var Lrno = JSON.parse(pageload[0]);
                var lrnodts = JSON.parse(pageload[1])

                $("#ddllrno").empty().append('<option value="">Select an Option</option>');
                $.each(Lrno, function () {
                    $("#ddllrno").append($("<option></option>").val(this["LRNUMBER"]).html(this["LRNUMBER"]));
                    $('#ddllrno').trigger("chosen:updated");
                });
         


                BindTab(lrnodts, '1');
            }
        });
        $(".loader").hide("slow");
    }
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

                    { className: "text-capitalize", 'data': 'LR No' },
                    { className: "text-capitalize", 'data': 'Truck No' },
                    { className: "text-capitalize", 'data': 'Reverse Transpoter' },
                    { className: "text-capitalize", 'data': 'Received Date' },
                    

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'Receipt at Plant',
                        text: '<img src="../../Images/excel.png" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'Receipt at Plant'


                    }, {
                        extend: 'print',
                        title: 'Receipt at Plant',
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
                    "emptyTable": "Receipt at Plant No records found.."
                },
                'bSort': false,
                'aoColumns': [
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
    $('#ddllrno').change(function () {

        var optionselected = $(this);
        var OValue = optionselected.val();
        $(".loader").show("slow");
        $.ajax({

            url: 'ReceiptatFetchByLRno',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: { 'LRNNO': OValue },
            success: function (data) {

                var Res = data.split('|');
                var Lrnno = JSON.parse(Res[0]);
                var Lrndts = JSON.parse(Res[1]);

                $('#txttruckno').val(Lrnno[0].TRUCKNUMBER);
                $('#txtreversetranspoter').val(Lrnno[0].REVERSETPTNAME);

                BindChildTab(Lrndts);

            }
        });
        $(".loader").hide("slow");
    });
    function BindChildTab(ResData, type) {
        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var exampleRecord = ResData[0];
        var rcount = $("#dynamic-tableChild > tbody > tr").length;

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
                cols1.push({
                    title: item.title,
                    targets: index
                });
                cols1DATA.push({
                    data: item.title,
                });
            });


            $('#dynamic-tableChild').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "bSort": false,
                "ascrollX": "100%",
                "bPaginate": false,
                "bFilter": false,
                "aColumns": [
                    null, // Assuming you want an additional column
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
                    { 'data': 'Customer' },
                    { 'data': 'Destination' },
                    { 'data': 'Invoice No' },
                    { 'data': 'Rack Type' },
                    //{ 'data': 'A-FRAME1' },
                    {
                        title: "Aframe",
                        data: 'Aframe',
                        render: function (data, type, row) {
                            var $dnid = '';

                            $dnid += '<input class="form-control " id="R18AFRAME1' + '" maxlength="200" style="width:75px; margin-left:-2px; height:37px;margin-bottom: -36px;" readonly value="' + data + '">';

                                // Empty text box with onclick event
                            $dnid += '<input class="form-control aframe" id="emptyTextBoxAFRAME1' + '" maxlength="200" style="width:75px;margin-left:80px;height: 35px;margin - top: -36px;">';
                           
                            return $dnid;
                        },
                        className: 'aframe-column-style'
                    },
                    {
                        title: "Arm",
                        data: 'Arm',
                        render: function (data, type, row) {
                            var $dnid = '';

                            $dnid += '<input class="form-control" id="R18ARM1' + '" maxlength="200" style="width:75px;margin-left:-2px; height:37px;margin-bottom: -36px;" readonly value="' + data + '">';

                                // Empty text box
                            $dnid += '<input class="form-control arm" id="emptyTextBoxARM1' + '" maxlength="200" style="width:75px;margin-left:80px;height: 35px;margin - top: -36px;">';
                            
                            return $dnid;
                        },
                        className: 'arm-column-style'
                    },

                    {
                        title: "Stopper",
                        data: 'Stopper',
                        render: function (data, type, row) {
                            var $dnid = '';

                            
                            $dnid += '<input class="form-control" id="R18STOPPER1' + '" maxlength="200" style="width:75px;  margin-left:-2px; height:37px;margin-bottom: -36px " readonly value="' + data + '">';
                              
                                // Empty text box
                            //$dnid += '<div class="Stopper-cell">';
                            $dnid += '<input class="form-control stopper" id="emptyTextBoxSTOPPER1' + '" maxlength="200" style="width:75px;margin-left:80px;height: 35px;margin - top: -36px;">';
                            //$dnid += '</div>';
                            // Readonly text box with data value


                            return $dnid;
                        },
                        className: 'stopper-column-style'
                    },

                    {
                        title: "Channel",
                        data: 'Channel',
                        render: function (data, type, row) {
                            var $dnid = '';

                            // First row with readonly input
                         //   $dnid += '<div class="channel-cell">';
                            $dnid += '<input class="form-control" id="R18CHANNEL1' + '" maxlength="200" style="width:75px; margin-left:-2px; height:37px;margin-bottom: -36px " readonly value="' + data + '">';
                           // $dnid += '</div>';

                            // Second row with empty input
                           // $dnid += '<div class="channel-cell">';
                            $dnid += '<input class="form-control channel" id="emptyTextBoxCHANNEL1' + '" maxlength="200" style="width:75px; text-align:centre; margin-left:80px;height: 35px;margin - top: -36px;">';
                           // $dnid += '</div>';

                            return $dnid;
                        },
                        className: 'channel-column-style'
                    },
                    {
                        title: "No Of Rack",
                        data: 'No Of Rack',
                        render: function (data, type, row) {
                            var $dnid = '';


                            // Empty text box
                            $dnid += '<input class="form-control channel" id="emptyTextBoxnoofrack' + '" maxlength="200" style="width:75px; margin-left:10px;  margin-top: 15px;">';

                            return $dnid;
                        }
                    },

                ],
            });
        } else {
            $('#dynamic-tableChild tbody').remove();
            $('#dynamic-tableChild thead').remove();
            $('#dynamic-tableChild').dataTable({
                "language": {
                    "emptyTable": "Receipt At Plant No Records Found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "100%", bSearchable: false, bSortable: false },
                    { sWidth: "100%", bSearchable: false, bSortable: false },
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
    $("#dynamic-tableChild").on("change", ".aframe", function () {
        
        var currentRow = $(this).closest('tr');
        var Aframe = currentRow.find('td:eq(4) input').val();
        var emptyboxframe = currentRow.find('.aframe').val(); // Use class selector

        if (emptyboxframe > Aframe) {
            bootbox.alert({
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger" style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Enter a value less than Aframe!" + '</span>',
                size: 'small',
            });
            $('.loadingGIF').hide();
            currentRow.find('.aframe').val(''); // Use class selector
        }
    });
    $("#dynamic-tableChild").on("change", ".arm", function () {
        var currentRow = $(this).closest('tr');
        var arm = currentRow.find('td:eq(5) input').val();
        var emptyboxarm = currentRow.find('.arm').val();
        if (emptyboxarm > arm) {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Enter Lesser than Arm!" + '</span>',
                size: 'small',
            });

            $('.loadingGIF').hide();
            currentRow.find('.arm').val('');
        }
    })
    $("#dynamic-tableChild").on("change", ".stopper", function () {
        var currentRow = $(this).closest('tr');
        var stopper = currentRow.find('td:eq(6) input').val();
        var emptyboxstopper = currentRow.find('.stopper').val();
        if (emptyboxstopper > stopper) {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Enter Lesser than stopper!" + '</span>',
                size: 'small',
            });
            $('.loadingGIF').hide();
            currentRow.find('.stopper').val('');
        }
    })
    $("#dynamic-tableChild").on("change", ".channel", function () {
        var currentRow = $(this).closest('tr');
        var channel = currentRow.find('td:eq(7) input').val();
        var emptyboxchannel = currentRow.find('.channel').val();
        if (emptyboxchannel > channel ) {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + "Please Enter Lesser than channel!" + '</span>',
                size: 'small',

            });
            $('.loadingGIF').hide();
            currentRow.find('.channel').val('');
        }
    })

    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);

    $("#btnsubmit").click(function () {
        $(".loader").show("slow");
        var errormessage = "";
        if ($('#ddllrno').val() == "") {
            errormessage += "Please Select LR No.</br> ";
        }
        if ($('#txtreceiveddate').val() == "") {
            errormessage += "Please Enter Received Date.</br>";
        }
        if ($('#emptyTextBoxAFRAME1').val() == "") {
            errormessage += "Please Enter Aframe Picked Qty.</br>";
        }
        if ($('#emptyTextBoxARM1').val() == "") {
            errormessage += "Please Enter Arm Picked Qty.</br>";
        }
        if ($('#emptyTextBoxSTOPPER1').val() == "") {
            errormessage += "Please Enter Stopper Picked Qty.</br>";
        }
        if ($('#emptyTextBoxCHANNEL1').val() == "") {
            errormessage += "Please Enter Channel Picked Qty.</br>";
        }
        if ($('#emptyTextBoxnoofrack').val() == "") {
            errormessage += "Please Enter No Of Rack.</br>";
        }
        if (errormessage.length == 0) {
            var intcount = 0;
            var customer = "";
            var destination = "";
            var invoice = '';
            var Racktype = "";
            var Aframe = "";
            var emptyboxframe = "";
            var arm = "";
            var emptyboxarm = "";
            var stopper = "";
            var emptyboxstopper = "";
            var channel = "";
            var emptyboxchannel = "";
            var noofrack = "";
            $("#dynamic-tableChild tbody > tr").each(function () {

                intcount++;
                debugger;
                customer += jQuery('td:eq(0)', this).text() + "^";
                destination += jQuery('td:eq(1)', this).text() + "^";
                invoice += jQuery('td:eq(2)', this).text() + "^";
                Racktype += jQuery('td:eq(3)', this).text() + "^";
                Aframe += jQuery('td:eq(4) input', this).val() + "^";
                emptyboxframe += $('#emptyTextBoxAFRAME1').val() + "^";
                arm += jQuery('td:eq(5) input', this).val() + "^";
                emptyboxarm += $('#emptyTextBoxARM1').val() + "^";
                stopper += jQuery('td:eq(6) input', this).val() + "^";
                emptyboxstopper += $('#emptyTextBoxSTOPPER1').val() + "^";
                channel += jQuery('td:eq(7) input', this).val() + "^";
                emptyboxchannel += $('#emptyTextBoxCHANNEL1').val() + "^";
                noofrack += $('#emptyTextBoxnoofrack').val() + "^";


            });
            if (intcount > 0) {
                debugger;
                customer = customer.substring(0, customer.length - 1);
                destination = destination.substring(0, destination.length - 1);
                invoice = invoice.substring(0, invoice.length - 1);
                Racktype = Racktype.substring(0, Racktype.length - 1);
                Aframe = Aframe.substring(0, Aframe.length - 1);
                emptyboxframe = emptyboxframe.substring(0, emptyboxframe.length - 1);
                arm = arm.substring(0, arm.length - 1);
                emptyboxarm = emptyboxarm.substring(0, emptyboxarm.length - 1);
                stopper = stopper.substring(0, stopper.length - 1);
                emptyboxstopper = emptyboxstopper.substring(0, emptyboxstopper.length - 1);
                channel = channel.substring(0, channel.length - 1);
                emptyboxchannel = emptyboxchannel.substring(0, emptyboxchannel.length - 1);
                noofrack = noofrack.substring(0, noofrack.length - 1);
            }


            if (window.FormData !== undefined) {
                //$('.loadingGIF').show();
                // $(".loader").show("slow");
                $.ajax({
                    url: 'ReceiptatPlantInsert',
                    dataType: "json",
                    type: "POST",


                    data: $.param({ 'LRNUMBER': $('#ddllrno').val() })
                        + '&' + $.param({ 'TRUCKNO': $('#txttruckno').val() })
                        + '&' + $.param({ 'REVERSETRANSPOTER': $('#txtreversetranspoter').val() })
                        + '&' + $.param({ 'RECEIVEDDATE': $('#txtreceiveddate').val() })
                        + '&' + $.param({ 'CUSTOMER': customer })
                        + '&' + $.param({ 'DESTINATION': destination })
                        + '&' + $.param({ 'INVOICENO': invoice })
                        + '&' + $.param({ 'RACKTYPE': Racktype })
                        + '&' + $.param({ 'AFRAMEINVOICEQTY': Aframe })
                        + '&' + $.param({ 'AFRAMEPICKUPQTY': emptyboxframe })
                        + '&' + $.param({ 'ARMINVOICEQTY': arm })
                        + '&' + $.param({ 'ARMPICKUPQTY': emptyboxarm })
                        + '&' + $.param({ 'STOPPERINVOICEQTY': stopper })
                        + '&' + $.param({ 'STOPPERPICKUPQTY': emptyboxstopper })
                        + '&' + $.param({ 'CHANNELINVOICEQTY': channel })
                        + '&' + $.param({ 'CHANNELPICKUPQTY': emptyboxchannel })
                        + '&' + $.param({ 'NOOFRACK': noofrack }),

                    success: function (data) {
                        var Res = data.split('|');
                        var result = Res[0];
                        var msg = Res[1];
                        if (result.toUpperCase() == "TRUE") {
                            //$("form").trigger("reset");
                            //$("#actiontype").text("Save")

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
                                size: 'small'
                            });
                            $('.loadingGIF').hide();
                        }
                    }
                });
            }
        } else {
            bootbox.alert({
                /*message: '<span class="class="alert alert-solid-danger">'+ msg + '</span >',*/
                message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + errormessage + '</span>',
                size: 'small'
            });
            $('.loadingGIF').hide();

        }
    });
});