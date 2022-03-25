//global
const rgb2hex = (rgb) => `#${rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/).slice(1).map(n => parseInt(n, 10).toString(16).padStart(2, '0')).join('')}`;
var urlRegex = /^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$/;

function b64toBlob(b64Data, contentType, sliceSize) {
    contentType = contentType || '';
    sliceSize = sliceSize || 512;
    var byteCharacters = atob(b64Data);
    var byteArrays = [];
    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    var blob = new Blob(byteArrays, { type: contentType });
    return blob;
}

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}
//obj
var newClass = 1;
var newColor;
var resize = $('#upload-template').croppie({ //load edit crop anh
    enableExif: true,
    enableOrientation: true,
    viewport: { // Default { width: 100, height: 100, type: 'square' } 
        width: 362,
        height: 555,
        type: 'square' //square
    },
    boundary: {
        width: 290,
        height: 580,
    }
});
//dcontact
const currentRow = {
    id: '',
    color: '',
    bullet: '',
    font: '',
    text: '',
    link: '',
    code: '',
    bdate: '',
}
var idRow;
var color;
var bullet;
var font;
var text;
var link;
var code;
var bdate;
//function
function updateRow(id) {
    if (id != null && id != undefined && id != '' && text != "" && link != "") {
        var idr = "#" + id;
        // color = $(idr).children(".button").css("background-color");
        // color = rgb2hex(color);
        // bullet = $(idr).children(".button").children('i.fa').attr("class");
        // font = $(idr).children(".button").children('div.card--item__text').children("label").css("font-family");
        // text = $(idr).children(".button").children('div.card--item__text').children("label").text();
        // link = $(idr).children(".button").attr('url');
        // code = $('#codeAccess').attr('code');
        $.ajax({
            origin: '*',
            type: "post",
            url: "/DcontactAndDcrad/updateRow",
            data: {
                id_row: idRow,
                color_row: color,
                bullet_row: bullet,
                font_row: font,
                text_row: text,
                link_row: link,
                code_row: code,
                bdate_row: bdate,
            },
            success: function (msg) {
                console.log("has been update row")
                console.log(msg)
                console.log('<<<<<<<<<<<<<<<<<start debug>>>>>>>>>>>>>>>>>')
                console.log("id: " + idRow)
                console.log("font: " + font)
                console.log("bdate: " + bdate)
                console.log("text: " + text)
                console.log("link: " + link)
                console.log("code: " + code)
                console.log("bullet: " + bullet)
                console.log("color: " + color)
                console.log('<<<<<<<<<<<<<<<<<end debug>>>>>>>>>>>>>>>>>')
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    } else console.log('Id error: ' + id)
}

function resetEditpanel() {
    $('.bulletResult').children('i').removeClass();
    // get target & title
    $('#linkUrl').val('');
    $('#codeAccess').val('');
}

function getdataRow(e) {
    idRow = $(e).parent('li').attr('id');
    font = $(e).children('div.card--item__text').children('label').css('font-family');
    bdate = '1111-11-11'
    text = $(e).children('.card--item__text').children('label').text();
    link = $(e).attr('url');
    code = $(e).attr('code');
    bullet = $(e).children('i').attr('class');
    color = rgb2hex($(e).css('background-color'));
    console.log("id: " + idRow)
    console.log("font: " + font)
    console.log("bdate: " + bdate)
    console.log("text: " + text)
    console.log("link: " + link)
    console.log("code: " + code)
    console.log("bullet: " + bullet)
    console.log("color: " + color)
}

function blindingRow() {
    $('#titleUrl').val(text); /*gan gia tri row sangurl-area*/
    $('#linkUrl').val(link);
    $('#codeAccess').val(code);
    $('.bulletResult').children('i').addClass(bullet);
    $('#bgColor').val(color);
}
//event
$(document).ready(function () {
    //crop avatar
    var resize_avatar = $('#upload-avatar').croppie({
        enableExif: true,
        enableOrientation: true,
        viewport: { // Default { width: 100, height: 100, type: 'square' } 
            width: 100,
            height: 100,
            type: 'circle'
        },
        boundary: {
            width: 300,
            height: 300,
        }
    });
    $('#image-avatar').on('change', function () {
        var reader = new FileReader();
        reader.onload = function (e) {
            resize_avatar.croppie('bind', {
                url: e.target.result
            }).then(function () {
                console.log('jQuery bind complete');
            });
        }
        reader.readAsDataURL(this.files[0]);
    });
    $('.btn-upload-avatar').on('click', function (ev) {
        resize_avatar.croppie('result', {
            type: 'canvas',
            size: 'viewport'
        }).then(function (img) {
            document.getElementById('avatar-result').src = "" + img;
            var formData = new FormData();
            var block = img.split(";");
            var contentType = block[0].split(":")[1];
            var realData = block[1].split(",")[1];
            var blob = b64toBlob(realData, contentType);
            formData.append("image", blob);
            //formData.append('image', img+"");
            $.ajax({
                type: "POST",
                url: "api/ImageAPI/UploadFiles",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    $('#avatar-result').attr('src', response);
                    $.post("/DcontactAndDcrad/updateImage", { piece: 'avatar', path: response }).done(function (data) {
                        console.log(data + 'avt has been updated');
                    });
                    console.log(response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('body').html('error: ' + jqXHR.responseText);
                    console.log('error: ' + jqXHR.status);
                }
            });
        });
    });
    ////////////////////
    // DELETE ROW
    /* $('.social-list').on('click', '.report', function () {
         var parentRow = $(this).parent().attr('id');
         // alert(parentRow)
         $('.social-list #' + parentRow).remove();
     })*/
    $('.cmnRadio').change(function () {
        var radioValue = $('.cmnRadio:checked').val();
        if (radioValue == 'font') {
            $('.font-area').show();
            $('.bullet-area').hide();
            $('.gate-area').hide();
        }
        if (radioValue == 'bullet') {
            $('.font-area').hide();
            $('.bullet-area').show();
            $('.gate-area').hide()
        }
        if (radioValue == 'gate') {
            $('.font-area').hide();
            $('.bullet-area').hide();
            $('.gate-area').show();
        }
    });
    $('#toggleCode').on('change', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
            $('.code-form').show();
        } else {
            $('.code-form').hide();
            code = 0;
        }
    });
    $('#toggleAge').on('change', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
            $('.age-form').show();
        } else {
            $('.age-form').hide();
            bdate = '';
        }
    });
    $('.applyCode').on('click', function () {
        code = $('#codeAccess').val();
        if (code < 1000 || code > 9999) {
            $('.err-code').show();
            $('#codeAccess').css('border', '2px solid #e74c3c');
        } else {
            $('#codeAccess').attr('code', code);
            $('#codeAccess').css('border', '2px solid #2ecc71');
            $('.err-code').hide();
            updateRow(idRow);
        }
    });
    $('.applyAge').on('click', function () {
        var age = $('#ageConfirm').val();
        //[ ] bday la kieu date
        if (age < 14 || age > 99) {
            $('.err-age').show();
            $('#ageConfirm').css('border', '2px solid #e74c3c');
        } else {
            bdate = '1111-11-1';
            $('#ageConfirm').css('border', '2px solid #2ecc71');
            $('.err-age').hide();
        }
    });
    $('.contener').on('click', function () {
        //    $('.contener').width('68.5%');
        $('.container__right--url').css('display', 'flex');
        $('.container__right--avatar').css('display', 'none');
        $('.container__right--template').css('display', 'none');
    });
    // CHANGE COLOR BACKGROUND
    $('#bgColor').on('change', function () {
        // $('.newClass').css('background-color',$(this).val());
        var idOfColor = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfColor).css('background-color', $(this).val());
        color = $(this).val();
        updateRow(idRow);
    });
    //    CHANGE BULLET
    /* $('.iconSocial').on('click', function () {
         
         var idIcon = $(this).attr('id');
         var classIcon = $(this).children('i').attr('class');
         $('.bulletResult').children('i').attr('class', classIcon);
         var idOfBullet = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
         $(idOfBullet).children('i').attr('class', classIcon);
 
     });*/
    // ADD NEW LINK
    /*   $('#addNewUrl').on('click', function () {
           $('.social-list').append('<li id="' + uuidv4() + '"><span class="report"><abbr title="Click here to delete this link"><i class="fa fa-trash-o"></i></abbr></span><div class="button" role="button" style="background-color: #273c75; height: 26.88px" id="' + uuidv4() + '" > <i class=""></i> <div class="card--item__text"> <label></label> </div> </div> </li>');
       });*/
    //set color
    //change title
    $('body > div.container > main > nav.container__right > div.right__field > div.url-area').on('keyup', function () {
        var id = "#" + $(this).attr('target');
        text = $('#titleUrl').val();
        $(id).children('.card--item__text').children('label').text(text);
        link = $('#linkUrl').val();
        if (urlRegex.test(link) == false) {
            link = "";
            $('#linkUrl').css('color', 'red');
            $('#linkUrl').css('border-color', 'red');

        } else {
            $('#linkUrl').css('color', '#000');
            $('#linkUrl').css('border-color', '#000');
        }
        
        $(id).attr('url', link);
    }).on('focusout', function () {
        updateRow(idRow);
    });
    $('.card__contener--body').on('click', '.button', function () {
        resetEditpanel();
        var getID = $(this).attr('id');
        $('div.url-area').attr('target', getID);
        getdataRow(this);
        blindingRow();
    });
    // FONT SELECTOR
    $('#fs').on('change', function () {
        var fontVal = $('#fs option:selected').val();
        font = fontVal;
        var idOfFont = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfFont).children('.card--item__text').children('label').css("font-family", font);
        updateRow(idRow)
    })
    // TEMPLATE
    $('.template__detail').on('click', '.img-temp', function () {
        var src = $(this).attr('src');
        $('.card__contener').css('background-image', "url('" + src + "')");
        $.post("/DcontactAndDcrad/updateImage", { piece: 'updateTemplate', path: src }).done(function (data) {
            console.log(data + 'card background has been updated');
        });
        //[o] post update template
    })
    //for (i = 1; i <= 10; i++) {
    //    var image = "https://source.unsplash.com/random/329x531?sig=" + i + "";
    //    $('.template__detail ul').append(' <li> <div class="inf-temp" id="template1"> <img src="' + image + '" alt="" class="img-temp" id="srcImg1" /> <p class="name-temp">Template ' + i + '</p> </div> </li>');
    //}
    // CROP AVATAR and TEMPLATE DISPLAY
    $('.file__label').on('click', function () {
        $('.container__right--avatar').css('display', 'flex');
        $('.container__right--url').css('display', 'none');
        $('.container__right--template').css('display', 'none');
    });
    $('.btn-addTemp').on('click', function () {
        $('.container__right--avatar').css('display', 'none');
        $('.container__right--url').css('display', 'none');
        $('.container__right--template').css('display', 'flex');
        $('#image-template').click();
    })
    $('.file__label').on('click', function () {
        $('#image-avatar').click();
    })
    //AJAX FUCNTION
    //delete row
    $('.social-list').on('click', '.report', function () {
        var parentRow = $(this).parent().attr('id');
        // alert(parentRow)
        $('.social-list #' + parentRow).remove();
        $.ajax({
            origin: '*',
            type: "post",
            url: "/DcontactAndDcrad/deleteRow",
            data: {
                id: parentRow
            },
            success: function (msg) {
                var parentRow = $(this).parent().attr('id');
                $('.social-list #' + parentRow).remove();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    })
    //add row
    $('#addNewUrl').on('click', function () {
        $.ajax({
            origin: '*',
            type: "post",
            url: "/DcontactAndDcrad/addRow",
            data: {},
            success: function (msg) {
                $('.social-list').append(msg);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    });
    //update bullet row
    $('.iconSocial').on('click', function () {
        var idIcon = $(this).attr('id');
        var classIcon = $(this).children('i').attr('class');
        var idOfBullet = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        var removeIcon = $(idOfBullet + '> i').attr('class');
        // reclick to remove bullet
        if (classIcon == removeIcon) {
            $(idOfBullet).children('i').attr('class', "");
            bullet = '';
        } else {
            bullet = classIcon;
            $('.bulletResult').children('i').attr('class', classIcon);
            $(idOfBullet).children('i').attr('class', classIcon);
        }
        updateRow(idRow);
    });
    $('#image-template').on('change', function () { //chon file
        var reader = new FileReader();
        reader.onload = function (e) {
            resize.croppie('bind', {
                url: e.target.result
            }).then(function () {
                console.log('jQuery bind complete');
            });
        }
        reader.readAsDataURL(this.files[0]);
    });
    $('.btn-upload-template').on('click', function (ev) { //button upload image
        resize.croppie('result', {
            type: 'canvas',
            size: 'viewport'
        }).then(function (img) {
            $('.card__contener').css('background-image', "url('" + img + "')"); //cho load anh
            //[ ] select template here
            var formData = new FormData();
            var block = img.split(";");
            var contentType = block[0].split(":")[1];
            var realData = block[1].split(",")[1];
            var blob = b64toBlob(realData, contentType);
            formData.append("image", blob);
            //formData.append('image', img+"");
            $.ajax({
                type: "POST",
                url: "api/ImageAPI/UploadFiles",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                success: function (response) {
                    $('.card__contener').css('background-image', "url('" + response + "')"); //cho load anh
                    $.post("/DcontactAndDcrad/updateImage", { piece: 'addAndUpdateTemplate', path: response }).done(function (data) {
                        console.log(data + 'card background has been updated');
                    });
                    $('.template__detail ul').append(' <li> <div class="inf-temp" id="' + uuidv4 + '"> <img src="' + response + '" alt="" class="img-temp" id="srcImg1" /> <p class="name-temp">Template ' + $('li div.inf-temp').length + '</p> </div> </li>');
                    console.log(response);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('body').html('error: ' + jqXHR.responseText);
                    console.log('error: ' + jqXHR.status);
                }
            });
        });
    });
});
///////////////