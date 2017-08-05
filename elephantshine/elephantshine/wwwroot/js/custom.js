(function($){
	"use strict";
	$(window).load(function() {
		$('.page-loader').delay(350).fadeOut('slow');
	});

	$(document).ready(function() {

		var hero        = $('#hero'),
			modules     = $('.module-hero, .module, .module-small'),
			navbar      = $('.navbar-custom'),
			worksgrid   = $('#works-grid'),
			filters     = $('#filters'),
			wrapper     = $('.wrapper'),
			footer      = $('.footer'),
			mobileTest;

	    shareSocial();

        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
			mobileTest = true;
		} else {
			mobileTest = false;
		}

        modules.each(function () {
			if ($(this).attr('data-background')) {
				$(this).css('background-image', 'url(' + $(this).attr('data-background') + ')');
			}
		});

		if (mobileTest === true) {
			$('.module-parallax').css({'background-attachment': 'scroll'});
		} else {
			$('#hero.module-parallax').parallax('50%', 0.2);
		}

		$(window).resize(function() {
			if (hero.length > 0 && hero.hasClass('js-fullheight') ) {
				hero.height($( window ).height());
			} else if (hero.length > 0) {
				hero.height($( window ).height() * 0.7);
			}
		}).resize();

		$('#slides').superslides({
			play: 10000,
			animation: 'fade',
			animation_speed: 800,
			pagination: true
		});

		$(".rotate").textrotator({
			animation: "dissolve",
			separator: "|",
			speed: 3000
		});

		if (navbar.length > 0 && hero.length > 0) {
			$(window).scroll(function() {
				var topScroll = $(window).scrollTop();
				if (topScroll >= 5) {
					navbar.removeClass('navbar-transparent');
				} else {
					navbar.addClass('navbar-transparent');
				}
			}).scroll();
		} else {
			navbar.removeClass('navbar-transparent');
		}

		$('a', filters).on('click', function() {
			var selector = $(this).attr('data-filter');

			$('.current', filters).removeClass('current');
			$(this).addClass('current');

			worksgrid.isotope({
				filter: selector
			});

			return false;
		});

		$(window).on('resize', function() {
			worksgrid.imagesLoaded(function() {
				worksgrid.isotope({
					layoutMode: 'masonry',
					itemSelector: '.work-item',
					transitionDuration: '0.3s',
				});
			});
		}).resize();

		$('.slider-images').owlCarousel({
			singleItem: true,
			autoHeight: false,
			navigation: true,
			pagination: false,
			autoPlay:   3000,
			navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
		});

		$('.carousel-clients').owlCarousel({
			singleItem: false,
			autoHeight: false,
			navigation: true,
			pagination: false,
			autoPlay:   3000,
			navigationText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
		});

		$(window).on('resize', function() {
			var width = Math.max($(window).width(), window.innerWidth);

			if (width > 767) {
				wrapper.css('margin-bottom', footer.outerHeight());
			} else {
				wrapper.css('margin-bottom', 0);
			}
		}).resize();

		$('.progress-bar').each(function() {
			$(this).appear(function() {
				var percent = $(this).attr('aria-valuenow');
				$(this).animate({'width' : percent + '%'});
				$(this).find('.progress-value').countTo({from: 0, to: percent, speed: 900, refreshInterval: 30});
			});
		});

		$('.counter').each(function() {
			$(this).appear(function() {
				var number = $(this).find('.counter-timer').attr('data-to');
				$(this).find('.counter-timer').countTo({from: 0, to: number, speed: 1500, refreshInterval: 30});
			});
		});

		$('.video-popup').magnificPopup({
			type: 'iframe'
		});

		$('.image-popup').magnificPopup({
			type: 'image'
		});

		$('a.project-gallery').magnificPopup({
			type: 'image',
			gallery: { enabled: true },
		});

		var wow = new WOW({
			mobile: false
		});

		wow.init();

		$(document).on('click','.navbar-collapse.in',function(e) {
			if( $(e.target).is('a') && $(e.target).attr('class') != 'dropdown-toggle' ) {
				$(this).collapse('hide');
			}
		});

		$('.section-scroll').on('click', function(e) {
			var target = this.hash;
			var $target = $(target);

			$('html, body').stop().animate({
				'scrollTop': $target.offset().top
			}, 900, 'swing');

			e.preventDefault();
		});

		$(window).scroll(function() {
			if ($(this).scrollTop() > 300) {
				$('.scroll-up').addClass('scroll-top-show');
			} else {
				$('.scroll-up').removeClass('scroll-top-show');
			}
		});

		$('a[href="#totop"]').on('click', function() {
			$('html, body').animate({ scrollTop: 0 }, 'slow');
			return false;
		});

		$('body').fitVids();

	    var utility = {
	        encodeUrl: encodeURIComponent(document.URL),
	        encodeTitle: encodeURIComponent(document.title)
        }

	    function shareSocial() {
	        $(".shareSocial").click(function (e) {
	            e.preventDefault();
	            var name = $(e.target).data("name");
	            if (name === "fb") {
	                window.open('https://www.facebook.com/sharer/sharer.php?u=' + utility.encodeUrl + '&amp;t=' + utility.encodeUrl);
	            } else if (name === "tr") {
	                window.open('https://twitter.com/intent/tweet?text=' + utility.encodeTitle + ':%20' + utility.encodeUrl);
	            } else if (name === "g+") {
	                window.open('https://plus.google.com/share?url=' + utility.encodeUrl);
	            }
	        });
        }
	});

})(jQuery);
