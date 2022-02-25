$(document).ready(function () {
    $(".modal").on('click', '.fa-close, button#close', function () {
        $('.modal').removeClass('open');
    });
    $("header").load("header.html");
});