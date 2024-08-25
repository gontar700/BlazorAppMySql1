$(document).ready(function() {
    // Fade in the image and move it slightly upwards
    $('#university').fadeIn(3000).css('transform', 'translateY(-20px)');

    // Fade in the login form and move it upwards
    $('.login-form').css('display', 'block'); // Ensure the form is displayed
    setTimeout(function () {
        $('.login-form').css({
            'opacity': 1,
            'transform': 'translateY(0px)'
        });
    }, 2000); // Start the animation after the image has finished fading in
 });