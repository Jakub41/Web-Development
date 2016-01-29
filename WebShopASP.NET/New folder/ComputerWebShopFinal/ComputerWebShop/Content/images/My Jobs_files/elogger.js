

// Queue for sending requests
function XDomainQueue() {
	this.queue = [];
	this.queueSpace = 0;
	
	this.enqueue = function(element){
		this.queue.push(element);
	}
	
	this.dequeue = function(){
		var element = undefined;
		if (this.queue.length){
			element = this.queue[this.queueSpace];
			if (++this.queueSpace * 2 >= this.queue.length){
				this.queue = this.queue.slice(this.queueSpace);
				this.queueSpace = 0;
			}
		}
		return element;
	}
}

// Globals (keep these to a minimum)
XDomain_Queue = new Array();
XDomain_FragmentSize = 1400;
XDomain_QueuePollInterval = 210;

// Get ACK via Cookie
// If cookie doesn't exist or is blank then we have an ACK
// and we are ok to initiate send, otherwise we need to wait
XDomain_getACK = function(instance_id) {
	var dc = document.cookie;
	var prefix = instance_id+"=";
	var begin = dc.indexOf("; " + prefix);
	if (begin == -1) {
		begin = dc.indexOf(prefix);
		if (begin != 0) return true;
	} else {
		begin += 2;
	}
	var end = document.cookie.indexOf(";", begin);
	if (end == -1) {
		end = dc.length;
	}
	return dc.substring(begin + prefix.length, end) == "";
}

// Send SYN via Cookie
XDomain_setSYN = function(instance_id, packet) {
	var expires = new Date(new Date().getTime() + 10000).toGMTString();
	var cookiestr = instance_id+"="+packet+";expires="+expires+";path=/;domain=.elance.com";
    document.cookie = cookiestr;
}

// Send Queue Polling
XDomain_PollQueue = function(instance_id) {
	// Check if we have an ACK from the listener so we are ok for sending
	if( !XDomain_getACK(instance_id) ) {
		return;
	}
	// If our queue is empty, nothing to do
	var elem;
	if( (elem = XDomain_Queue[instance_id].dequeue()) == undefined ) {
		return;
	}
	// Otherwise, we have a useful request, so better send it
	elem[0].send(elem[1], elem[2], elem[3]);
}


/**
 * Class Constructor
 **/
var XDomain = function(domain, unique_id) {
	var id = unique_id+'_'+new Date().getTime();
	this.listener_frame = 'XDomainListenerFrame_' + id;
	this.instance_id = 'XDomainListener_' + id;
	this.domain = domain;
	this.seq_id = 0;
	XDomain_setSYN(this.instance_id, '0:0:init');
	
 	// Create frame
	var iFrame = document.createElement("iframe");
	iFrame.setAttribute("id", this.listener_frame);
	iFrame.setAttribute("name", this.listener_frame);
	iFrame.style.display = 'none';
	var baseSrc = this.domain+'/web/static/xdomain_listener.html';
	var querySrc = '?i=' + this.instance_id;
	iFrame.src = baseSrc+querySrc;
	document.body.appendChild(iFrame);
	XDomain_Queue[this.instance_id] = new XDomainQueue();
	
	// Start counter to inspect queue value for invoking a call.
	setInterval("XDomain_PollQueue('"+this.instance_id+"')", XDomain_QueuePollInterval);
}

/**
 * Class Functions
 **/
