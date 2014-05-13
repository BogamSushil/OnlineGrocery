var toggle = function () {
    alert('in');
    $(this).closest('').toggleClass('flip').find('.back').html(new Date().getTime());

}
$(document).ready(function () {
    $('.hover').hover(function () {
        $(this).closest(".panel").addClass('flip');
    }, function () {
        $(this).closest(".panel").removeClass('flip');
    });

    $('.click').click(function () { $(this).toggleClass('flip').find('.back').html(new Date().getTime()); });

    eazyWizyProductList._init();
});

var eazyWizyProductList = {
    CONTAINER: '.ew-div-productlist-partial',
    DIVNUTRI: '.ew-div-nutri-details',
    DIVPRODUCT: '.ew-div-product-details',
    CLICK: 'click',

    _init: function () {
        var thiz = this;
        thiz._bindingEvents();
    },

    _bindingEvents: function () {
        var thiz = this;
        //nutritional div
        $(thiz.CONTAINER).find('.ew-div-nutriinfo').bind(this.CLICK, $.proxy(this._onClickNutriInfo, this));

        $(this.DIVNUTRI).find('.ew-div-nutriinfo-close').bind(this.CLICK, $.proxy(this._onClickCloseNutriDetails, this));
       
    },

    _onClickNutriInfo: function (event) {
        var thiz = this;
        var _elementNutriDetails = $(thiz.CONTAINER).find(thiz.DIVNUTRI);
        var _elementProdDetails = $(thiz.CONTAINER).find(thiz.DIVPRODUCT);
        $(_elementNutriDetails).clearQueue();
        $(_elementNutriDetails).stop();
        $(_elementProdDetails).clearQueue();
        $(_elementProdDetails).stop();

        $(_elementProdDetails).animate({ height: "0px" }, "fast");
        $(_elementNutriDetails).animate({ height: "305px" }, "fast");
    },

    _onClickCloseNutriDetails: function (event) {
        var thiz = this;
        var _elementNutriDetails = $(thiz.CONTAINER).find(thiz.DIVNUTRI);
        var _elementProdDetails = $(thiz.CONTAINER).find(thiz.DIVPRODUCT);
        $(_elementNutriDetails).clearQueue();
        $(_elementNutriDetails).stop();
        $(_elementProdDetails).clearQueue();
        $(_elementProdDetails).stop();

        $(_elementProdDetails).animate({ height: "305px" }, "fast");
        $(_elementNutriDetails).animate({ height: "0px" }, "fast");
    }
}