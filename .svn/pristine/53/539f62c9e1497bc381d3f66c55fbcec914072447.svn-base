jQuery(function ($) {
    $("input").on("keypress", function (e) {
        if ($.trim($(this).val()) == 0) {
            if (e.which === 32 && !this.value.length)
                e.preventDefault();
        }
    });
    $("textarea").on("keypress", function (e) {
        if ($.trim($(this).val()) == 0) {
            if (e.which === 32 && !this.value.length)
                e.preventDefault();
        }
    });
    $("#loadingid").hide();
    function buildMenu(parent, items) {
        var childli; var parentli;
        var SUBparentli;
        $.each(items, function () {
            if (this.ParentId == "") {
                var id = this.Id;
                parentli = $('<li class="' + this.BGCOLOR + '"><a href="' + this.URL + '" class="dropdown-toggle"><i class="menu-icon ' + this.CSSSUBClass + '"> <span class="icon-bg ' + this.BGCOLOR + '"></span></i><span class="menu-text">' + this.MenuText + '</span><b class="arrow fa fa-angle-down"></b></a></li>');
                parentli.appendTo(parent);
                if (this.List && this.List.length > 0) {
                    var ul = $('<ul class="submenu"></ul>');
                    ul.appendTo(parentli);
                    buildMenu(ul, this.List);
                }
                if (!this.Active) {
                    parentli.addClass('ui-state-disabled');
                }
            }
            else {
                if (this.SUBMENU == "SUB") {
                    var id = this.Id;
                    SUBparentli = $('<li ><a href="#" class="dropdown-toggle"><i class="menu-icon ' + this.CSSSUBClass + '"></i><span class="menu-text">' + this.MenuText + '</span><b class="arrow fa fa-angle-down"></b></a></li>');
                    SUBparentli.appendTo(parent);
                    if (this.List && this.List.length > 0) {
                        var ul = $('<ul class="submenu"></ul>');
                        ul.appendTo(SUBparentli);
                        buildMenu(ul, this.List);
                    }
                    if (!this.Active) {
                        SUBparentli.addClass('ui-state-disabled');
                    }
                }
                else {
                    childli = $('<li ><a href="' + this.URL + '"><i class="menu-icon fa fa-circle"></i><span class="menu-text">' + this.MenuText + '</span></a><b class="arrow"></b></li>');
                    childli.appendTo(parent);
                    if (this.List && this.List.length > 0) {
                        var ul = $('<ul class="submenu"></ul>');
                        ul.appendTo(childli);
                        buildMenu(ul, this.List);
                    }
                    if (!this.Active) {
                        childli.addClass('ui-state-disabled');
                    }
                }
            }
            if ($('li ul').hasClass('submenu')) {
                $('.submenu').parents('li').addClass('haschild parentli');
            }
            else {
                $('#menuul li').addClass('nochild parentli');
            }
            if ($('#menuul li').hasClass('nochild')) {
                $('#menuul li.nochild a').removeClass('dropdown-toggle');
                $('#menuul li.nochild a b').remove();
            }
        });
        setNavigation();
    }
    if ($('.page-content h3').hasClass('header')) {
        $('.page-content h3').parent('.col-lg-12').addClass('detail-pane');
        $('.page-content h3').parent('.col-xs-12').addClass('detail-pane');
    }
    $('.widget-box1').addClass('clearfix');
    $('#id-disable-check').on('click', function () {
        var inp = $('#form-input-readonly').get(0);
        if (inp.hasAttribute('disabled')) {
            inp.setAttribute('readonly', 'true');
            inp.removeAttribute('disabled');
            inp.value = "This text field is readonly!";
        }
        else {
            inp.setAttribute('disabled', 'disabled');
            inp.removeAttribute('readonly');
            inp.value = "This text field is disabled!";
        }
    });
    if (!ace.vars['touch']) {
        $('.chosen-select').chosen({ allow_single_deselect: true });
        //resize the chosen on window resize
        $(window)
        .off('resize.chosen')
        .on('resize.chosen', function () {
            $('.chosen-select').each(function () {
                var $this = $(this);
                $this.next().css({ 'width': $this.parent().width() });
            })
        }).trigger('resize.chosen');
        //resize chosen on sidebar collapse/expand
        $(document).on('settings.ace.chosen', function (e, event_name, event_val) {
            if (event_name != 'sidebar_collapsed') return;
            $('.chosen-select').each(function () {
                var $this = $(this);
                $this.next().css({ 'width': $this.parent().width() });
            })
        });
        $('#chosen-multiple-style .btn').on('click', function (e) {
            var target = $(this).find('input[type=radio]');
            var which = parseInt(target.val());
            if (which == 2) $('#form-field-select-4').addClass('tag-input-style');
            else $('#form-field-select-4').removeClass('tag-input-style');
        });
    }
    $('[data-rel=tooltip]').tooltip({ container: 'body' });
    $('[data-rel=popover]').popover({ container: 'body' });
    autosize($('textarea[class*=autosize]'));
    $('textarea.limited').inputlimiter({
        remText: '%n character%s remaining...',
        limitText: 'max allowed : %n.'
    });
    $.mask.definitions['~'] = '[+-]';
    $('.input-mask-date').mask('99/99/9999');
    $('.input-mask-phone').mask('(999) 999-9999');
    $('.input-mask-eyescript').mask('~9.99 ~9.99 999');
    $(".input-mask-product").mask("a*-999-a999", { placeholder: " ", completed: function () { alert("You typed the following: " + this.val()); } });
    $("#input-size-slider").css('width', '200px').slider({
        value: 1,
        range: "min",
        min: 1,
        max: 8,
        step: 1,
        slide: function (event, ui) {
            var sizing = ['', 'input-sm', 'input-lg', 'input-mini', 'input-small', 'input-medium', 'input-large', 'input-xlarge', 'input-xxlarge'];
            var val = parseInt(ui.value);
            $('#form-field-4').attr('class', sizing[val]).attr('placeholder', '.' + sizing[val]);
        }
    });
    $("#input-span-slider").slider({
        value: 1,
        range: "min",
        min: 1,
        max: 12,
        step: 1,
        slide: function (event, ui) {
            var val = parseInt(ui.value);
            $('#form-field-5').attr('class', 'col-xs-' + val).val('.col-xs-' + val);
        }
    });
    //"jQuery UI Slider"
    //range slider tooltip example
    $("#slider-range").css('height', '200px').slider({
        orientation: "vertical",
        range: true,
        min: 0,
        max: 100,
        values: [17, 67],
        slide: function (event, ui) {
            var val = ui.values[$(ui.handle).index() - 1] + "";
            if (!ui.handle.firstChild) {
                $("<div class='tooltip right in' style='display:none;left:16px;top:-6px;'><div class='tooltip-arrow'></div><div class='tooltip-inner'></div></div>")
                .prependTo(ui.handle);
            }
            $(ui.handle.firstChild).show().children().eq(1).text(val);
        }
    }).find('span.ui-slider-handle').on('blur', function () {
        $(this.firstChild).hide();
    });
    $("#slider-range-max").slider({
        range: "max",
        min: 1,
        max: 10,
        value: 2
    });
    $("#slider-eq > span").css({ width: '90%', 'float': 'left', margin: '15px' }).each(function () {
        // read initial values from markup and remove that
        var value = parseInt($(this).text(), 10);
        $(this).empty().slider({
            value: value,
            range: "min",
            animate: true
        });
    });
    $("#slider-eq > span.ui-slider-purple").slider('disable');//disable third item
    $('.id-input-file-1 , .id-input-file-2').ace_file_input({
        no_file: 'No File ...',
        btn_choose: 'Choose',
        btn_change: 'Change',
        droppable: false,
        onchange: null,
        thumbnail: false //| true | large
        //whitelist:'gif|png|jpg|jpeg'
        //blacklist:'exe|php'
        //onchange:''
        //
    });
    //pre-show a file name, for example a previously selected file
    //$('#id-input-file-1').ace_file_input('show_file_list', ['myfile.txt'])
    $('.id-input-file-3').ace_file_input({
        style: 'well',
        btn_choose: 'Drop files here or click to choose',
        btn_change: null,
        no_icon: 'ace-icon fa fa-cloud-upload',
        droppable: true,
        thumbnail: 'small'//large | fit
        ,
        preview_error: function (filename, error_code) {
        }
    }).on('change', function () {
        //console.log($(this).data('ace_input_files'));
        //console.log($(this).data('ace_input_method'));
    });
    //$('#id-input-file-3')
    //.ace_file_input('show_file_list', [
    //{type: 'image', name: 'name of image', path: 'http://path/to/image/for/preview'},
    //{type: 'file', name: 'hello.txt'}
    //]);
    //dynamically change allowed formats by changing allowExt && allowMime function
    $('#id-file-format').removeAttr('checked').on('change', function () {
        var whitelist_ext, whitelist_mime;
        var btn_choose
        var no_icon
        if (this.checked) {
            btn_choose = "Drop images here or click to choose";
            no_icon = "ace-icon fa fa-picture-o";
            whitelist_ext = ["jpeg", "jpg", "png", "gif", "bmp"];
            whitelist_mime = ["image/jpg", "image/jpeg", "image/png", "image/gif", "image/bmp"];
        }
        else {
            btn_choose = "Drop files here or click to choose";
            no_icon = "ace-icon fa fa-cloud-upload";
            whitelist_ext = null;//all extensions are acceptable
            whitelist_mime = null;//all mimes are acceptable
        }
        var file_input = $('#id-input-file-3');
        file_input
        .ace_file_input('update_settings',
        {
            'btn_choose': btn_choose,
            'no_icon': no_icon,
            'allowExt': whitelist_ext,
            'allowMime': whitelist_mime
        })
        file_input.ace_file_input('reset_input');
        file_input
        .off('file.error.ace')
        .on('file.error.ace', function (e, info) {
        });
    });
    $('#spinner1').ace_spinner({ value: 0, min: 0, max: 200, step: 10, btn_up_class: 'btn-info', btn_down_class: 'btn-info' })
    .closest('.ace-spinner')
    .on('changed.fu.spinbox', function () {
        //console.log($('#spinner1').val())
    });
    $('#spinner2').ace_spinner({ value: 0, min: 0, max: 10000, step: 100, touch_spinner: true, icon_up: 'ace-icon fa fa-caret-up bigger-110', icon_down: 'ace-icon fa fa-caret-down bigger-110' });
    $('#spinner3').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus bigger-110', icon_down: 'ace-icon fa fa-minus bigger-110', btn_up_class: 'btn-success', btn_down_class: 'btn-danger' });
    $('#spinner4').ace_spinner({ value: 0, min: -100, max: 100, step: 10, on_sides: true, icon_up: 'ace-icon fa fa-plus', icon_down: 'ace-icon fa fa-minus', btn_up_class: 'btn-purple', btn_down_class: 'btn-purple' });
    //datepicker plugin
    //link
    $('.date-picker').datetimepicker({
        format: 'DD/MM/YYYY',
    });
    //or change it into a date range picker
    $('.input-daterange').datepicker({ autoclose: true });
    $('.clatype').datetimepicker({
        format: 'DD/MM/YYYY',
    });
    //to translate the daterange picker, please copy the "examples/daterange-fr.js" contents here before initialization
    $('input[name=date-range-picker]').daterangepicker({
        'applyClass': 'btn-sm btn-success',
        'cancelClass': 'btn-sm btn-default',
        locale: {
            applyLabel: 'Apply',
            cancelLabel: 'Cancel',
        }
    })
    .prev().on(ace.click_event, function () {
        $(this).next().focus();
    });
    $('.timepicker1').timepicker({
        minuteStep: 1,
        showSeconds: false,
        showMeridian: true,
        disableFocus: true,
        icons: {
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down'
        }
    }).on('focus', function () {
        $('#timepicker1').timepicker('showWidget');
    }).next().on(ace.click_event, function () {
        $(this).prev().focus();
    });
    if (!ace.vars['old_ie']) $('.date-timepicker1').datetimepicker({
        //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
        showClose: true,
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-arrows ',
            clear: 'fa fa-trash',
            close: 'fa fa-times-circle'
        }
    }).next().on(ace.click_event, function () {
        $(this).prev().focus();
    });
    if (!ace.vars['old_ie']) $('.date-timepickerasn').datetimepicker({
        //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
        showClose: true,
        defaultDate: 'now',
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-arrows ',
            clear: 'fa fa-trash',
            close: 'fa fa-times-circle'
        }
    }).next().on(ace.click_event, function () {
        $(this).prev().focus();
    });
    if (!ace.vars['old_ie']) $('.date-timepickerevent').datetimepicker({
        //format: 'MM/DD/YYYY h:mm:ss A',//use this option to display seconds
        showClose: true,
        useCurrent: false,
        defaultDate: 'now',
        icons: {
            time: 'fa fa-clock-o',
            date: 'fa fa-calendar',
            up: 'fa fa-chevron-up',
            down: 'fa fa-chevron-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-arrows ',
            clear: 'fa fa-trash',
            close: 'fa fa-times-circle'
        }
    }).next().on(ace.click_event, function () {
        $(this).prev().focus();
    });
    $("#eventdatetime").on("dp.show", function (e) {
        $('#eventdatetime').data("DateTimePicker").minDate(e.date);
    });
    $('#colorpicker1').colorpicker();
    //$('.colorpicker').last().css('z-index', 2000);//if colorpicker is inside a modal, its z-index should be higher than modal'safe
    $('#simple-colorpicker-1').ace_colorpicker();
    $(".knob").knob();
    var tag_input = $('#form-field-tags');
    try {
        tag_input.tag(
          {
              placeholder: tag_input.attr('placeholder'),
              //enable typeahead by specifying the source array
              source: ace.vars['US_STATES'],//defined in ace.js >> ace.enable_search_ahead
          }
        )
        //programmatically add/remove a tag
        var $tag_obj = $('#form-field-tags').data('tag');
        $tag_obj.add('Programmatically Added');
        var index = $tag_obj.inValues('some tag');
        $tag_obj.remove(index);
    }
    catch (e) {
        //display a textarea for old IE, because it doesn't support this plugin or another one I tried!
        tag_input.after('<textarea id="' + tag_input.attr('id') + '" name="' + tag_input.attr('name') + '" rows="3">' + tag_input.val() + '</textarea>').remove();
        //autosize($('#form-field-tags'));
    }
    /////////
    $('#modal-form input[type=file]').ace_file_input({
        style: 'well',
        btn_choose: 'Drop files here or click to choose',
        btn_change: null,
        no_icon: 'ace-icon fa fa-cloud-upload',
        droppable: true,
        thumbnail: 'large'
    })
    //chosen plugin inside a modal will have a zero width because the select element is originally hidden
    //and its width cannot be determined.
    //so we set the width after modal is show
    $('#modal-form').on('shown.bs.modal', function () {
        if (!ace.vars['touch']) {
            $(this).find('.chosen-container').each(function () {
                $(this).find('a:first-child').css('width', '210px');
                $(this).find('.chosen-drop').css('width', '210px');
                $(this).find('.chosen-search input').css('width', '200px');
            });
        }
    })
    $(document).one('ajaxloadstart.page', function (e) {
        autosize.destroy('textarea[class*=autosize]')
        $('.limiterBox,.autosizejs').remove();
        $('.daterangepicker.dropdown-menu,.colorpicker.dropdown-menu,.bootstrap-datetimepicker-widget.dropdown-menu').remove();
    });
    //Keyupfunction for vendorcode,vendorname,partcode,partnmae
    $('#vendorcode').autocomplete({
        source: function (request, response) {
            var vendorcode = $('#vendorcode').val();
            var VENDORCODERESULT = "";
            var VENDORCODE1;
            $('.error').remove();
            $.ajax({
                url: '../Admin/ChosenGetVendorCode',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result != "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.VENDORCODE, value: item.VENDORCODE };
                        }));
                    }
                    else {
                        $('#vendorcode').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $('#vendorname').autocomplete({
        source: function (request, response) {
            var vendorname = $('#vendorname').val();
            var VENDORNAMEresult = "";
            var VENDORNAME1;
            $('.error').remove();
            $.ajax({
                url: '../Admin/ChosenGetVendorName',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'VendorName=' + vendorname,
                success: function (Result) {
                    if (Result != "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.VENDORNAME, value: item.VENDORNAME };
                        }));
                    }
                    else {
                        $('#vendorname').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#partno").autocomplete({
        source: function (request, response) {
            var partno = $('#partno').val();
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            var prtnoresult = "";
            var prtno1;
            $.ajax({
                url: '../Admin/ChosenGetPartNo',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'Partno=' + partno + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result != "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.PARTNO, value: item.PARTNO };
                        }));
                    }
                    else {
                        $('#partno').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#partname").autocomplete({
        source: function (request, response) {
            var prtnmresult = "";
            var prtnm1;
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            $.ajax({
                url: '../Admin/ChosenGetPartname',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'Partname=' + $('#partname').val() + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result !== "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.PARTNAME, value: item.PARTNAME };
                        }));
                    }
                    else {
                        $('#partname').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#plantcode").autocomplete({
        source: function (request, response) {
            var plantcoderesult = "";
            var plantcode1;
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            $.ajax({
                url: '../Stores/ChosenPlantCode',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'PlantCode=' + $('#plantcode').val() + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result !== "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.PLANTCODE, value: item.PLANTCODE };
                        }));
                    }
                    else {
                        $('#plantcode').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#grnno").autocomplete({
        source: function (request, response) {
            var grnresult = "";
            var grn1;
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            $.ajax({
                url: '../Stores/ChosenGetGrnNo',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'GrnNo=' + $('#grnno').val() + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result !== "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.GRNNO, value: item.GRNNO };
                        }));
                    }
                    else {
                        $('#grnno').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#pono").autocomplete({
        source: function (request, response) {
            var ponoresult = "";
            var pono1;
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            $.ajax({
                url: '../Admin/ChosenSearchByPONo',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'pono=' + $('#pono').val() + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result !== "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.pono, value: item.pono };
                        }));
                    }
                    else {
                        $('#pono').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    $("#asnno").autocomplete({
        source: function (request, response) {
            var Asnresult = "";
            var Asn1;
            var vendorcode = $('#vendorcode').val();
            if (vendorcode != "undefined") {
                vendorcode = $('#vendorcode').val();
            }
            else {
                vendorcode = '';
            }
            $.ajax({
                url: '../Stores/ChosenAsnId',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "GET",
                data: 'AsnId=' + $('#asnno').val() + '&Vendorcode=' + vendorcode,
                success: function (Result) {
                    if (Result !== "[]") {
                        var resJ = JSON.parse(Result);
                        response($.map(resJ, function (item) {
                            return { label: item.ADVANCESHIPMENTNOTICEID, value: item.ADVANCESHIPMENTNOTICEID };
                        }));
                    }
                    else {
                        $('#asnno').val('');
                        sweetAlert("No Entries Found");
                    }
                }
            });
        }
    });
    //changefunction for vendorcode,vendorname,partcode,partnmae
    $('#vendorcode').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#vendorcode").val('');
        }
        $("#loadingid").show("slow");
        var vendorcode = $('#vendorcode').val();
        $('.error').remove();
        $.ajax({
            url: '../Admin/ChangeGetVendorCode',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'Vendorcode=' + vendorcode,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#vendorname").empty();
                    $("#vendorname").val(result[0]["VENDORNAME"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $('#vendorname').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#vendorname").val('');
        }
        $("#loadingid").show("slow");
        var vendorname = $('#vendorname').val();
        $.ajax({
            url: '../Admin/ChangeGetVendorName',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'VendorName=' + vendorname,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#vendorcode").empty();
                    $("#vendorcode").val(result[0]["VENDORCODE"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $('#partno').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#partno").val('');
        }
        $("#loadingid").show("slow");
        var partno = $('#partno').val();
        $.ajax({
            url: '../Admin/ChangeGetPartNo',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'Partno=' + partno,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#partname").empty();
                    $("#partname").val(result[0]["PARTNAME"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $('#partname').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#partname").val('');
        }
        $("#loadingid").show("slow");
        var partname = $('#partname').val();
        $.ajax({
            url: '../Admin/ChangeGetPartname',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'Partname=' + partname,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#partno").empty();
                    $("#partno").val(result[0]["PARTNO"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $('#plantcode').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#plantcode").val('');
        }
        $("#loadingid").show("slow");
        var plantcode = $('#plantcode').val();
        $.ajax({
            url: '',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'PlantCode=' + plantcode,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#plantcode").empty();
                    $("#plantcode").val(result[0]["PLANTCODE"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $('#asnno').change(function () {
        if ($(this).siblings().hasClass('error')) {
            $(".error").remove();
            $("#asnno").val('');
        }
        $("#loadingid").show("slow");
        var asnid = $('#asnno').val();
        $.ajax({
            url: '../Stores/ChangeGetAsnbyInvoice',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "GET",
            data: 'AsnbyInvoice=' + asnid,
            success: function (data) {
                var dd = JSON.parse(data);
                if (dd.length > 0) {
                    var result = JSON.parse(data);
                    $("#invoiceno").empty();
                    $("#invoiceno").val(result[0]["INVOICENO"]);
                }
            }
        });
        $("#loadingid").hide("slow");
    });
    $("#loadingid").hide();
    $(document).ajaxStart(function () {
        $("#loadingid").show();
    });
    $(document).ajaxStop(function () {
        $("#loadingid").hide();
    });
    $('.fromdate').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('.todate').datetimepicker({
        useCurrent: false, //Important! See issue #1075
        format: 'DD/MM/YYYY'
    });
    $(".fromdate").on("dp.change", function (e) {
        $('.todate').data("DateTimePicker").minDate(e.date);
    });
    $(".todate").on("dp.change", function (e) {
        $('.fromdate').data("DateTimePicker").maxDate(e.date);
    });
})
function setNavigation() {
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $('.submenu a').each(function () {
        if (this.href === path) {
            $(this).closest('li').addClass('active');
            if ($(this).closest('li').hasClass('active')) {
                $(this).parents('li').addClass('open');
            }
            $(this).closest('li').removeClass('open');
        }
    });
    $('.parentli a').each(function () {
        if (this.href === path) {
            $(this).closest('li').addClass('active');
        }
    });
}
function breadcrumb() {
    var bredcrumbtext = '';
    var bredcrumbmenu = '';
    var nomenu = $("ul.breadcrumb > span.page-menu").text();
    var notext = $("ul.breadcrumb > span.page-name").text();
    if (nomenu == "") {
        bredcrumbmenu = $("li.open > a.dropdown-toggle > span.menu-text").text();
        $(".page-menu").text(bredcrumbmenu);
    } if (notext == "") {
        bredcrumbtext = $("li.open > ul.submenu > li.active > a > span.menu-text").text();
        $(".page-name").text(bredcrumbtext);
    } if (bredcrumbmenu == "" && bredcrumbtext == "") {
        $('ul.breadcrumb .active').addClass('hide');
    }
}