// XDomain Communication Functions (Core)
XDomain.prototype.makeCallThroughXDomain = function(context, fnc, args) {
	if( !context ) {
		return false;
	}
	var time = new Date().getTime();
	var request = 'context='+context+'&fnc='+fnc+'&numargs='+args.length;
	for(var i=0; i<args.length; i++) {
		var arg = encodeURIComponent(new String(args[i]));
		request += '&arg'+i+'='+arg;
	}
	request = escape(request);

	//chunk up requests into 4K chars or less
	if( request.length > XDomain_FragmentSize ) {
		var requests = new Array();
		var cmd;
		for(var i=0; i<request.length; i+=XDomain_FragmentSize) {
			requests.push(request.substr(i, XDomain_FragmentSize));
		}
		for(var j=0; j<requests.length; j++) {
			if( j == 0 ) {
				cmd = "start";
			} else if( j < (requests.length-1) ) {
				cmd = "part";
			} else {
				cmd = "end";
			}
			XDomain_Queue[this.instance_id].enqueue(new Array(this, j, cmd, requests[j]));
		}
	} else {
		XDomain_Queue[this.instance_id].enqueue(new Array(this, 0, "end", request));
	}
	
	return true;
}

// Send packet via cookie
XDomain.prototype.send = function(frag_id, cmd, message){
	// generate a packet to send
	var packet = (this.seq_id++)+":"+frag_id+":"+cmd;
	if( message ) {
		packet += ":" + message;
	}
//	if( window.console ){
//		console.log("XDomain XFER:\n",packet);
//	}
	// setup a cookie
	XDomain_setSYN(this.instance_id, packet);
}


// collab util namespace
EOL.namespace('collabutil');

window.addEvent("focus", function() {EOL.collabutil.focused = true; EOL.collabutil.flash = false;});
window.addEvent("blur", function() {EOL.collabutil.focused = false;});

/**
 * Global Alert
 */

// global alerts namespace
EOL.collabutil.globalAlerts = new Object();

// Initializer flag
EOL.collabutil.globalAlerts.init = false;

// Page globals for alert tracking
EOL.collabutil.globalAlerts.alerts = new Array();

// Alert popup specific globals
EOL.collabutil.globalAlerts.fadeoutTime = 10000;
EOL.collabutil.globalAlerts.height = 127;
// how long before ignore global alerts: 1 hour in mili-seconds
EOL.collabutil.globalAlerts.staleness = 1000 * 60 * 60; 

// Window flashing
EOL.collabutil.title = document.title;
EOL.collabutil.flash = false;
EOL.collabutil.flashInterval = null;
EOL.collabutil.flashTimeout = 1000;


// initializer
EOL.collabutil.globalAlerts.initGlobalAlert = function() {
	EOL.collabutil.globalAlerts.init = true;
	EOL.collabutil.user = getCookie('uname').toLowerCase();
	
	// init alert column
	var elem = document.createElement('div');
	elem.id = 'GlobalAlertContainer';
	elem.className = 'GlobalAlertContainer';
	var table = document.createElement('table');
	var tbody = document.createElement('tbody');
	var tr = document.createElement('tr');
	var td = document.createElement('td');
	td.id = 'GlobalAlertContainerContent';
	td.className = 'GlobalAlertContainerContent';
	tr.appendChild(td);
	tbody.appendChild(tr);
	table.appendChild(tbody);
	elem.appendChild(table);
	document.body.appendChild(elem);
	
	// init ghost alert
	elem = document.createElement('div');
	elem.id = 'GlobalAlertGhost';
	elem.className = 'GlobalAlert GlobalAlertBubble';
	$('GlobalAlertContainerContent').appendChild(elem);
	var fadeEffect = new Fx.Tween('GlobalAlertGhost', { duration:0, transition:Fx.Transitions.Expo.easeOut });
	fadeEffect.start('height', EOL.collabutil.globalAlerts.height);
}


// create html content for a new alert
EOL.collabutil.globalAlerts.createGlobalAlert = function(id) {
	// setup new alert for display
	$('GlobalAlertGhost').id = 'GlobalAlert'+id;
	var main = document.createElement('div');
	main.id = 'GlobalAlertMain'+id;
	$('GlobalAlert'+id).appendChild(main);

	// create a new ghost and fade it into view
	// gives the illusion that the new alert slowly pops up
	// b/c the expanding ghost pushes the new alert up as it grows
	var elem = document.createElement('div');
	elem.id = 'GlobalAlertGhost';
	elem.className = 'GlobalAlert GlobalAlertBubble';
	$('GlobalAlertContainerContent').appendChild(elem);
	var fadeEffect = new Fx.Tween('GlobalAlertGhost', { duration:2000, transition:Fx.Transitions.Expo.easeOut });
	fadeEffect.start('height', EOL.collabutil.globalAlerts.height);
}


