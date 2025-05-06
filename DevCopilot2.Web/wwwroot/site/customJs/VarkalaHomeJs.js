$(document).ready(function (e) {
    $('#home-nav-link').addClass('active');
    $('.category-btn').click(function (e) {
        LoadProductCategories($(this));
    });
    //start speical discount banner section
    var finishDate = $('#countdown').attr('data-value');
    const serverDate = new Date(finishDate); // Replace with the actual server value

    const timeRemaining = serverDate;
    var countdown = new Countdown('countdown', timeRemaining);

});
var isFirstScroll = true;
document.addEventListener('scroll', function (event) {
    if (isFirstScroll)
        LoadProductCategories($('#default-category-product'));
    isFirstScroll = false;
}, true);
function LoadProductCategories(categoryElement) {
    var value = $(categoryElement).attr('data-value');
    $('.category-btn').removeAttr('active');
    $('.category-btn').removeClass('text-dark');
    $('.category-btn').addClass('text-muted');
    $('.category-products').addClass('d-none');
    $(`.category-products[data-category="${value}"]`).removeClass('d-none');
    $(categoryElement).attr('active', '');
    $(categoryElement).addClass('text-dark');
    $(categoryElement).removeClass('text-muted');
    setTimeout(function() {
        AOS.refresh();
    }, 100); // Adjust the delay as needed
}