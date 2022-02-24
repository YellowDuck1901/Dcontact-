const rgb2hex = (rgb) => `#${rgb.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/).slice(1).map(n => parseInt(n, 10).toString(16).padStart(2, '0')).join('')}`;
var regexUrl = /^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$/;
function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  };
$(document).ready(function () {
   $('.cmnRadio').change(function() {
       var radioValue = $('.cmnRadio:checked').val();
       if(radioValue == 'font') {
           $('.font-area').show();
           $('.bullet-area').hide();
           $('.gate-area').hide();
        }
        if(radioValue == 'bullet') {
           $('.font-area').hide();
           $('.bullet-area').show();
           $('.gate-area').hide();
        }
        if(radioValue == 'gate') {
           $('.font-area').hide();
           $('.bullet-area').hide();
            $('.gate-area').show();
       }
   });

   $('#toggleCode').on('change', function() {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        $('.code-form').show();
    } else {
        $('.code-form').hide();
    }
   });

   $('#toggleAge').on('change', function() {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
        $('.age-form').show();
    } else {
        $('.age-form').hide();
    }
   });

   $('.applyCode').on('click', function() {
       var code = $('#codeAccess').val();
       if(code < 1000 || code > 9999) {
           $('.err-code').show();
           $('#codeAccess').css('border','2px solid #e74c3c');
       } else {
           $('#codeAccess').css('border','2px solid #2ecc71');
           $('.err-code').hide();
       }
   });

   $('.applyAge').on('click', function() {
       var age = $('#ageConfirm').val();
       if(age < 14 || age > 99) {
           $('.err-age').show();
           $('#ageConfirm').css('border','2px solid #e74c3c');
       } else {
        $('#ageConfirm').css('border','2px solid #2ecc71');
           $('.err-age').hide();
       }
   });

   $('.contener').on('click', function() {
    //    $('.contener').width('68.5%');
       $('.container__right').css('display','flex');
   });

// CHANGE COLOR BACKGROUND
var newColor;
    $('#bgColor').on('change',function () {
        // $('.newClass').css('background-color',$(this).val());
       newColor = $(this).val();
    });
    var newClass = 1;
  
//    CHANGE BULLET
    $('.iconSocial').on('click', function() {
        // $('.bulletResult').text("");
        var idIcon = $(this).attr('id');
        var classIcon = $(this).children('i').attr('class');
        
        $('.bulletResult').children('i').attr('class',classIcon);
        
        var idOfBullet = "#" +  $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfBullet).children('i').attr('class',classIcon);

    });

    // ADD NEW LINK

    $('#addNewUrl').on('click', function() {
       $('.social-list').append(' <li> <div class="button" role="button" style="background-color: #273c75; height: 26.88px" id="'+uuidv4()+'" > <i class=""></i> <div class="card--item__text"> <label></label> </div> </div> </li>');
       });

        //set color
        $('#bgColor').on('change',function () {
           var idOfColor = "#" +  $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
            $(idOfColor).css('background-color',$(this).val());
        //    newColor = $(this).val();
        });

       $('body > div.container > main > nav.container__right > div.right__field > div.url-area').on('keyup', function() {
          var title = $('#titleUrl').val();
       var id = "#" + $(this).attr('target') + "";
       $(id).children('.card--item__text').children('label').text(title);
       });

       $('.card__contener--body').on('click', '.button', function() {
        $('.bulletResult').children('i').removeClass();
        // get target & title
        var getID = $(this).attr('id');
        $('div.url-area').attr('target',getID);
        var title = $(this).children('.card--item__text').children('label').text();
        $('#titleUrl').val(title);
        // get bullet
            var crntBullet = $(this).children('i').attr('class');
            $('.bulletResult').children('i').addClass(crntBullet);

        // get color
            var backgroundColor = $(this).css('background-color');
            $('#bgColor').val(rgb2hex(backgroundColor));

           
       });

        // FONT SELECTOR
    $('#fs').on('change', function() {
        var fontVal =  $('#fs option:selected').val();
        var idOfFont = "#" +  $('body > div.container > main > nav.container__right > div.right__field > div.url-area').attr('target');
        $(idOfFont).children('.card--item__text').children('label').css("font-family", fontVal);

    })
    // TEMPLATE
    $('.template__detail').on('click','.img-temp', function() {
        var src = $(this).attr('src');
        $('.card__contener').css('background-image',"url('"+src+"')");
    })

    for(i = 1; i <= 10; i++) {
        var image = "https://source.unsplash.com/random/329x531?sig="+i+"";
        $('.template__detail ul').append(' <li> <div class="inf-temp" id="template1"> <img src="'+image+'" alt="" class="img-temp" id="srcImg1" /> <p class="name-temp">Template '+i+'</p> </div> </li>');
    }

    // MODAL
    $('.file__label').on('click', function() {
        $('.modalAvt').css('display','flex');
        $('.template__header').css('z-index','0');
    })
    
    $('.btn-cancelAvt').on('click', function() {
        $('.modalAvt').css('display','none');
        $('.template__header').css('z-index','1');
    })

   
});
