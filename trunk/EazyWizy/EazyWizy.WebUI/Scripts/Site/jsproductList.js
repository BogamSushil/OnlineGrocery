var toggle = function () {
    alert('in');
    $(this).closest('').toggleClass('flip').find('.back').html(new Date().getTime());

}
$(document).ready(function () {
    $('.hover').hover(function () {
        $(this).closest(".panel").addClass('flip');
    }, function () {
        $(this).closest(".panel").removeClass('flip');
    });

    $('.click').click(function () { $(this).toggleClass('flip').find('.back').html(new Date().getTime()); });
});