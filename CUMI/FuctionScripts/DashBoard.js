jQuery(function ($) {
    var moldred = "";
    function callpageload() {
        jQuery.ajax({
            url: 'Home/GetDashboardDetails',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (Result) {
                debugger;
                var pageload = Result.split('|');

                 moldred = JSON.parse(pageload[0]);
                var moldalert = JSON.parse(pageload[1]);
                var RDC = JSON.parse(pageload[2]);
                var PD = JSON.parse(pageload[3]);
                var redmoldlist = JSON.parse(pageload[4]);
                var alertmoldlist = JSON.parse(pageload[5]);
                var RDCPiclist = JSON.parse(pageload[6]);
                var MoldPicklist = JSON.parse(pageload[7]);
            
                $('#totalmoldred').text(moldred[0].NoOfMoldReachPMCount);
                $('#totalmoldalert').text(moldalert[0]["No.Of.Mold Reach Alert Count"]);
                $('#totalrdc').text(RDC[0]["Pending RDC Mold List"]);
                $('#totalpd').text(PD[0]["Pending Production Mold List"]);
           
                InOutTablrdetails(redmoldlist, '1');
                InOutLogdetails(alertmoldlist, '1');
                RDCPicklistGrid(RDCPiclist, '1');
                MoldPicklistGridView(MoldPicklist, '1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Status: " + textStatus); alert("Error: " + errorThrown);
            }
        });
    }
    callpageload();


    function RDCPicklistGrid(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-RDCPicklistGridtable tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-RDCPicklistGridtable').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-RDCPicklistGridtable').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "search :" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    //'searchable': false,
                    //'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'RDC Pick List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-RDCPicklistGridtable tbody').remove();
            $('#dynamic-RDCPicklistGridtable thead').remove();
            $('#dynamic-RDCPicklistGridtable').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                //"info": false,
                //"paging": false,
                //"searching": false
            });

        }

    }
    function MoldPicklistGridView(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-MoldPicklisttable tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-MoldPicklisttable').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-MoldPicklisttable').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "search :" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    //'searchable': false,
                    //'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Mold Pick List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-MoldPicklisttable tbody').remove();
            $('#dynamic-MoldPicklisttable thead').remove();
            $('#dynamic-MoldPicklisttable').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                //"info": false,
                //"paging": false,
                //"searching": false
            });

        }

    }
    function InOutTablrdetails(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-table tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-table').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-table').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "search :" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    //'searchable': false,
                    //'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Mold Details Reached PM Count',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
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
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                //"info": false,
                //"paging": false,
                //"searching": false
            });

        }

    }
    function InOutLogdetails(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailsinward tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailsinward').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailsinward').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "search :" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Mould Details Reached Alert Count',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailsinward tbody').remove();
            $('#dynamic-tableDetailsinward thead').remove();
            $('#dynamic-tableDetailsinward').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": false
            });

        }

    }
    var flotSampleData6 = [
        [0, 52.0372905861701],
        [1, 51.6784633777384],
        [2, 50.93706057748914],
        [3, 55.04861395738878],
        [4, 52.445845968822006],
        [5, 50.93681261537495],
        [6, 55.41621667661467],
        [7, 57.10873028593814],
        [8, 54.09853093352003],
        [9, 57.63062912168092],
        [10, 61.76480188676403],
        [11, 59.58083036192488],
        [12, 63.457788916796034],
        [13, 62.954499198759066],
        [14, 59.19255443449066],
        [15, 71.107062594673295],
        [16, 71.797400811178974],
        [17, 74.60795847250468],
        [18, 64.94400805384853],
        [19, 61.50219489844255],
        [20, 61.93683013860428],
        [21, 42.12178115474478],
        [22, 44.02455260206408],
        [23, 41.58732990833391],
        [24, 63.551262643229364],
        [25, 65.22361572255349],
        [26, 60.441963794914685],
        [27, 64.64029619252607],
        [28, 62.426770751912244],
        [29, 65.57029913775051]
    ];
    var flotSampleData7 = [
        [0, 49.94286358017402],
        [1, 54.75195271853288],
        [2, 59.622053296327735],
        [3, 60.738689215257594],
        [4, 56.70698194695498],
        [5, 57.52045722160073],
        [6, 55.4934878455616],
        [7, 58.43501666521898],
        [8, 57.525488680182036],
        [9, 55.80777569057727],
        [10, 53.682652257555645],
        [11, 54.81436164727582],
        [12, 55.59622041652305],
        [13, 56.550206500228064],
        [14, 58.12076521746503],
        [15, 59.03652643269743],
        [16, 62.50683301850617],
        [17, 59.48044106699237],
        [18, 60.22405802611539],
        [19, 55.75619342134348],
        [20, 54.27524732322225],
        [21, 49.460602930912856],
        [22, 47.34020893802209],
        [23, 50.1570575505057],
        [24, 49.823374599769124],
        [25, 50.42642956481375],
        [26, 53.119011480591055],
        [27, 54.465212016350385],
        [28, 51.37591924922336],
        [29, 49.66602279516306]
    ];

    var flotSampleData8 = [
        [0, 51.35337906430415],
        [1, 55.09767736474683],
        [2, 56.11713418071085],
        [3, 56.62830445362504],
        [4, 58.374859207924956],
        [5, 62.842829855606894],
        [6, 63.69291962857514],
        [7, 60.69251163731542],
        [8, 61.650299044110085],
        [9, 64.06410201262507],
        [10, 67.43880456193354],
        [11, 70.2095435826324],
        [12, 73.01907211149363],
        [13, 75.305867265774],
        [14, 73.15232973097093],
        [15, 75.67663616265044],
        [16, 77.50025675637558],
        [17, 74.56982822506586],
        [18, 78.22708826685283],
        [19, 75.88418124127114],
        [20, 78.96304160187246],
        [21, 83.19746269424613],
        [22, 81.99514960164132],
        [23, 80.34748479228385],
        [24, 83.01785617267964],
        [25, 80.47961270294873],
        [26, 81.43180744942623],
        [27, 80.54908115981],
        [28, 80.89790184638714],
        [29, 82.57585847055765]
    ];

    var flotSampleData9 = [
        [0, 51.99566242992652],
        [1, 56.40988734156261],
        [2, 52.712016949483605],
        [3, 52.13903665420402],
        [4, 55.38856697356215],
        [5, 56.51241090203006],
        [6, 54.13065859506406],
        [7, 54.52096980767574],
        [8, 58.778974107485055],
        [9, 55.51439929034389],
        [10, 56.957814217917],
        [11, 57.61073578735697],
        [12, 59.7297766750641],
        [13, 61.93295319184848],
        [14, 62.50241769531192],
        [15, 60.542234578733925],
        [16, 58.29300184711166],
        [17, 55.342699297074184],
        [18, 58.10368017734648],
        [19, 55.992767202287844],
        [20, 59.85513950723005],
        [21, 55.06877119665919],
        [22, 54.32937925983862],
        [23, 55.85921051952968],
        [24, 55.51272173544296],
        [25, 53.28302387501565],
        [26, 49.99125994698088],
        [27, 45.20738945047653],
        [28, 46.435525588283454],
        [29, 41.869140235144116]
    ];

    $.plot('#flotChart1', [{
        data: flotSampleData6,
        color: '#fff'
    }], {
        series: {
            shadowSize: 0,
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: { colors: [{ opacity: 0 }, { opacity: 0.1 }] }
            }
        },
        grid: {
            borderWidth: 0,
            labelMargin: 0
        },
        yaxis: {
            show: false,
            min: 0,
            max: 120
        },
        xaxis: { show: false }
    });

    $.plot('#flotChart2', [{
        data: flotSampleData7,
        color: '#fff'
    }], {
        series: {
            shadowSize: 0,
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: { colors: [{ opacity: 0 }, { opacity: 0.2 }] }
            }
        },
        grid: {
            borderWidth: 0,
            labelMargin: 0
        },
        yaxis: {
            show: false,
            min: 0,
            max: 120
        },
        xaxis: { show: false }
    });

    $.plot('#flotChart3', [{
        data: flotSampleData8,
        color: '#fff'
    }], {
        series: {
            shadowSize: 0,
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: { colors: [{ opacity: 0 }, { opacity: 0.2 }] }
            }
        },
        grid: {
            borderWidth: 0,
            labelMargin: 0
        },
        yaxis: {
            show: false,
            min: 0,
            max: 120
        },
        xaxis: { show: false }
    });

    $.plot('#flotChart4', [{
        data: flotSampleData9,
        color: '#fff'
    }], {
        series: {
            shadowSize: 0,
            lines: {
                show: true,
                lineWidth: 2,
                fill: true,
                fillColor: { colors: [{ opacity: 0 }, { opacity: 0.2 }] }
            }
        },
        grid: {
            borderWidth: 0,
            labelMargin: 0
        },
        yaxis: {
            show: false,
            min: 0,
            max: 120
        },
        xaxis: { show: false }
    });

    setInterval(function () {
        // callpageload();
    }, 30);


    $("#ddlunitcode").change(function () {
        $.ajax({
            url: 'Home/GetDashboardbycompanycode',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: $.param({ 'unitcode': $("#ddlunitcode").val() }),
            success: function (Result) {


                var pageload = Result.split('|');

                var trolleydetails = JSON.parse(pageload[0]);
                var trolleyin = JSON.parse(pageload[1]);
                var trolleyout = JSON.parse(pageload[2]);
                var inoutcount = JSON.parse(pageload[3]);

                setTimeout(function () {
                    var url = '../Login';
                    window.location.href = url;

                }, 300);

                $('#totaltrollyshowroom').text(trolleydetails[0]["TOTALCOUNT"]);
                $('#introllyshowroom').text(trolleyin[0]["INCOUNT"]);
                $('#outtrollyshowroom').text(trolleyout[0]["OUTCOUNT"]);

                InOutTablrdetails(inoutcount, '1');

            }
        });
    });


    $('#totaltrollyshowroom').click(function () {

        callinwardpopup();
    })

    function callinwardpopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardTotalDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[0]);

                if (resJ.length > 0) {
                    BindPopupinwardTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopupinwardTab(resJ, '0');


                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopupinwardTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailstotal tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailstotal').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailstotal').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    // 'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total IN-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailstotal tbody').remove();
            $('#dynamic-tableDetailstotal thead').remove();
            $('#dynamic-tableDetailstotal').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": false,
                "paging": false,
                //"searching": false
            });

        }

    }


    $('#introllyshowroom').click(function () {
        callpalletepopup();
    })

    function callpalletepopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardInDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[0]);

                if (resJ.length > 0) {
                    BindPopuppalleteTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopuppalleteTab(resJ, '0');

                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopuppalleteTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailsinward tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailsinward').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailsinward').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //    { 'data': 'INDATE' },
                //    { 'data': 'INTIME' },
                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total IN-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailsinward tbody').remove();
            $('#dynamic-tableDetailsinward thead').remove();
            $('#dynamic-tableDetailsinward').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": false
            });

        }

    }


    $('#outtrollyshowroom').click(function () {

        callbinpopup();
    })

    function callbinpopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardOutDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[0]);

                if (resJ.length > 0) {
                    BindPopupbinTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopupbinTab(resJ, '0');

                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopupbinTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailsoutward tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailsoutward').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailsoutward').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //    { 'data': 'EMPLOYEE' },
                //    { 'data': 'OUTDATE' },
                //    { 'data': 'OUTTIME' },

                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total OUT-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailsoutward tbody').remove();
            $('#dynamic-tableDetailsoutward thead').remove();
            $('#dynamic-tableDetailsoutward').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": false
            });

        }

    }



    $('#totaltrollystore').click(function () {

        callStoreinwardpopup();
    })
    function callStoreinwardpopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardTotalDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[1]);

                if (resJ.length > 0) {
                    BindPopupStoreinwardTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopupStoreinwardTab(resJ, '0');


                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopupStoreinwardTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailsstoretotal tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailsstoretotal').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailsstoretotal').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    // 'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total IN-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailsstoretotal tbody').remove();
            $('#dynamic-tableDetailsstoretotal thead').remove();
            $('#dynamic-tableDetailsstoretotal').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": false,
                "paging": false,
                //"searching": false
            });

        }

    }


    $('#introllystore').click(function () {
        callpalleteinstorepopup();
    })

    function callpalleteinstorepopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardInDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[1]);

                if (resJ.length > 0) {
                    BindPopupinstorepalleteTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopupinstorepalleteTab(resJ, '0');

                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopupinstorepalleteTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailsstoreinward tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailsstoreinward').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailsstoreinward').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //    { 'data': 'INDATE' },
                //    { 'data': 'INTIME' },
                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total IN-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailsstoreinward tbody').remove();
            $('#dynamic-tableDetailsstoreinward thead').remove();
            $('#dynamic-tableDetailsstoreinward').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": false
            });

        }

    }


    $('#outtrollystore').click(function () {

        callbinoutstorepopup();
    })

    function callbinoutstorepopup() {
        $(".loader").show("slow");
        $.ajax({
            url: 'Home/GetDashboardOutDetails',
            dataType: "json",
            type: "POST",
            success: function (Result) {

                var pageload = Result.split('|');
                var resJ = JSON.parse(pageload[1]);

                if (resJ.length > 0) {
                    BindPopupoutstorebinTab(resJ, '1');
                    $("html, body").animate({ scrollTop: 0 }, "slow");
                }
                else {
                    BindPopupoutstorebinTab(resJ, '0');

                }

                $('.loadingGIF').hide();

            }

        });
        $(".loader").hide("slow");
    }
    function BindPopupoutstorebinTab(ResData, type) {

        var cols = [];
        var cols1 = [];
        var cols1DATA = [];
        var nullvalue;
        var colsresult;
        var finalcolsresult;
        var elements = Array();
        var exampleRecordUpload = ResData[0];
        // TABLE BIND
        var len = $('#dynamic-tableDetailstoreoutward tbody').children().length;
        if (type == '0' || len != '0') {
            $('#dynamic-tableDetailstoreoutward').dataTable().fnDestroy();
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

            //  var dyTable1 = $('#dynamic-tableUpload');

            $('#dynamic-tableDetailstoreoutward').dataTable({
                data: ResData,
                "bAutoWidth": false,
                "info": false,
                "ascrollX": "100%",
                "language": { "search": "" },
                "aColumns": [
                    finalcolsresult,
                    { "bSortable": false }
                ],
                "aSorting": [],
                'bSort': false,
                'columnDefs': [{
                    'targets': 0,
                    'searchable': true,
                    'orderable': false,
                    'className': 'dt-body-center'
                }],

                columnDefs: cols1,
                //columns: [
                //    { 'data': 'SLNO' },
                //    { 'data': 'ASSETNO' },
                //    { 'data': 'EMPLOYEE' },
                //    { 'data': 'OUTDATE' },
                //    { 'data': 'OUTTIME' },

                //],
                columns: cols1DATA,
                dom: 'tfp',
                buttons: [
                    {
                        extend: 'excelHtml5',
                        title: 'Total OUT-Asset List',
                        text: '<img src="../../assets/images/excel.png" style="height: 25px;">',
                        footer: false
                    }
                ]

            });
        }
        else {
            $('#dynamic-tableDetailstoreoutward tbody').remove();
            $('#dynamic-tableDetailstoreoutward thead').remove();
            $('#dynamic-tableDetailstoreoutward').dataTable({
                "language": {
                    "emptyTable": "No records found.."
                },
                'bSort': false,
                'aoColumns': [
                    { sWidth: "10%", bSearchable: false, bSortable: false },
                    { sWidth: "10", bSearchable: false, bSortable: false },

                ],
                "scrollCollapse": false,
                "info": true,
                "paging": true,
                "searching": false
            });

        }

    }

});