// kill an alert, either immediately or delay the kill
EOL.collabutil.globalAlerts.killGlobalAlert = function(id, ts) {
	if( ts != 0 &&
	    eval('EOL.collabutil.globalAlerts.alerts["'+id+'"] != ' + ts) ) {
		return;
	}
	EOL.collabutil.globalAlerts.alerts[id] = null;
	$('GlobalAlert'+id).id = 'GlobalAlert'+id+'_Dead';
	$('GlobalAlertMain'+id).id = 'GlobalAlertMain'+id+'_Dead';
	var fadeEffectAlpha = new Fx.Tween('GlobalAlert'+id+'_Dead', { duration:1500, transition:Fx.Transitions.Expo.easeOut});
	var fadeEffectOpacity = new Fx.Tween('GlobalAlert'+id+'_Dead', { duration:1500, transition:Fx.Transitions.Expo.easeOut,
	                              onComplete: function() { $('GlobalAlertContainerContent').removeChild($('GlobalAlert'+id+'_Dead')); }});
	fadeEffectAlpha.start('alpha', 0);
	fadeEffectOpacity.start('opacity', 0.1);
}


// build a global alert from user and message and insert/replace in page
EOL.collabutil.globalAlerts.buildGlobalAlert = function(user, msg) {
	var bidid = msg.bidid;
	var id = bidid;
	var ts = new Date().getTime() + EOL.collabutil.globalAlerts.fadeoutTime;

	// build main content for new page element
	var elem = document.createElement('div');
	elem.id = 'GlobalAlertMain'+id;
	
	// title
	subelem = document.createElement('div');
	subelem.className = 'GlobalAlertMainTitle left';
	var link = document.createElement('div');
	link.className = 'GlobalAlertMainTitleText clickable';
	link.bidid = bidid;
	smartAddEvent(link, 'click', function(evt, e) {
        javascript:EOL.messageBoard.open('/collab?bidid='+e.bidid+'&mode=M', '805', '700', 'yes');
    });
	if( msg.workroomName.length > 30 ) {
		msg.workroomName = msg.workroomName.substr(0,28)+'...';
	}
	link.innerHTML = msg.workroomName;
	subelem.appendChild(link);
	
	// subtitle
	var subtitle = document.createElement('div');
	subtitle.className = 'GlobalAlertMainSubtitleText';
	var span = document.createElement('span');
	span.innerHTML = 'New message posted by&nbsp;';
	subtitle.appendChild(span);
	var span = document.createElement('span');
	span.className = 'GlobalAlertMainTextHighlight';
	if( user.length > 16 ) {
		user = user.substr(0,15)+'...';
	}
	span.innerHTML = user;
	subtitle.appendChild(span);
	subelem.appendChild(subtitle);
	elem.appendChild(subelem);
	
	// close
	subelem = document.createElement('div');
	subelem.className = 'right GlobalAlertMainClose';
	subelem.innerHTML = '<!-- -->';
	subelem.idx = id;
	smartAddEvent(subelem, 'click', function(evt, elem) {EOL.collabutil.globalAlerts.killGlobalAlert(elem.idx, 0);});
	elem.appendChild(subelem);
	subelem = document.createElement('div');
	subelem.className = 'clearBoth';
	elem.appendChild(subelem);
	
	// text
	subelem = document.createElement('div');
	subelem.className = 'GlobalAlertMainText GlobalAlertMainRule';
	subelem.innerHTML = '<!-- -->';
	elem.appendChild(subelem);
	subelem = document.createElement('div');
	subelem.className = 'GlobalAlertMainText GlobalAlertMainTextPlain GlobalAlertMainTextContainer';
	var text = msg.messageContent.collabHtmlEnc();
	text = text.replace(/<br[\/ \t]*>/g," ");
	text = text.replace(/[\n\r]/g," ");
	if(text.length > 80){
	    text = text.substring(0, 80) + " ...";
	}
	text = '"'+text+'"';
	link = document.createElement('span');
	link.className = 'clickable';
	link.bidid = bidid;
	smartAddEvent(link, 'click', function(evt, e) {
        javascript:EOL.messageBoard.open('/collab?bidid='+e.bidid+'&mode=M', '805', '700', 'yes');
    });
	link.innerHTML = text;
	subelem.appendChild(link);
	elem.appendChild(subelem);
	
	// replace node
	if( !$('GlobalAlert'+id) ) {
		EOL.collabutil.globalAlerts.createGlobalAlert(id);
	}
	$('GlobalAlert'+id).replaceChild(elem, $('GlobalAlertMain'+id));
	
	// housekeeping
	if( (EOL.collab != undefined &&
	     EOL.collab.focused) ||
	     EOL.collabutil.focused) {
		EOL.collabutil.globalAlerts.alerts[id] = ts;
		setTimeout("EOL.collabutil.globalAlerts.killGlobalAlert(" + id + "," + ts + ")", EOL.collabutil.globalAlerts.fadeoutTime);
	}
}


