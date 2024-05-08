$(function () {

    $('#btnlogin').click(function (e) {
        
            $.ajax({
                url: 'Login/LoginCheck',
                dataType: 'json',
                type: 'POST',
                data: { "Username": $("#txtusername").val(), "Upassword": $("#txtpassword").val() },
                success: function (data) {
                    
                    var Res = data.split('|');
                    var result = Res[0];
                    var msg = Res[1];
                    if (result.toUpperCase() == "TRUE") {
                        var url = 'Home';
                        window.location.href = url;
                       //window.location.reload();
                    }
                    else {
                        bootbox.alert({
                            message: '<span class="text-danger"><i class="icon icon ion-ios-close-circle-outline tx-50 tx-danger"style="margin-left: 100px;font-size: 50px;"></i><br>' + msg + '</span>',
                            size: 'small'
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Status: " + textStatus); alert("Error: " + errorThrown);
                }
            });
        
    })
    .on('changed.fu.wizard', function() {
    })
  .on('stepclick.fu.wizard', function (e) {
      //e.preventDefault();//this will prevent clicking and selecting steps
  });
    $('#skip-validation').removeAttr('checked').on('click', function () {
        $validation = this.checked;
        if (this.checked) {
            $('#validation-form').addClass('hide');
            $('#sample-form').show();
        }
        else {
            $('#sample-form').hide();
            $('#validation-form').removeClass('hide');
        }
    })




});