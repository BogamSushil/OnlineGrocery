//The remove and add would be applied to the cart class for the icons.
//Currently using dummy data
var CartFunctions = {
    CLICK: 'click',
    _init: function () {
        //
        this._bindEvents();
    },
    _destroy: function () {
        //
    },

    _bindEvents: function () {
        var thiz = this;
        $('.product_shopping-cart').bind(thiz.CLICK, $.proxy(this._onCartClick, thiz));
    },

    _onCartClick: function (event) {
        //
        //if the click comesfrom red call removed..wewill dealwith it later
        //Now only using AddToCart
        var thiz = this;
        thiz._addToCart();
    },

    _addToCart: function () {
        var thiz = this;
        var prooduct_id = $(thiz).attr("productId");//product ID
        var product_quantity = 1;//$(thiz).attr("");//product quantity
        if (producti_id != null && product_quantity != null && product_quantity > 0) {
            $.post("/Cart/AddToCart", { "productId": product_id },
                function (data) {
                    //Update the cart message
                });
        }
    },

    _removeFromCart: function () { }
}

$(document).ready(function () {
    CartFunctions._init();
});