$(document).ready(function(){
	$('.new__li').click(function() {
		$(this).toggleClass('expandable');
		return false;
	});
	$('.sort__button_left').click(function() {
		$(this).removeClass('sort__button_active');
		$('.items').addClass('items__active');
		$('.sort__button_right').addClass('sort__button_active');
	});
	$('.sort__button_right').click(function() {
		$(this).removeClass('sort__button_active');
		$('.sort__button_left').addClass('sort__button_active');
		$('.items').removeClass('items__active');
	});
});