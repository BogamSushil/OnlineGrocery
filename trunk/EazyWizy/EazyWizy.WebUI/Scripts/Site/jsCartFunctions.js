//The remove and add would be applied to the cart class for the icons.
//Currently using dummy data
var CartFunctions = {
    CLICK: 'click',
    HOVER: 'hover',
    MOUSELEAVE:'mouseleave',
    _init: function () {
        this._bindEvents();
    },
    _destroy: function (event) {
        //Destrutor to be written here
        event = null;
    },

    _bindEvents: function () {
        var thiz = this;
        $('.addToCart').bind(thiz.CLICK, $.proxy(thiz._onAddCartClick, thiz));
        $('.removefromcart').bind(thiz.CLICK, $.proxy(thiz._removeFromCart, thiz));
        $('#headercart').bind(thiz.HOVER, $.proxy(thiz._onCartHover, thiz));
        $('#bodycart').bind(thiz.MOUSELEAVE, $.proxy(thiz._onCartMouseLeave, thiz));
    },

    _onAddCartClick: function (event) {
        var thiz = this;
        thiz._addToCart(event);
    },

    _onCartHover:function () {
        $('#bodycart').slideDown("slow");
        //$('#search').css('margin-right', '98px');
    },

    _onCartMouseLeave:function () {
        $('#bodycart').slideUp("slow");
        //$('#search').css('margin-right', '324px');
    },

    _addToCart: function (event) {
        var thiz = this;
        var currentEvtSelector = event.target.className; //$('.' + $(event)[0].target.className);
        var product_id = $("."+currentEvtSelector).attr('productid');//Still Issue with getting the product ID

        var product_quantity = 1;//$(thiz).attr("");//product quantity
        //Need To Add Quantity From DropDown
        //Identify product id
        if (product_id != null && product_quantity != null && product_quantity > 0) {
            $.post("/Cart/AddToCart", { "productId": product_id, "quantity": product_quantity },
                function (data) {
                    alert(data.Message + " Total : " + data.CartTotal + " ItemCount : " + data.ItemCount);
                    $.post('Cart/Summary', function (result) {
                        $('#headercart').html(result);
                    });
                    $.post('Cart/CartDetails', function (result) {
                        $('#bodycart').html(result);
                    });
                })
                .done(function () {
                    //alert( "second success" );
                })
                .fail(function () {
                    //alert( "error" );
                })
                .always(function () {
                    //alert( "finished" );
                }
                );
            
        }       
        //thiz._destroy(event);
    },

    

    _removeFromCart: function (event) {
        var thiz = this;
        var currentEvtSelector = $('.' + $(event)[0].target.className);
        var product_id = $(currentEvtSelector).attr('productid');
                
        if (product_id != null && product_id != '') {
            $.post("/Cart/RemoveFromCart", { "productId": product_id },
                function (data) {
                    alert(data.Message + " Total : " + data.CartTotal + " ItemCount : " + data.ItemCount);
                    $.post('Cart/Summary', function (result) {
                        $('#headercart').html(result);
                    });
                })
                .done(function () {
                    //alert( "second success" );
                })
                .fail(function () {
                    //alert( "error" );
                })
                .always(function () {
                    //alert( "finished" );
                });
        }
    }
}
    

$(document).ready(function () {
    CartFunctions._init();
    $('#headercart').hover(function () {
        CartFunctions._onCartHover();
    });
});