/**
 * Statistics Tracking
 */

EOL.collabutil.collabAsyncReq = null;

// report load time to BE
EOL.collabutil.reportLoadTimeStats = function(loadTime) {
    setTimeout("EOL.collabutil.reportLoadTimeStatsHelper('" + loadTime + "')", 30000); 
}


// report errors to BE
EOL.collabutil.reportClientErrors = function(code, type, message) {
    setTimeout("EOL.collabutil.reportClientErrorsHelper('" + code + "', '" + type  + "', '" + message + "')", 30000);
}

// report load time to BE
EOL.collabutil.reportLoadTimeStatsHelper = function(loadTime) {
	var postParams = 'errorType=loadtime&loadTime=' + loadTime + '&z=1';
	var options = {url: '/php/collab/main/ReportClientInfoAHR.php?t=' + getDateTime(), method:'post', data:postParams};
	EOL.collabutil.collabAsyncReq = new Request(options);
	EOL.collabutil.collabAsyncReq.send();
}


// report errors to BE
EOL.collabutil.reportClientErrorsHelper = function(code, type, message) {
    var errorStr = ((typeof(code) != 'undefined') ? code : '') + " " +
                   ((typeof(type) != 'undefined') ? type : '') + " " +
                   ((typeof(message) != 'undefined') ? message : ''); 
	var postParams = 'loadTime=0&errorType=' + errorStr ;
	var options = {url: '/php/collab/main/ReportClientInfoAHR.php?t=' + getDateTime(), method:'post', data:postParams};
	EOL.collabutil.collabAsyncReq = new Request(options);
	EOL.collabutil.collabAsyncReq.send();
}



/**
 * Utilities & General APIs
 */

// handle an incoming message
EOL.collabutil.handleCClientGlobalMessage = function(user, msg, successCallback) {
	// lazy load
	if( !EOL.collabutil.globalAlerts.init ) {
		EOL.collabutil.globalAlerts.initGlobalAlert();
	}
	
	// decompose message
	var msgObj = eval('(' + msg + ')');
	
	// if its too old, quit now
	var ts = new Date().getTime();
	if( msgObj.timestamp * 1000 + EOL.collabutil.globalAlerts.staleness < ts ) return;
	
	// handle flashing
	if( user != EOL.collabutil.user ) {
		EOL.collabutil.initFlash();
	}

    if (typeof(successCallback) == 'function') {
        successCallback();
    }
	// build and insert the global alert
	EOL.collabutil.globalAlerts.buildGlobalAlert(user, msgObj);
}


// display warning when leaving due to message url click
EOL.collabutil.navigateAway = function() {
	return confirm('You are about to navigate away from Elance.com. Please confirm.');
}

