
//HomePage Nav bar
var isMobileNavOpen = false;
$(".hamburger-menu-button").click(function(){
    if(!isMobileNavOpen){
        $(".hamburger-menu-button").addClass("active");
        isMobileNavOpen = true;
        $(".mobile-nav-content").fadeIn(250);
    }
    else{
        $(".hamburger-menu-button").removeClass("active");
        isMobileNavOpen = false;
        $(".mobile-nav-content").fadeOut(250);
    }
})



//Add desktop nav on scroll
jQuery(function($) {

    var $nav = $('nav.desktop-scroll-top');
    var $win = $(window);
    var winH = $win.height();   // Get the window height.

    $win.on("scroll", function () {
        if ($(this).scrollTop() > winH ) {
            $nav.addClass("active");
        } else {
            $nav.removeClass("active");
        }
    }).on("resize", function(){ // If the user resizes the window
       winH = $(this).height(); // you'll need the new height value
    });

});


//Gallary PopUpSlider
var gallery = $('.gallery  a').simpleLightbox({
        docClose:false
    });
    
    