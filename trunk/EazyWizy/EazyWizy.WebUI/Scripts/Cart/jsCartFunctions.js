var CartFunctions = {
    _init: function () {
        //
    },
    _destroy: function () {
        //
    },

    _addToCart: function () {
        var thiz = this;
        var prooduct_id = $(thiz).attr("");//product ID
        var product_quantity=$(thiz).attr("");//product quantity
        if (producti_id != null && product_quantity != null && product_quantity > 0)
        {
            $.post("/Cart/AddToCart", { "productId": product_id },
                function (data) {
                    //Update the cart message
                });
        }
    },

    _removeFromCart:function(){}
}