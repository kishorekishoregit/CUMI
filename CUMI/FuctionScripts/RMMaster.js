jQuery(function ($) {
    ////-------------------------------------------------------------------------------------------Bind Pageload 
    //Grid Bind Value
    BindTable();
    
    $("#txtpacksize").keypress(function (e) {
        var keyCode = e.which;

        // Check if the pressed key is a numeric character (0-9)
        if (!(keyCode >= 48 && keyCode <= 57)) { // 0-9
            e.preventDefault();
        }
    });

  
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
            url: 'GetRMMasterPageLoad',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {

                var pageload = Result.split('|');
                var recordstatus = JSON.parse(pageload[0]);
                var uom = JSON.parse(pageload[1]);
                var Plant = JSON.parse(pageload[2]);
                var group = JSON.parse(pageload[3]);
                var itemsubgrp = JSON.parse(pageload[4]);
                var category = JSON.parse(pageload[5]);
                var resJ = JSON.parse(pageload[6]);
                // var resJ = JSON.parse(pageload[3]);


                $.each(recordstatus, function () {
                    $("#ddlstatus").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlstatus').trigger("chosen:updated");
                });

                $("#ddluom").empty().append('<option value="">Select an Option</option>');
                $.each(uom, function () {
                    $("#ddluom").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddluom').trigger("chosen:updated");
                });

                $("#ddlplant").empty().append('<option value="">Select an Option</option>');
                $.each(Plant, function () {
                    $("#ddlplantlst").append($("<option></option>").val(this["PLANT"]).html(this["PLANTNAME"]));
                    $('#ddlplantlst').trigger("chosen:updated");
                });

                $("#ddlgroup").empty().append('<option value="">Select an Option</option>');
                $.each(group, function () {
                    $("#ddlgroup").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlgroup').trigger("chosen:updated");
                });

                $("#ddlitemsubgroup").empty().append('<option value="">Select an Option</option>');
                $.each(itemsubgrp, function () {
                    $("#ddlitemsubgroup").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlitemsubgroup').trigger("chosen:updated");
                });

                $("#ddlcategory").empty().append('<option value="">Select an Option</option>');
                $.each(category, function () {
                    $("#ddlcategory").append($("<option></option>").val(this["METASUBCODE"]).html(this["METADATADESCRIPTION"]));
                    $('#ddlcategory').trigger("chosen:updated");
                });

                BindTab(resJ, '1');
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
                "language": { "search": "Search:" },
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


                    { className: "text-capitalize", 'data': 'Plant'},
                    // { 'data': 'Employee Code' },
                    { className: "text-capitalize", 'data': 'Location' },
                    { className: "text-capitalize", 'data': 'RM Item Code' },
                    { className: "text-capitalize", 'data': 'Variant Code' },
                    { className: "text-capitalize", 'data': 'RM Description' },
                    { className: "text-capitalize", 'data': 'UOM' },
                    { className: "text-capitalize", 'data': 'Pack Size' },
                    { className: "text-capitalize", 'data': 'Group' },
                    { className: "text-capitalize", 'data': 'Item Sub-Group' },
                    { className: "text-capitalize", 'data': 'Category' },
                    { className: "text-capitalize", 'data': 'Record Status' },
                    { className: "text-capitalize", 'data': 'Remarks' },
                    {

                        className: "text-capitalize", 'data': 'Edit',
                        'render': function (data, type, full, meta) {
                            var pakarr = data;
                            return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit" style="width: 30px;height: 30px;padding: 1px;"><i class="typcn typcn-edit" aria-hidden="true"></i></button></div><span style = "visibility:hidden; font-size: 1px;">' + pakarr + '</span>'
                            //return '<button type="button" class="btn-az-secondary editdetails" id="Edit" data-toggle="tooltip" data-placement="top" title="Edit"><i class=" typcn typcn-edit" aria-hidden="true"></i></button><span style = "visibility: hidden;" >' + data + '</span >'


                        }
                    },

                ],
                dom: 'Bfrtip',
                buttons: [

                    {
                        extend: 'excelHtml5',
                        title: 'RM Master',
                        text: '<img src="../../Images/excel.png" title="Excel" style="height: 25px;">',
                        footer: false
                    },
                    {
                        extend: 'pdfHtml5',

                        text: '<img src="../../Images/pdf.png" title="PDF" style="height: 25px;">',
                        orientation: 'landscape',
                        pageSize: 'A4',
                        footer: false,
                        title: 'RM Master'


                    }, {
                        extend: 'print',
                        title: 'RM Master',
                        text: '<img src="../../Images/print.png" title="Print" style="height: 25px;">',
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
                    "emptyTable": "RM Master No records found.."
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
    $("#ddlplant").change(function () {
        $.ajax({
            url: 'GetLocationpartcodebyID',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'partcode': $('#ddlplant').val().trim() }),
            success: function (data) {
                var Res = data.split('|');
                var plant = JSON.parse(Res[0]);
                $('#txtlocation').val(plant[0].Location);

            }

        });

    });

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
        var location = $('#txtlocation').val();
        var split = location.split('-');
        var dates3 = split[0]
        var formData = new FormData();

        if (window.FormData !== undefined) {
            $('.loadingGIF').show()
            var formData = new FormData();
            formData.append('PLANTCODE', $('#ddlplant').val());
            formData.append('LOCATION', dates3);
            formData.append('RMITEMCODE', $('#txtrmitemcode').val());
            formData.append('VARIANTCODE', $('#txtvariantcode').val());
            formData.append('DESCRIPTION', $('#txtdescription').val());
            formData.append('UOM', $('#ddluom').val());
            formData.append('PACKSIZE', $('#txtpacksize').val());
            formData.append('GROUP', $('#ddlgroup').val());
            formData.append('ITEMSUBGROUP', $('#ddlitemsubgroup').val());
            formData.append('CATEGORY', $('#ddlcategory').val());
            formData.append('RECORDSTATUS', $('#ddlstatus').val());
            formData.append('REMARKS', $('#txtremarks').val());
            formData.append('AUTOID', $("#hdautoid").val());
            formData.append('actiontype', $('#actiontype').text());
            $(".loader").show("slow");
            $.ajax({
                url: 'InsertRMMaster',
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

                        $('#txtplantcode').val("");
                        $("#txtlocation").val("");
                        $('#ddluom').val("")
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

    function Clear() {
        location.reload();
    };
    $('#btnclear').click(Clear);


    $("#dynamic-table").on("click", ".editdetails", function () {
        var autoid = $(this).closest('tr').find('td:eq(12) > span').html();
        //var EMPLOYEECODE = $(this).closest('tr').find('td:eq(0)').html();
        GetRMDetailsByID(autoid)
    });

    function GetRMDetailsByID(autoid) {
        $(".loader").show("slow");
        $.ajax({
            url: 'EditRMMaster',
            dataType: 'json',
            type: 'POST',
            data: $.param({ 'ID': autoid }),
            success: function (data) {
                var resJ = JSON.parse(data);

               

                //  $.each(resJ, function (i, item) {
                $("#hdautoid").val(resJ[0].AUTOID);
                // $("#ddlplantlst").val(resJ[0].PLANTCODE);
                $("#ddlplant").val(resJ[0].PLANTCODE).trigger('change');
                $("#txtlocation").val(resJ[0].LOCATION);
                $("#txtrmitemcode").val(resJ[0].RMITEMCODE);
                $("#txtvariantcode").val(resJ[0].VARIANTCODE);
                $("#txtdescription").val(resJ[0].DESCRIPTION);
                $("#ddluom").val(resJ[0].UOM);
                $("#ddluom").trigger('change');
                $("#txtpacksize").val(resJ[0].PACKSIZE);
                $("#ddlgroup").val(resJ[0].GROUP);
                $("#ddlgroup").trigger('change');
               // $("#ddlitemsubgroup").val(resJ[0].ITEMSUBGROUP);
              //  $("#ddlitemsubgroup").trigger('change');
               // $("#ddlitemsubgroup").val(resJ[0].ITEMSUBGROUP).trigger('change');
                $('#ddlitemsubgroup').val(resJ[0].ITEMSUBGROUP.split(',')).trigger('change');
                $("#ddlcategory").val(resJ[0].CATEGORY);
                $("#ddlcategory").trigger('change');             
                $('#ddlstatus').val(resJ[0].RECORDSTATUS)
                $('#ddlstatus').trigger('change');
                $("#txtremarks").val(resJ[0].REMARKS);
                $("#actiontype").text("Update");

            }

        });

        $(".loader").hide("slow");
    }


    $("#txtmailid").change(function () {
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var emailaddress = $("#txtmailid").val();
        if (!emailReg.test(emailaddress)) {
            alert("Please Enter the Correct  EmailId");
            $('#txtmailid').val("");
            $('#txtmailid').focus();
        }
    });

    $("#txtnumber").change(function () {

        var mobileNum = $("#txtnumber").val();
        var validateMobNum = /^\d*(?:\.\d{1,2})?$/;
        if (validateMobNum.test(mobileNum) && mobileNum.length == 10) {
            //alert("Valid Mobile Number");
        }
        else {
            alert("Invalid Mobile Number");
            $("#txtnumber").val("");
        }


    });
});