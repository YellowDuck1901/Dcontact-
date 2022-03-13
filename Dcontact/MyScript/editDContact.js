const rgb2hex = (rgb) => `#${rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/).slice(1).map(n => parseInt(n, 10).toString(16).padStart(2, '0')).join('')}`;
var regexUrl = /^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$/;

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0,
            v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
};

/**
 * Convert a base64 string in a Blob according to the data and contentType.
 * 
 * @param b64Data {String} Pure base64 string without contentType
 * @param contentType {String} the content type of the file i.e (image/jpeg - image/png - text/plain)
 * @param sliceSize {Int} SliceSize to process the byteCharacters
 * @see http://stackoverflow.com/questions/16245767/creating-a-blob-from-a-base64-string-in-javascript
 * @return Blob
 */
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
            formData.append("image",blob);
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
                    $.post("/DcontactAndDcard/updateImage", { path: response })
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
        }
    });
    $('#toggleAge').on('change', function () {
        if ($(this).is(':checked')) {
            $(this).attr('value', 'true');
            $('.age-form').show();
        } else {
            $('.age-form').hide();
        }
    });
    $('.applyCode').on('click', function () {
        var code = $('#codeAccess').val();
        if (code < 1000 || code > 9999) {
            $('.err-code').show();
            $('#codeAccess').css('border', '2px solid #e74c3c');
        } else {
            $('#codeAccess').css('border', '2px solid #2ecc71');
            $('.err-code').hide();
        }
    });
    $('.applyAge').on('click', function () {
        var age = $('#ageConfirm').val();
        if (age < 14 || age > 99) {
            $('.err-age').show();
            $('#ageConfirm').css('border', '2px solid #e74c3c');
        } else {
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
    var newColor;
    $('#bgColor').on('change', function () {
        // $('.newClass').css('background-color',$(this).val());
        newColor = $(this).val();
        updateRow(idRow);
    });
    var newClass = 1;
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
    $('#bgColor').on('change', function () {
        var idOfColor = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfColor).css('background-color', $(this).val());
        //    newColor = $(this).val();
    });
    //change title
    $('body > div.container > main > nav.container__right > div.right__field > div.url-area').on('keyup', function () {
        var title = $('#titleUrl').val();
        var id = "#" + $(this).attr('target') + "";
        $(id).children('.card--item__text').children('label').text(title);
    }).on('focusout', function () {
        updateRow(idRow);
    });
    
    $('.card__contener--body').on('click', '.button', function () {
        $('.bulletResult').children('i').removeClass();
        // get target & title
        var getID = $(this).attr('id');
        $('div.url-area').attr('target', getID);
        var title = $(this).children('.card--item__text').children('label').text();
        $('#titleUrl').val(title);  /*gan gia tri row sangurl-area*/
        // get bullet
        var crntBullet = $(this).children('i').attr('class');
        $('.bulletResult').children('i').addClass(crntBullet);
        // get color
        var backgroundColor = $(this).css('background-color');
        $('#bgColor').val(rgb2hex(backgroundColor));
        //get li clicked
        idRow = $(this).parent('li').attr('id');
      
       
    });

    function updateRow(id) {
        var idr = "#" + id;
        var color = $(idr).children(".button").css("background-color"); color = rgb2hex(color);
        var bullet = $(idr).children(".button").children('i.fa').attr("class");
        var font = $(idr).children(".button").children('div.card--item__text').children("label").css("font-family");
        var text = $(idr).children(".button").children('div.card--item__text').children("label").text();


        $.ajax({
            origin: '*',
            type: "post",
            url: "/DcontactAndDcrad/updateRow",
            data: {
                id_row: id,
                color_row: color,
                bullet_row: bullet,
                font_row: font,
                text_row: text
            },
            success: function (msg) {

            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    }

    // FONT SELECTOR
    $('#fs').on('change', function () {
        var fontVal = $('#fs option:selected').val();
        var idOfFont = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfFont).children('.card--item__text').children('label').css("font-family", fontVal);

        updateRow(idRow)

    })
    // TEMPLATE
    /*    $('.template__detail').on('click', '.img-temp', function () {
            var src = $(this).attr('src');
            $('.card__contener').css('background-image', "url('" + src + "')");
        })*/
    for (i = 1; i <= 10; i++) {
        var image = "https://source.unsplash.com/random/329x531?sig=" + i + "";
        $('.template__detail ul').append(' <li> <div class="inf-temp" id="template1"> <img src="' + image + '" alt="" class="img-temp" id="srcImg1" /> <p class="name-temp">Template ' + i + '</p> </div> </li>');
    }
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
                id : parentRow
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
            data: {
            },
            success: function (msg) {

                $('.social-list').append(msg);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('body').html(jqXHR.responseText);
            }
        });
    });

    //update title row
    $('body > div.container > main > nav.container__right > div.right__field > div.url-area').on('keyup', function () {

    })

    //update bullet row
    $('.iconSocial').on('click', function () {

        var idIcon = $(this).attr('id');
        var classIcon = $(this).children('i').attr('class');

        $('.bulletResult').children('i').attr('class', classIcon);

        var idOfBullet = "#" + $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfBullet).children('i').attr('class', classIcon);

        updateRow(idRow);

    });

});

//upload template
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
    });
});
///////////////


