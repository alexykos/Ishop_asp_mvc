(function($) {
$(function() {

	$('div.header__nav-toggle').click(function() {
		var nav = $('nav.header__nav');
		if (nav.is('.active')) {
			nav.removeClass('active');
		} else {
			nav.addClass('active');
		}
	});

	$(document).click(function() {
		$('nav.header__nav').removeClass('active');
	});

	$('div.header__nav-toggle, nav.header__nav').click(function(e) {
		e.stopPropagation();
	});

	if ( $('div.slider').length ) {
		$('div.slider').slick({
			dots: true,
			// autoplay: true,
			// autoplaySpeed: 5000,
		});
	}

	if ( $('div.offers').length ) {
		$('div.offers div.items').slick({
			// autoplay: true,
			// autoplaySpeed: 5000,
			slidesToShow: 3,
			responsive: [{
				breakpoint: 768,
				settings: {
					slidesToShow: 2,
				}
			}, {
				breakpoint: 480,
				settings: {
					slidesToShow: 1,
				}
			}],
		});
	}

	if ( $('div.related').length ) {
		$('div.related div.items').slick({
			// autoplay: true,
			// autoplaySpeed: 5000,
			slidesToShow: 4,
			responsive: [{
				breakpoint: 1000,
				settings: {
					slidesToShow: 3,
				}
			}, {
				breakpoint: 768,
				settings: {
					slidesToShow: 2,
				}
			}, {
				breakpoint: 480,
				settings: {
					slidesToShow: 1,
				}
			}],
		});
	}

	$('div.catalog__title').click(function() {
		var catalog = $(this).closest('div.catalog');
		var ul = catalog.find('ul');
		if ( catalog.is('.catalog--hidden') ) {
			catalog.removeClass('catalog--hidden');
		} else {
			catalog.addClass('catalog--hidden');
		}
		if ( ul.is(':hidden') ) {
			catalog.addClass('active');
		} else {
			catalog.removeClass('active');
		}
	});

	$('div.filter__title').click(function() {
		var filter = $(this).closest('div.filter');
		var filter_inner = $('div.filter__inner');
		if ( filter_inner.is(':hidden') ) {
			filter.addClass('active');
		} else {
			filter.removeClass('active');
		}
	});

	$('<div id="up" class="up">наверх</div>').appendTo('footer.footer').click(function() {
		$('html, body').animate({scrollTop: 0}, 700);
	});
	var win = $(window);
	var up = $('#up');
	win.on('scroll', function() {
		if ( win.scrollTop() > win.height() / 3 ) {
			up.fadeIn();
		} else {
			up.fadeOut();
		}
	}).scroll();

	$('ul.tabs__caption').on('click', 'li:not(.active)', function() {
		$(this)
			.addClass('active').siblings().removeClass('active')
			.closest('div.tabs').find('div.tabs__content').removeClass('active').eq($(this).index()).addClass('active');
	});

	$('div.product__photo div.thumb').click(function() {
		$(this).addClass('active').siblings().removeClass('active');
		var big = $(this).data('big');
		$('div.product__photo-big img').attr('src', big);
	});
	
});
})(jQuery);