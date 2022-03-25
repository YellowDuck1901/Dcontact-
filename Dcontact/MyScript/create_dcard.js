var imageSrc;
var onQR;
var imgPosition;
var regexURL = /^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$/;
$(document).ready(function () {
    function imagePosition(position) {
        if (imageSrc != null) {
            imgPosition = position;
            $('.cmn-card img').attr('src', '');
            position = '.' + 'card--' + position + ' img';
            $(position).attr('src', imageSrc);
        }
        console.log(position)
    }


    //qrcode
    $("#toggleQR").on('change', function () {
        if ($(this).is(':checked')) {
            onQR = true;
            $(this).attr('value', 'true');
            var data = $('#linkContact').val();
            var srcQR = "http://chart.googleapis.com/chart?chs=100x100&cht=qr&chl=" + data;
            var img = '<img style="margin: 0 auto" class="img-qr" src="' + srcQR + '">';
            UploadQRImage(srcQR);
            if (imgPosition === "right") {
                /*imagePosition("left");*/
                $('.qrcode').css('left', 0);
            }

            $('.qrcode').html(img);
        }
        else {
            $(this).attr('value', 'false');
            $('#qr').remove();
            onQR = false;
            $('.qrcode').hide();
            $(this).prop('checked', false);
        }
    });

    //position avatar
    $('#avtBtn').on('click', function () {
        var rdoChecked = $('input[name="positionAvt"]:checked').val();
        if (rdoChecked === "left") {
            imagePosition("left");
            $('.qrcode').css('right', 0);
            $('.qrcode').css('left', '');
        }

        if (rdoChecked === "right") {
            imagePosition("right");
            $('.qrcode').css('left', 0);
            $('.qrcode').css('right', '');
        }

        imagePosition(rdoChecked)
    })
    
    var resize_avatar = $('#upload-avatar').croppie({
        enableExif: true,
        enableOrientation: true,
        viewport: { // Default { width: 100, height: 100, type: 'square' } 
            width: 80,
            height: 80,
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
            $('.cmn-card img').attr('src', '');
            $('#modal__crop--avt').css('display', 'none');
            $('.cropAvatar').hide();
            document.getElementById('avatar-result').src = "" + img;
            imageSrc = img+"";
        });
    });

    //upload template
    var resize = $('#upload-template').croppie({   //load edit crop anh
        enableExif: true,
        enableOrientation: true,
        viewport: { // Default { width: 100, height: 100, type: 'square' } 
            width: 500,
            height: 250,
            type: 'square' //square
        },
        boundary: {
            width: 550,
            height: 300,
        }
    });

    $('#image-template').on('change', function () {  //chon file
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

    $('.btn-upload-template').on('click', function (ev) {  //button upload image
        resize.croppie('result', {
            type: 'canvas',
            size: 'viewport'
        }).then(function (img) {
            $("#toggleBg").click();
            $('#modal__crop--bg').css('display','none');
            $('.card').css('background-color', '');
            $('#cardBg').css('background-image', "url('" + img + "')");   //cho load anh
        });
    });
///////////////


});