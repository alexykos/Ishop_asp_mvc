$(document).ready(function(){
	$('.registration__klient_label1').click(function(){
		$('.registration__organization').removeClass('registration__organization_active');
	});
	$('.registration__klient_label2').click(function(){
		$('.registration__organization').addClass('registration__organization_active');
	});
	$(".input_mask").inputmask("+7(999)999-99-99");		
	$.extend($.inputmask.defaults.aliases, {
		'non-negative-integer': {
			regex: {
				number: function (groupSeparator, groupSize) { return new RegExp("^(\\d*)$"); }
			},
			alias: "decimal"
		}
	});
});