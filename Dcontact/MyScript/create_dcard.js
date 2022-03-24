    var imageSrc;
$(document).ready(function () {
    function imagePosition(position) {
        if (imageSrc != null) {
            $('.cmn-card img').attr('src', '');
            position = '.' + 'card--' + position + ' img';
            $(position).attr('src', imageSrc);
        }
    }


    //position avatar
    $('#avtBtn').on('click', function () {
        var rdoChecked = $('input[name="positionAvt"]:checked').val();
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
            $('.card').css('background-color', '');
            $('#cardBg').css('background-image', "url('" + img + "')");   //cho load anh
        });
    });
///////////////


});