//Global
const compare = (a, b) => a.getTime() > b.getTime();
var currentdate = new Date();
var currentYear = currentdate.getFullYear();

function isEmpty(e) {
    if (e.replace(/^\s+|\s+$/gm, '').length == 0) {
        return true;
    }
    return false;
}
//stepper core
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

function stepperUpdate() {
    if (stepper.value >= 1) {
        $("#amount").val(stepper.value);
    } else stepper.value = 1;
}
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
$('#amount').on('keyup blur', function () {
    var val = $('#amount').val();
    stepper.setValue(val > 0 ? val : 1);
    stepperUpdate();
});
//  Error action
//add error
function addError(obj, error) {
    $(obj).parent().children('span').html(error);
}
//  Remove error 
function removeError(obj) {
    $(obj).parent().children('span').children().remove();
}
$(document).ready(function () {
    //init value
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
    // object
    const address = {
        dom: '#address',
        value: '',
        regex: /^.+$/,
        Error: $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Can be empty"></i>'),
        check() {
            return (this.regex.test(this.value) && this.value.trim() ? '' : this.Error)
        }
    }
    const phone = {
        dom: '#phone',
        value: '',
        regex: /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/,
        Error: {
            'empty': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Phone number can not be empty"></i>'),
            'wrong': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Phone number not correct format"></i>')
        },
        check() {
            return (this.value.trim() ? (this.regex.test(this.value) ? '' : this.Error.wrong) : this.Error.empty)
        }
    }
    const credit = {
        dom: '#creditNumber',
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
            dom: 'expdate',
            value: new Date(),
            Error: {
                'wrong': $('<i class="fa fa-exclamation-circle error"  ctitle="error" title="Your card had been Exprice, please try again!"></i>')
            },
            check() {
                return compare(new Date(this.value), new Date()) ? '' : this.Error.wrong;
            }
        },
        cvv: {
            dom: '#cvv',
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
    //check 
    function checkAddress() {
        removeError(address.dom)
        address.value = $(address.dom).val();
        var result = address.check();
        if (result) {
            addError(address.dom, result);
            return false;
        }
        return true;
    }

    function checkPhone() {
        removeError(phone.dom)
        phone.value = $(phone.dom).val();
        var result = phone.check();
        if (result) {
            addError(phone.dom, result)
            return false;
        }
        return true;
    }

    function checkCreditNumber() {
        removeError(credit.dom)
        credit.value = $(credit.dom).val();
        var result = credit.check();
        if (result) {
            addError(credit.dom, result)
            return false;
        }
        return true;
    }

    function checkCVV() {
        removeError(credit.cvv.dom)
        credit.cvv.value = $(credit.cvv.dom).val();
        var result = credit.cvv.check();
        if (result) {
            addError(credit.cvv.dom, result)
            return false;
        }
        return true;
    }

    function checkExpDate() {
        removeError(credit.ExpDate.dom)
        var exp = $('#exYear').val() + '-' + $('#exMonth').val() + '-' + '1';
        //credit.ExpDate.value = new Date(exp);
        credit.ExpDate.value = exp;
        var result = credit.ExpDate.check();
        if (result) {
            addError(credit.ExpDate.dom, result)
            return false;
        }
        return true;
    }

    function deleteFile(path) {
        const fs = require("fs")

        const pathToFile = path;

        fs.unlink(pathToFile, function (err) {
            if (err) {
                throw err
            } else {
                console.log("Successfully deleted the file.")
            }
        })
    }
    //check on typing
    $(phone.dom).on('focusout', function (e) {
        checkPhone();
    }).on('keyup', function (e) {
        removeError(phone.dom)
    })
    $("#creditNumber").on("keyup", function () {
        var value = $(this).val();
        if (credit.visa.regex.test(value)) {
            $("#visa").addClass('accept');
            credit.currentCredit = 'visa'
        } else if (credit.mastercard.regex.test(value)) {
            $("#mastercard").addClass('accept');
            credit.currentCredit = 'mastercard'
        } else {
            $('.accept').removeClass('accept');
        }
    }).on('focusout', function () {
        checkCreditNumber();
    });
    $('#cvv').focusout(function () {
        checkCVV();
    });
    //summit 
    $('#confirm').on('click', function (e) {
        e.preventDefault();
        var _address = checkAddress();
        var _phone = checkPhone();
        var _credit = checkCreditNumber();
        var _cvv = checkCVV();
        var _expDate = checkExpDate();
        if (_address && _phone && _credit && _cvv && _expDate) {
            modalConfirm();
            $('#modal__confirm').addClass('open');
            return;
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
    $('#buy').on('click', function () {

        $.ajax({
            origin: '*',
            type: "post",
            url: "DcontactAndDcrad/oder_dcardForm",
            data: {
                cardBackGround: $("#image_merge_url").attr("src"),
                address: address.value,
                phone: phone.value,
                amount: stepper.value,
                cardNumber: credit.value,
                cvv: credit.cvv.value,
                exp: credit.ExpDate.value
            },
            beforeSend: function () {
                $('body > div.modal.js-modal.open > div > div.modal__contener').html('<center><div class="spinner"></div></center>');
                $('body > div.modal.js-modal.open > div > div.modal_foo.right__site').html("");
            },
            success: function (msg) {
                $('body > div.modal.js-modal.open > div > div.modal__contener').html('<h2>' + msg + '</h2>');
                $('body > div.modal.js-modal.open > div > div.modal_foo.right__site').html('<button type="button" id="close">Close</button>');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    });
    //modal confirm
    function modalConfirm(e) {
        $('#address_value').text(address.value);
        $('#phone_value').text(phone.value);
        $('#amount_value').text(stepper.value);
        $('#credit_value').text(credit.value);
        $('#total').text((parseInt(stepper.value) * 6) + "$");
    }
});