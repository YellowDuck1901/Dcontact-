$(document).ready(function () {
    $('.report').on('click', function () {
        $('.modalReport').addClass('open');
    })
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
            alert('successful report')

        }
    });

    $('.btn-resendCode').on('click', function () {
        alert('successful')
    });


});
