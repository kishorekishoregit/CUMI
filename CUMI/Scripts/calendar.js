jQuery(function ($) {

    /* initialize the external events
        -----------------------------------------------------------------*/
    $('.fc-button').click(function (event) {
        event.preventDefault();
        event.stopPropagation();
        return false;
    });

    /* initialize the calendar
	-----------------------------------------------------------------*/

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var vendorval = $('#vendorcode :selected').text();
    var vendortext = $('#vendorcode').val();



    var calendar = $('#calendar').fullCalendar({

        //isRTL: true,
        //firstDay: 1,// >> change first day of week

        buttonHtml: {
            prev: '<i class="ace-icon fa fa-chevron-left"></i>',
            next: '<i class="ace-icon fa fa-chevron-right"></i>'
        },

        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        editable: false,
        droppable: false, // this allows things to be dropped onto the calendar !!!

        selectable: true,
        selectHelper: true,
        eventClick: function (calEvent, jsEvent, view) {

            //display a modal
            var modal =
            '<div class="modal fade">\
			  <div class="modal-dialog">\
			   <div class="modal-content">\
<div class="modal-header"><h4 class="pull-left text-info"> ' + calEvent.title + '</h4> <button type="button" class="close" data-dismiss="modal" style="margin-top:3px;"><i class="fa fa-times-circle fa-1x text-danger"></i></button></div>\
				 <div class="modal-body">\
				   <form class="no-margin">\
					 <p> ' + calEvent.description + ' </p>\
				   </form>\
				 </div>\
			  </div>\
			 </div>\
			</div>';

            var modal = $(modal).appendTo('body');
            modal.find('form').on('submit', function (ev) {
                
                ev.preventDefault();
                var vendorval = $('#vendorcode :selected').text();
                var id = $('#id').val();
                calEvent.title = $(this).find("input[type=text]").val();
                calendar.fullCalendar('updateEvent', calEvent);
                modal.modal("hide");
            });

            modal.find('button[data-action=delete]').on('click', function () {
                calendar.fullCalendar('removeEvents', function (ev) {
                    return (ev._id == calEvent._id);
                })
                modal.modal("hide");
            });

            modal.modal('show').on('hidden', function () {
                modal.remove();
            });

        }

    });




    //Page Load 
    BindPageLoad();
    function BindPageLoad() {
        
        $(".loader").show("slow");
        var vendorcode = $('#loginuser').text();
        $('#vendorcode').val(vendorcode);
        if ($('#vendorcode').val() != "") {
            $.ajax({
                url: 'CalenderUsers',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: 'vendorcode=' + $('#vendorcode').val(),
                success: function (Result) {
                    var resJcal = JSON.parse(Result);
                    // DATABIND
                    var obj = resJcal;
                    var eventscal = [];
                    var caldate;
                    $('#calendar').fullCalendar('removeEvents');
                    for (i = 0; i < obj.length; i++) {

                        caldate = obj[i].EVENTDATE;
                        var date = new Date(caldate);
                        var title = obj[i].SUBJECT.split('~')[0];
                        var description = obj[i].description;
                        $('#id').val(title)
                        calendar.fullCalendar('renderEvent',
                        {
                            title: title,
                            start: date,
                            end: date,
                            className: 'label-info',
                            description: description
                        },
                        true // make the event "stick"
                        );
                        calendar.fullCalendar('unselect');
                    }

                }
            });
        }
        $(".loader").hide("slow");
    }


    // clear 
    $('#btnclear').click(function () {
        location.reload();
    })
});

function calajaxmethod(title, start, vendortext, id, description) {
    var date = convert(start);
    $.ajax({
        url: 'CalendarEventsInsert',
        dataType: "json",
        type: "POST",
        data: 'users=' + vendortext + '&actiontype=' + $('#actiontype').text() + '&title=' + title + '&start=' + date + '&id=' + id,
        success: function (data) {
            bootbox.dialog({
                message: '<span class=""><i class="ace-icon fa fa-user"></i>' + ' ' +'Employee/User Code'+ data +' '+'Added Successfully' + '</span>',
                buttons: {
                    "success": {
                        "label": "OK",
                        "className": "btn-sm btn-primary reload"
                    }
                },
            });
            $('.reload').click(function () {
                location.reload();
            });
        }
    });
}

function convert(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
}

$(document).ajaxStart(function () {
    $("#loadingid").show();
});

$(document).ajaxStop(function () {
    $("#loadingid").hide();
});