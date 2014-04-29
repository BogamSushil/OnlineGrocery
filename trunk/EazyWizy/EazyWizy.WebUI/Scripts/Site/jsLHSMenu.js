var eazyWizyLHSMenu = {
    HOVERSTATE: 'ew-hover',
    MOUSEENTER: 'mouseenter',
    MOUSELEAVE: 'mouseleave',
    CLICK: 'click',
    ITEMSELECTOR: '.ew-lhs-li',
    FILTERSELECTOR: '.ew-div-filters',
    FILTERTEMPLATE: "<span class='ew-span-filter'><span>#data#<span/><img class='ew-lhs-filterimage' src='Content/Images/crossFilter.png' alt='' /></span>",

    _init: function () {
        this._bindingEvents();
        this._initializeSubMenu();
    },

    _bindingEvents: function () {
        $('#divLHSMenu').find('.ew-lhs-li').bind(this.MOUSEENTER, $.proxy(this._onMouseEnter, this));
        $('#divLHSMenu').find('.ew-lhs-li').bind(this.MOUSELEAVE, $.proxy(this._onMouseLeave, this));
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

    _addFilter: function (event) {
        //var targetElement = $(event.target).closest('li');
        //var txt = targetElement.text();
        //var fs = $(this.FILTERSELECTOR);
        //var ft = this.FILTERTEMPLATE;
        //fs.append(ft.replace('#data#', txt));

        //ft.find('.ew-span-filter').bind(this.CLICK, $.proxy(this._removeFilter, this));
    },

    _removeFilter: function (event) {
        var targetElement = $(event.target).closest('ew-span-filter');
        targetElement.remove();
    }
}

var eazyWizyBrandFilter = {
    _init: function () {
        $('.ew-lhs-div-brand-filter').accordion({
            heightStyle: "fill"
        });

        $(".ew-lhs-div-brand-filter").resizable({
            minHeight: 140,
            minWidth: 200,
            resize: function () {
                $("#accordion").accordion("refresh");
            }
        });

    }

}

$(document).ready(function () {
    eazyWizyLHSMenu._init();
    eazyWizyBrandFilter._init();
});