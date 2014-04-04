//Title: Hovercard plugin by PC 
//Documentation: http://designwithpc.com/Plugins/Hovercard
//Author: PC 
//Website: http://designwithpc.com
//Twitter: @chaudharyp
//
//Version 1.0 
//Aug 31st 2011 -First Release.
//
//Version 1.1
//Sep 29th 2011 
//Bug fixed: When using hovercard in hovercard, the inner hovercard shows up as well when parent hovercard opens.
//
//Version 1.2
//Sep 30th 2011
//Enhancement: The hovercard now adjust (either open on left or right) in accordance to the view port.
//Added option:
//openOnLeft: force hovercard to open on left. (eg: if the hovered name appear in the end of sentence)
//
//Version 2.0
//Nov 8th 2011
//Bug Fixes: zindex issue.
//Enhancements: Supercool built in social profile cards like Twitter and Facebook!
//Added options:
//showTwitterCard: displays a built in twitter card format for a twitter screenname. Maximum 150 twitter lookup per hour.
//twitterScreenName: twitter screen name for the hovercard. If no username/screenname is provided, hovercard attempts to look up for hovered text.
//showFacebookCard: displays a built in facebook card format for a facebook username/pages/events etc. Works best with Facebook pages.
//facebookUserName: facebook username/pages/events/groups for the hovercard. If no username is provided, hovercard attempts to look up for hovered text.