cleanLink = function (str){
    return str.replace(/</g,' ').replace(/>/g,'');
}

// replace strings with acceptable content
String.prototype.collabHtmlEnc = function(basic) {
	var str = this;
	if( !basic ) {
		str = str.replace(/((<https?|ftp):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|]>)/ig, cleanLink );
		str = str.replace(/</g, "&lt;");
		str = str.replace(/>/g, "&gt;");
		str = str.replace(/\'\'/g, "\"");
		str = str.replace(/\t/g,"&nbsp;&nbsp;&nbsp;&nbsp;");
		var linkexp = /(\b(https?|ftp):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
	    var limitedlinkexp = />(https?|ftp):\/\/([-A-Z0-9+&@#\/%?=~_|!:,.;]{40,}?)[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|]</ig;
		if( str.match(/(https?:\/\/([-A-Z0-9+_])*\.?elance\.com)/i)){
			str = str.replace(linkexp,"<a href='$&' target='_blank'>$&</a>");
		} else {
			str = str.replace(linkexp,"<a onclick=\'return EOL.collabutil.navigateAway();\' href='$&' target='_blank' title='$&'>$&</a>").replace(limitedlinkexp,'>$1://$2...<');
		}
		str = str.replace(/\"/g, "&quot;");
	}
	str = str.replace(/\n/g, "<br>");
	return str;
}


// update availability from global toggle
EOL.collabutil.updateMyAvailabilityStatus = function(show) {
    setCookie( "show", show, "", "/", ".elance.com" );
    XDomainCClient.makeCallThroughXDomain('JClientContentFrame', 'JClient.updatePresenceToEveryone', new Array());
}


// handle errors on global nav
EOL.collabutil.handleCClientGlobalShowErrors = function(code, type, message) {
	switch(code) {
		case '20009':
			EOL.collabutil.displayOffline();
			break;
	}
}


// Window Title Flashing
EOL.collabutil.initFlash = function() {
	if( EOL.collab == undefined &&
	    !EOL.collabutil.focused &&
		!EOL.collabutil.flash ) {
		EOL.collabutil.flash = true;
		EOL.collabutil.flashInterval = setInterval("EOL.collabutil.flashTitle()", EOL.collabutil.flashTimeout);
	}
}

EOL.collabutil.flashTitle = function() {
	if( EOL.collabutil.flash ) {
		if( document.title == EOL.collabutil.title ) {
			document.title = "**New Message(s)**";
		} else {
			document.title = EOL.collabutil.title;
		}
	} else {
		clearInterval(EOL.collabutil.flashInterval);
		document.title = EOL.collabutil.title;
	}
}


// toggle nav UI to display offline
EOL.collabutil.displayOffline = function() {
	if( $('nav-availability-icon') != undefined ) {
		$$('#nav-myaccount .nav-availability-item').addClass('displayNone');
		$('nav-availability-icon').removeClass('nav-presenceOn');
		$('nav-availability-icon').addClass('nav-presenceOff');
	}
}





EOL.collabutil.trackPostJobClick= function(){
    var userId = getCookie('userid');
        $el('send', 'useraction', "CLICK", 'Job',
            {
                "userId": userId,
                "pageName": "JobPost"

        });
}

EOL.collabutil.trackInboxNavigationClick = function(metric) {
    $el('send', 'useraction', "CLICK", 'inboxTypeFilterSelect', {
        "userId": getCookie('userid'),
        "pageName": "Inbox",
        "metricTags" : { "filter" : metric }
    });
}


/**
 * Elance logger API
 *
 * Installation
 *
 * 1. Add this script to your page.
 * 2. Add <script>t0 = +new Date;</script> in the head of the page. (don't need this for latest browsers).
 * 3. elogger('appName', 'hostname', 'loggerServer', [options]) to initialize logger script
 *
 *
 */

;(function(window, document, eloggerNamespace) {
    var metricBagHelper = {
        isArray: function (a) {
            return (!!a) && (a.constructor === Array);
        },
        isObject: function (a) {
            return (!!a) && (a.constructor === Object);
        },
        replaceOrRecursion: function (objectOrArray, key) {
            if (objectOrArray[key] instanceof HTMLInputElement) {
                objectOrArray[key] = objectOrArray[key].outerHTML;
            } else {
                this.replaceHTMLInput(objectOrArray[key]);
            }
        },
        replaceHTMLInput: function (objectOrArray) {
            if (this.isArray(objectOrArray)) {
                for (var key = 0; key < objectOrArray.length; key++) {
                    this.replaceOrRecursion(objectOrArray, key);
                }
            } else if (this.isObject(objectOrArray)) {
                for (var key in objectOrArray) {
                    this.replaceOrRecursion(objectOrArray, key);
                }
            }
        }
    };

    var extend = function (target, source) {
            target = target || {};
            var temp;
            for (var prop in source) {
                temp = source[prop];
                if (temp && Object.prototype.toString.call(temp) == '[object Array]') {
                    target[prop] = temp.slice(0);
                }
                else if (temp && typeof temp === 'object') {
                    target[prop] = extend(target[prop], temp);
                } else {
                    target[prop] = temp;
                }
            }
            return target;
        },
        setObjectValue = function(obj, name, options1, options2) {
            if (options1 && options1[name]) {
                obj[name] = options1[name];
            }
            else if (options2 && options2[name]) {
                obj[name] = options2[name];
            }
        },
        addListener = function() {
            if(window.addEventListener) {
                return function(obj, eventName, listener) {
                    obj.addEventListener(eventName, listener, false);
                };
            }
            else {
                return function(obj, eventName, listener) {
                    obj.attachEvent("on" + eventName, listener);
                };
            }
        }(),

        logData = function(options, postObject, callback) {
            var postData;

            try {
                postData = JSON.stringify(postObject);
            }
            catch(e) {
                //console.log('[elogger] JSON.stringify is missing');
            }

            if (!postData) {
                options.callback('false');
                return;
            }

            var image = new Image();
            image.onerror = function() {
                if (callback) callback();
                options.callback('true', postObject);
            };

            image.src = options.loggerServer + encodeURIComponent(postData);

            window.setTimeout(function(){ image = null}, 1E4);
        },
        emptyFunction = function(){
            //console.log('[elogger]: Set to dummy function. [below sample rate|disabled]');
        },
        jQueryAjaxTimingInitilized = false,
        onloadTimingInitilized = false;


    var logJQueryAjaxTiming = function($el) {
        if (!window.Zepto && !window.jQuery) {
            return;
        }

        var $d = $(document);

        var onComplete = function(options) {
            var originalComplete = options.complete,
                url = options.url,
                startTime = +new Date;

            return function(xhr, status) {
                //console.log('Took %s ms to %s for url %s ' , +new Date - startTime, status, url);
                originalComplete.apply(originalComplete, arguments);
                ////console.log('onComplete', xhr, status);
                var parser = document.createElement('a');
                parser.href = url;
                $el('send', 'time', parser.pathname, location.pathname, +new Date - startTime, {
                    metricTags: {
                        status: status,
                        objectType: 'ajax'
                    }
                });
            }
        };

        $d.on('ajaxBeforeSend', function(e, xhr, options){
            // This gets fired for every Ajax request performed on the page.
            // The xhr object and $.ajax() options are available for editing.
            // Return false to cancel this request.
            options.complete = onComplete(options);
            ////console.log('ajaxBeforeSend', e, xhr, options);
        });

        $d.on('ajaxError', function (xhr, options, error) {
            ////console.log('ajaxError', xhr, options, error);
        });
    };

    var logOnloadTiming = function($el) {
        var DOMContentLoadedTime,
            navigationTiming = window.performance && window.performance.timing,
            logUsingNavigationTiming = function() {
                var timing = window.performance.timing,
                    dnsTime = timing.domainLookupEnd - timing.domainLookupStart,
                    connectionTime = timing.connectEnd - timing.connectStart,
                    serverTime = timing.responseEnd - timing.requestStart,
                    fetchTime = timing.responseEnd - timing.fetchStart,
                    domContentLoadedTime = timing.domComplete - timing.domLoading,
                    onLoadTime = timing.loadEventEnd - timing.responseStart,
                    networkTime= timing.connectEnd - timing.navigationStart,
                    pageLoadTime= timing.loadEventEnd - timing.navigationStart;


                $el('send', 'time', null, location.pathname, pageLoadTime, {
                    subTimeMetrics: [
                        $el('set', 'time', 'onLoad', null, onLoadTime),
                        $el('set', 'time', 'DOMContentLoaded',null, domContentLoadedTime)
                    ],
                    metricTags: {
                        objectType: 'page'
                    }
                });
            };

        addListener(window, "load", function(event) {
            var location = window.location;
            if (!navigationTiming) {
                t0 && $el('send', 'time', null, location.pathname, '-1', {
                    subTimeMetrics: [
                        $el('set', 'time', 'onLoad', null, +new Date - t0),
                        $el('set', 'time', 'DOMContentLoaded', null, DOMContentLoadedTime)
                    ],
                    metricTags: {
                        objectType: 'page'
                    }
                });
            }
            else {
                window.setTimeout(logUsingNavigationTiming, 1);
            }
        });

        if (navigationTiming && navigationTiming.loadEventEnd > 0) {
            logUsingNavigationTiming();
        }

        addListener(document, "DOMContentLoaded", function(event) {
            t0 && (DOMContentLoadedTime = +new Date - t0);
        });

    };

    /*
     {
     "type": "userAction",
     "metricName": "user.action",
     "timestampMillis": 1374877028490,
     "metricValue": 1,
     "metricTags": {
     "CustomTagN": "CustomTagVN"
     },
     "objectName": "jobs.recommended",
     "userId": "1380381",
     "experimentIds": [
     "ASD-12344",
     "ASP-23342"
     ],
     "actionType": "CLICK",
     "impressionId": "I345",
     "bucketId": null
     }
     */

    /**
     * enable default true. Enable/disable logging data to metrics server
     * appVersion default 1. (NOT USED). Version of the app/website.
     * sampleRate default 100%. Specifies what percentage of users should be tracked.
     * namespace default is $el. Global method that you will call with data. If this variable is already used in your page, you can overwrite to a different global variable.
     *
     *
     * time - Timing related options
     *     onloadTiming default true. Log on load timing for the page.
     *     jQueryAjaxTiming default false. If you are using jQuery/Zepto, it will log request timing for all ajax request. (response end time - request start time).
     *
     *
     * userAction - user action related global values.
     *     impressionId String
     *     bucketId String
     *     experimentIds Array
     *          impressionId,  bucketId and experimentIds will be logged with all userAction request if provided here
     *
     *
     * callback function. This will be called after request is made to the metrics server with two parameters
     *     success: boolean If request was a success or failure (DO NOT USE THIS!)
     *     postData: object that was posted to metrics server
     */

    var defaultOptions = {
        'sampleRate': 100,
        'namespace': '$el',
        'appVersion': 1,
        'enable': true,
        'callback': function(success, postData) {
            //console.log('[elogger] Logged data successfully? ------- %s', success, postData);
        },
        'time': {
            'onloadTiming': true,
            'jQueryAjaxTiming': false
        }
    };


    var elogger = function(options) {

        var time = function(action, type, timingObjectName, timingPageName, timingValue, timingOptions) {

            //timingType = timingType.toUpperCase();

            var argumentsLength = arguments.length,
                timingObject = {
                    type: "browserTime",
                    elapsedTimeMillis : timingValue,
                    host: options.hostname
                };
            if(timingObjectName) {
                timingObject.objectName = timingObjectName;
            }
            if(timingPageName) {
                timingObject.pageName = timingPageName;
            }

            setObjectValue(timingObject, 'subTimeMetrics', timingOptions);
            setObjectValue(timingObject, 'metricTags', timingOptions);

            return timingObject;
        };

        var useraction = function(action, type, actionType, objectName, userActionOptions) {

            actionType = actionType.toUpperCase();
            userActionOptions = userActionOptions || {};

            var userActionObject = {
                    "type": "userAction",
                    "objectName": objectName,
                    "actionType": actionType,
                    "userId": userActionOptions.userId || options.userId || '1111111'
                },
                defaultUserActionOptions = options.userAction;

            setObjectValue(userActionObject, 'pageName', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'metricName', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'metricValue', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'metricTags', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'experimentIds', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'impressionId', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'bucketId', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'logEntries', userActionOptions, defaultUserActionOptions);
            setObjectValue(userActionObject, 'channelId', userActionOptions, options);

            return userActionObject;
        };

        var _cookieManager = function($el) {
            this.$el = $el;
            var oldMetricBag = JSON.parse(getCookie('metric_bag'));
            var metricBag = {};

            var uniqueId = function() {
                return '_' + Math.random().toString(36).substr(2, 9);
            };
            var updateCookie = function() {
                metricBagHelper.replaceHTMLInput(metricBag);
                setCookie('metric_bag', JSON.stringify(metricBag), 1, '/', '.elance.com');
            };

            // sending the remaining metrics
            for (var i in oldMetricBag) {
                var elements = [];
                for (var j in oldMetricBag[i]) {
                    elements.push(oldMetricBag[i][j]);
                }
                this.$el.apply(this.$el, elements);
            }

            updateCookie();

            // cookie metric encoding: { uniqueId: ['send', 'useraction', 'click' , 'jobInfoPanelTags', { userId: ..., metricTags .... }], uniqueId2 : [ ... ] }
            // accepts arguments that would have been passed to $el
            this.add = function(arr) {
                var id = uniqueId();
                metricBag[id] = arr;
                updateCookie();
                return id;
            }

            this.remove = function(id) {
                delete metricBag[id];
                updateCookie();
            }
        };

        var all = function(action, type){
            var obj, callback;

            type = type.toLowerCase();
            if (type=='time') {
                obj = time.apply(this, arguments);
            }
            else if (type == 'useraction') {
                if (cookieManager) {
                    var id = cookieManager.add(arguments);
                    callback = function(){
                        cookieManager.remove(id);
                    }
                }
                obj = useraction.apply(this, arguments);
            }
            else if (typeof type === 'object') {
                obj = type;
            }


            if (action == "set") {
                return obj;
            }
            else if (action == "send") {
                if (options.enable) {
                    logData(options, obj, callback);
                }
            }
        };

        var cookieManager = new _cookieManager(all);

        return all;
    };

    var initLogger = function(options) {
        var $el = elogger(options);
        window[options.namespace] = $el;

        if (!onloadTimingInitilized && options.time.onloadTiming) {
            logOnloadTiming($el);
            onloadTimingInitilized = true;
        }
        if (!jQueryAjaxTimingInitilized && options.time.jQueryAjaxTiming) {
            logJQueryAjaxTiming($el);
            jQueryAjaxTimingInitilized = true;
        }
    };

    var prepareLogger = function(appName, hostname, loggerServer, userOptions) {
        if (!loggerServer) {
            //console.warn('[elogger]: Please provide loggerServer url');
            return;
        }

        var options = extend({appName: appName, loggerServer: loggerServer, hostname: hostname}, defaultOptions);
        options = extend(options, userOptions || {});

        if (options.enable === false || Math.random() * 100 > options.sampleRate) {
            //console.log('[elogger]: Below sampleRate %s or disabled by config', options.sampleRate);
            window[options.namespace] = emptyFunction;
            return;
        }

        initLogger(options);
    };

    window[eloggerNamespace] = prepareLogger;

    if (window.__elogger__) {
        prepareLogger.apply(prepareLogger, window.__elogger__);
        try{
            delete window.__elogger__;
        }catch(e){}
    }

}(window, document, window.eloggerNamespace || 'elogger'));
