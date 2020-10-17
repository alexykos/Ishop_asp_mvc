(function($) {
$(function () {

                        $(".item__buy, .product__buy").click(function () {

                            var recordToAdd = $(this).attr("data-id");
                            alert("recordToAdd =" + recordToAdd);
                            if (recordToAdd != '') {

                                $.post("/ShoppingCart/AddToCart", { "id": recordToAdd },
                                    function (data) {
                                        $('.header__cart-count-2').text(data.CartCount);
                                        $('a[data-id=' + recordToAdd + ']').toggleClass(".item__buy a_visited");

                                    });

                            };
                        });

                        $('.new__li0 #update-container').click(function (e) {


                            //e.preventDefault();
                            var tree_id = $(this).attr("tree_id");
                            //alert("tree_id = " + tree_id);
                            @*$.ajax({
                                url: '@Url.Action("IndexClassPart", "Home", "new { id = 41}")',
                                success: function (data) {
                                    $('#list_items').html(data);
                                }
                            });*@
                            $.post("/Home/IndexClassPart", { "id": tree_id },
                            function (data) {
                                //alert("tree_id11 = " + data);
                                $('#list_items111').html(data);
                                //alert("tree_id12 = " + data);
                            });
                        });


                    });
})(jQuery);