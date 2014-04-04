// JavaScript Document

$(document).ready(function () {

    $("#horizontal").kendoSplitter({
        panes: [
            { collapsible: true, resizable: false, size: "250px" },
            { collapsible: false, resizable: false }
        ]
    });
    $("#panelbar").kendoPanelBar({
        expandMode: "single"
    });

    $("#horizontal").hover(function () {
        $(".k-icon.k-collapse-prev").attr('title', 'Click here to collapse');
        $(".k-icon.k-expand-prev").attr('title', 'Click here to expand');
    });
    $("#menu-images").kendoMenu({
        dataSource: [
            {
                text: "Welcome Admin!", imageUrl: "content/shared/icons/web/user.png",
                items: [
                    { text: "Edit Profile", imageUrl: "content/shared/icons/web/edit_profile.png" },
                    { text: "Logout", imageUrl: "content/shared/icons/web/logout.png" }
                ]
            }
        ]
    }).css({ float: "left" }).data("kendoMenu");
    $("#menu-images li").css({ width: "100%" });


    $("#tabstrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });
   
    $('#centerKey').on('click', function () {
        $('.ddlogin').show();
    });
    $('.hexeg, .hexeg2, .hexeg3, .hexeg4, .hexeg5, .hexeg6, .hexeg7, .hexeg8, .carousel-control').on('click', function () {
        $('#window2').hide();
    });
        
    //This is info show for Bulk user upload
    $('#info_show').click(function () {
        $('.inboxbulkuser_info').slideToggle();
    });
    $('#rollUp').click(function () {
        $('.inboxbulkuser_info').slideUp();
    });


    if ((navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPad/i))) {
        $('.progressDetails .pText1').css('font-size', '10px');
    }   
  

/* ========================================================================
 * Bootstrap: carousel.js v3.0.0
 * http://twbs.github.com/bootstrap/javascript.html#carousel
 * ========================================================================
 * Copyright 2012 Twitter, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ======================================================================== */


+function ($) { "use strict";

  // CAROUSEL CLASS DEFINITION
  // =========================

  var Carousel = function (element, options) {
    this.$element    = $(element)
    this.$indicators = this.$element.find('.carousel-indicators')
    this.options     = options
    this.paused      =
    this.sliding     =
    this.interval    =
    this.$active     =
    this.$items      = null

    this.options.pause == 'hover' && this.$element
      .on('mouseenter', $.proxy(this.pause, this))
      .on('mouseleave', $.proxy(this.cycle, this))
  }

  Carousel.DEFAULTS = {
  //  interval: 5000
  //, pause: 'hover'
  	pause: true
  , wrap: true
  }

  Carousel.prototype.cycle =  function (e) {
    e || (this.paused = false)

    this.interval && clearInterval(this.interval)

    this.options.interval
      && !this.paused
      && (this.interval = setInterval($.proxy(this.next, this), this.options.interval))

    return this
  }

  Carousel.prototype.getActiveIndex = function () {
    this.$active = this.$element.find('.item.active')
    this.$items  = this.$active.parent().children()

    return this.$items.index(this.$active)
  }

  Carousel.prototype.to = function (pos) {
    var that        = this
    var activeIndex = this.getActiveIndex()

    if (pos > (this.$items.length - 1) || pos < 0) return

    if (this.sliding)       return this.$element.one('slid', function () { that.to(pos) })
    if (activeIndex == pos) return this.pause().cycle()

    return this.slide(pos > activeIndex ? 'next' : 'prev', $(this.$items[pos]))
  }

  Carousel.prototype.pause = function (e) {
    e || (this.paused = true)

    if (this.$element.find('.next, .prev').length && $.support.transition.end) {
      this.$element.trigger($.support.transition.end)
      this.cycle(true)
    }

    this.interval = clearInterval(this.interval)

    return this
  }

  Carousel.prototype.next = function () {
    if (this.sliding) return
    return this.slide('next')
  }

  Carousel.prototype.prev = function () {
    if (this.sliding) return
    return this.slide('prev')
  }

  Carousel.prototype.slide = function (type, next) {
    var $active   = this.$element.find('.item.active')
    var $next     = next || $active[type]()
    var isCycling = this.interval
    var direction = type == 'next' ? 'left' : 'right'
    var fallback  = type == 'next' ? 'first' : 'last'
    var that      = this

    if (!$next.length) {
      if (!this.options.wrap) return
      $next = this.$element.find('.item')[fallback]()
    }

    this.sliding = true

    isCycling && this.pause()

    var e = $.Event('slide.bs.carousel', { relatedTarget: $next[0], direction: direction })

    if ($next.hasClass('active')) return

    if (this.$indicators.length) {
      this.$indicators.find('.active').removeClass('active')
      this.$element.one('slid', function () {
        var $nextIndicator = $(that.$indicators.children()[that.getActiveIndex()])
        $nextIndicator && $nextIndicator.addClass('active')
      })
    }

    if ($.support.transition && this.$element.hasClass('slide')) {
      this.$element.trigger(e)
      if (e.isDefaultPrevented()) return
      $next.addClass(type)
      $next[0].offsetWidth // force reflow
      $active.addClass(direction)
      $next.addClass(direction)
      $active
        .one($.support.transition.end, function () {
          $next.removeClass([type, direction].join(' ')).addClass('active')
          $active.removeClass(['active', direction].join(' '))
          that.sliding = false
          setTimeout(function () { that.$element.trigger('slid') }, 0)
        })
        .emulateTransitionEnd(600)
    } else {
      this.$element.trigger(e)
      if (e.isDefaultPrevented()) return
      $active.removeClass('active')
      $next.addClass('active')
      this.sliding = false
      this.$element.trigger('slid')
    }

    isCycling && this.cycle()

    return this
  }


  // CAROUSEL PLUGIN DEFINITION
  // ==========================

  var old = $.fn.carousel

  $.fn.carousel = function (option) {
    return this.each(function () {
      var $this   = $(this)
      var data    = $this.data('bs.carousel')
      var options = $.extend({}, Carousel.DEFAULTS, $this.data(), typeof option == 'object' && option)
      var action  = typeof option == 'string' ? option : options.slide

      if (!data) $this.data('bs.carousel', (data = new Carousel(this, options)))
      if (typeof option == 'number') data.to(option)
      else if (action) data[action]()
      else if (options.interval) data.pause().cycle()
    })
  }

  $.fn.carousel.Constructor = Carousel


  // CAROUSEL NO CONFLICT
  // ====================

  $.fn.carousel.noConflict = function () {
    $.fn.carousel = old
    return this
  }


  // CAROUSEL DATA-API
  // =================

  $(document).on('click.bs.carousel.data-api', '[data-slide], [data-slide-to]', function (e) {
	var $this   = $(this), href
    var $target = $($this.attr('data-target') || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')) //strip for ie7
    var options = $.extend({}, $target.data(), $this.data())
    var slideIndex = $this.attr('data-slide-to')
    if (slideIndex) options.interval = false

    $target.carousel(options)

    if (slideIndex = $this.attr('data-slide-to')) {
		
	  	
      $target.data('bs.carousel').to(slideIndex)
    }

    e.preventDefault()
  })

  $(window).on('load', function () {
    $('[data-ride="carousel"]').each(function () {
      var $carousel = $(this)
      $carousel.carousel($carousel.data())
	  
	})
	//$('div#centerKey').append('<span class="dd77"></span>'); //Optional Shadow
  })
   
 

}(window.jQuery);

});


var getInternalDisplay = function (fileName, isUrl) {
    if (isUrl) {
        return iframeDiv.replace('{0}', fileName);
    }
    var extentionIndex = fileName.lastIndexOf('.');
    var extension = fileName.substr(extentionIndex + 1).toLowerCase();
    if (extension == "m4v" || extension == "mov" || extension == "mp4" || extension == "ogg" || extension == "webm") {
        return videoDiv.replace('{0}', fileName);
    }
    else if (extension == "mp3") {
        return audioDiv.replace('{0}', fileName);
    }
    else if (extension == "bmp" || extension == "jpg" || extension == "jpeg" || extension == "gif" || extension == 'png') {
        return imageDiv.replace('{0}', fileName);
    }
    else if (extension == "pdf" || extension == "txt")  {
        return iframeDiv.replace('{0}', fileName);
    }
    else if(extension == "html" || extension == "htm")
    {
        return "html" + fileName;
    }
    else {
        return "download" + iframeDiv.replace('{0}', fileName);;
        //return iframeDiv.replace('{0}', 'http://docs.google.com/viewer?url=' + fileName);
    }
}

var videoDiv = "<video  autoplay controls style='width: 100%; height: 100%;'> <source src='{0}' /> </video>";
var audioDiv = "<audio  autoplay controls style='width: 100%; height: 100%;'> <source src='{0}' /> </video>";
var imageDiv = "<img alt='img' id='imageCtr' src='{0}' style 'height:100%; width:100%;' />";
var iframeDiv = "<iframe id='urlContainer' src= '{0}' style='width: 100%; height: 100%;'></iframe>"