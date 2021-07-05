
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
$(window).ready(function () {
    $("#pageLoad").delay(500).fadeOut("slow", function () {
        $("body").css({ overflow: "auto" });
    });
});
$(window).on("load", function () {
    loadImages();
});


function loadImages() {
    let images = $(".unloadedImage");
    
    for (let image of images) {
        let newImage = document.createElement("img");
        newImage.src = $(image).data("src");
        $(newImage).on('load', function () {
            $(image).attr("src", this.src);
           /* let sources = $(image).siblings("source");
            let loadedSrc = sources[sources.length - 1];
            let loaded = $(loadedSrc).attr('loaded');
            if (typeof loaded !== typeof undefined && loaded !== false) {
                // $(sources[sources.length - 1]).remove();
                // $(image).parent().prepend(loadedSrc);
                $(image).parent().prepend(loadedSrc);
                //$(sources[0]).attr("srcset", $(loadedSrc).attr("srcset"));
                //$(sources[0]).attr("type", $(loadedSrc).attr("type"));
            }*/
            $(image).removeClass("unloadedImage");
            if ($(image).parent().parent().hasClass("blur")) {
                $(image).parent().parent().removeClass("blur");
                $(image).addClass("unblur");
            }
        });
        $(newImage).on('error', function () {
            $(image).removeClass("unloadedImage");
            $(image).parent().parent().removeClass("blur");
            $(image).addClass("unblur");
        });
    }
}
    
    