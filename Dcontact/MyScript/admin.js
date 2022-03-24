$(document).ready(function () {

    $('.main-report').on('click', '.btn-decline', function () {
        idRow = $(this).parent().attr('id');
        $.post("/Admin/Delete_Report", { id_row: idRow }).done(function (data) {
            $("#" + idRow).parent().remove();
        })
    });

    $('.main-report').on('click', '.btn-accept', function () {
        idRow = $(this).parent().attr('id');
        $.post("/Admin/Accept_Report", { id_row: idRow }).done(function (data) {
            $("#" + idRow).parent().remove();
            location.reload()
        })
    });

    $('.main-lock').on('click', '.btn-lock', function () {
        id_User = $(this).parent().attr('id');
        if ($(this).text() === 'BLOCK') {
            $.post("/Admin/Block_User", { id_user: id_User }).done(function (data) {
                //$("#" + idRow).parent().remove();
                //$(".btn-lock").html('Unblock');
                $(this).html("UNBLOCK");
            })
        } else {
            $.post("/Admin/Unblock_User", { id_user: id_User }).done(function (data) {
                //$("#" + idRow).parent().remove();
                //$(".btn-lock").html('Unblock');
                $(this).html("BLOCK");
            })
        }
    });


});
