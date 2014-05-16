var eazyWizyLHSMenu = {
    HOVERSTATE: 'ew-hover',
    MOUSEENTER: 'mouseenter',
    MOUSELEAVE: 'mouseleave',
    CLICK: 'click',
    ITEMSELECTOR: '.ew-lhs-li',

    _init: function () {
        this._bindingEvents();
        this._initializeSubMenu();
    },

    _bindingEvents: function () {
        $('#divLHSMenu').find(this.ITEMSELECTOR).bind(this.MOUSEENTER, $.proxy(this._onMouseEnter, this));
        $('#divLHSMenu').find(this.ITEMSELECTOR).bind(this.MOUSELEAVE, $.proxy(this._onMouseLeave, this));
    },

    _onMouseEnter: function (event) {
        var targetElement = $(event.target).closest(this.ITEMSELECTOR);
        targetElement.addClass(this.HOVERSTATE);
        event.stopPropagation();

        //Initiate Sub Div
        this._subMenuDisplay(targetElement);
    },

    _onMouseLeave: function (event) {
        var targetElement = $(event.target).closest(this.ITEMSELECTOR);       
        targetElement.removeClass(this.HOVERSTATE);
        event.stopPropagation();

        //Hide Sub Div
        this._subMenuHide(targetElement);
    },

    _subMenuDisplay: function (_target) {
        //parent element width
        var _width = $('#divLHSMenu').width();
        var _left =  $('#divLHSMenu').position().left;
        var _top = $('#divLHSMenu').position().top;

        var subDiv = _target.find('.ew-lhs-subdiv');
        subDiv.css('top', _top);
        subDiv.css('left', _left + _width + 1);
        subDiv.fadeIn(function () { subDiv.css('display', 'block'); });        
    },

    _subMenuHide: function (_target) {
        var subDiv = _target.find('.ew-lhs-subdiv').fadeOut(function () {
            subDiv.css('display', 'none');
        });
    },

    _initializeSubMenu: function (event) { 
        $('.ew-lhs-sub-li').bind(this.CLICK, $.proxy(this._addFilter, this))
    },

    _removeFilter: function (event) {
        var targetElement = $(event.target).closest('ew-span-filter');
        targetElement.remove();
    }
}

//Brand Filter Menu Manipulation
var eazyWizyBrandFilter = {
    HOVERSTATE: 'ew-brand-filter-hover',
    MOUSEENTER: 'mouseenter',
    MOUSELEAVE: 'mouseleave',
    CLICK: 'click',
    ITEMSELECTOR: '.ew-lhs-filter-li',
    ITEMSELECTORBRAND: '.ew-lhs-div-brand-filter',
    ITEMSELECTORBRANDLIST: '.ew-lhs-div-brandlist-filter',

    _init: function () {
        this._bindingEvents();
    },

    _bindingEvents: function () {
        $('.ew-lhs-div-brand-filter .ew-lhs-filter-li').bind(this.CLICK, $.proxy(this._onClick, this))
        $('.ew-lhs-div-filterclear').bind(this.CLICK, $.proxy(this._onClickClear, this))

        $('.div-lhs-filter').find(this.ITEMSELECTOR).bind(this.MOUSEENTER, $.proxy(this._onMouseEnter, this));
        $('.div-lhs-filter').find(this.ITEMSELECTOR).bind(this.MOUSELEAVE, $.proxy(this._onMouseLeave, this));

        this._bindOnClickHeaderFilter();  
    },

    _bindOnClickHeaderFilter: function () {
        $('.ew-lhs-brand-header-filter').bind(this.CLICK, $.proxy(this._onBrandFilterClick, this));
    },
    _unBindOnClickHeaderFilter: function () {
        $('.ew-lhs-brand-header-filter').unbind(this.CLICK);
    },

    _onBrandFilterClick: function (event) {
        var thiz = this;
        var targetElement = $(event.target);
        if (!(targetElement.is('span') || targetElement.is('img'))) {
            thiz._unBindOnClickHeaderFilter();
            var _actHeight = $(this.ITEMSELECTORBRANDLIST).height();
            $(this.ITEMSELECTORBRANDLIST).toggle(function(){
                $(this).animate({ height: "0px" }, 500, "linear", function () {
                    thiz._bindOnClickHeaderFilter();
                })
            },
            function () {
                $(this).animate({ height:_actHeight }, 500, "linear", function () {
                    thiz._bindOnClickHeaderFilter();
                })           
            });
        }       
    },

    _onClick: function (event) {
        var targetElement = $(event.target);
        if (targetElement.is('input'))
        {
            return;
        }
        else {
            if (!targetElement.is('li')) {
                targetElement = targetElement.closest(this.ITEMSELECTOR);
            }
            var _checkbox = targetElement.find(':checkbox');
            if (_checkbox.prop('checked')) {
                _checkbox.prop('checked', false);
            }
            else {
                _checkbox.prop('checked', true);
            }
        }
    },

    _onMouseEnter: function (event) {
        var targetElement = $(event.target).closest(this.ITEMSELECTOR);
        targetElement.addClass(this.HOVERSTATE);
        event.stopPropagation();
    },

    _onMouseLeave: function (event) {
        var targetElement = $(event.target).closest(this.ITEMSELECTOR);       
        targetElement.removeClass(this.HOVERSTATE);
        event.stopPropagation();
    },

    _onClickClear: function () {
        $(this.ITEMSELECTORBRAND).find(':checkbox').each(function () {
            $(this).prop('checked', false);
        });
    }
}

$(document).ready(function () {
    eazyWizyLHSMenu._init();
    eazyWizyBrandFilter._init();
});