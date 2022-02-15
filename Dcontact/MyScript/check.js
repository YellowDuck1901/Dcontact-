$(document).ready(function() {
    $('#changemail').on('click', function() {
        $('.open').removeClass('open')
        $('#sendCode').addClass('open')
    });
    $('#btn-code').on('click', function() {
        var rex = /^\d{6}$/;
        if (rex.test($('#code').val())) {
            $('.open').removeClass('open')
            $('#newEmail').addClass('open')
            return;
        }
        $('#code').next().text("Wrong Code, Try Again!")
    });
    $('#btn-profile').on('click', function() {
        $('#account').addClass('open')
        return;
    });
    $('#btn-email').on('click', function() {
        var rexx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (rexx.test($('#change_newemail').val())) {
            $('.open').removeClass('open')
            $('#sendCode_confirm').addClass('open')
        }
        $('#change_newemail').next().text("Wrong Email, Try Again!")
    });
    $('#btn-confirmCode').on('click', function() {
        var rexconfirmCode = /^\d{6}$/;
        if (rexconfirmCode.test($('#codeConfirm').val())) {
            $('.open').removeClass('open')
            $('#account').addClass('open')
            return;
        }
        $('#codeConfirm').next().text("Wrong Code, Try Again!")
    });
    $("#btn-statistical").click(function() {
        $(".main").show();
        $(".main-upi").hide();
        $(".main-lock").hide();
        $(".main-report").hide();
    });
    $("#btn-upi").click(function() {
        $(".main").hide();
        $(".main-upi").show();
        $(".main-lock").hide();
        $(".main-report").hide();
    });
    $("#btn-lock").click(function() {
        $(".main").hide();
        $(".main-upi").hide();
        $(".main-lock").show();
        $(".main-report").hide();
    });
    $("#btn-report").click(function() {
        $(".main").hide();
        $(".main-upi").hide();
        $(".main-lock").hide();
        $(".main-report").show();
    });
});
$(document).ready(function() {
    $(".modal").on('click', '.fa-close, button#close', function() {
        $('.modal').removeClass('open');
    });
});