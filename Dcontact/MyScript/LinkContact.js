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
        } else {
            $('.errorMail').show();
        }
    });

    $('.btn-verifyCode').on('click', function () {
        var regexCode = /^[0-9]{6}$/;
        var codeInput = $('#codeVerify').val();
        if (regexCode.test(codeInput)) {
            $('.errorCode').hide();
            alert('successful')
        } else {
            $('.errorCode').show();
        }
    })

});