(function ($) {
    $.fn.hovercard = function (options) {

        //Set defauls for the control
        var defaults = {
            width: 300,
            openOnLeft: false,
            cardImgSrc: "",
            detailsHTML: "",
            twitterScreenName: '',
            showTwitterCard: false,
            facebookUserName: '',
            showFacebookCard: false,
            showCustomCard: false,
            customDataUrl: '',
            background: "#ffffff",
            delay:1000,
            onHoverIn: function () { },
            onHoverOut: function () { $("#bAssemblyContainer").css("overflow", "hidden"); }
        };
        //Update unset options with defaults if needed
        var options = $.extend(defaults, options);

        //CSS for hover card. Change per your need, and move these styles to your stylesheet (recommended).
        if ($('#css-hovercard').length <= 0) {
            var hovercardTempCSS = '<style id="css-hovercard" type="text/css">' +
                                    '.hc-preview { position: relative; display:inline; }' +
                                    '.hc-name { font-weight:bold; position:relative; display:inline-block; }' +
                                    '.hc-details { padding:12px;font-size:12px !important; color:#666 !important; line-height:1.3em;  position:absolute;-moz-border-radius:3px;-webkit-border-radius:3px;border-radius:3px;top:-10px;z-index:50;padding:1em 10px 10px;-moz-box-shadow:5px 5px 5px #888;-webkit-box-shadow:5px 5px 5px #888;box-shadow:5px 5px 5px #888;display:none;}' +
                                    '.hc-pic { width:70px; margin-top:-1em; }' +
                                    '.hc-details-open-right {left:-70px; margin-right:60px; text-align:left; } ' +
                                    '.hc-details-open-right > .hc-pic { float:right; }' +
                                    '.hc-details-open-left { right:-10px; text-align:right; margin-left:80px; } ' +
                                    '.hc-details-open-left > .hc-pic { float:left; } ' +
                                    '.hc-details .s-action{ position: absolute; top:8px; right:5px; } ' +
                                    '.hc-details .s-card{ border-top: solid 1px #eee; margin-top:10px; padding-top:10px; overflow:hidden; } ' +
                                    '.hc-details .s-card .s-strong{ font-weight:bold; color: #555; } ' +
                                    '.hc-details .s-img{ float: left; margin-right: 10px;} ' +
                                    '.hc-details .s-name{ color:#222; font-weight:bold;} ' +
                                    '.hc-details .s-loc{ float:left;} ' +
                                    '.hc-details .s-href{ clear:both; float:left;} ' +
                                    '.hc-details .s-desc{ float:left; font-family: Georgia; font-style: italic; margin-top:5px;width:100%;} ' +
                                    '.hc-details .s-username{ text-decoration:none;} ' +
                                    '.hc-details .s-stats { display:block; float:left; margin-top:5px; clear:both; padding:0px;}' +
                                    '.hc-details ul.s-stats li{ list-style:none; float:left; display:block; padding:0px 10px !important; border-left:solid 1px #eaeaea;} ' +
                                    '.hc-details ul.s-stats li:first-child{ border:none; padding-left:0 !important;} ' +
                                    '.hc-details .s-count { font-weight: bold;} ' +
                                    '.hc-details-open-right .arrow {  position: absolute;   -moz-transform: rotate(45deg); -webkit-transform: rotate(45deg); -o-transform: rotate(45deg); transform: rotate(45deg);  width: 20px;  height: 20px; left: -5px;  top: 5px;  background-color: White; z-index:-1} ' +
                                '.</style>")';

            $(hovercardTempCSS).appendTo('head');
        }
        //Executing functionality on all selected elements
        return this.each(function () {
            var obj = $(this);

            //wrap a parent span to the selected element
            obj.wrap('<div class="hc-preview" />');

            //add a relatively positioned class to the selected element
            //obj.addClass("hc-name");

            //if card image src provided then generate the image elementk
            var hcImg = '';
            if (options.cardImgSrc.length > 0) {
                hcImg = '<img class="hc-pic" src="' + options.cardImgSrc + '" />';
            }

            //generate details span with html provided by the user
            var hcDetails = '<div class="hc-details" >' + hcImg + options.detailsHTML + '</div>';

            //append this detail after the selected element
            obj.after(hcDetails);

            //obj.siblings(".hc-details").eq(0).css({ 'width': options.width, 'background': options.background });
            obj.siblings(".hc-details").eq(0).css({ 'background': options.background });

            //Get the view port size
            var viewportWidth = window.innerWidth ? window.innerWidth : $(window).width();
            if (jQuery.browser.msie && parseInt(jQuery.browser.version) == 7) {
                viewportWidth -= 3;
            }

            //toggle hover card details on hover
            obj.closest(".hc-preview").hover(function () {

                //Up the z indiex for the .hc-name to overlay on .hc-details
                obj.css("zIndex", "100");
                //add a relatively positioned class to the selected element
                obj.addClass("hc-name");

                var $this = $(this);
                var offsetForHovercard = 0;

                //offsetForHovercard = $this.offset().left + options.width + 10;
                var curHCDetails = $this.find(".hc-details").eq(0);

                if (offsetForHovercard > viewportWidth || options.openOnLeft) {

                    curHCDetails.removeClass("hc-details-open-right")

                        .addClass("hc-details-open-left")
                        .css("margin-left", obj.width() + 75)

                        .stop(true, true).delay(options.delay).fadeIn(800);
                } else {

                    curHCDetails.removeClass("hc-details-open-left")

                        .addClass("hc-details-open-right arrow")
                        .css("margin-left", obj.width() + 75)

                        .stop(true, true).delay(options.delay).fadeIn(800);
                }

                //Default functionality on hoverin, and also allows callback
                if (typeof options.onHoverIn == 'function') {
                    $("#bAssemblyContainer").css("overflow", "visible");
                    //check for custom profile. If already loaded don't load again
                    if (options.showCustomCard && curHCDetails.find('.s-card').length <= 0) {

                        //Read data-hovercard url from the hovered element, otherwise look in the options. For custom card, complete url is required than just username.
                        var dataUrl = options.customDataUrl;
                        if (typeof obj.attr('data-hovercard') == 'undefined') {
                            //do nothing. detecting typeof obj.attr('data-hovercard') != 'undefined' didn't work as expected.
                        } else if (obj.attr('data-hovercard').length > 0) {
                            dataUrl = obj.attr('data-hovercard');
                        }

                        LoadSocialProfile("custom", dataUrl, curHCDetails);
                    }

                    //check for twitter profile. If already loaded don't load again
                    if (options.showTwitterCard && curHCDetails.find('.s-card').length <= 0) {

                        //Look for twitter screen name in data-hovercard first, then in options, otherwise try with the hovered text
                        var tUsername = options.twitterScreenName.length > 0 ? options.twitterScreenName : obj.text();
                        if (typeof obj.attr('data-hovercard') == 'undefined') {
                            //do nothing. detecting typeof obj.attr('data-hovercard') != 'undefined' didn't work as expected.
                        } else if (obj.attr('data-hovercard').length > 0) {
                            tUsername = obj.attr('data-hovercard');
                        }

                        LoadSocialProfile("twitter", tUsername, curHCDetails);
                    }

                    //check for facebook profile. If already loaded don't load again
                    if (options.showFacebookCard && curHCDetails.find('.s-card').length <= 0) {

                        //Look for twitter screen name in data-hovercard first, then in options, otherwise try with the hovered text
                        var fbUsername = options.facebookUserName.length > 0 ? options.facebookUserName : obj.text();
                        if (typeof obj.attr('data-hovercard') == 'undefined') {
                            //do nothing. detecting typeof obj.attr('data-hovercard') != 'undefined' didn't work as expected.
                        } else if (obj.attr('data-hovercard').length > 0) {
                            fbUsername = obj.attr('data-hovercard');
                        }

                        LoadSocialProfile("facebook", fbUsername, curHCDetails);
                    }

                    //Callback function
                    options.onHoverIn.call(this);
                }

            }, function () {
                $("#bAssemblyContainer").css("overflow", "auto");
                obj.css("zIndex", "0");
                //add a relatively positioned class to the selected element
                obj.removeClass("hc-name");
                $(this).find(".hc-details").eq(0).stop(true, true).fadeOut(400, function () {
                    if (typeof options.onHoverOut == 'function') {
                        options.onHoverOut.call(this);
                    }
                });
            });

            //Private base function to load any social profile
            function LoadSocialProfile(type, username, curHCDetails) {
                var cardHTML, urlToRequest, customCallback, loadingHTML, errorHTML;
                switch (type) {
                    case "twitter":
                        {
                            urlToRequest = 'http://api.twitter.com/1/users/lookup.json?screen_name=' + username;
                            cardHTML = function (profileData) {
                                profileData = profileData[0];
                                return '<div class="s-card">' +
                                                        (profileData.profile_image_url ? ('<img class="s-img" src="' + profileData.profile_image_url + '" />') : '') +
                                                        (profileData.name ? ('<label class="s-name">' + profileData.name + ' </label>') : '') +
                                                        (profileData.screen_name ? ('(<a class="s-username" title="Visit Twitter profile for "' + profileData.name + '" href="http://twitter.com/' + profileData.screen_name + '">@' + profileData.screen_name + '</a>)<br/>') : '') +
                                                        (profileData.location ? ('<label class="s-loc">' + profileData.location + '</label>') : '') +
                                                        (profileData.description ? ('<p class="s-desc">' + profileData.description + '</p>') : '') +
                                                        (profileData.url ? ('<a class="s-href" href="' + profileData.url + '">' + profileData.url + '</a><br/>') : '') +

                                                        '<ul class="s-stats">' +
                                                            (profileData.statuses_count ? ('<li>Tweets<br /><span class="s-count">' + profileData.statuses_count + '</span></li>') : '') +
                                                            (profileData.friends_count ? ('<li>Following<br /><span class="s-count">' + profileData.friends_count + '</span></li>') : '') +
                                                            (profileData.followers_count ? ('<li>Followers<br /><span class="s-count">' + profileData.followers_count + '</span></li>') : '') +
                                                        '</ul>' +
                                                    '</div>';
                            };
                            loadingHTML = 'Contacting Twitter...';
                            errorHTML = 'Invalid username or you have exceeded Twitter request limit.<br/><small>Please note, Twitter only allows 150 requests per hour.</small>';
                            customCallback = function () { };

                            //Append the twitter script to the document to add a follow button
                            if ($('#t-follow-script').length <= 0) {
                                var script = document.createElement('script');
                                script.type = 'text/javascript';
                                script.src = '//platform.twitter.com/widgets.js';
                                script.id = 't-follow-script';
                                $('body').append(script);
                            }
                            curHCDetails.append('<span class="s-action"><a href="https://twitter.com/' + username + '" data-show-count="false" data-button="grey" data-width="65px" class="twitter-follow-button">Follow</a></span>');

                        }
                        break;
                    case "facebook":
                        {
                            urlToRequest = 'https://graph.facebook.com/' + username,
                            cardHTML = function (profileData) {
                                return '<div class="s-card">' +
                                        '<img class="s-img" src="http://graph.facebook.com/' + profileData.id + '/picture" />' +
                                        '<label class="s-name">' + profileData.name + ' </label><br/>' +
                                        (profileData.link ? ('<a class="s-loc" href="' + profileData.link + '">' + profileData.link + '</a><br/>') : '') +
                                        (profileData.likes ? ('<label class="s-loc">Liked by </span> ' + profileData.likes + '</label><br/>') : '') +
                                        (profileData.description ? ('<p class="s-desc">' + profileData.description + '</p>') : '') +
                                        (profileData.start_time ? ('<p class="s-desc"><span class="s-strong">Start Time:</span><br/>' + profileData.start_time + '</p>') : '') +
                                        (profileData.end_time ? ('<p class="s-desc"><span class="s-strong">End Time:<br/>' + profileData.end_time + '</p>') : '') +
                                        (profileData.founded ? ('<p class="s-desc"><span class="s-strong">Founded:</span><br/>' + profileData.founded + '</p>') : '') +
                                        (profileData.mission ? ('<p class="s-desc"><span class="s-strong">Mission:</span><br/>' + profileData.mission + '</p>') : '') +
                                        (profileData.company_overview ? ('<p class="s-desc"><span class="s-strong">Overview:</span><br/>' + profileData.company_overview + '</p>') : '') +
                                        (profileData.products ? ('<p class="s-desc"><span class="s-strong">Products:</span><br/>' + profileData.products + '</p>') : '') +
                                        (profileData.website ? ('<p class="s-desc"><span class="s-strong">Web:</span><br/><a href="' + profileData.website + '">' + profileData.website + '</a></p>') : '') +
                                        (profileData.email ? ('<p class="s-desc"><span class="s-strong">Email:</span><br/><a href="' + profileData.email + '">' + profileData.email + '</a></p>') : '') +
                                        '</div>';
                            };
                            loadingHTML = "Contacting Facebook...";
                            errorHTML = "The requested user, page, or event could not be found. Please try a different one.";
                            customCallback = function (profileData) {
                                //Append the twitter script to the document to add a follow button
                                if ($('#fb-like-script').length <= 0) {
                                    var script = document.createElement('script');
                                    script.type = 'text/javascript';
                                    script.text = '(function(d, s, id) {' +
                                              'var js, fjs = d.getElementsByTagName(s)[0];' +
                                              'if (d.getElementById(id)) {return;}' +
                                              'js = d.createElement(s); js.id = id;' +
                                              'js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=140270912730552";' +
                                              'fjs.parentNode.insertBefore(js, fjs);' +
                                            '}(document, "script", "facebook-jssdk"));';
                                    script.id = 'fb-like-script';
                                    $('body').prepend(script);
                                    $('body').prepend('<div id="fb-root"></div>');
                                }
                                curHCDetails.append('<span class="s-action"><div class="fb-like" data-href="' + profileData.link + '" data-send="false" data-layout="button_count" data-width="90" data-show-faces="false"></div></span>');
                            }
                        }
                        break;
                    case "custom":
                        {
                            urlToRequest = username,
                            cardHTML = function (profileData) {
                                return '<div class="s-card">' +
                                        (profileData.image ? ('<img class="s-img" src=' + profileData.image + ' />') : '') +
                                        (profileData.name ? ('<label class="s-name">' + profileData.name + ' </label><br/>') : '') +
                                        (profileData.link ? ('<a class="s-loc" href="' + profileData.link + '">' + profileData.link + '</a><br/>') : '') +
                                        (profileData.bio ? ('<p class="s-desc">' + profileData.bio + '</p>') : '') +
                                        (profileData.website ? ('<p class="s-desc"><span class="s-strong">Web:</span><br/><a href="' + profileData.website + '">' + profileData.website + '</a></p>') : '') +
                                        (profileData.email ? ('<p class="s-desc"><span class="s-strong">Email:</span><br/><a href="' + profileData.email + '">' + profileData.email + '</a></p>') : '') +
                                        '</div>';
                            };
                            loadingHTML = "Loading...";
                            errorHTML = "Sorry, no data found.";
                            customCallback = function () { };
                        }
                        break;
                    default: { } break;
                }

                $.ajax({
                    url: urlToRequest,
                    type: 'GET',
                    dataType: 'jsonp', //jsonp for cross domain request
                    timeout: 4000, //timeout if cross domain request didn't respond, or failed silently
                    beforeSend: function () {
                        curHCDetails.find('.s-message').remove();
                        curHCDetails.append('<p class="s-message">' + loadingHTML + '</p>');
                    },
                    success: function (data) {
                        if (data.length <= 0) {
                            curHCDetails.find('.s-message').html(errorHTML);
                        }
                        else {
                            curHCDetails.find('.s-message').remove();
                            curHCDetails.prepend(cardHTML(data));
                            customCallback(data);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        curHCDetails.find('.s-message').html(errorHTML);
                    }
                });

            };
        });

    };
})(jQuery);