//Abhishek Vyas
//01-May-2014
//Caution: If you dont understand anything please ask me..

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
var eazyWizyLHSSubFilter = {
    HOVERSTATE: 'ew-brand-filter-hover',
    MOUSEENTER: 'mouseenter',
    MOUSELEAVE: 'mouseleave',
    CLICK: 'click',
    ITEMSELECTOR: '.ew-lhs-filter-li',
    MENULISTCONTAINERDIVHEADER: '.ew-lhs-header-filter',
    MENUSELECTOR: '.ew-lhs-div-filter',   

    _init: function () {
        this._bindingEvents();
    },

    _bindingEvents: function () {
        $(this.ITEMSELECTOR).bind(this.CLICK, $.proxy(this._onClick, this))
        $('.ew-lhs-div-filterclear').bind(this.CLICK, $.proxy(this._onClickClear, this))

        $('.div-lhs-filter').find(this.ITEMSELECTOR).bind(this.MOUSEENTER, $.proxy(this._onMouseEnter, this));
        $('.div-lhs-filter').find(this.ITEMSELECTOR).bind(this.MOUSELEAVE, $.proxy(this._onMouseLeave, this));

        this._bindOnClickHeaderFilter();  
    },

    _bindOnClickHeaderFilter: function () {
        $('.ew-lhs-header-filter').bind(this.CLICK, $.proxy(this._onHeaderFilterClick, this));
    },

    _unBindOnClickHeaderFilter: function () {
        $('.ew-lhs-header-filter').unbind(this.CLICK);
    },

    _onHeaderFilterClick: function (event) {
        var thiz = this;
        var targetElement = $(event.target);
        var listDiv = targetElement.closest(thiz.MENULISTCONTAINERDIVHEADER).next();
        //Below condition is to avoid closing of list menu and if one clicks on "clear" lable or 'x' icon
        //then it should work as clearing the filters but not hiding the menu
        if (!(targetElement.is('span') || targetElement.is('img'))) {
            //below is to avoid constinuous click of menu header so better unbind the click event.
            thiz._unBindOnClickHeaderFilter();

            //Animation to close the menu                
            var _actHeight = $(listDiv).height();          
            $(listDiv).clearQueue();
            $(listDiv).stop();
            $(listDiv).toggle(function () {
                $(this).animate({ height: "0px" }, 500, "linear", function () {
                    //Re-binding the click event once animation completes
                    thiz._bindOnClickHeaderFilter();
                })
            },
            function () {
                $(this).animate({ height: _actHeight }, 500, "linear", function () {
                    //Re-binding the click event once animation completes
                    thiz._bindOnClickHeaderFilter();
                })           
            });
        }       
    },

    _onClick: function (event) {
        var targetElement = $(event.target);
        //If the click event is because of input [checkbox] then there is no need to do anything.
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

    _onClickClear: function (event) {
        var targetElement = event.target;
        var divMenuSelector = 'div' + this.MENUSELECTOR; //Remember div.ew-lhs-div-filter
        var topMostDivElement = $(targetElement).closest(divMenuSelector); //to find parent div so that only that checkboxes cleared
                                                            //which are present in that particual filter 
        topMostDivElement.find(':checkbox').each(function () {
            $(this).prop('checked', false);
        });
    }
}
$(document).ready(function () {
    eazyWizyLHSMenu._init();
    eazyWizyLHSSubFilter._init();
});