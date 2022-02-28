var resize = $('#upload-background').croppie({
    enableExif: true,
    enableOrientation: true,
    viewport: { // Default { width: 100, height: 100, type: 'square' } 
        width: 400,
        height: 150,
        type: 'square' //square
    },
    boundary: {
        width: 500,
        height: 250,
    }
});

$('#image-background').on('change', function () {
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

$('.btn-upload-background').on('click', function (ev) {
    resize.croppie('result', {
        type: 'canvas',
        size: 'viewport'
    }).then(function (img) {
        $('.card').css('background-image', "url('" + img + "')");
    });
});