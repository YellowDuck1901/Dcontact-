const goTo = (url) => (location.href = url);
$(document).ready(function() {
    $('.contener').on('click', '*', function(e) {
        goTo('edit');
    })
});