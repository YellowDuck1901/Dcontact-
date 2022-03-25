var idRow;
var link;
function bind(e) {
    var text = $('#'+e).children('div').children('label').text()
    $('#text').html(text)
    $('#url').html(link);
}
$(document).ready(function () {
    //$('.report').on('click', function () {
    //    $('.modalReport').addClass('open');
    //})
    $('.modalReport').on('click', '.fa-close', function () {
        $('.modalReport').removeClass('open');
    })

    // CHECK REGEX EMAIL AND CODE
    $('.btn-sendCode').on('click', function () {
        var regexMail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
        var mailInput = $('#mailReport').val();
        if (regexMail.test(mailInput)) {
            $('.bodySendMail').hide();
            $('.bodyVerify').show();
            $('.errorMail').hide();
            // TIMER COUNTDOWN
            var timer2 = "5:01";
            var interval = setInterval(function () {
                var timer = timer2.split(':');
                //by parsing integer, I avoid all extra string processing
                var minutes = parseInt(timer[0], 10);
                var seconds = parseInt(timer[1], 10);
                --seconds;
                minutes = (seconds < 0) ? --minutes : minutes;
                if (minutes < 0) clearInterval(interval);
                seconds = (seconds < 0) ? 59 : seconds;
                seconds = (seconds < 10) ? '0' + seconds : seconds;
                //minutes = (minutes < 10) ?  minutes : minutes;
                $('.countdown').html(minutes + ':' + seconds);
                timer2 = minutes + ':' + seconds;
                // if(timer2 != "0:00") {
                //   $('.btn-resendCode').attr('disabled','true');
                // } 
            }, 1000);

        } else {
            $('.errorMail').show();
        }
    });

    $('.btn-verifyCode').on('click', function () {
        var regexCode = /^[0-9]{6}$/;
        var codeInput = $('#codeVerify').val();
        if (regexCode.test(codeInput)) {
            $('.errorCode').hide();
            $(".bodyVerify").hide();
            $('.reportDesc').show();

        } else {
            $('.errorCode').show();
        }
    });
    $('.btn-sendReport').on('click', function () {
        var txtDesc = $('#txtDesc').val();
        if (txtDesc == "") {
            $('.errorReport').show();
        } else {
            $('.errorReport').hide();
            $('.modalReport').removeClass("open");
        }
    });

    $('.btn-resendCode').on('click', function () {
        alert('successful')
    });

    $('.card__contener--body').on('click', '.report', function () {
        idRow = $(this).next().attr('id');
        $.post("LinkContact/gate", { id: idRow }).done(function (data) {
            if (data) {
                $('.modalReport').addClass('open');
                link = data;
                bind(idRow);
            } else {
                $('#gate--code').addClass('open')
                $('form#code').attr('id', 'rcode')
            }
        });

    });
    $('.modal-container').on('submit', "form#rcode", function (e) {
        e.preventDefault()
        $.post("/LinkContact/gate_code", { id_row: idRow, code: $('#codeAccess').val() }).done(function (data) {
            if (data) {
                link = data;
                bind(idRow);
                $('.modalReport').addClass('open');
                $('form#rcode').attr('id', 'code')
                $('#gate--code').removeClass('open');
            } else {
                alert('The access code is invalid. 1')
            }
        });
    } )
    

    $('#btn-sendReport').on('click', function () {
        var txtDes = $('#txtDesc').val();
        $.post("/LinkContact/Report", { id_row: idRow, txt_Des: txtDes }).done(function (data) {
            console.log(data);
        })
    })
    
});
