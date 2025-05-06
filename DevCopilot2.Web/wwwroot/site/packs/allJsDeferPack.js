!function(a){"function"==typeof define&&define.amd?define(["jquery"],a):"object"==typeof exports?module.exports=a:a(jQuery)}(function(a){function b(b){var g=b||window.event,h=i.call(arguments,1),j=0,l=0,m=0,n=0,o=0,p=0;if(b=a.event.fix(g),b.type="mousewheel","detail"in g&&(m=-1*g.detail),"wheelDelta"in g&&(m=g.wheelDelta),"wheelDeltaY"in g&&(m=g.wheelDeltaY),"wheelDeltaX"in g&&(l=-1*g.wheelDeltaX),"axis"in g&&g.axis===g.HORIZONTAL_AXIS&&(l=-1*m,m=0),j=0===m?l:m,"deltaY"in g&&(m=-1*g.deltaY,j=m),"deltaX"in g&&(l=g.deltaX,0===m&&(j=-1*l)),0!==m||0!==l){if(1===g.deltaMode){var q=a.data(this,"mousewheel-line-height");j*=q,m*=q,l*=q}else if(2===g.deltaMode){var r=a.data(this,"mousewheel-page-height");j*=r,m*=r,l*=r}if(n=Math.max(Math.abs(m),Math.abs(l)),(!f||f>n)&&(f=n,d(g,n)&&(f/=40)),d(g,n)&&(j/=40,l/=40,m/=40),j=Math[j>=1?"floor":"ceil"](j/f),l=Math[l>=1?"floor":"ceil"](l/f),m=Math[m>=1?"floor":"ceil"](m/f),k.settings.normalizeOffset&&this.getBoundingClientRect){var s=this.getBoundingClientRect();o=b.clientX-s.left,p=b.clientY-s.top}return b.deltaX=l,b.deltaY=m,b.deltaFactor=f,b.offsetX=o,b.offsetY=p,b.deltaMode=0,h.unshift(b,j,l,m),e&&clearTimeout(e),e=setTimeout(c,200),(a.event.dispatch||a.event.handle).apply(this,h)}}function c(){f=null}function d(a,b){return k.settings.adjustOldDeltas&&"mousewheel"===a.type&&b%120===0}var e,f,g=["wheel","mousewheel","DOMMouseScroll","MozMousePixelScroll"],h="onwheel"in document||document.documentMode>=9?["wheel"]:["mousewheel","DomMouseScroll","MozMousePixelScroll"],i=Array.prototype.slice;if(a.event.fixHooks)for(var j=g.length;j;)a.event.fixHooks[g[--j]]=a.event.mouseHooks;var k=a.event.special.mousewheel={version:"3.1.12",setup:function(){if(this.addEventListener)for(var c=h.length;c;)this.addEventListener(h[--c],b,!1);else this.onmousewheel=b;a.data(this,"mousewheel-line-height",k.getLineHeight(this)),a.data(this,"mousewheel-page-height",k.getPageHeight(this))},teardown:function(){if(this.removeEventListener)for(var c=h.length;c;)this.removeEventListener(h[--c],b,!1);else this.onmousewheel=null;a.removeData(this,"mousewheel-line-height"),a.removeData(this,"mousewheel-page-height")},getLineHeight:function(b){var c=a(b),d=c["offsetParent"in a.fn?"offsetParent":"parent"]();return d.length||(d=a("body")),parseInt(d.css("fontSize"),10)||parseInt(c.css("fontSize"),10)||16},getPageHeight:function(b){return a(b).height()},settings:{adjustOldDeltas:!0,normalizeOffset:!0}};a.fn.extend({mousewheel:function(a){return a?this.bind("mousewheel",a):this.trigger("mousewheel")},unmousewheel:function(a){return this.unbind("mousewheel",a)}})});!function(a){"function"==typeof define&&define.amd?define(["jquery"],a):"object"==typeof exports?module.exports=a:a(jQuery)}(function(a){function b(b){var g=b||window.event,h=i.call(arguments,1),j=0,l=0,m=0,n=0,o=0,p=0;if(b=a.event.fix(g),b.type="mousewheel","detail"in g&&(m=-1*g.detail),"wheelDelta"in g&&(m=g.wheelDelta),"wheelDeltaY"in g&&(m=g.wheelDeltaY),"wheelDeltaX"in g&&(l=-1*g.wheelDeltaX),"axis"in g&&g.axis===g.HORIZONTAL_AXIS&&(l=-1*m,m=0),j=0===m?l:m,"deltaY"in g&&(m=-1*g.deltaY,j=m),"deltaX"in g&&(l=g.deltaX,0===m&&(j=-1*l)),0!==m||0!==l){if(1===g.deltaMode){var q=a.data(this,"mousewheel-line-height");j*=q,m*=q,l*=q}else if(2===g.deltaMode){var r=a.data(this,"mousewheel-page-height");j*=r,m*=r,l*=r}if(n=Math.max(Math.abs(m),Math.abs(l)),(!f||f>n)&&(f=n,d(g,n)&&(f/=40)),d(g,n)&&(j/=40,l/=40,m/=40),j=Math[j>=1?"floor":"ceil"](j/f),l=Math[l>=1?"floor":"ceil"](l/f),m=Math[m>=1?"floor":"ceil"](m/f),k.settings.normalizeOffset&&this.getBoundingClientRect){var s=this.getBoundingClientRect();o=b.clientX-s.left,p=b.clientY-s.top}return b.deltaX=l,b.deltaY=m,b.deltaFactor=f,b.offsetX=o,b.offsetY=p,b.deltaMode=0,h.unshift(b,j,l,m),e&&clearTimeout(e),e=setTimeout(c,200),(a.event.dispatch||a.event.handle).apply(this,h)}}function c(){f=null}function d(a,b){return k.settings.adjustOldDeltas&&"mousewheel"===a.type&&b%120===0}var e,f,g=["wheel","mousewheel","DOMMouseScroll","MozMousePixelScroll"],h="onwheel"in document||document.documentMode>=9?["wheel"]:["mousewheel","DomMouseScroll","MozMousePixelScroll"],i=Array.prototype.slice;if(a.event.fixHooks)for(var j=g.length;j;)a.event.fixHooks[g[--j]]=a.event.mouseHooks;var k=a.event.special.mousewheel={version:"3.1.12",setup:function(){if(this.addEventListener)for(var c=h.length;c;)this.addEventListener(h[--c],b,!1);else this.onmousewheel=b;a.data(this,"mousewheel-line-height",k.getLineHeight(this)),a.data(this,"mousewheel-page-height",k.getPageHeight(this))},teardown:function(){if(this.removeEventListener)for(var c=h.length;c;)this.removeEventListener(h[--c],b,!1);else this.onmousewheel=null;a.removeData(this,"mousewheel-line-height"),a.removeData(this,"mousewheel-page-height")},getLineHeight:function(b){var c=a(b),d=c["offsetParent"in a.fn?"offsetParent":"parent"]();return d.length||(d=a("body")),parseInt(d.css("fontSize"),10)||parseInt(c.css("fontSize"),10)||16},getPageHeight:function(b){return a(b).height()},settings:{adjustOldDeltas:!0,normalizeOffset:!0}};a.fn.extend({mousewheel:function(a){return a?this.bind("mousewheel",a):this.trigger("mousewheel")},unmousewheel:function(a){return this.unbind("mousewheel",a)}})});
!function(e){"function"==typeof define&&define.amd?define(["jquery"],e):"undefined"!=typeof module&&module.exports?module.exports=e:e(jQuery,window,document)}(function(e){!function(t){var o="function"==typeof define&&define.amd,a="undefined"!=typeof module&&module.exports,n="https:"==document.location.protocol?"https:":"http:",i="cdnjs.cloudflare.com/ajax/libs/jquery-mousewheel/3.1.13/jquery.mousewheel.min.js";o||(a?require("jquery-mousewheel")(e):e.event.special.mousewheel||e("head").append(decodeURI("%3Cscript src="+n+"//"+i+"%3E%3C/script%3E"))),t()}(function(){var t,o="mCustomScrollbar",a="mCS",n=".mCustomScrollbar",i={setTop:0,setLeft:0,axis:"y",scrollbarPosition:"inside",scrollInertia:950,autoDraggerLength:!0,alwaysShowScrollbar:0,snapOffset:0,mouseWheel:{enable:!0,scrollAmount:"auto",axis:"y",deltaFactor:"auto",disableOver:["select","option","keygen","datalist","textarea"]},scrollButtons:{scrollType:"stepless",scrollAmount:"auto"},keyboard:{enable:!0,scrollType:"stepless",scrollAmount:"auto"},contentTouchScroll:25,documentTouchScroll:!0,advanced:{autoScrollOnFocus:"input,textarea,select,button,datalist,keygen,a[tabindex],area,object,[contenteditable='true']",updateOnContentResize:!0,updateOnImageLoad:"auto",autoUpdateTimeout:60},theme:"light",callbacks:{onTotalScrollOffset:0,onTotalScrollBackOffset:0,alwaysTriggerOffsets:!0}},r=0,l={},s=window.attachEvent&&!window.addEventListener?1:0,c=!1,d=["mCSB_dragger_onDrag","mCSB_scrollTools_onDrag","mCS_img_loaded","mCS_disabled","mCS_destroyed","mCS_no_scrollbar","mCS-autoHide","mCS-dir-rtl","mCS_no_scrollbar_y","mCS_no_scrollbar_x","mCS_y_hidden","mCS_x_hidden","mCSB_draggerContainer","mCSB_buttonUp","mCSB_buttonDown","mCSB_buttonLeft","mCSB_buttonRight"],u={init:function(t){var t=e.extend(!0,{},i,t),o=f.call(this);if(t.live){var s=t.liveSelector||this.selector||n,c=e(s);if("off"===t.live)return void m(s);l[s]=setTimeout(function(){c.mCustomScrollbar(t),"once"===t.live&&c.length&&m(s)},500)}else m(s);return t.setWidth=t.set_width?t.set_width:t.setWidth,t.setHeight=t.set_height?t.set_height:t.setHeight,t.axis=t.horizontalScroll?"x":p(t.axis),t.scrollInertia=t.scrollInertia>0&&t.scrollInertia<17?17:t.scrollInertia,"object"!=typeof t.mouseWheel&&1==t.mouseWheel&&(t.mouseWheel={enable:!0,scrollAmount:"auto",axis:"y",preventDefault:!1,deltaFactor:"auto",normalizeDelta:!1,invert:!1}),t.mouseWheel.scrollAmount=t.mouseWheelPixels?t.mouseWheelPixels:t.mouseWheel.scrollAmount,t.mouseWheel.normalizeDelta=t.advanced.normalizeMouseWheelDelta?t.advanced.normalizeMouseWheelDelta:t.mouseWheel.normalizeDelta,t.scrollButtons.scrollType=g(t.scrollButtons.scrollType),h(t),e(o).each(function(){var o=e(this);if(!o.data(a)){o.data(a,{idx:++r,opt:t,scrollRatio:{y:null,x:null},overflowed:null,contentReset:{y:null,x:null},bindEvents:!1,tweenRunning:!1,sequential:{},langDir:o.css("direction"),cbOffsets:null,trigger:null,poll:{size:{o:0,n:0},img:{o:0,n:0},change:{o:0,n:0}}});var n=o.data(a),i=n.opt,l=o.data("mcs-axis"),s=o.data("mcs-scrollbar-position"),c=o.data("mcs-theme");l&&(i.axis=l),s&&(i.scrollbarPosition=s),c&&(i.theme=c,h(i)),v.call(this),n&&i.callbacks.onCreate&&"function"==typeof i.callbacks.onCreate&&i.callbacks.onCreate.call(this),e("#mCSB_"+n.idx+"_container img:not(."+d[2]+")").addClass(d[2]),u.update.call(null,o)}})},update:function(t,o){var n=t||f.call(this);return e(n).each(function(){var t=e(this);if(t.data(a)){var n=t.data(a),i=n.opt,r=e("#mCSB_"+n.idx+"_container"),l=e("#mCSB_"+n.idx),s=[e("#mCSB_"+n.idx+"_dragger_vertical"),e("#mCSB_"+n.idx+"_dragger_horizontal")];if(!r.length)return;n.tweenRunning&&Q(t),o&&n&&i.callbacks.onBeforeUpdate&&"function"==typeof i.callbacks.onBeforeUpdate&&i.callbacks.onBeforeUpdate.call(this),t.hasClass(d[3])&&t.removeClass(d[3]),t.hasClass(d[4])&&t.removeClass(d[4]),l.css("max-height","none"),l.height()!==t.height()&&l.css("max-height",t.height()),_.call(this),"y"===i.axis||i.advanced.autoExpandHorizontalScroll||r.css("width",x(r)),n.overflowed=y.call(this),M.call(this),i.autoDraggerLength&&S.call(this),b.call(this),T.call(this);var c=[Math.abs(r[0].offsetTop),Math.abs(r[0].offsetLeft)];"x"!==i.axis&&(n.overflowed[0]?s[0].height()>s[0].parent().height()?B.call(this):(G(t,c[0].toString(),{dir:"y",dur:0,overwrite:"none"}),n.contentReset.y=null):(B.call(this),"y"===i.axis?k.call(this):"yx"===i.axis&&n.overflowed[1]&&G(t,c[1].toString(),{dir:"x",dur:0,overwrite:"none"}))),"y"!==i.axis&&(n.overflowed[1]?s[1].width()>s[1].parent().width()?B.call(this):(G(t,c[1].toString(),{dir:"x",dur:0,overwrite:"none"}),n.contentReset.x=null):(B.call(this),"x"===i.axis?k.call(this):"yx"===i.axis&&n.overflowed[0]&&G(t,c[0].toString(),{dir:"y",dur:0,overwrite:"none"}))),o&&n&&(2===o&&i.callbacks.onImageLoad&&"function"==typeof i.callbacks.onImageLoad?i.callbacks.onImageLoad.call(this):3===o&&i.callbacks.onSelectorChange&&"function"==typeof i.callbacks.onSelectorChange?i.callbacks.onSelectorChange.call(this):i.callbacks.onUpdate&&"function"==typeof i.callbacks.onUpdate&&i.callbacks.onUpdate.call(this)),N.call(this)}})},scrollTo:function(t,o){if("undefined"!=typeof t&&null!=t){var n=f.call(this);return e(n).each(function(){var n=e(this);if(n.data(a)){var i=n.data(a),r=i.opt,l={trigger:"external",scrollInertia:r.scrollInertia,scrollEasing:"mcsEaseInOut",moveDragger:!1,timeout:60,callbacks:!0,onStart:!0,onUpdate:!0,onComplete:!0},s=e.extend(!0,{},l,o),c=Y.call(this,t),d=s.scrollInertia>0&&s.scrollInertia<17?17:s.scrollInertia;c[0]=X.call(this,c[0],"y"),c[1]=X.call(this,c[1],"x"),s.moveDragger&&(c[0]*=i.scrollRatio.y,c[1]*=i.scrollRatio.x),s.dur=ne()?0:d,setTimeout(function(){null!==c[0]&&"undefined"!=typeof c[0]&&"x"!==r.axis&&i.overflowed[0]&&(s.dir="y",s.overwrite="all",G(n,c[0].toString(),s)),null!==c[1]&&"undefined"!=typeof c[1]&&"y"!==r.axis&&i.overflowed[1]&&(s.dir="x",s.overwrite="none",G(n,c[1].toString(),s))},s.timeout)}})}},stop:function(){var t=f.call(this);return e(t).each(function(){var t=e(this);t.data(a)&&Q(t)})},disable:function(t){var o=f.call(this);return e(o).each(function(){var o=e(this);if(o.data(a)){o.data(a);N.call(this,"remove"),k.call(this),t&&B.call(this),M.call(this,!0),o.addClass(d[3])}})},destroy:function(){var t=f.call(this);return e(t).each(function(){var n=e(this);if(n.data(a)){var i=n.data(a),r=i.opt,l=e("#mCSB_"+i.idx),s=e("#mCSB_"+i.idx+"_container"),c=e(".mCSB_"+i.idx+"_scrollbar");r.live&&m(r.liveSelector||e(t).selector),N.call(this,"remove"),k.call(this),B.call(this),n.removeData(a),$(this,"mcs"),c.remove(),s.find("img."+d[2]).removeClass(d[2]),l.replaceWith(s.contents()),n.removeClass(o+" _"+a+"_"+i.idx+" "+d[6]+" "+d[7]+" "+d[5]+" "+d[3]).addClass(d[4])}})}},f=function(){return"object"!=typeof e(this)||e(this).length<1?n:this},h=function(t){var o=["rounded","rounded-dark","rounded-dots","rounded-dots-dark"],a=["rounded-dots","rounded-dots-dark","3d","3d-dark","3d-thick","3d-thick-dark","inset","inset-dark","inset-2","inset-2-dark","inset-3","inset-3-dark"],n=["minimal","minimal-dark"],i=["minimal","minimal-dark"],r=["minimal","minimal-dark"];t.autoDraggerLength=e.inArray(t.theme,o)>-1?!1:t.autoDraggerLength,t.autoExpandScrollbar=e.inArray(t.theme,a)>-1?!1:t.autoExpandScrollbar,t.scrollButtons.enable=e.inArray(t.theme,n)>-1?!1:t.scrollButtons.enable,t.autoHideScrollbar=e.inArray(t.theme,i)>-1?!0:t.autoHideScrollbar,t.scrollbarPosition=e.inArray(t.theme,r)>-1?"outside":t.scrollbarPosition},m=function(e){l[e]&&(clearTimeout(l[e]),$(l,e))},p=function(e){return"yx"===e||"xy"===e||"auto"===e?"yx":"x"===e||"horizontal"===e?"x":"y"},g=function(e){return"stepped"===e||"pixels"===e||"step"===e||"click"===e?"stepped":"stepless"},v=function(){var t=e(this),n=t.data(a),i=n.opt,r=i.autoExpandScrollbar?" "+d[1]+"_expand":"",l=["<div id='mCSB_"+n.idx+"_scrollbar_vertical' class='mCSB_scrollTools mCSB_"+n.idx+"_scrollbar mCS-"+i.theme+" mCSB_scrollTools_vertical"+r+"'><div class='"+d[12]+"'><div id='mCSB_"+n.idx+"_dragger_vertical' class='mCSB_dragger' style='position:absolute;'><div class='mCSB_dragger_bar' /></div><div class='mCSB_draggerRail' /></div></div>","<div id='mCSB_"+n.idx+"_scrollbar_horizontal' class='mCSB_scrollTools mCSB_"+n.idx+"_scrollbar mCS-"+i.theme+" mCSB_scrollTools_horizontal"+r+"'><div class='"+d[12]+"'><div id='mCSB_"+n.idx+"_dragger_horizontal' class='mCSB_dragger' style='position:absolute;'><div class='mCSB_dragger_bar' /></div><div class='mCSB_draggerRail' /></div></div>"],s="yx"===i.axis?"mCSB_vertical_horizontal":"x"===i.axis?"mCSB_horizontal":"mCSB_vertical",c="yx"===i.axis?l[0]+l[1]:"x"===i.axis?l[1]:l[0],u="yx"===i.axis?"<div id='mCSB_"+n.idx+"_container_wrapper' class='mCSB_container_wrapper' />":"",f=i.autoHideScrollbar?" "+d[6]:"",h="x"!==i.axis&&"rtl"===n.langDir?" "+d[7]:"";i.setWidth&&t.css("width",i.setWidth),i.setHeight&&t.css("height",i.setHeight),i.setLeft="y"!==i.axis&&"rtl"===n.langDir?"989999px":i.setLeft,t.addClass(o+" _"+a+"_"+n.idx+f+h).wrapInner("<div id='mCSB_"+n.idx+"' class='mCustomScrollBox mCS-"+i.theme+" "+s+"'><div id='mCSB_"+n.idx+"_container' class='mCSB_container' style='position:relative; top:"+i.setTop+"; left:"+i.setLeft+";' dir='"+n.langDir+"' /></div>");var m=e("#mCSB_"+n.idx),p=e("#mCSB_"+n.idx+"_container");"y"===i.axis||i.advanced.autoExpandHorizontalScroll||p.css("width",x(p)),"outside"===i.scrollbarPosition?("static"===t.css("position")&&t.css("position","relative"),t.css("overflow","visible"),m.addClass("mCSB_outside").after(c)):(m.addClass("mCSB_inside").append(c),p.wrap(u)),w.call(this);var g=[e("#mCSB_"+n.idx+"_dragger_vertical"),e("#mCSB_"+n.idx+"_dragger_horizontal")];g[0].css("min-height",g[0].height()),g[1].css("min-width",g[1].width())},x=function(t){var o=[t[0].scrollWidth,Math.max.apply(Math,t.children().map(function(){return e(this).outerWidth(!0)}).get())],a=t.parent().width();return o[0]>a?o[0]:o[1]>a?o[1]:"100%"},_=function(){var t=e(this),o=t.data(a),n=o.opt,i=e("#mCSB_"+o.idx+"_container");if(n.advanced.autoExpandHorizontalScroll&&"y"!==n.axis){i.css({width:"auto","min-width":0,"overflow-x":"scroll"});var r=Math.ceil(i[0].scrollWidth);3===n.advanced.autoExpandHorizontalScroll||2!==n.advanced.autoExpandHorizontalScroll&&r>i.parent().width()?i.css({width:r,"min-width":"100%","overflow-x":"inherit"}):i.css({"overflow-x":"inherit",position:"absolute"}).wrap("<div class='mCSB_h_wrapper' style='position:relative; left:0; width:999999px;' />").css({width:Math.ceil(i[0].getBoundingClientRect().right+.4)-Math.floor(i[0].getBoundingClientRect().left),"min-width":"100%",position:"relative"}).unwrap()}},w=function(){var t=e(this),o=t.data(a),n=o.opt,i=e(".mCSB_"+o.idx+"_scrollbar:first"),r=oe(n.scrollButtons.tabindex)?"tabindex='"+n.scrollButtons.tabindex+"'":"",l=["<a href='#' class='"+d[13]+"' "+r+" />","<a href='#' class='"+d[14]+"' "+r+" />","<a href='#' class='"+d[15]+"' "+r+" />","<a href='#' class='"+d[16]+"' "+r+" />"],s=["x"===n.axis?l[2]:l[0],"x"===n.axis?l[3]:l[1],l[2],l[3]];n.scrollButtons.enable&&i.prepend(s[0]).append(s[1]).next(".mCSB_scrollTools").prepend(s[2]).append(s[3])},S=function(){var t=e(this),o=t.data(a),n=e("#mCSB_"+o.idx),i=e("#mCSB_"+o.idx+"_container"),r=[e("#mCSB_"+o.idx+"_dragger_vertical"),e("#mCSB_"+o.idx+"_dragger_horizontal")],l=[n.height()/i.outerHeight(!1),n.width()/i.outerWidth(!1)],c=[parseInt(r[0].css("min-height")),Math.round(l[0]*r[0].parent().height()),parseInt(r[1].css("min-width")),Math.round(l[1]*r[1].parent().width())],d=s&&c[1]<c[0]?c[0]:c[1],u=s&&c[3]<c[2]?c[2]:c[3];r[0].css({height:d,"max-height":r[0].parent().height()-10}).find(".mCSB_dragger_bar").css({"line-height":c[0]+"px"}),r[1].css({width:u,"max-width":r[1].parent().width()-10})},b=function(){var t=e(this),o=t.data(a),n=e("#mCSB_"+o.idx),i=e("#mCSB_"+o.idx+"_container"),r=[e("#mCSB_"+o.idx+"_dragger_vertical"),e("#mCSB_"+o.idx+"_dragger_horizontal")],l=[i.outerHeight(!1)-n.height(),i.outerWidth(!1)-n.width()],s=[l[0]/(r[0].parent().height()-r[0].height()),l[1]/(r[1].parent().width()-r[1].width())];o.scrollRatio={y:s[0],x:s[1]}},C=function(e,t,o){var a=o?d[0]+"_expanded":"",n=e.closest(".mCSB_scrollTools");"active"===t?(e.toggleClass(d[0]+" "+a),n.toggleClass(d[1]),e[0]._draggable=e[0]._draggable?0:1):e[0]._draggable||("hide"===t?(e.removeClass(d[0]),n.removeClass(d[1])):(e.addClass(d[0]),n.addClass(d[1])))},y=function(){var t=e(this),o=t.data(a),n=e("#mCSB_"+o.idx),i=e("#mCSB_"+o.idx+"_container"),r=null==o.overflowed?i.height():i.outerHeight(!1),l=null==o.overflowed?i.width():i.outerWidth(!1),s=i[0].scrollHeight,c=i[0].scrollWidth;return s>r&&(r=s),c>l&&(l=c),[r>n.height(),l>n.width()]},B=function(){var t=e(this),o=t.data(a),n=o.opt,i=e("#mCSB_"+o.idx),r=e("#mCSB_"+o.idx+"_container"),l=[e("#mCSB_"+o.idx+"_dragger_vertical"),e("#mCSB_"+o.idx+"_dragger_horizontal")];if(Q(t),("x"!==n.axis&&!o.overflowed[0]||"y"===n.axis&&o.overflowed[0])&&(l[0].add(r).css("top",0),G(t,"_resetY")),"y"!==n.axis&&!o.overflowed[1]||"x"===n.axis&&o.overflowed[1]){var s=dx=0;"rtl"===o.langDir&&(s=i.width()-r.outerWidth(!1),dx=Math.abs(s/o.scrollRatio.x)),r.css("left",s),l[1].css("left",dx),G(t,"_resetX")}},T=function(){function t(){r=setTimeout(function(){e.event.special.mousewheel?(clearTimeout(r),W.call(o[0])):t()},100)}var o=e(this),n=o.data(a),i=n.opt;if(!n.bindEvents){if(I.call(this),i.contentTouchScroll&&D.call(this),E.call(this),i.mouseWheel.enable){var r;t()}P.call(this),U.call(this),i.advanced.autoScrollOnFocus&&H.call(this),i.scrollButtons.enable&&F.call(this),i.keyboard.enable&&q.call(this),n.bindEvents=!0}},k=function(){var t=e(this),o=t.data(a),n=o.opt,i=a+"_"+o.idx,r=".mCSB_"+o.idx+"_scrollbar",l=e("#mCSB_"+o.idx+",#mCSB_"+o.idx+"_container,#mCSB_"+o.idx+"_container_wrapper,"+r+" ."+d[12]+",#mCSB_"+o.idx+"_dragger_vertical,#mCSB_"+o.idx+"_dragger_horizontal,"+r+">a"),s=e("#mCSB_"+o.idx+"_container");n.advanced.releaseDraggableSelectors&&l.add(e(n.advanced.releaseDraggableSelectors)),n.advanced.extraDraggableSelectors&&l.add(e(n.advanced.extraDraggableSelectors)),o.bindEvents&&(e(document).add(e(!A()||top.document)).unbind("."+i),l.each(function(){e(this).unbind("."+i)}),clearTimeout(t[0]._focusTimeout),$(t[0],"_focusTimeout"),clearTimeout(o.sequential.step),$(o.sequential,"step"),clearTimeout(s[0].onCompleteTimeout),$(s[0],"onCompleteTimeout"),o.bindEvents=!1)},M=function(t){var o=e(this),n=o.data(a),i=n.opt,r=e("#mCSB_"+n.idx+"_container_wrapper"),l=r.length?r:e("#mCSB_"+n.idx+"_container"),s=[e("#mCSB_"+n.idx+"_scrollbar_vertical"),e("#mCSB_"+n.idx+"_scrollbar_horizontal")],c=[s[0].find(".mCSB_dragger"),s[1].find(".mCSB_dragger")];"x"!==i.axis&&(n.overflowed[0]&&!t?(s[0].add(c[0]).add(s[0].children("a")).css("display","block"),l.removeClass(d[8]+" "+d[10])):(i.alwaysShowScrollbar?(2!==i.alwaysShowScrollbar&&c[0].css("display","none"),l.removeClass(d[10])):(s[0].css("display","none"),l.addClass(d[10])),l.addClass(d[8]))),"y"!==i.axis&&(n.overflowed[1]&&!t?(s[1].add(c[1]).add(s[1].children("a")).css("display","block"),l.removeClass(d[9]+" "+d[11])):(i.alwaysShowScrollbar?(2!==i.alwaysShowScrollbar&&c[1].css("display","none"),l.removeClass(d[11])):(s[1].css("display","none"),l.addClass(d[11])),l.addClass(d[9]))),n.overflowed[0]||n.overflowed[1]?o.removeClass(d[5]):o.addClass(d[5])},O=function(t){var o=t.type,a=t.target.ownerDocument!==document&&null!==frameElement?[e(frameElement).offset().top,e(frameElement).offset().left]:null,n=A()&&t.target.ownerDocument!==top.document&&null!==frameElement?[e(t.view.frameElement).offset().top,e(t.view.frameElement).offset().left]:[0,0];switch(o){case"pointerdown":case"MSPointerDown":case"pointermove":case"MSPointerMove":case"pointerup":case"MSPointerUp":return a?[t.originalEvent.pageY-a[0]+n[0],t.originalEvent.pageX-a[1]+n[1],!1]:[t.originalEvent.pageY,t.originalEvent.pageX,!1];case"touchstart":case"touchmove":case"touchend":var i=t.originalEvent.touches[0]||t.originalEvent.changedTouches[0],r=t.originalEvent.touches.length||t.originalEvent.changedTouches.length;return t.target.ownerDocument!==document?[i.screenY,i.screenX,r>1]:[i.pageY,i.pageX,r>1];default:return a?[t.pageY-a[0]+n[0],t.pageX-a[1]+n[1],!1]:[t.pageY,t.pageX,!1]}},I=function(){function t(e,t,a,n){if(h[0].idleTimer=d.scrollInertia<233?250:0,o.attr("id")===f[1])var i="x",s=(o[0].offsetLeft-t+n)*l.scrollRatio.x;else var i="y",s=(o[0].offsetTop-e+a)*l.scrollRatio.y;G(r,s.toString(),{dir:i,drag:!0})}var o,n,i,r=e(this),l=r.data(a),d=l.opt,u=a+"_"+l.idx,f=["mCSB_"+l.idx+"_dragger_vertical","mCSB_"+l.idx+"_dragger_horizontal"],h=e("#mCSB_"+l.idx+"_container"),m=e("#"+f[0]+",#"+f[1]),p=d.advanced.releaseDraggableSelectors?m.add(e(d.advanced.releaseDraggableSelectors)):m,g=d.advanced.extraDraggableSelectors?e(!A()||top.document).add(e(d.advanced.extraDraggableSelectors)):e(!A()||top.document);m.bind("contextmenu."+u,function(e){e.preventDefault()}).bind("mousedown."+u+" touchstart."+u+" pointerdown."+u+" MSPointerDown."+u,function(t){if(t.stopImmediatePropagation(),t.preventDefault(),ee(t)){c=!0,s&&(document.onselectstart=function(){return!1}),L.call(h,!1),Q(r),o=e(this);var a=o.offset(),l=O(t)[0]-a.top,u=O(t)[1]-a.left,f=o.height()+a.top,m=o.width()+a.left;f>l&&l>0&&m>u&&u>0&&(n=l,i=u),C(o,"active",d.autoExpandScrollbar)}}).bind("touchmove."+u,function(e){e.stopImmediatePropagation(),e.preventDefault();var a=o.offset(),r=O(e)[0]-a.top,l=O(e)[1]-a.left;t(n,i,r,l)}),e(document).add(g).bind("mousemove."+u+" pointermove."+u+" MSPointerMove."+u,function(e){if(o){var a=o.offset(),r=O(e)[0]-a.top,l=O(e)[1]-a.left;if(n===r&&i===l)return;t(n,i,r,l)}}).add(p).bind("mouseup."+u+" touchend."+u+" pointerup."+u+" MSPointerUp."+u,function(){o&&(C(o,"active",d.autoExpandScrollbar),o=null),c=!1,s&&(document.onselectstart=null),L.call(h,!0)})},D=function(){function o(e){if(!te(e)||c||O(e)[2])return void(t=0);t=1,b=0,C=0,d=1,y.removeClass("mCS_touch_action");var o=I.offset();u=O(e)[0]-o.top,f=O(e)[1]-o.left,z=[O(e)[0],O(e)[1]]}function n(e){if(te(e)&&!c&&!O(e)[2]&&(T.documentTouchScroll||e.preventDefault(),e.stopImmediatePropagation(),(!C||b)&&d)){g=K();var t=M.offset(),o=O(e)[0]-t.top,a=O(e)[1]-t.left,n="mcsLinearOut";if(E.push(o),W.push(a),z[2]=Math.abs(O(e)[0]-z[0]),z[3]=Math.abs(O(e)[1]-z[1]),B.overflowed[0])var i=D[0].parent().height()-D[0].height(),r=u-o>0&&o-u>-(i*B.scrollRatio.y)&&(2*z[3]<z[2]||"yx"===T.axis);if(B.overflowed[1])var l=D[1].parent().width()-D[1].width(),h=f-a>0&&a-f>-(l*B.scrollRatio.x)&&(2*z[2]<z[3]||"yx"===T.axis);r||h?(U||e.preventDefault(),b=1):(C=1,y.addClass("mCS_touch_action")),U&&e.preventDefault(),w="yx"===T.axis?[u-o,f-a]:"x"===T.axis?[null,f-a]:[u-o,null],I[0].idleTimer=250,B.overflowed[0]&&s(w[0],R,n,"y","all",!0),B.overflowed[1]&&s(w[1],R,n,"x",L,!0)}}function i(e){if(!te(e)||c||O(e)[2])return void(t=0);t=1,e.stopImmediatePropagation(),Q(y),p=K();var o=M.offset();h=O(e)[0]-o.top,m=O(e)[1]-o.left,E=[],W=[]}function r(e){if(te(e)&&!c&&!O(e)[2]){d=0,e.stopImmediatePropagation(),b=0,C=0,v=K();var t=M.offset(),o=O(e)[0]-t.top,a=O(e)[1]-t.left;if(!(v-g>30)){_=1e3/(v-p);var n="mcsEaseOut",i=2.5>_,r=i?[E[E.length-2],W[W.length-2]]:[0,0];x=i?[o-r[0],a-r[1]]:[o-h,a-m];var u=[Math.abs(x[0]),Math.abs(x[1])];_=i?[Math.abs(x[0]/4),Math.abs(x[1]/4)]:[_,_];var f=[Math.abs(I[0].offsetTop)-x[0]*l(u[0]/_[0],_[0]),Math.abs(I[0].offsetLeft)-x[1]*l(u[1]/_[1],_[1])];w="yx"===T.axis?[f[0],f[1]]:"x"===T.axis?[null,f[1]]:[f[0],null],S=[4*u[0]+T.scrollInertia,4*u[1]+T.scrollInertia];var y=parseInt(T.contentTouchScroll)||0;w[0]=u[0]>y?w[0]:0,w[1]=u[1]>y?w[1]:0,B.overflowed[0]&&s(w[0],S[0],n,"y",L,!1),B.overflowed[1]&&s(w[1],S[1],n,"x",L,!1)}}}function l(e,t){var o=[1.5*t,2*t,t/1.5,t/2];return e>90?t>4?o[0]:o[3]:e>60?t>3?o[3]:o[2]:e>30?t>8?o[1]:t>6?o[0]:t>4?t:o[2]:t>8?t:o[3]}function s(e,t,o,a,n,i){e&&G(y,e.toString(),{dur:t,scrollEasing:o,dir:a,overwrite:n,drag:i})}var d,u,f,h,m,p,g,v,x,_,w,S,b,C,y=e(this),B=y.data(a),T=B.opt,k=a+"_"+B.idx,M=e("#mCSB_"+B.idx),I=e("#mCSB_"+B.idx+"_container"),D=[e("#mCSB_"+B.idx+"_dragger_vertical"),e("#mCSB_"+B.idx+"_dragger_horizontal")],E=[],W=[],R=0,L="yx"===T.axis?"none":"all",z=[],P=I.find("iframe"),H=["touchstart."+k+" pointerdown."+k+" MSPointerDown."+k,"touchmove."+k+" pointermove."+k+" MSPointerMove."+k,"touchend."+k+" pointerup."+k+" MSPointerUp."+k],U=void 0!==document.body.style.touchAction&&""!==document.body.style.touchAction;I.bind(H[0],function(e){o(e)}).bind(H[1],function(e){n(e)}),M.bind(H[0],function(e){i(e)}).bind(H[2],function(e){r(e)}),P.length&&P.each(function(){e(this).bind("load",function(){A(this)&&e(this.contentDocument||this.contentWindow.document).bind(H[0],function(e){o(e),i(e)}).bind(H[1],function(e){n(e)}).bind(H[2],function(e){r(e)})})})},E=function(){function o(){return window.getSelection?window.getSelection().toString():document.selection&&"Control"!=document.selection.type?document.selection.createRange().text:0}function n(e,t,o){d.type=o&&i?"stepped":"stepless",d.scrollAmount=10,j(r,e,t,"mcsLinearOut",o?60:null)}var i,r=e(this),l=r.data(a),s=l.opt,d=l.sequential,u=a+"_"+l.idx,f=e("#mCSB_"+l.idx+"_container"),h=f.parent();f.bind("mousedown."+u,function(){t||i||(i=1,c=!0)}).add(document).bind("mousemove."+u,function(e){if(!t&&i&&o()){var a=f.offset(),r=O(e)[0]-a.top+f[0].offsetTop,c=O(e)[1]-a.left+f[0].offsetLeft;r>0&&r<h.height()&&c>0&&c<h.width()?d.step&&n("off",null,"stepped"):("x"!==s.axis&&l.overflowed[0]&&(0>r?n("on",38):r>h.height()&&n("on",40)),"y"!==s.axis&&l.overflowed[1]&&(0>c?n("on",37):c>h.width()&&n("on",39)))}}).bind("mouseup."+u+" dragend."+u,function(){t||(i&&(i=0,n("off",null)),c=!1)})},W=function(){function t(t,a){if(Q(o),!z(o,t.target)){var r="auto"!==i.mouseWheel.deltaFactor?parseInt(i.mouseWheel.deltaFactor):s&&t.deltaFactor<100?100:t.deltaFactor||100,d=i.scrollInertia;if("x"===i.axis||"x"===i.mouseWheel.axis)var u="x",f=[Math.round(r*n.scrollRatio.x),parseInt(i.mouseWheel.scrollAmount)],h="auto"!==i.mouseWheel.scrollAmount?f[1]:f[0]>=l.width()?.9*l.width():f[0],m=Math.abs(e("#mCSB_"+n.idx+"_container")[0].offsetLeft),p=c[1][0].offsetLeft,g=c[1].parent().width()-c[1].width(),v="y"===i.mouseWheel.axis?t.deltaY||a:t.deltaX;else var u="y",f=[Math.round(r*n.scrollRatio.y),parseInt(i.mouseWheel.scrollAmount)],h="auto"!==i.mouseWheel.scrollAmount?f[1]:f[0]>=l.height()?.9*l.height():f[0],m=Math.abs(e("#mCSB_"+n.idx+"_container")[0].offsetTop),p=c[0][0].offsetTop,g=c[0].parent().height()-c[0].height(),v=t.deltaY||a;"y"===u&&!n.overflowed[0]||"x"===u&&!n.overflowed[1]||((i.mouseWheel.invert||t.webkitDirectionInvertedFromDevice)&&(v=-v),i.mouseWheel.normalizeDelta&&(v=0>v?-1:1),(v>0&&0!==p||0>v&&p!==g||i.mouseWheel.preventDefault)&&(t.stopImmediatePropagation(),t.preventDefault()),t.deltaFactor<5&&!i.mouseWheel.normalizeDelta&&(h=t.deltaFactor,d=17),G(o,(m-v*h).toString(),{dir:u,dur:d}))}}if(e(this).data(a)){var o=e(this),n=o.data(a),i=n.opt,r=a+"_"+n.idx,l=e("#mCSB_"+n.idx),c=[e("#mCSB_"+n.idx+"_dragger_vertical"),e("#mCSB_"+n.idx+"_dragger_horizontal")],d=e("#mCSB_"+n.idx+"_container").find("iframe");d.length&&d.each(function(){e(this).bind("load",function(){A(this)&&e(this.contentDocument||this.contentWindow.document).bind("mousewheel."+r,function(e,o){t(e,o)})})}),l.bind("mousewheel."+r,function(e,o){t(e,o)})}},R=new Object,A=function(t){var o=!1,a=!1,n=null;if(void 0===t?a="#empty":void 0!==e(t).attr("id")&&(a=e(t).attr("id")),a!==!1&&void 0!==R[a])return R[a];if(t){try{var i=t.contentDocument||t.contentWindow.document;n=i.body.innerHTML}catch(r){}o=null!==n}else{try{var i=top.document;n=i.body.innerHTML}catch(r){}o=null!==n}return a!==!1&&(R[a]=o),o},L=function(e){var t=this.find("iframe");if(t.length){var o=e?"auto":"none";t.css("pointer-events",o)}},z=function(t,o){var n=o.nodeName.toLowerCase(),i=t.data(a).opt.mouseWheel.disableOver,r=["select","textarea"];return e.inArray(n,i)>-1&&!(e.inArray(n,r)>-1&&!e(o).is(":focus"))},P=function(){var t,o=e(this),n=o.data(a),i=a+"_"+n.idx,r=e("#mCSB_"+n.idx+"_container"),l=r.parent(),s=e(".mCSB_"+n.idx+"_scrollbar ."+d[12]);s.bind("mousedown."+i+" touchstart."+i+" pointerdown."+i+" MSPointerDown."+i,function(o){c=!0,e(o.target).hasClass("mCSB_dragger")||(t=1)}).bind("touchend."+i+" pointerup."+i+" MSPointerUp."+i,function(){c=!1}).bind("click."+i,function(a){if(t&&(t=0,e(a.target).hasClass(d[12])||e(a.target).hasClass("mCSB_draggerRail"))){Q(o);var i=e(this),s=i.find(".mCSB_dragger");if(i.parent(".mCSB_scrollTools_horizontal").length>0){if(!n.overflowed[1])return;var c="x",u=a.pageX>s.offset().left?-1:1,f=Math.abs(r[0].offsetLeft)-u*(.9*l.width())}else{if(!n.overflowed[0])return;var c="y",u=a.pageY>s.offset().top?-1:1,f=Math.abs(r[0].offsetTop)-u*(.9*l.height())}G(o,f.toString(),{dir:c,scrollEasing:"mcsEaseInOut"})}})},H=function(){var t=e(this),o=t.data(a),n=o.opt,i=a+"_"+o.idx,r=e("#mCSB_"+o.idx+"_container"),l=r.parent();r.bind("focusin."+i,function(){var o=e(document.activeElement),a=r.find(".mCustomScrollBox").length,i=0;o.is(n.advanced.autoScrollOnFocus)&&(Q(t),clearTimeout(t[0]._focusTimeout),t[0]._focusTimer=a?(i+17)*a:0,t[0]._focusTimeout=setTimeout(function(){var e=[ae(o)[0],ae(o)[1]],a=[r[0].offsetTop,r[0].offsetLeft],s=[a[0]+e[0]>=0&&a[0]+e[0]<l.height()-o.outerHeight(!1),a[1]+e[1]>=0&&a[0]+e[1]<l.width()-o.outerWidth(!1)],c="yx"!==n.axis||s[0]||s[1]?"all":"none";"x"===n.axis||s[0]||G(t,e[0].toString(),{dir:"y",scrollEasing:"mcsEaseInOut",overwrite:c,dur:i}),"y"===n.axis||s[1]||G(t,e[1].toString(),{dir:"x",scrollEasing:"mcsEaseInOut",overwrite:c,dur:i})},t[0]._focusTimer))})},U=function(){var t=e(this),o=t.data(a),n=a+"_"+o.idx,i=e("#mCSB_"+o.idx+"_container").parent();i.bind("scroll."+n,function(){0===i.scrollTop()&&0===i.scrollLeft()||e(".mCSB_"+o.idx+"_scrollbar").css("visibility","hidden")})},F=function(){var t=e(this),o=t.data(a),n=o.opt,i=o.sequential,r=a+"_"+o.idx,l=".mCSB_"+o.idx+"_scrollbar",s=e(l+">a");s.bind("contextmenu."+r,function(e){e.preventDefault()}).bind("mousedown."+r+" touchstart."+r+" pointerdown."+r+" MSPointerDown."+r+" mouseup."+r+" touchend."+r+" pointerup."+r+" MSPointerUp."+r+" mouseout."+r+" pointerout."+r+" MSPointerOut."+r+" click."+r,function(a){function r(e,o){i.scrollAmount=n.scrollButtons.scrollAmount,j(t,e,o)}if(a.preventDefault(),ee(a)){var l=e(this).attr("class");switch(i.type=n.scrollButtons.scrollType,a.type){case"mousedown":case"touchstart":case"pointerdown":case"MSPointerDown":if("stepped"===i.type)return;c=!0,o.tweenRunning=!1,r("on",l);break;case"mouseup":case"touchend":case"pointerup":case"MSPointerUp":case"mouseout":case"pointerout":case"MSPointerOut":if("stepped"===i.type)return;c=!1,i.dir&&r("off",l);break;case"click":if("stepped"!==i.type||o.tweenRunning)return;r("on",l)}}})},q=function(){function t(t){function a(e,t){r.type=i.keyboard.scrollType,r.scrollAmount=i.keyboard.scrollAmount,"stepped"===r.type&&n.tweenRunning||j(o,e,t)}switch(t.type){case"blur":n.tweenRunning&&r.dir&&a("off",null);break;case"keydown":case"keyup":var l=t.keyCode?t.keyCode:t.which,s="on";if("x"!==i.axis&&(38===l||40===l)||"y"!==i.axis&&(37===l||39===l)){if((38===l||40===l)&&!n.overflowed[0]||(37===l||39===l)&&!n.overflowed[1])return;"keyup"===t.type&&(s="off"),e(document.activeElement).is(u)||(t.preventDefault(),t.stopImmediatePropagation(),a(s,l))}else if(33===l||34===l){if((n.overflowed[0]||n.overflowed[1])&&(t.preventDefault(),t.stopImmediatePropagation()),"keyup"===t.type){Q(o);var f=34===l?-1:1;if("x"===i.axis||"yx"===i.axis&&n.overflowed[1]&&!n.overflowed[0])var h="x",m=Math.abs(c[0].offsetLeft)-f*(.9*d.width());else var h="y",m=Math.abs(c[0].offsetTop)-f*(.9*d.height());G(o,m.toString(),{dir:h,scrollEasing:"mcsEaseInOut"})}}else if((35===l||36===l)&&!e(document.activeElement).is(u)&&((n.overflowed[0]||n.overflowed[1])&&(t.preventDefault(),t.stopImmediatePropagation()),"keyup"===t.type)){if("x"===i.axis||"yx"===i.axis&&n.overflowed[1]&&!n.overflowed[0])var h="x",m=35===l?Math.abs(d.width()-c.outerWidth(!1)):0;else var h="y",m=35===l?Math.abs(d.height()-c.outerHeight(!1)):0;G(o,m.toString(),{dir:h,scrollEasing:"mcsEaseInOut"})}}}var o=e(this),n=o.data(a),i=n.opt,r=n.sequential,l=a+"_"+n.idx,s=e("#mCSB_"+n.idx),c=e("#mCSB_"+n.idx+"_container"),d=c.parent(),u="input,textarea,select,datalist,keygen,[contenteditable='true']",f=c.find("iframe"),h=["blur."+l+" keydown."+l+" keyup."+l];f.length&&f.each(function(){e(this).bind("load",function(){A(this)&&e(this.contentDocument||this.contentWindow.document).bind(h[0],function(e){t(e)})})}),s.attr("tabindex","0").bind(h[0],function(e){t(e)})},j=function(t,o,n,i,r){function l(e){u.snapAmount&&(f.scrollAmount=u.snapAmount instanceof Array?"x"===f.dir[0]?u.snapAmount[1]:u.snapAmount[0]:u.snapAmount);var o="stepped"!==f.type,a=r?r:e?o?p/1.5:g:1e3/60,n=e?o?7.5:40:2.5,s=[Math.abs(h[0].offsetTop),Math.abs(h[0].offsetLeft)],d=[c.scrollRatio.y>10?10:c.scrollRatio.y,c.scrollRatio.x>10?10:c.scrollRatio.x],m="x"===f.dir[0]?s[1]+f.dir[1]*(d[1]*n):s[0]+f.dir[1]*(d[0]*n),v="x"===f.dir[0]?s[1]+f.dir[1]*parseInt(f.scrollAmount):s[0]+f.dir[1]*parseInt(f.scrollAmount),x="auto"!==f.scrollAmount?v:m,_=i?i:e?o?"mcsLinearOut":"mcsEaseInOut":"mcsLinear",w=!!e;return e&&17>a&&(x="x"===f.dir[0]?s[1]:s[0]),G(t,x.toString(),{dir:f.dir[0],scrollEasing:_,dur:a,onComplete:w}),e?void(f.dir=!1):(clearTimeout(f.step),void(f.step=setTimeout(function(){l()},a)))}function s(){clearTimeout(f.step),$(f,"step"),Q(t)}var c=t.data(a),u=c.opt,f=c.sequential,h=e("#mCSB_"+c.idx+"_container"),m="stepped"===f.type,p=u.scrollInertia<26?26:u.scrollInertia,g=u.scrollInertia<1?17:u.scrollInertia;switch(o){case"on":if(f.dir=[n===d[16]||n===d[15]||39===n||37===n?"x":"y",n===d[13]||n===d[15]||38===n||37===n?-1:1],Q(t),oe(n)&&"stepped"===f.type)return;l(m);break;case"off":s(),(m||c.tweenRunning&&f.dir)&&l(!0)}},Y=function(t){var o=e(this).data(a).opt,n=[];return"function"==typeof t&&(t=t()),t instanceof Array?n=t.length>1?[t[0],t[1]]:"x"===o.axis?[null,t[0]]:[t[0],null]:(n[0]=t.y?t.y:t.x||"x"===o.axis?null:t,n[1]=t.x?t.x:t.y||"y"===o.axis?null:t),"function"==typeof n[0]&&(n[0]=n[0]()),"function"==typeof n[1]&&(n[1]=n[1]()),n},X=function(t,o){if(null!=t&&"undefined"!=typeof t){var n=e(this),i=n.data(a),r=i.opt,l=e("#mCSB_"+i.idx+"_container"),s=l.parent(),c=typeof t;o||(o="x"===r.axis?"x":"y");var d="x"===o?l.outerWidth(!1)-s.width():l.outerHeight(!1)-s.height(),f="x"===o?l[0].offsetLeft:l[0].offsetTop,h="x"===o?"left":"top";switch(c){case"function":return t();case"object":var m=t.jquery?t:e(t);if(!m.length)return;return"x"===o?ae(m)[1]:ae(m)[0];case"string":case"number":if(oe(t))return Math.abs(t);if(-1!==t.indexOf("%"))return Math.abs(d*parseInt(t)/100);if(-1!==t.indexOf("-="))return Math.abs(f-parseInt(t.split("-=")[1]));if(-1!==t.indexOf("+=")){var p=f+parseInt(t.split("+=")[1]);return p>=0?0:Math.abs(p)}if(-1!==t.indexOf("px")&&oe(t.split("px")[0]))return Math.abs(t.split("px")[0]);if("top"===t||"left"===t)return 0;if("bottom"===t)return Math.abs(s.height()-l.outerHeight(!1));if("right"===t)return Math.abs(s.width()-l.outerWidth(!1));if("first"===t||"last"===t){var m=l.find(":"+t);return"x"===o?ae(m)[1]:ae(m)[0]}return e(t).length?"x"===o?ae(e(t))[1]:ae(e(t))[0]:(l.css(h,t),void u.update.call(null,n[0]))}}},N=function(t){function o(){return clearTimeout(f[0].autoUpdate),0===l.parents("html").length?void(l=null):void(f[0].autoUpdate=setTimeout(function(){return c.advanced.updateOnSelectorChange&&(s.poll.change.n=i(),s.poll.change.n!==s.poll.change.o)?(s.poll.change.o=s.poll.change.n,void r(3)):c.advanced.updateOnContentResize&&(s.poll.size.n=l[0].scrollHeight+l[0].scrollWidth+f[0].offsetHeight+l[0].offsetHeight+l[0].offsetWidth,s.poll.size.n!==s.poll.size.o)?(s.poll.size.o=s.poll.size.n,void r(1)):!c.advanced.updateOnImageLoad||"auto"===c.advanced.updateOnImageLoad&&"y"===c.axis||(s.poll.img.n=f.find("img").length,s.poll.img.n===s.poll.img.o)?void((c.advanced.updateOnSelectorChange||c.advanced.updateOnContentResize||c.advanced.updateOnImageLoad)&&o()):(s.poll.img.o=s.poll.img.n,void f.find("img").each(function(){n(this)}))},c.advanced.autoUpdateTimeout))}function n(t){function o(e,t){return function(){
return t.apply(e,arguments)}}function a(){this.onload=null,e(t).addClass(d[2]),r(2)}if(e(t).hasClass(d[2]))return void r();var n=new Image;n.onload=o(n,a),n.src=t.src}function i(){c.advanced.updateOnSelectorChange===!0&&(c.advanced.updateOnSelectorChange="*");var e=0,t=f.find(c.advanced.updateOnSelectorChange);return c.advanced.updateOnSelectorChange&&t.length>0&&t.each(function(){e+=this.offsetHeight+this.offsetWidth}),e}function r(e){clearTimeout(f[0].autoUpdate),u.update.call(null,l[0],e)}var l=e(this),s=l.data(a),c=s.opt,f=e("#mCSB_"+s.idx+"_container");return t?(clearTimeout(f[0].autoUpdate),void $(f[0],"autoUpdate")):void o()},V=function(e,t,o){return Math.round(e/t)*t-o},Q=function(t){var o=t.data(a),n=e("#mCSB_"+o.idx+"_container,#mCSB_"+o.idx+"_container_wrapper,#mCSB_"+o.idx+"_dragger_vertical,#mCSB_"+o.idx+"_dragger_horizontal");n.each(function(){Z.call(this)})},G=function(t,o,n){function i(e){return s&&c.callbacks[e]&&"function"==typeof c.callbacks[e]}function r(){return[c.callbacks.alwaysTriggerOffsets||w>=S[0]+y,c.callbacks.alwaysTriggerOffsets||-B>=w]}function l(){var e=[h[0].offsetTop,h[0].offsetLeft],o=[x[0].offsetTop,x[0].offsetLeft],a=[h.outerHeight(!1),h.outerWidth(!1)],i=[f.height(),f.width()];t[0].mcs={content:h,top:e[0],left:e[1],draggerTop:o[0],draggerLeft:o[1],topPct:Math.round(100*Math.abs(e[0])/(Math.abs(a[0])-i[0])),leftPct:Math.round(100*Math.abs(e[1])/(Math.abs(a[1])-i[1])),direction:n.dir}}var s=t.data(a),c=s.opt,d={trigger:"internal",dir:"y",scrollEasing:"mcsEaseOut",drag:!1,dur:c.scrollInertia,overwrite:"all",callbacks:!0,onStart:!0,onUpdate:!0,onComplete:!0},n=e.extend(d,n),u=[n.dur,n.drag?0:n.dur],f=e("#mCSB_"+s.idx),h=e("#mCSB_"+s.idx+"_container"),m=h.parent(),p=c.callbacks.onTotalScrollOffset?Y.call(t,c.callbacks.onTotalScrollOffset):[0,0],g=c.callbacks.onTotalScrollBackOffset?Y.call(t,c.callbacks.onTotalScrollBackOffset):[0,0];if(s.trigger=n.trigger,0===m.scrollTop()&&0===m.scrollLeft()||(e(".mCSB_"+s.idx+"_scrollbar").css("visibility","visible"),m.scrollTop(0).scrollLeft(0)),"_resetY"!==o||s.contentReset.y||(i("onOverflowYNone")&&c.callbacks.onOverflowYNone.call(t[0]),s.contentReset.y=1),"_resetX"!==o||s.contentReset.x||(i("onOverflowXNone")&&c.callbacks.onOverflowXNone.call(t[0]),s.contentReset.x=1),"_resetY"!==o&&"_resetX"!==o){if(!s.contentReset.y&&t[0].mcs||!s.overflowed[0]||(i("onOverflowY")&&c.callbacks.onOverflowY.call(t[0]),s.contentReset.x=null),!s.contentReset.x&&t[0].mcs||!s.overflowed[1]||(i("onOverflowX")&&c.callbacks.onOverflowX.call(t[0]),s.contentReset.x=null),c.snapAmount){var v=c.snapAmount instanceof Array?"x"===n.dir?c.snapAmount[1]:c.snapAmount[0]:c.snapAmount;o=V(o,v,c.snapOffset)}switch(n.dir){case"x":var x=e("#mCSB_"+s.idx+"_dragger_horizontal"),_="left",w=h[0].offsetLeft,S=[f.width()-h.outerWidth(!1),x.parent().width()-x.width()],b=[o,0===o?0:o/s.scrollRatio.x],y=p[1],B=g[1],T=y>0?y/s.scrollRatio.x:0,k=B>0?B/s.scrollRatio.x:0;break;case"y":var x=e("#mCSB_"+s.idx+"_dragger_vertical"),_="top",w=h[0].offsetTop,S=[f.height()-h.outerHeight(!1),x.parent().height()-x.height()],b=[o,0===o?0:o/s.scrollRatio.y],y=p[0],B=g[0],T=y>0?y/s.scrollRatio.y:0,k=B>0?B/s.scrollRatio.y:0}b[1]<0||0===b[0]&&0===b[1]?b=[0,0]:b[1]>=S[1]?b=[S[0],S[1]]:b[0]=-b[0],t[0].mcs||(l(),i("onInit")&&c.callbacks.onInit.call(t[0])),clearTimeout(h[0].onCompleteTimeout),J(x[0],_,Math.round(b[1]),u[1],n.scrollEasing),!s.tweenRunning&&(0===w&&b[0]>=0||w===S[0]&&b[0]<=S[0])||J(h[0],_,Math.round(b[0]),u[0],n.scrollEasing,n.overwrite,{onStart:function(){n.callbacks&&n.onStart&&!s.tweenRunning&&(i("onScrollStart")&&(l(),c.callbacks.onScrollStart.call(t[0])),s.tweenRunning=!0,C(x),s.cbOffsets=r())},onUpdate:function(){n.callbacks&&n.onUpdate&&i("whileScrolling")&&(l(),c.callbacks.whileScrolling.call(t[0]))},onComplete:function(){if(n.callbacks&&n.onComplete){"yx"===c.axis&&clearTimeout(h[0].onCompleteTimeout);var e=h[0].idleTimer||0;h[0].onCompleteTimeout=setTimeout(function(){i("onScroll")&&(l(),c.callbacks.onScroll.call(t[0])),i("onTotalScroll")&&b[1]>=S[1]-T&&s.cbOffsets[0]&&(l(),c.callbacks.onTotalScroll.call(t[0])),i("onTotalScrollBack")&&b[1]<=k&&s.cbOffsets[1]&&(l(),c.callbacks.onTotalScrollBack.call(t[0])),s.tweenRunning=!1,h[0].idleTimer=0,C(x,"hide")},e)}}})}},J=function(e,t,o,a,n,i,r){function l(){S.stop||(x||m.call(),x=K()-v,s(),x>=S.time&&(S.time=x>S.time?x+f-(x-S.time):x+f-1,S.time<x+1&&(S.time=x+1)),S.time<a?S.id=h(l):g.call())}function s(){a>0?(S.currVal=u(S.time,_,b,a,n),w[t]=Math.round(S.currVal)+"px"):w[t]=o+"px",p.call()}function c(){f=1e3/60,S.time=x+f,h=window.requestAnimationFrame?window.requestAnimationFrame:function(e){return s(),setTimeout(e,.01)},S.id=h(l)}function d(){null!=S.id&&(window.requestAnimationFrame?window.cancelAnimationFrame(S.id):clearTimeout(S.id),S.id=null)}function u(e,t,o,a,n){switch(n){case"linear":case"mcsLinear":return o*e/a+t;case"mcsLinearOut":return e/=a,e--,o*Math.sqrt(1-e*e)+t;case"easeInOutSmooth":return e/=a/2,1>e?o/2*e*e+t:(e--,-o/2*(e*(e-2)-1)+t);case"easeInOutStrong":return e/=a/2,1>e?o/2*Math.pow(2,10*(e-1))+t:(e--,o/2*(-Math.pow(2,-10*e)+2)+t);case"easeInOut":case"mcsEaseInOut":return e/=a/2,1>e?o/2*e*e*e+t:(e-=2,o/2*(e*e*e+2)+t);case"easeOutSmooth":return e/=a,e--,-o*(e*e*e*e-1)+t;case"easeOutStrong":return o*(-Math.pow(2,-10*e/a)+1)+t;case"easeOut":case"mcsEaseOut":default:var i=(e/=a)*e,r=i*e;return t+o*(.499999999999997*r*i+-2.5*i*i+5.5*r+-6.5*i+4*e)}}e._mTween||(e._mTween={top:{},left:{}});var f,h,r=r||{},m=r.onStart||function(){},p=r.onUpdate||function(){},g=r.onComplete||function(){},v=K(),x=0,_=e.offsetTop,w=e.style,S=e._mTween[t];"left"===t&&(_=e.offsetLeft);var b=o-_;S.stop=0,"none"!==i&&d(),c()},K=function(){return window.performance&&window.performance.now?window.performance.now():window.performance&&window.performance.webkitNow?window.performance.webkitNow():Date.now?Date.now():(new Date).getTime()},Z=function(){var e=this;e._mTween||(e._mTween={top:{},left:{}});for(var t=["top","left"],o=0;o<t.length;o++){var a=t[o];e._mTween[a].id&&(window.requestAnimationFrame?window.cancelAnimationFrame(e._mTween[a].id):clearTimeout(e._mTween[a].id),e._mTween[a].id=null,e._mTween[a].stop=1)}},$=function(e,t){try{delete e[t]}catch(o){e[t]=null}},ee=function(e){return!(e.which&&1!==e.which)},te=function(e){var t=e.originalEvent.pointerType;return!(t&&"touch"!==t&&2!==t)},oe=function(e){return!isNaN(parseFloat(e))&&isFinite(e)},ae=function(e){var t=e.parents(".mCSB_container");return[e.offset().top-t.offset().top,e.offset().left-t.offset().left]},ne=function(){function e(){var e=["webkit","moz","ms","o"];if("hidden"in document)return"hidden";for(var t=0;t<e.length;t++)if(e[t]+"Hidden"in document)return e[t]+"Hidden";return null}var t=e();return t?document[t]:!1};e.fn[o]=function(t){return u[t]?u[t].apply(this,Array.prototype.slice.call(arguments,1)):"object"!=typeof t&&t?void e.error("Method "+t+" does not exist"):u.init.apply(this,arguments)},e[o]=function(t){return u[t]?u[t].apply(this,Array.prototype.slice.call(arguments,1)):"object"!=typeof t&&t?void e.error("Method "+t+" does not exist"):u.init.apply(this,arguments)},e[o].defaults=i,window[o]=!0,e(window).bind("load",function(){e(n)[o](),e.extend(e.expr[":"],{mcsInView:e.expr[":"].mcsInView||function(t){var o,a,n=e(t),i=n.parents(".mCSB_container");if(i.length)return o=i.parent(),a=[i[0].offsetTop,i[0].offsetLeft],a[0]+ae(n)[0]>=0&&a[0]+ae(n)[0]<o.height()-n.outerHeight(!1)&&a[1]+ae(n)[1]>=0&&a[1]+ae(n)[1]<o.width()-n.outerWidth(!1)},mcsInSight:e.expr[":"].mcsInSight||function(t,o,a){var n,i,r,l,s=e(t),c=s.parents(".mCSB_container"),d="exact"===a[3]?[[1,0],[1,0]]:[[.9,.1],[.6,.4]];if(c.length)return n=[s.outerHeight(!1),s.outerWidth(!1)],r=[c[0].offsetTop+ae(s)[0],c[0].offsetLeft+ae(s)[1]],i=[c.parent()[0].offsetHeight,c.parent()[0].offsetWidth],l=[n[0]<i[0]?d[0]:d[1],n[1]<i[1]?d[0]:d[1]],r[0]-i[0]*l[0][0]<0&&r[0]+n[0]-i[0]*l[0][1]>=0&&r[1]-i[1]*l[1][0]<0&&r[1]+n[1]-i[1]*l[1][1]>=0},mcsOverflow:e.expr[":"].mcsOverflow||function(t){var o=e(t).data(a);if(o)return o.overflowed[0]||o.overflowed[1]}})})})});
"use strict";!function($){$(window).outerWidth()>992&&$(".custom-scrollbar").mCustomScrollbar({scrollInertia:200,theme:"dark-thin"})}(jQuery);
/*! smooth-scroll v16.1.3 | (c) 2020 Chris Ferdinandi | MIT License | http://github.com/cferdinandi/smooth-scroll */
window.Element&&!Element.prototype.closest&&(Element.prototype.closest=function(e){var t,n=(this.document||this.ownerDocument).querySelectorAll(e),o=this;do{for(t=n.length;0<=--t&&n.item(t)!==o;);}while(t<0&&(o=o.parentElement));return o}),(function(){if("function"==typeof window.CustomEvent)return;function e(e,t){t=t||{bubbles:!1,cancelable:!1,detail:void 0};var n=document.createEvent("CustomEvent");return n.initCustomEvent(e,t.bubbles,t.cancelable,t.detail),n}e.prototype=window.Event.prototype,window.CustomEvent=e})(),(function(){for(var r=0,e=["ms","moz","webkit","o"],t=0;t<e.length&&!window.requestAnimationFrame;++t)window.requestAnimationFrame=window[e[t]+"RequestAnimationFrame"],window.cancelAnimationFrame=window[e[t]+"CancelAnimationFrame"]||window[e[t]+"CancelRequestAnimationFrame"];window.requestAnimationFrame||(window.requestAnimationFrame=function(e,t){var n=(new Date).getTime(),o=Math.max(0,16-(n-r)),a=window.setTimeout((function(){e(n+o)}),o);return r=n+o,a}),window.cancelAnimationFrame||(window.cancelAnimationFrame=function(e){clearTimeout(e)})})(),(function(e,t){"function"==typeof define&&define.amd?define([],(function(){return t(e)})):"object"==typeof exports?module.exports=t(e):e.SmoothScroll=t(e)})("undefined"!=typeof global?global:"undefined"!=typeof window?window:this,(function(M){"use strict";var q={ignore:"[data-scroll-ignore]",header:null,topOnEmptyHash:!0,speed:500,speedAsDuration:!1,durationMax:null,durationMin:null,clip:!0,offset:0,easing:"easeInOutCubic",customEasing:null,updateURL:!0,popstate:!0,emitEvents:!0},I=function(){var n={};return Array.prototype.forEach.call(arguments,(function(e){for(var t in e){if(!e.hasOwnProperty(t))return;n[t]=e[t]}})),n},r=function(e){"#"===e.charAt(0)&&(e=e.substr(1));for(var t,n=String(e),o=n.length,a=-1,r="",i=n.charCodeAt(0);++a<o;){if(0===(t=n.charCodeAt(a)))throw new InvalidCharacterError("Invalid character: the input contains U+0000.");1<=t&&t<=31||127==t||0===a&&48<=t&&t<=57||1===a&&48<=t&&t<=57&&45===i?r+="\\"+t.toString(16)+" ":r+=128<=t||45===t||95===t||48<=t&&t<=57||65<=t&&t<=90||97<=t&&t<=122?n.charAt(a):"\\"+n.charAt(a)}return"#"+r},F=function(){return Math.max(document.body.scrollHeight,document.documentElement.scrollHeight,document.body.offsetHeight,document.documentElement.offsetHeight,document.body.clientHeight,document.documentElement.clientHeight)},L=function(e){return e?(t=e,parseInt(M.getComputedStyle(t).height,10)+e.offsetTop):0;var t},x=function(e,t,n){0===e&&document.body.focus(),n||(e.focus(),document.activeElement!==e&&(e.setAttribute("tabindex","-1"),e.focus(),e.style.outline="none"),M.scrollTo(0,t))},H=function(e,t,n,o){if(t.emitEvents&&"function"==typeof M.CustomEvent){var a=new CustomEvent(e,{bubbles:!0,detail:{anchor:n,toggle:o}});document.dispatchEvent(a)}};return function(o,e){var b,a,A,O,C={};C.cancelScroll=function(e){cancelAnimationFrame(O),O=null,e||H("scrollCancel",b)},C.animateScroll=function(a,r,e){C.cancelScroll();var i=I(b||q,e||{}),c="[object Number]"===Object.prototype.toString.call(a),t=c||!a.tagName?null:a;if(c||t){var s=M.pageYOffset;i.header&&!A&&(A=document.querySelector(i.header));var n,o,u,l,m,d,f,h,p=L(A),g=c?a:(function(e,t,n,o){var a=0;if(e.offsetParent)for(;a+=e.offsetTop,e=e.offsetParent;);return a=Math.max(a-t-n,0),o&&(a=Math.min(a,F()-M.innerHeight)),a})(t,p,parseInt("function"==typeof i.offset?i.offset(a,r):i.offset,10),i.clip),y=g-s,v=F(),w=0,S=(n=y,u=(o=i).speedAsDuration?o.speed:Math.abs(n/1e3*o.speed),o.durationMax&&u>o.durationMax?o.durationMax:o.durationMin&&u<o.durationMin?o.durationMin:parseInt(u,10)),E=function(e){var t,n,o;l||(l=e),w+=e-l,d=s+y*(n=m=1<(m=0===S?0:w/S)?1:m,"easeInQuad"===(t=i).easing&&(o=n*n),"easeOutQuad"===t.easing&&(o=n*(2-n)),"easeInOutQuad"===t.easing&&(o=n<.5?2*n*n:(4-2*n)*n-1),"easeInCubic"===t.easing&&(o=n*n*n),"easeOutCubic"===t.easing&&(o=--n*n*n+1),"easeInOutCubic"===t.easing&&(o=n<.5?4*n*n*n:(n-1)*(2*n-2)*(2*n-2)+1),"easeInQuart"===t.easing&&(o=n*n*n*n),"easeOutQuart"===t.easing&&(o=1- --n*n*n*n),"easeInOutQuart"===t.easing&&(o=n<.5?8*n*n*n*n:1-8*--n*n*n*n),"easeInQuint"===t.easing&&(o=n*n*n*n*n),"easeOutQuint"===t.easing&&(o=1+--n*n*n*n*n),"easeInOutQuint"===t.easing&&(o=n<.5?16*n*n*n*n*n:1+16*--n*n*n*n*n),t.customEasing&&(o=t.customEasing(n)),o||n),M.scrollTo(0,Math.floor(d)),(function(e,t){var n=M.pageYOffset;if(e==t||n==t||(s<t&&M.innerHeight+n)>=v)return C.cancelScroll(!0),x(a,t,c),H("scrollStop",i,a,r),!(O=l=null)})(d,g)||(O=M.requestAnimationFrame(E),l=e)};0===M.pageYOffset&&M.scrollTo(0,0),f=a,h=i,c||history.pushState&&h.updateURL&&history.pushState({smoothScroll:JSON.stringify(h),anchor:f.id},document.title,f===document.documentElement?"#top":"#"+f.id),"matchMedia"in M&&M.matchMedia("(prefers-reduced-motion)").matches?x(a,Math.floor(g),!1):(H("scrollStart",i,a,r),C.cancelScroll(!0),M.requestAnimationFrame(E))}};var t=function(e){if(!e.defaultPrevented&&!(0!==e.button||e.metaKey||e.ctrlKey||e.shiftKey)&&"closest"in e.target&&(a=e.target.closest(o))&&"a"===a.tagName.toLowerCase()&&!e.target.closest(b.ignore)&&a.hostname===M.location.hostname&&a.pathname===M.location.pathname&&/#/.test(a.href)){var t,n;try{t=r(decodeURIComponent(a.hash))}catch(e){t=r(a.hash)}if("#"===t){if(!b.topOnEmptyHash)return;n=document.documentElement}else n=document.querySelector(t);(n=n||"#top"!==t?n:document.documentElement)&&(e.preventDefault(),(function(e){if(history.replaceState&&e.updateURL&&!history.state){var t=M.location.hash;t=t||"",history.replaceState({smoothScroll:JSON.stringify(e),anchor:t||M.pageYOffset},document.title,t||M.location.href)}})(b),C.animateScroll(n,a))}},n=function(e){if(null!==history.state&&history.state.smoothScroll&&history.state.smoothScroll===JSON.stringify(b)){var t=history.state.anchor;"string"==typeof t&&t&&!(t=document.querySelector(r(history.state.anchor)))||C.animateScroll(t,null,{updateURL:!1})}};C.destroy=function(){b&&(document.removeEventListener("click",t,!1),M.removeEventListener("popstate",n,!1),C.cancelScroll(),O=A=a=b=null)};return (function(){if(!("querySelector"in document&&"addEventListener"in M&&"requestAnimationFrame"in M&&"closest"in M.Element.prototype))throw"Smooth Scroll: This browser does not support the required JavaScript methods and browser APIs.";C.destroy(),b=I(q,e||{}),A=b.header?document.querySelector(b.header):null,document.addEventListener("click",t,!1),b.updateURL&&b.popstate&&M.addEventListener("popstate",n,!1)})(),C}}));
!function(t){t.fn.persianDatepicker=function(a){var s=this.data("persianDatepicker");return s?!0===a?s:this:this.each(function(){return t(this).data("persianDatepicker",new e(this,a))})};var e=function(){function e(e,a){var s=this;s.el=t(e);var n=s.el;s.options=t.extend(!1,{},{months:["فروردین","اردیبهشت","خرداد","تیر","مرداد","شهریور","مهر","آبان","آذر","دی","بهمن","اسفند"],dowTitle:["شنبه","یکشنبه","دوشنبه","سه شنبه","چهارشنبه","پنج شنبه","جمعه"],shortDowTitle:["ش","ی","د","س","چ","پ","ج"],showGregorianDate:!1,persianNumbers:!0,formatDate:"YYYY/MM/DD",selectedBefore:!1,selectedDate:null,startDate:null,endDate:null,prevArrow:"◄",nextArrow:"►",theme:"default",alwaysShow:!1,selectableYears:null,selectableMonths:[1,2,3,4,5,6,7,8,9,10,11,12],cellWidth:25,cellHeight:20,fontSize:13,isRTL:!1,closeOnBlur:!0,calendarPosition:{x:0,y:0},onShow:function(){},onHide:function(){},onSelect:function(){},onRender:function(){}},a);var i=s.options;if(_fontSize=i.fontSize,_cw=parseInt(i.cellWidth),_ch=parseInt(i.cellHeight),s.cellStyle="style='width:"+_cw+"px;height:"+_ch+"px;line-height:"+_ch+"px; font-size:"+_fontSize+"px; ' ",s.headerStyle="style='height:"+_ch+"px;line-height:"+_ch+"px; font-size:"+(_fontSize+4)+"px;' ",s.selectUlStyle="style='margin-top:"+_ch+"px;height:"+(7*_ch+20)+"px; font-size:"+(_fontSize-2)+"px;' ",s.selectMonthLiStyle="style='height:"+(7*_ch+7)/4+"px;line-height:"+(7*_ch+7)/4+"px; width:"+6.7*_cw/3+"px;width:"+6.7*_cw/3+"px\\9;' ",s.selectYearLiStyle="style='height:"+(7*_ch+10)/6+"px;line-height:"+(7*_ch+10)/6+"px; width:"+(6.7*_cw-14)/3+"px;width:"+(6.7*_cw-15)/3+"px\\9;' ",s.footerStyle="style='height:"+_ch+"px;line-height:"+_ch+"px; font-size:"+_fontSize+"px;' ",s.jDateFunctions=new jDateFunctions,null!=s.options.startDate&&("today"==s.options.startDate&&(s.options.startDate=s.now().toString("YYYY/MM/DD")),"today"==s.options.endDate&&(s.options.endDate=s.now().toString("YYYY/MM/DD")),s.options.selectedDate=s.options.startDate),null==s.options.selectedDate&&!s.options.showGregorianDate){var o=new RegExp("^([1-9][0-9][0-9][0-9])/([0]?[1-9]|[1][0-2])/([0]?[1-9]|[1-2][0-9]|[3][0-1])$");n.is("input")?o.test(n.val())&&(s.options.selectedDate=n.val()):o.test(n.html())&&(s.options.selectedDate=n.html())}if(s._persianDate=null!=s.options.selectedDate?(new persianDate).parse(s.options.selectedDate):s.now(),null!=i.selectableYears&&-1==i.selectableYears._indexOf(s._persianDate.year)&&(s._persianDate.year=i.selectableYears[0]),-1==s.options.selectableMonths._indexOf(s._persianDate.month)&&(s._persianDate.month=i.selectableMonths[0]),s.persianDate=s._persianDate,s._id="pdp-"+Math.round(1e7*Math.random()),s.persianDate.formatDate=i.formatDate,s.calendar=t('<div id="'+s._id+'" class="pdp-'+i.theme+'" />'),null!=s.options.startDate){s.options.selectableYears=[];for(var r=s.persianDate.parse(s.options.startDate).year;r<=s.persianDate.parse(s.options.endDate).year;r++)s.options.selectableYears.push(r)}(n.attr("pdp-id")||"").length||n.attr("pdp-id",s._id),n.addClass("pdp-el").on("click",function(t){s.show(t)}).on("focus",function(t){s.show(t)}),i.closeOnBlur&&n.on("blur",function(t){s.calendar.is(":hover")||s.hide(t)}),i.selectedBefore&&(null!=s.options.selectedDate?s.showDate(n,s.persianDate.parse(s.options.selectedDate).toString("YYYY/MM/DD/"+s.jDateFunctions.getWeekday(s.persianDate.parse(s.options.selectedDate)),s.now().gDate,i.showGregorianDate)):s.showDate(n,s.now().toString("YYYY/MM/DD/"+s.jDateFunctions.getWeekday(s.now())),s.now().gDate,i.showGregorianDate)),i.isRTL&&n.addClass("rtl"),s.calendar.length&&!i.alwaysShow&&s.calendar.hide(),t(document).bind("mouseup",function(e){var a=e.target,o=s.calendar;n.is(a)||o.is(a)||0!==o.has(a).length||!o.is(":visible")||s.hide();var r=t(".pdp-"+i.theme+" .yearSelect");r.is(e.target)||0!==r.has(e.target).length||r.hide(),(r=t(".pdp-"+i.theme+" .monthSelect")).is(e.target)||0!==r.has(e.target).length||r.hide()});var d=function(){var t=n.offset();s.calendar.css({top:t.top+n.outerHeight()+i.calendarPosition.y+"px",left:t.left+i.calendarPosition.x+"px"})};s.onresize=d,t(window).resize(d),t("body").append(s.calendar),s.render(),d()}return e.prototype={show:function(){this.calendar.show(),t.each(t(".pdp-el").not(this.el),function(t,e){e.length&&e.options.onHide(e.calendar)}),this.options.onShow(this.calendar),this.onresize()},hide:function(){this.options.onHide(this.calendar),this.options&&!this.options.alwaysShow&&this.calendar.hide()},render:function(){this.calendar.children().remove(),this.header(),this.dows(),this.content(),this.footer(),this.options.onRender()},header:function(){var e=this;_monthYear=t('<div class="" />'),_monthYear.appendTo(this.calendar),_head=t('<div class="pdp-header" '+e.headerStyle+" />"),_head.appendTo(this.calendar),_next=t('<div class="nextArrow" />').html(this.options.nextArrow).attr("title","ماه بعد"),null==e.options.endDate||e.persianDate.parse(e.options.endDate).year>e.persianDate.year||e.persianDate.parse(e.options.endDate).month>e.persianDate.month?(_next.bind("click",function(){for(nextMonth=e.persianDate.month+1;-1==e.options.selectableMonths._indexOf(nextMonth)&&nextMonth<13;nextMonth++);e.persianDate.addMonth(nextMonth-e.persianDate.month),e.render()}),_next.removeClass("disabled")):_next.addClass("disabled"),_next.appendTo(_head);var s=t('<ul class="monthSelect" '+e.selectUlStyle+" />").hide(),n=t('<ul class="yearSelect" '+e.selectUlStyle+" />").hide(),o=t("<span/>").html(e.options.months[e.persianDate.month-1]).mousedown(function(){return!1}).click(function(t){t.stopPropagation(),n.css({display:"none"}),s.css({display:"inline-block"})}),r=t("<span/>").html(e.options.persianNumbers?e.jDateFunctions.toPersianNums(e.persianDate.year):e.persianDate.year).mousedown(function(){return!1}).click(function(t){t.stopPropagation(),s.css({display:"none"}),n.css({display:"inline-block"}),n.scrollTop(70)});_startDate=null!=e.options.startDate?e.persianDate.parse(e.options.startDate):e.persianDate.parse("1/1/1"),_endDate=null!=e.options.endDate?e.persianDate.parse(e.options.endDate):e.persianDate.parse("9999/1/1");var d=function(s,o){var r=!1;void 0===s&&void 0===o?(b=e.persianDate.year-7,a=e.persianDate.year+14):0==o?(b=s-6,a=s,r=!0):0==s&&(b=o+1,a=b+6);var d=[];for(i=b;i<a&&b>0;i++)d.push(parseInt(i));t.each(e.options.selectableYears||(r?d.reverse():d),function(a,s){var i=t("<li "+e.selectYearLiStyle+" />").html(e.options.persianNumbers?e.jDateFunctions.toPersianNums(s):s);s==e.persianDate.year&&i.addClass("selected"),i.attr("value",s),i.bind("click",function(){e.persianDate.date=1,e.persianDate.year=parseInt(s),_endDate.year!=s&&9999!=_endDate.year||(e.persianDate.month=_endDate.month),_startDate.year!=s&&9999!=_startDate.year||(e.persianDate.month=_startDate.month),e.render()}),r?n.prepend(i):n.append(i)})};for(d(),i=1;i<=12;i++){var l=e.options.months[i-1],h=-1==e.options.selectableMonths._indexOf(i)||_startDate.year==e.persianDate.year&&_startDate.month>i||_endDate.year==e.persianDate.year&&i>_endDate.month?t('<li class="disableMonth" '+e.selectMonthLiStyle+" />").html(l):t("<li "+e.selectMonthLiStyle+" />").html(l);i==e.persianDate.month&&h.addClass("selected"),h.data("month",{month:l,monthNum:i}),h.hasClass("disableMonth")||h.bind("click",function(){e.persianDate.date=1,e.persianDate.month=t(this).data("month").monthNum,e.render()}),s.append(h)}n.bind("scroll",function(){null==e.options.selectableYears&&(c=t(this).find("li").length,firstYear=parseInt(t(this).children("li:first").val()),lastYear=parseInt(t(this).children("li:last").val()),lisHeight=c/3*(t(this).find("li:first").height()+4),_com=500*t(this).scrollTop().toString().length,t(this).scrollTop()<100*_com.toString().length&&firstYear>=1&&d(firstYear,0),_com=100*t(this).scrollTop().toString().length,lisHeight-t(this).scrollTop()>-_com&&lisHeight-t(this).scrollTop()<_com&&(d(0,lastYear),t(this).scrollTop(t(this).scrollTop()-50)),t(this).scrollTop()<_com.toString().length&&firstYear>=30&&t(this).scrollTop(100*_com.toString().length))}),_monthYear.append(s).append(n);var p=t('<div class="monthYear" />').append(o).append("<span>&nbsp;&nbsp;</span>").append(r);_head.append(p),_prev=t('<div class="prevArrow" />').html(this.options.prevArrow).attr("title","ماه قبل"),null==e.options.startDate||e.persianDate.parse(e.options.startDate).year<e.persianDate.year||e.persianDate.parse(e.options.startDate).month<e.persianDate.month?(_prev.bind("click",function(){e.persianDate.addMonth(-1),e.render()}),_prev.removeClass("disabled")):_prev.addClass("disabled"),_prev.appendTo(_head)},dows:function(){for(_row=t('<div class="dows" />'),i=0;i<7;i++)_cell=t('<div class="dow cell " '+this.cellStyle+" />").html(this.options.shortDowTitle[i]),_cell.appendTo(_row);_row.appendTo(this.calendar)},content:function(){var e=this;_days=t('<div class="days" />'),_days.appendTo(this.calendar),jd=e.persianDate,jd.date=1,_start=e.jDateFunctions.getWeekday(e.persianDate),_end=e.jDateFunctions.getLastDayOfMonth(e.persianDate);for(var a=0,s=0;a<6;a++){_row=t("<div />");for(var n=0;n<7;n++,s++)s<_start||s-_start+1>_end?_cell=t('<div class="nul cell " '+e.cellStyle+" />").html("&nbsp;"):(_dt=e.getDate(e.persianDate,s-_start+1),_today="",_selday="",_disday="",0==e.now().compare(_dt)&&(_today="today"),null==e.options.startDate||-1!=e.persianDate.parse(e.options.startDate).compare(_dt)&&1!=e.persianDate.parse(e.options.endDate).compare(_dt)||(_disday="disday"),null!=e.options.selectedDate?e.persianDate.parse(e.options.selectedDate).date==s-_start+1&&(_selday="selday"):s-_start+1==e.now().date&&(_selday="selday"),_fri=6==n?"friday":"",_cell=t('<div class="day cell '+_fri+" "+_today+" "+_selday+" "+_disday+'" '+e.cellStyle+" />"),_cell.attr("data-jdate",_dt.toString("YYYY/MM/DD")),_cell.attr("data-gdate",e.jDateFunctions.getGDate(_dt)._toString("YYYY/MM/DD")),_cell.html(e.options.persianNumbers?e.jDateFunctions.toPersianNums(s-_start+1):s-_start+1),(null==e.options.startDate||-1!=e.persianDate.parse(e.options.startDate).compare(_dt)&&1!=e.persianDate.parse(e.options.endDate).compare(_dt))&&_cell.bind("click",function(){e.calendar.find(".day").removeClass("selday"),t(this).addClass("selday"),e.options.showGregorianDate?e.showDate(e.el,t(this).data("jdate"),t(this).data("gdate"),!0):e.showDate(e.el,t(this).data("jdate"),t(this).data("gdate"),!1),e.hide()})),_cell.appendTo(_row);_row.appendTo(_days)}},footer:function(){var e=this;_footer=t('<div class="pdp-footer" '+e.footerStyle+" />"),_footer.appendTo(this.calendar),e.options.selectableMonths._indexOf(e.persianDate.month)>-1&&(_goToday=t('<a class="goToday" />'),_goToday.attr("data-jdate",e.now().toString("YYYY/MM/DD/DW")),_goToday.attr("data-gdate",e.jDateFunctions.getGDate(e.now())),_goToday.attr("href","javascript:;").html("هم اکنون"),null==e.options.startDate&&_goToday.bind("click",function(){e.persianDate=e.now(),e.showDate(e.el,t(this).data("jdate"),t(this).data("gdate"),e.options.showGregorianDate),e.calendar.find(".day").removeClass("selday"),e.render(),e.calendar.find(".today").addClass("selday"),e.hide()}),_goToday.appendTo(_footer))},showDate:function(t,e,a,s){e=this.persianDate.parse(e).toString(this.options.formatDate),a=new Date(a)._toString(this.options.formatDate),t.is("input:text")?s?t.val(a):t.val(e):s?t.html(a):t.html(e),t.attr("data-jDate",e),t.attr("data-gDate",a),this.options.onSelect()},getDate:function(t,e){return t.date=e,t.day=this.jDateFunctions.getWeekday(t),t},now:function(){return this.jDateFunctions.gregorian_to_jalali(new Date)}},e}();Number.prototype.padLeft=function(t,e){var a=String(t||10).length-String(this).length+1;return a>0?new Array(a).join(e||"0")+this:this},Date.prototype._toString=function(t){return months=["Januray","February","March","April","May","June","Julay","August","September","October","November","December"],dows=["Sun","Mon","Tue","Wed","Tur","Fri","Sat"],void 0===t||"default"==t?this.toLocaleDateString():t.replace("YYYY",this.getFullYear()).replace("MM",this.getMonth()+1).replace("DD",this.getDate()).replace("0M",this.getMonth()+1>9?this.getMonth()+1:"0"+(this.getMonth()+1)).replace("0D",this.getDate()>9?this.getDate():"0"+this.getDate()).replace("hh",0==this.getHours()?(new Date).getHours():this.getHours()).replace("mm",0==this.getMinutes()?(new Date).getMinutes():this.getMinutes()).replace("ss",0==this.getSeconds()?(new Date).getSeconds():this.getSeconds()).replace("0h",this.getHours()>9?this.getHours():"0"+this.getHours()).replace("0m",this.getMinutes()>9?this.getMinutes():"0"+this.getMinutes()).replace("0s",this.getSeconds()>9?this.getSeconds():"0"+this.getSeconds()).replace("ms",0==this.getMilliseconds()?(new Date).getMilliseconds():this.getMilliseconds()).replace("tm",this.getHours()>=12&&this.getMinutes()>0?"PM":"AM").replace("NM",months[this.getMonth()]).replace("DW",this.getDay()).replace("ND",dows[this.getDay()])},Array.prototype._indexOf=function(e){return t.inArray(e,this)}}(jQuery);var persianDate=function(){function t(){this.months=["فروردین","اردیبهشت","خرداد","تیر","مرداد","شهریور","مهر","آبان","آذر","دی","بهمن","اسفند"],this.dowTitle=["شنبه","یکشنبه","دوشنبه","سه شنبه","چهارشنبه","پنج شنبه","جمعه"],this.year=1300,this.month=1,this.date=1,this.day=1,this.gDate=new Date}return t.prototype={now:function(){return(new jDateFunctions).gregorian_to_jalali(new Date)},addDay:function(e){for(var a=new jDateFunctions,s=e>0?e:-e,n=0;n<s;n++){var i=new t;i.month=this.month,i.year=this.year,i=i.addMonth(-1);var o=e>0?a.getLastDayOfMonth(this):a.getLastDayOfMonth(i);e>0?this.date+=1:this.date-=1,e>0?this.date>o&&(this.date=1,this.addMonth(1)):e<0&&(this.month>1&&this.date>o?(this.date=1,this.addMonth(1)):0==this.date&&(this.addMonth(-1),this.date=o))}return this},addMonth:function(t){for(var e=t>0?t:-t,a=0;a<e;a++)t>0?this.month+=1:this.month-=1,13==this.month?(this.month=1,this.addYear(1)):0==this.month&&(this.month=12,this.addYear(-1));return this},addYear:function(t){return this.year+=t,this},compare:function(t){return t.year==this.year&&t.month==this.month&&t.date==this.date?0:t.year>this.year?1:t.year==this.year&&t.month>this.month?1:t.year==this.year&&t.month==this.month&&t.date>this.date?1:-1},parse:function(e){arr=e.split("/"),y=arr[0],m=arr[1],d=arr[2];var a=new t;return jdf=new jDateFunctions,a.year=parseInt(y),a.month=parseInt(m),a.date=parseInt(d),a.day=jdf.getWeekday(a),a.gDate=jdf.jalali_to_gregorian(a),a},toString:function(t){return void 0===t?this.year+"/"+this.month+"/"+this.date:t.replace("YYYY",this.year).replace("MM",this.month).replace("DD",this.date).replace("0M",this.month>9?this.month:"0"+this.month.toString()).replace("0D",this.date>9?this.date:"0"+this.date.toString()).replace("hh",this.gDate.getHours()).replace("mm",this.gDate.getMinutes()).replace("ss",this.gDate.getSeconds()).replace("0h",this.gDate.getHours()>9?this.gDate.getHours():"0"+this.gDate.getHours()).replace("0m",this.gDate.getMinutes()>9?this.gDate.getMinutes():"0"+this.gDate.getMinutes()).replace("0s",this.gDate.getSeconds()>9?this.gDate.getSeconds():"0"+this.gDate.getSeconds()).replace("tm",this.gDate.getHours()>=12&&this.gDate.getMinutes()>0?"ب.ظ":"ق.ظ").replace("ms",this.gDate.getMilliseconds()).replace("NM",this.months[this.month-1]).replace("DW",this.day).replace("ND",this.dowTitle[this.day])}},t}(),jDateFunctions=function(){function t(){}return t.prototype={toPersianNums:function(t){for(strnum=t.toString(),nums=["۰","۱","۲","۳","۴","۵","۶","۷","۸","۹"],res="",i=0;i<strnum.length;i++)res+=nums[parseInt(strnum[i])];return res},gregorian_to_jalali:function(t){return gy=t.getFullYear(),gm=t.getMonth()+1,gd=t.getDate(),g_d_m=[0,31,59,90,120,151,181,212,243,273,304,334],gy>1600?(jy=979,gy-=1600):(jy=0,gy-=621),gy2=gm>2?gy+1:gy,days=365*gy+parseInt((gy2+3)/4)-parseInt((gy2+99)/100)+parseInt((gy2+399)/400)-80+gd+g_d_m[gm-1],jy+=33*parseInt(days/12053),days%=12053,jy+=4*parseInt(days/1461),days%=1461,days>365&&(jy+=parseInt((days-1)/365),days=(days-1)%365),jm=days<186?1+parseInt(days/31):7+parseInt((days-186)/30),jd=1+(days<186?days%31:(days-186)%30),t=new Date,pd=new persianDate,pd.year=jy,pd.month=jm,pd.date=jd,pd.gDate=t,pd},jalali_to_gregorian:function(t){for(jy=t.year,jm=t.month,jd=t.date,jy>979?(gy=1600,jy-=979):gy=621,days=365*jy+8*parseInt(jy/33)+parseInt((jy%33+3)/4)+78+jd+(jm<7?31*(jm-1):30*(jm-7)+186),gy+=400*parseInt(days/146097),days%=146097,days>36524&&(gy+=100*parseInt(--days/36524),days%=36524,days>=365&&days++),gy+=4*parseInt(days/1461),days%=1461,days>365&&(gy+=parseInt((days-1)/365),days=(days-1)%365),gd=days+1,sal_a=[0,31,gy%4==0&&gy%100!=0||gy%400==0?29:28,31,30,31,30,31,31,30,31,30,31],gm=0;gm<13&&(v=sal_a[gm],!(gd<=v));gm++)gd-=v;return dt=new Date,new Date(gy,gm-1,gd,dt.getHours(),dt.getMinutes(),dt.getSeconds(),dt.getMilliseconds())},getGDate:function(t){return this.jalali_to_gregorian(t)},getWeekday:function(t){return[1,2,3,4,5,6,0][this.jalali_to_gregorian(t).getDay()]},getLastDayOfMonth:function(t){return y=t.year,m=t.month,m>=1&&m<=6?31:m>=7&&m<12?30:this.isLeapYear(y)?30:29},isLeapYear:function(t){return b=t%33,(t>1342?[1,5,9,13,17,22,26,30]:[1,5,9,13,17,21,26,30])._indexOf(b)>-1}},t}();

(function(b){b.fn.waitMe=function(p){return this.each(function(){function y(){var a=f.attr("data-waitme_id");f.removeClass("waitMe_container").removeAttr("data-waitme_id");f.find('.waitMe[data-waitme_id="'+a+'"]').remove()}var f=b(this),z,g,e,r=!1,t="background-color",u="",q="",v,a,w,n={init:function(){function n(a){l.css({top:"auto",transform:"translateY("+a+"px) translateZ(0)"})}a=b.extend({effect:"bounce",text:"",bg:"rgba(255,255,255,0.7)",color:"#000",maxSize:"",waitTime:-1,textPos:"vertical",
fontSize:"",source:"",onClose:function(){}},p);w=(new Date).getMilliseconds();v=b('<div class="waitMe" data-waitme_id="'+w+'"></div>');switch(a.effect){case "none":e=0;break;case "bounce":e=3;break;case "rotateplane":e=1;break;case "stretch":e=5;break;case "orbit":e=2;r=!0;break;case "roundBounce":e=12;break;case "win8":e=5;r=!0;break;case "win8_linear":e=5;r=!0;break;case "ios":e=12;break;case "facebook":e=3;break;case "rotation":e=1;t="border-color";break;case "timer":e=2;var c=b.isArray(a.color)?
a.color[0]:a.color;u="border-color:"+c;break;case "pulse":e=1;t="border-color";break;case "progressBar":e=1;break;case "bouncePulse":e=3;break;case "img":e=1}""!==u&&(u+=";");if(0<e){if("img"===a.effect)q='<img src="'+a.source+'">';else for(var d=1;d<=e;++d)b.isArray(a.color)?(c=a.color[d],void 0==c&&(c="#000")):c=a.color,q=r?q+('<div class="waitMe_progress_elem'+d+'"><div style="'+t+":"+c+'"></div></div>'):q+('<div class="waitMe_progress_elem'+d+'" style="'+t+":"+c+'"></div>');g=b('<div class="waitMe_progress '+
a.effect+'" style="'+u+'">'+q+"</div>")}a.text&&(c=b.isArray(a.color)?a.color[0]:a.color,z=b('<div class="waitMe_text" style="color:'+c+";"+(""!=a.fontSize?"font-size:"+a.fontSize:"")+'">'+a.text+"</div>"));var k=f.find("> .waitMe");k&&k.remove();c=b('<div class="waitMe_content '+a.textPos+'"></div>');c.append(g,z);v.append(c);"HTML"==f[0].tagName&&(f=b("body"));f.addClass("waitMe_container").attr("data-waitme_id",w).append(v);k=f.find("> .waitMe");var l=f.find(".waitMe_content");k.css({background:a.bg});
""!==a.maxSize&&"none"!=a.effect&&(c=g.outerHeight(),g.outerWidth(),"img"===a.effect?(g.css({height:a.maxSize+"px"}),g.find(">img").css({maxHeight:"100%"}),l.css({marginTop:-l.outerHeight()/2+"px"})):a.maxSize<c&&("stretch"==a.effect?(g.css({height:a.maxSize+"px",width:a.maxSize+"px"}),g.find("> div").css({margin:"0 5%"})):(c=a.maxSize/c-.2,d="-50%","roundBounce"==a.effect?(d="-75%",a.text&&(d="75%")):"win8"==a.effect||"timer"==a.effect||"orbit"==a.effect?(d="-20%",a.text&&(d="20%")):"ios"==a.effect&&
(d="-15%",a.text&&(d="15%")),"rotation"==a.effect&&a.text&&(d="75%"),g.css({transform:"scale("+c+") translateX("+d+")",whiteSpace:"nowrap"}))));l.css({marginTop:-l.outerHeight()/2+"px"});if(f.outerHeight()>b(window).height()){c=b(window).scrollTop();var h=l.outerHeight(),m=f.offset().top,x=f.outerHeight();d=c-m+b(window).height()/2;0>d&&(d=Math.abs(d));0<=d-h&&d+h<=x?m-c>b(window).height()/2&&(d=h):d=c>m+x-h?c-m-h:c-m+h;n(d);b(document).scroll(function(){var a=b(window).scrollTop()-m+b(window).height()/
2;0<=a-h&&a+h<=x&&n(a)})}0<a.waitTime&&setTimeout(function(){y()},a.waitTime);k.on("destroyed",function(){if(a.onClose&&b.isFunction(a.onClose))a.onClose(f);k.trigger("close",{el:f})});b.event.special.destroyed={remove:function(a){a.handler&&a.handler()}};return k},hide:function(){y()}};if(n[p])return n[p].apply(this,Array.prototype.slice.call(arguments,1));if("object"===typeof p||!p)return n.init.apply(this,arguments)})};b(window).on("load",function(){b("body.waitMe_body").addClass("hideMe");setTimeout(function(){b("body.waitMe_body").find(".waitMe_container:not([data-waitme_id])").remove();
b("body.waitMe_body").removeClass("waitMe_body hideMe")},200)})})(jQuery);

!function(n){function t(r){if(i[r])return i[r].exports;var u=i[r]={i:r,l:!1,exports:{}};return n[r].call(u.exports,u,u.exports,t),u.l=!0,u.exports}var i={};t.m=n;t.c=i;t.d=function(n,i,r){t.o(n,i)||Object.defineProperty(n,i,{configurable:!1,enumerable:!0,get:r})};t.n=function(n){var i=n&&n.__esModule?function(){return n.default}:function(){return n};return t.d(i,"a",i),i};t.o=function(n,t){return Object.prototype.hasOwnProperty.call(n,t)};t.p="";t(t.s=0)}([function(n,t,i){i(1);n.exports=i(4)},function(n,t,i){"use strict";var u=Object.assign||function(n){for(var i,r,t=1;t<arguments.length;t++){i=arguments[t];for(r in i)Object.prototype.hasOwnProperty.call(i,r)&&(n[r]=i[r])}return n},r;i(2);r=i(3);!function(n){function i(n){return n=u({},t,n),function(n){return["nfc-top-left","nfc-top-right","nfc-bottom-left","nfc-bottom-right"].indexOf(n)>-1}(n.positionClass)||(console.warn("An invalid notification position class has been specified."),n.positionClass=t.positionClass),n.onclick&&"function"!=typeof n.onclick&&(console.warn("Notification on click must be a function."),n.onclick=t.onclick),"number"!=typeof n.showDuration&&(n.showDuration=t.showDuration),r.isString(n.theme)&&0!==n.theme.length||(console.warn("Notification theme must be a string with length"),n.theme=t.theme),n}function f(n){return n=i(n),function(){var s=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},u=s.title,f=s.message,i=e(n.positionClass),t,o,h;if(!u&&!f)return console.warn("Notification must contain a title or a message!");t=r.createElement("div","ncf",n.theme);(!0===n.closeOnClick&&t.addEventListener("click",function(){return i.removeChild(t)}),n.onclick&&t.addEventListener("click",function(t){return n.onclick(t)}),n.displayCloseButton)&&(o=r.createElement("button"),o.innerText="X",!1===n.closeOnClick&&o.addEventListener("click",function(){return i.removeChild(t)}),r.append(t,o));(r.isString(u)&&u.length&&r.append(t,r.createParagraph("ncf-title")(u)),r.isString(f)&&f.length&&r.append(t,r.createParagraph("nfc-message")(f)),r.append(i,t),n.showDuration&&n.showDuration>0)&&(h=setTimeout(function(){i.removeChild(t);0===i.querySelectorAll(".ncf").length&&document.body.removeChild(i)},n.showDuration),(n.closeOnClick||n.displayCloseButton)&&t.addEventListener("click",function(){return clearTimeout(h)}))}}function e(n){var t=document.querySelector("."+n);return t||(t=r.createElement("div","ncf-container",n),r.append(document.body,t)),t}var t={closeOnClick:!0,displayCloseButton:!1,positionClass:"nfc-top-right",onclick:!1,showDuration:3500,theme:"success"};n.createNotification?console.warn("Window already contains a create notification function. Have you included the script twice?"):n.createNotification=f}(window)},function(){"use strict";!function(){function n(n){this.el=n;for(var r=n.className.replace(/^\s+|\s+$/g,"").split(/\s+/),t=0;t<r.length;t++)i.call(this,r[t])}if(!(void 0===window.Element||"classList"in document.documentElement)){var t=Array.prototype,i=t.push,r=t.splice,u=t.join;n.prototype={add:function(n){this.contains(n)||(i.call(this,n),this.el.className=this.toString())},contains:function(n){return-1!=this.el.className.indexOf(n)},item:function(n){return this[n]||null},remove:function(n){if(this.contains(n)){for(var t=0;t<this.length&&this[t]!=n;t++);r.call(this,t,1);this.el.className=this.toString()}},toString:function(){return u.call(this," ")},toggle:function(n){return this.contains(n)?this.remove(n):this.add(n),this.contains(n)}};window.DOMTokenList=n,function(n,t,i){Object.defineProperty?Object.defineProperty(n,t,{get:i}):n.__defineGetter__(t,i)}(Element.prototype,"classList",function(){return new n(this)})}}()},function(n,t){"use strict";Object.defineProperty(t,"__esModule",{value:!0});var i=t.partial=function(n){for(var i=arguments.length,r=Array(i>1?i-1:0),t=1;t<i;t++)r[t-1]=arguments[t];return function(){for(var i=arguments.length,u=Array(i),t=0;t<i;t++)u[t]=arguments[t];return n.apply(void 0,r.concat(u))}},r=(t.append=function(n){for(var i=arguments.length,r=Array(i>1?i-1:0),t=1;t<i;t++)r[t-1]=arguments[t];return r.forEach(function(t){return n.appendChild(t)})},t.isString=function(n){return"string"==typeof n},t.createElement=function(n){for(var u,i=arguments.length,r=Array(i>1?i-1:0),t=1;t<i;t++)r[t-1]=arguments[t];return u=document.createElement(n),r.length&&r.forEach(function(n){return u.classList.add(n)}),u}),u=function(n,t){return n.innerText=t,n},f=function(n){for(var f=arguments.length,e=Array(f>1?f-1:0),t=1;t<f;t++)e[t-1]=arguments[t];return i(u,r.apply(void 0,[n].concat(e)))};t.createParagraph=function(){for(var t=arguments.length,i=Array(t),n=0;n<t;n++)i[n]=arguments[n];return f.apply(void 0,["p"].concat(i))}},function(){}]);

jQuery.extend({
    ConvertImageNameToWebP: function (imageName) {
        return imageName.replace(/\.(png|jpg|jpeg|gif)$/, '.webp');
    }
});

function getList(name) {
    var list = localStorage.getItem(name);
    return list ? JSON.parse(list) : [];
}

// Function to save the list to localStorage
function saveList(list, name) {
    localStorage.setItem(name, JSON.stringify(list));
}

(function ($) {
    $.extend({
        ToAmount: function (num) {
            if (!isNaN(num)) {
                return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " تومان";
            }
            return null;
        }
    });
})(jQuery);


function FillPageId(pageId) {
    $('#PageId').val(+pageId);
    $('#filterForm').submit();
    $('.filterForm').submit();
    $('.FilterForm').submit();
}
function ToAmount(number) {
    // Validate input (ensure it's a positive number)
    if (typeof number !== 'number' || isNaN(number) || number < 0) {
        console.error('Invalid input. Please provide a positive number.');
        return;
    }

    // Format the number with commas
    const formattedNumber = number.toLocaleString();

    // Add "تومان" at the end
    const result = formattedNumber + ' تومان';

    return result;
}
function toShamsi(dateString) {
    // Parse the input date string
    var date = new Date(dateString);

    // Convert the date to Persian date string
    var persianDate = date.toLocaleDateString('fa-IR', {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
    });

    return persianDate.replace(/-/g, '/');
}
$(document).ready(function () {
    $('input[type="file"]').change(function (e) {
        var input = this;
        var imageClassName = $(input).attr('data-value');
        var multiple = $(input).attr('multiple');
        if (!input.files) return;
        if (input.files.length === 1) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $(`.${imageClassName}`).attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
        if (multiple) {
            $(`.${imageClassName}`).empty();
            $(input.files).each(function (index, item) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    var imageHtml = `
                    <div class="col-4 col-sm-6 mb-sm-2">
                        <div class="avatar avatar-5xl">
                            <img class="rounded-circle"
                            src="${e.target.result}" alt="${index}" title="${index}">
                        </div>
                    </div>`;
                    $(`.${imageClassName}`).append(imageHtml);
                }

                reader.readAsDataURL(item);
            });
        }
    });
    $('a[data-type="submit"]').click(function (event) {
        event.preventDefault(); // Prevent the default form submission

        // Find the parent form of the clicked button
        var form = $(this).closest('form');

        // Submit the form
        form.submit();
    });
    AddSelectDefaultData();
});

function AddSelectDefaultData() {
    $('.select-with-default-value').each(function (index, element) {
        var value = $(element).attr('value');
        $(element).val(value).trigger('change');
        try {
            $(element).selectpicker('val', value);
        } catch {

        }
    });
}
function ReloadPage() {
    location.reload();
}

//const swalWithBootstrapButtons = Swal.mixin({
//    customClass: {
//        confirmButton: 'btn btn-success',
//        cancelButton: 'btn btn-danger'
//    },
//    buttonsStyling: false
//})
document.addEventListener('DOMContentLoaded', function () {
    var inputs = document.querySelectorAll('[data-val-required]');
    inputs.forEach(function (input) {
        var label = document.querySelector('label[for="' + input.id + '"]');
        if (label) {
            label.innerHTML += ' <span style="color: red;">*</span>';
        }
    });
    InitialAmountTexts();
});


function InitialAmountTexts() {
    $('input[name="Price"]').each(function () {
        AddAmountText(this);
    });
    $('input[name="Amount"]').each(function () {
        AddAmountText(this);
    });
    $('.price-input').each(function () {
        AddAmountText(this);
    });
    $('.Price-input').each(function () {
        AddAmountText(this);
    });

}

function AddAmountText(element) {
    var $span = $(element).next('p'); // Check if a <span> element exists after the input

    if ($span.length === 0) {
        // If no <span> exists, create a new one
        $span = $('<p>').css('color', 'grey').insertAfter(element);
    }

    // Define the change event for the input
    $(element).keyup(function () {
        // Get the input value, convert it to a number, and format it
        var price = parseFloat($(element).val().replace(/,/g, ''));
        var formattedPrice = isNaN(price) ? '' : price.toLocaleString('fa-IR') + ' تومان';

        // Update the text of the span
        $span.text(formattedPrice);
    });
    var initialPrice = parseFloat($(element).val().replace(/,/g, ''));
    var initialFormattedPrice = isNaN(initialPrice) ? '' : initialPrice.toLocaleString('fa-IR') + ' تومان';
    $span.text(initialFormattedPrice);
}


function IsUserAuthenticated() {
    var authenticated = $('#is-user-authenticated').val();
    return +authenticated === +1 || authenticated === true;
}
function ToUrl(url) {
    return url.toLowerCase()
        .trim()
        .replace(/ /g, "-")
        .replace(/%/g, "-")
        .replace(/&/g, "")
        .replace("  ", " ")
        .replace(" ", "-")
        .replace(/\s+/g, '-')
        .replace(/\u200C/g, "-")
        .replace(/--+/g, "-");
}

$('.export-btn').click(function (e) {
    e.preventDefault();
    var href = $(this).attr('href');
    var currentFilterFormHref = $('.filter-form').attr('action');
    $('.filter-form').attr('action', href);
    $('.filter-form').submit();
    $('.filter-form').attr('action', currentFilterFormHref);
});

async function GetFormData(formId) {
    var formAction = $(`#${formId}`).attr('action');
    try {
        const response = await $.ajax({
            type: 'GET',
            url: formAction,
            data: $(`#${formId}`).serialize(),
        });
        return response.data;
    } catch (error) {
        console.error('Error submitting form:', error);
        return null;
    }
}

async function Get(url) {
    try {
        const response = await $.ajax({
            url: url,
        });
        return response;
    } catch (error) {
        console.error('Error submitting form:', error);
        return null;
    }
}

async function GetFormResult(formId) {
    var formAction = $(`#${formId}`).attr('action');
    var type = $(`#${formId}`).attr('method');
    try {
        const response = await $.ajax({
            type: type,
            url: formAction,
            data: $(`#${formId}`).serialize(),
        });
        return response;
    } catch (error) {
        console.error('Error submitting form:', error);
        return null;
    }
}

function open_waiting(selector = 'body') {
    $(selector).waitMe({
        effect: 'stretch',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}
function close_waiting(selector = 'body') {
    $(selector).waitMe('hide');
}
$(document).ready(function () {
    var editors = $("[ckeditor]");
    if (editors.length > 0) {
        $.getScript('/Shared/ckeditor.js', function () {
            $(editors).each(function (index, value) {
                var id = $(value).attr('ckeditor');
                ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                    {
                        toolbar:
                        {
                            items: [
                                'heading',
                                '|',
                                'bold',
                                'italic',
                                'link',
                                '|',
                                'fontSize',
                                'fontColor',
                                '|',
                                'imageUpload',
                                'blockQuote',
                                'insertTable',
                                'undo',
                                'redo',
                                'codeBlock'
                            ]
                        },
                        language: 'fa',
                        table:
                        {
                            contentToolbar: [
                                'tableColumn',
                                'tableRow',
                                'mergeTableCells'
                            ]
                        },
                        licenseKey: '',
                        simpleUpload:
                        {
                            // The URL that the images are uploaded to.
                            uploadUrl: '/Uploader/UploadImage'
                        }

                    })
                    .then(editor => {
                        window.editor = editor;
                    }).catch(err => {
                        console.error(err);
                    });
            });
        });
    }

    $('[ajax-url-button]').on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        var itemId = $(this).attr('ajax-url-button');
        $.get(url).then(result => {
            if (result.status === 'Success') {
                $('#ajax-url-item-' + itemId).hide(500);
                setTimeout(function () {
                    $('#ajax-url-item-' + itemId).remove();
                    ShowSuccessMessage(result.message);
                    ShowInfoMessage("پس از لود مجدد صفحه آیتم ها به شما نمایش داده خواهند شد");
                }, 500);
            } else {
                ShowErrorMessage(result.message);
            }
        });
    });

    $('.ajax-link').click(async function (e) {
        e.preventDefault();
        open_waiting('body');
        var url = $(this).attr('href');
        var result = await Get(url);
        close_waiting('body');
        ShowMessageByResponse(result);
        location.reload();
    });

    const persianDatePicker = {
        months: ["فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"],
        dowTitle: ["شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه"],
        shortDowTitle: ["ش", "ی", "د", "س", "چ", "پ", "ج"],
        showGregorianDate: !1,
        persianNumbers: !0,
        formatDate: "YYYY/MM/DD",
        selectedBefore: !1,
        selectedDate: null,
        startDate: null,
        endDate: null,
        prevArrow: '\u25c4',
        nextArrow: '\u25ba',
        theme: 'default',
        alwaysShow: !1,
        selectableYears: null,
        selectableMonths: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
        cellWidth: 25, // by px
        cellHeight: 20, // by px
        fontSize: 13, // by px
        isRTL: !1,
        calendarPosition:
        {
            x: 0,
            y: 0,
        },
        onShow: function () { },
        onHide: function () { },
        onSelect: function () { },
        onRender: function () { }
    };

    $(".persian-date-picker").persianDatepicker(persianDatePicker);
    $(".PersianDate-input").persianDatepicker(persianDatePicker);
});


function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        positionClass: "nfc-top-right",
        showDuration: 3000,
        hideDuration: 1000,
        timeOut: 5000,
        extendedTimeOut: 1000,
        theme: theme
    })({
        title: title,
        message: decodeURI(text)
    });
}

function ShowMessageByResponse(response) {
    var status = response.status.toLowerCase();
    switch (status) {
        case 'danger':
            {
                ShowErrorMessage(response.message);
                break;
            }
        case 'warning':
            {
                ShowWarningMessage(response.message);
                break;
            }
        case 'info':
            {
                ShowInfoMessage(response.message);
                break;
            }
        case 'success':
            {
                ShowSuccessMessage(response.message);
                break;
            }
    }
}

function ShowSuccessMessage(text) {
    ShowMessage('اعلان موفقیت', text, 'success');
}

function ShowInfoMessage(text) {
    ShowMessage('اعلان اطلاعیه', text, 'info');
}

function ShowWarningMessage(text) {
    ShowMessage('اعلان اخطار', text, 'warning');
}

function ShowErrorMessage(text) {
    ShowMessage('اعلان خطا', text, 'error');
}

$(document).ready(function (e) {
    $("form").each(function () {
        try {
            var form = $(this);
            $(form).validate({
                errorClass: "text-danger",
                errorElement: "span",
                errorPlacement: function (error, element) {
                    error.insertAfter(element.closest('.form-floating'));
                }
            });
        }
        catch {

        }
    });

    // Handle the button click
    $(".add-sub-list-item-btn").click(function () {
        var objectName = $(this).data('object-name');
        var isValid = AreInputsValid(`[data-object-name='${objectName}'][data-property-name][id]`);
        if (isValid) {
            if (!ValidateUnique(objectName)) return;
            $(`.add-sub-item-form[data-object-name='${objectName}']`)
                .find(`[data-property-name]`)
                .each(function (index, input) {
                    var value = $(input).val();
                    console.log(input);
                    console.log(value);
                    $(input).attr('value', value);
                });
            var addFormHtml = $(`.add-sub-item-form[data-object-name='${objectName}']`).html();
            var html = `
            <div class="single-sub-list row g-5" data-object-name="${objectName}">
                ${addFormHtml}
                <button type="button" data-object-name="${objectName}" onclick="RemoveSubList(this)"
                class="btn btn-danger remove-sub-list-btn col-sm-4">حذف</button>
                <hr/>
            </div> 
            `;
            $(html)
                .insertBefore(`.add-sub-item-form[data-object-name='${objectName}']`);
            ReIndexSubLists(objectName);
            $(`.add-sub-item-form[data-object-name='${objectName}']`)
                .find(`[data-property-name]`)
                .each(function (index, input) {
                    var resetValue = $(input).data('reset-value');
                    if (resetValue !== undefined && resetValue !== null) {
                        $(input).val(resetValue);
                        $(input).nextAll('p').text('');
                    }
                });
        }
    });
    $('.remove-sub-list-btn').click(function (e) {
        RemoveSubList($(this));
    });
    InitialSubListValidation();
});

function InitialSubListValidation() {
    $('[data-property-name]').change(function (e) {
        IsSingleInputValid($(this));
    });
    $('[data-property-name]').keyup(function (e) {
        IsSingleInputValid($(this));
    });
}

function ValidateUnique(objectName) {
    var isValid = true;
    var uniqueFormProperties = $(`.add-sub-item-form[data-object-name="${objectName}"]`)
        .find(`[data-object-name='${objectName}'][data-property-name][is-unique]`);
    var dataUniqueProperteis = [];
    $(`.single-sub-list[data-object-name='${objectName}']`)
        .each(function (index, container) {
            $(this)
                .find(`[data-object-name='${objectName}'][data-property-name][is-unique]`)
                .each(function (subIndex, input) {
                    dataUniqueProperteis.push(input);
                });
        });

    $(uniqueFormProperties)
        .each(function (index, prop) {
            var propertyName = $(prop).data('property-name');
            var value = $(prop).val();
            var exists = dataUniqueProperteis.some(function (element) {
                return $(element).data('property-name') === propertyName
                    && $(element).val() === value;
            });
            if (exists) {
                ShowSingleInputValidationCustomMessage(prop, 'آیتمی با این مقدار از قبل وجود دارد', false);
                isValid = false;
            }
            else
                ShowSingleInputValidationCustomMessage(prop, '', true);
        });
    return isValid;
}

function RemoveSubList(button) {
    var objectName = $(button).attr('data-object-name');
    $(button).parent().remove();
    ReIndexSubLists(objectName);
}
function ReIndexSubLists(objectName) {
    $(`.single-sub-list[data-object-name='${objectName}']`).each(function (index, container) {
        $(this)
            .find(`[data-object-name='${objectName}'][data-property-name]`)
            .each(function (subIndex, input) {
                var propertyName = $(input).data('property-name');
                $(input).attr('name', `${objectName}[${index}].${propertyName}`);
                $(input).removeAttr('id');
                // $(input).attr('disabled','true');
                if ($(input).is('select') || $(input).is('textarea')) {
                    var value = $(input).attr('value');
                    $(input).val(value).trigger('change');
                }
            });
        $(this)
            .find(`[data-valmsg-for]`)
            .each(function (subIndex, element) {
                var propertyName = $(element).data('validation-property-name');
                $(element).attr('data-valmsg-for', `${objectName}[${index}].${propertyName}`);
            });
    });
    InitialAmountTexts();
    InitialSubListValidation();
}
function AreInputsValid(selector) {
    var isValid = true;
    $(selector).each(function (index, element) {
        var singleElementValid = IsSingleInputValid(element);
        isValid = isValid && singleElementValid;
    });
    return isValid;
}

function HasAttr(element, attr) {
    var attr = $(element).attr(attr);
    var hasAttr = (attr !== undefined && attr !== false);
    return hasAttr;
}

function IsSingleInputValid(input) {
    var value = $(input).val();
    var isRequired = HasAttr(input, 'data-val-required');
    if (isRequired) {
        if (value === undefined || value === null || value === '') {
            ShowSingleInputValidationErrorMessage(input, 'data-val-required');
            return false;
        }
    }
    if (value !== undefined && value !== null && value !== '') {
        var minValue = $(input).attr('data-val-range-min');
        if (+value < +minValue) {
            ShowSingleInputValidationErrorMessage(input, 'data-val-range');
            return false;
        }
        var maxValue = $(input).attr('data-val-range-max');
        if (+value > +maxValue) {
            ShowSingleInputValidationErrorMessage(input, 'data-val-range');
            return false;
        }
    }
    ShowSingleInputValidationErrorMessage(input, '', true);
    return true;
}
function ShowSingleInputValidationErrorMessage(input, messageAttr, isValid = false) {
    var inputName = $(input).attr('name');
    var span = $(`span[data-valmsg-for='${inputName}']`);
    if (isValid) {
        $(span).text('');
        $(span).removeClass('field-validation-error');
        $(span).addClass('field-validation-valid');
        return;
    }
    var errorMessage = $(input).attr(messageAttr);
    $(span).text(errorMessage);
    $(span).addClass('field-validation-error');
    $(span).removeClass('field-validation-valid');
}
function ShowSingleInputValidationCustomMessage(input, message, isValid = false) {
    var inputName = $(input).attr('name');
    var span = $(`span[data-valmsg-for='${inputName}']`);
    if (isValid) {
        $(span).text('');
        $(span).removeClass('field-validation-error');
        $(span).addClass('field-validation-valid');
        return;
    }
    $(span).text(message);
    $(span).addClass('field-validation-error');
    $(span).removeClass('field-validation-valid');
}
$(document).ready(async function (e) {
    await SetProvinces();
    $('.province-select').change(async function (e) {
        LoadCities($(this));
    });
});

async function SetProvinces() {
    var result = await Get("/Province/ComboJson");
    var provinces = result.data;

    var defaultOption = `<option value="NULL">لطفا انتخاب کنید</option>`;
    $('.province-select').prepend(defaultOption);
    $(provinces).each(function (index, province) {
        var html = `<option value="${province.value}">${province.title}</option>`;
        $('.province-select').append(html);
    });
    $('.province-select').each(async function (index, element) {
        var dataIndex = $(element).data('index');
        var city = $(`.city-select[data-index="${dataIndex}"]`);
        var cityId = $(city).data('value');
        if (cityId > 0) {
            var cityInformationResult = await Get(`/City/DetailJson?id=${cityId}`);
            var cityInformation = cityInformationResult.data;
            if (cityInformation) {
                $(element).val(cityInformation.provinceId);
                await LoadCities(element);
                console.log(cityInformation.provinceId);
                $(`.city-select[data-index="${dataIndex}"]`).val(cityId).trigger('change');
                console.log($(`.city-select[data-index="${dataIndex}"]`).val());
                console.log($(`.city-select[data-index="${dataIndex}"]`));
            }
        }  
    });
}

async function LoadCities(provinceSelect) {
    var provinceId = $(provinceSelect).val();
    var dataIndex = $(provinceSelect).data("index");
    $('#province-id-hidden-input').val(provinceId);
    var result = await GetFormResult('filter-cities-hidden-form');
    var cities = result.data;
    if (!cities) {
        ShowErrorMessage('مشکلی در دریافت اطلاعات پیش آمد')
        return;
    }
    $(`.city-select[data-index="${dataIndex}"]`).empty();
    $(cities).each(function (index, city) {
        var html = `<option value="${city.value}">${city.title}</option>`;
        $(`.city-select[data-index="${dataIndex}"]`).append(html);
    });
}
$(document).ready(async function (e) {
    await ShowAdminDropDown();
    function highlightThumb() {
        var sliderId = this.$el.attr("id");
        var thumbs = $('.swiper-thumbs[data-swiper="#' + sliderId + '"]');
        // if thumbs for this slider exist
        if (thumbs.length > 0) {
            thumbs.find(".swiper-thumb-item.active").removeClass("active");
            thumbs
                .find(".swiper-thumb-item")
                .eq(this.realIndex)
                .addClass("active");
        }
    }

    $(document).on('click', '.quick-view-btn', async function (e) {
        var value = $(this).attr("data-value");
        open_waiting('#quickview-modal-container');
        try {
            const response = await $.ajax({
                type: 'GET',
                url: `/proudcts/detail/partial/${value}`
            });
            if (response.status === 'danger') {
                ShowErrorMessage("مشکلی در نمایش محصول پیش آمد!.");
                close_waiting('#quickview-modal-container');
                return;
            }
            close_waiting('#quickview-modal-container');
            
            $('#quickview-modal-container').html(response);
            quickViewSlider = new Swiper("#quickViewSlider", {
                mode: "horizontal",
                loop: true,
                on: {
                    slideChangeTransitionStart: highlightThumb,
                },
            });
            let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
            await UpdateBasketProductsBtns(basketProducts);
            let favoriteProducts = JSON.parse(localStorage.getItem('favoriteProducts')) || [];
            await UpdateFavoriteProductsBtns(favoriteProducts);
            $(document).on("click", ".swiper-thumb-item", function (e) {
                e.preventDefault();
                var swiperId = $(this).parents(".swiper-thumbs").data("swiper");
                $(swiperId)[0].swiper.slideToLoop($(this).index());
            });
            $('.selectpicker').selectpicker('refresh');
            AddSelectDefaultData();
        } catch (error) {
            console.error('Error getting product information:', error);
            close_waiting('#quickview-modal-container');
            return null;
        }
    });
    //start basket and favorites
    let favoriteProducts = JSON.parse(localStorage.getItem('favoriteProducts')) || [];
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    await UpdateBasket(basketProducts);
    await UpdateFavoriteProductsBtns(favoriteProducts);
    await UpdateBasketProductsBtns(basketProducts);
    $(document).on('submit', '#remove-out-of-stock-order-details-form', async function (e) {
        e.preventDefault();
        await GetFormResult("remove-out-of-stock-order-details-form");
    });
    await RemoveOutOfStockOrderDetails();

    $(document).on('click', '.update-basket-btn', async function (e) {
        await UpdateBasket(basketProducts);
        await UpdateBasketProductsBtns(basketProducts);
        ShowSuccessMessage('سبد خرید شما بروز است');
    });

    $(document).on('click', '.favorite-btn', function () {
        const $btn = $(this);
        const value = $btn.data('value');

        if (!$btn.hasClass('checked')) {
            // Add to favorites
            favoriteProducts.push(value);
        } else {
            // Remove from favorites
            favoriteProducts = favoriteProducts.filter(item => item !== value);
        }
        $('.favorite-btn[data-value="' + value + '"]').toggleClass('checked');
        // Save updated favoriteProducts array to local storage
        localStorage.setItem('favoriteProducts', JSON.stringify(favoriteProducts));
        UpdateFavoriteProductsBtns(favoriteProducts);
    });

    $(document).on('click', '.add-to-cart-btn', async function () {
        var productId = $(this).data('product-id');
        open_waiting((`.single-product-item[data-value=${productId}]`));
        const $btn = $(this);
        await AddToCartBtnClicked($btn);
        close_waiting((`.single-product-item[data-value=${productId}]`));
    });

    $(document).on('change', '.size-select', function (e) {
        HandleSizeSelectedChange($(this));
    });

    ShowActivePhoneNumberOtp();

    $(document).on('submit', '#register-login-form', async function (e) {
        e.preventDefault();
        $("#submit-phone-number-btn").addClass('disabled');
        var phoneNumber = $("#register-login-phonenumber-input").val();
        if (phoneNumber == null || phoneNumber.length < 11) {
            ShowErrorMessage('شماره تلفن وارد شده معتبر نمی باشد');
            $("#submit-phone-number-btn").removeClass('disabled');
            return;
        }
        var expireDate = GetPhoneNumberOtpExpireDate(phoneNumber);
        var currentDateTime = new Date();
        if (expireDate == null || expireDate < currentDateTime) {

            var result = await GetFormResult('register-login-form');
            if (result === null) {
                ShowErrorMessage('مشکلی در انجام عملیات پیش آمد');
                $("#submit-phone-number-btn").removeClass('disabled');
                return;
            }
            if (result.status === "Danger") {
                ShowErrorMessage(result.message);
                $("#submit-phone-number-btn").removeClass('disabled');
                return;
            }
            ShowSuccessMessage('کد ورود برای شما ارسال شد');
            HandleOtpCodeSent(phoneNumber);
            $("#submit-phone-number-btn").removeClass('disabled');
        }
        else {
            ShowInfoMessage('کد تایید از قبل برای شما ارسال شده است');
            $("#submit-phone-number-btn").removeClass('disabled');
        }
        $('.phonenumber-input').val(phoneNumber);
        $('#register-login-form').addClass('d-none');
        $('#login-form').removeClass('d-none');
        $("#submit-phone-number-btn").removeClass('disabled');
    });

    var previousPhoneNumberOtpCode = '';
    $(document).on('keyup', '#phonenumber-otp-code-input', function (e) {
        var value = $(this).val();
        var length = value.length;
        if (length === 4) {
            $('#login-form').submit();
        }
    });

    $(document).on('keyup', '#register-login-phonenumber-input', function (e) {
        var value = $(this).val();
        var length = value.length;
        if (length === 11) {
            $('#register-login-form').submit();
        }
    });

    $(document).on('submit', '#login-form', async function (e) {
        e.preventDefault();
        var otpCode = $('#phonenumber-otp-code-input').val();
        if (otpCode === previousPhoneNumberOtpCode) {
            ShowErrorMessage('کد وارد شده اشتباه است ');
            return;
        }
        previousPhoneNumberOtpCode = otpCode;
        var response = await GetFormResult('login-form');
        ShowMessageByResponse(response);
        if (response.status.toLowerCase() === 'success') {
            var redirectUrl = $('#redirect-url-input').val();
            var phoneNumber = $("#register-login-phonenumber-input").val();
            RemovePhoneNumberFromList(phoneNumber);
            await AddLocalBasketItemsToServer(basketProducts);
            window.location.href = redirectUrl;
            return;
        }
    });

    $(document).on('input', '.phonenumber-input', function () {
        if ($(this).val().length > 11) {
            $(this).val($(this).val().substring(0, 11));
        }
    });

    $(document).on('input', '#phonenumber-otp-code-input', function () {
        if ($(this).val().length > 4) {
            $(this).val($(this).val().substring(0, 4));
        }
    });

    $(document).on('click', '.authorize-required', function (e) {
        if (!IsUserAuthenticated()) {
            e.preventDefault();
            var href = $(this).attr('href');
            $('#redirect-url-input').val(href);
            $('.modal').modal('hide');
            $('#loginModal').modal('show');
            ShowInfoMessage('برای دیدن این صفحه لطفا به سایت ورود کنید');
        }
    });

    $(document).on('click', '#edit-phonenumber-btn', function (e) {
        $('#phonenumber-otp-code-input').val('');
        $('#register-login-form').removeClass('d-none');
        $('#login-form').addClass('d-none');
    });

    $(document).on('click', '#resend-code-btn', function (e) {
        if (!$(this).hasClass('disabled')) {
            $(this).addClass('disabled');
            $('#resend-otp-code-form').submit();
        }
    });

    $(document).on('submit', '#resend-otp-code-form', async function (e) {
        e.preventDefault();
        $("#resend-code-btn").addClass('disabled');
        var response = await GetFormResult('resend-otp-code-form');
        ShowMessageByResponse(response);
        if (response.status.toLowerCase() === "success") {
            var phoneNumber = $('#register-login-phonenumber-input').val();
            HandleOtpCodeSent(phoneNumber);
        }
        $("#resend-code-btn").removeClass('disabled');
    });

});
$(window).on('load', function () {
    $('.defer-stylesheet').each(function () {
        var src = $(this).data('src');
        AppendStyleSheet(src);
    });
});
async function RemoveOutOfStockOrderDetails() {
    if (IsUserAuthenticated())
        if (+$("#remove-out-of-stock-order-details").val() === +1)
            $('#remove-out-of-stock-order-details-form').submit();
}

async function ShowAdminDropDown() {
    var response = await Get("/User/IsAdmin");
    if (response.data === true) {
        var dropDownHtml =
            `<div class="dropdown ps-3 ms-0">
                            <a class="topbar-link"
                               href="/admin"
                               title="پنل ادمین">
                               پنل ادمین
                            </a>
            </div>`;
        $(dropDownHtml).insertBefore("#logout-nav-item")
    }
}

function ShowActivePhoneNumberOtp() {
    var firstValidItem = GetFirstValidPhoneNumberOtpCode();
    if (firstValidItem != null) {
        $('.phonenumber-input').val(firstValidItem.phoneNumber);
        $('#register-login-form').addClass('d-none');
        $('#login-form').removeClass('d-none');
        StartResendOtpTimer(new Date(firstValidItem.sentDateTime));
    }
}
function HandleOtpCodeSent(phoneNumber) {
    UpsertOtpCode(phoneNumber);
}

function GetPhoneNumberOtpExpireDate(phoneNumber) {
    var list = getList('otpCodeList');
    var item = list.filter(a => a.phoneNumber == phoneNumber)[0];
    if (!item) return null;
    var sentDateTime = new Date(item.sentDateTime);
    return sentDateTime;
}

function GetFirstValidPhoneNumberOtpCode() {
    var list = getList('otpCodeList');
    var currentDateTime = new Date();
    var result = null
    list.forEach(function (item) {
        var expireDateTime = new Date(item.sentDateTime);
        if (expireDateTime > currentDateTime) {
            result = item;
            return item;
        }
    });
    return result;
}

function UpsertOtpCode(phoneNumber) {
    var list = getList('otpCodeList');
    var currentDateTime = new Date().toISOString();
    currentDateTime = new Date(currentDateTime);
    currentDateTime.setMinutes(currentDateTime.getMinutes() + 2);

    var found = false;

    list.forEach(function (item) {
        if (item.phoneNumber === phoneNumber) {
            item.sentDateTime = currentDateTime;
            found = true;
        }
    });

    if (!found) {
        list.push({ phoneNumber: phoneNumber, sentDateTime: currentDateTime });
    }

    StartResendOtpTimer(new Date(currentDateTime));
    saveList(list, 'otpCodeList');
}
var timerInterval; // Declare the variable outside the function

function StartResendOtpTimer(stopDateTime) {
    if (timerInterval) {
        clearInterval(timerInterval); // Clear any existing interval
    }

    timerInterval = setInterval(function () {
        var currentDateTime = new Date();
        var timeRemaining = stopDateTime.getTime() - currentDateTime.getTime();
        if (timeRemaining <= 0) {
            clearInterval(timerInterval);
            $('#resend-code-btn span').text('ارسال مجدد کد');
            $('#resend-code-btn').removeAttr('disabled');
            $('#resend-code-btn').removeClass('disabled');
        } else {
            var minutes = Math.floor(timeRemaining / 60000);
            var seconds = Math.floor((timeRemaining % 60000) / 1000);
            $('#resend-code-btn').attr('disabled', '');
            $('#resend-code-btn').addClass('disabled');
            var remainingTime = (minutes < 10 ? '0' : '') + minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
            $('#resend-code-btn span').text(
                `تا ارسال مجدد کد ${remainingTime}`
            );
        }
    }, 1000);
}


function RemovePhoneNumberFromList(phoneNumber) {
    var list = getList('otpCodeList');
    list = list.filter(a => a.phoneNumber != phoneNumber);
    saveList(list, 'otpCodeList');
}

//start cart section

async function AddToCartBtnClicked($btn) {
    basketProducts = await UpsertToBasket($btn);
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}
function HandleSizeSelectedChange(element) {
    var productId = $(element).attr('data-product-id');
    var value = $(element).val();
    $('.size-select[data-product-id="' + productId + '"]').val(value);
    $('.add-to-cart-btn[data-product-id="' + productId + '"]:not(.minimal)').addClass('d-real-none');
    $('.add-to-cart-btn[data-value="' + value + '"]').removeClass('d-real-none');

    var selectedValue = $(element).val();
    var matchingOption = $(element).find("option").filter(function () {
        return $(this).val() === selectedValue;
    });
    ShowSingleSizePrices(matchingOption);
}

function ShowSingleSizePrices(element) {
    var price = $(element).data('price');
    if (!price) return;
    var discount = $(element).data('discount');
    var quantity = $(element).data('quantity-remaining');
    var finalPrice = price - (discount > 0 ? discount : 0);
    var html = quantity <= 0 ? `` :
        `<li class="list-inline-item h4 fw-light mb-0">
          ${$.ToAmount(finalPrice)}
         </li>
         ${(discount > 0 ?
            `
             <li class="list-inline-item text-muted fw-light">
                                    <del>${$.ToAmount(price)}</del>
             </li>` :
            ``)}`;

    $(`.size-price-section-container`)
        .html(html);
}

function SetSizeInBasketAsDefaultSize(productId) {
    var sizes = [];
    $(`.size-select[data-product-id='${productId}'] option`).each(function () {
        sizes.push($(this).val());
    });
    if (sizes.length === 0) return;
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];

    var firstSizeInBasket = $.grep(sizes, function (sizeId) {
        return $.grep(basketProducts, function (product) {
            return sizeId === product.id.toString();
        }).length > 0;
    })[0];
    if (firstSizeInBasket !== undefined && firstSizeInBasket !== null && firstSizeInBasket > +0) {
        $(`.size-select[data-product-id='${productId}']`).val(firstSizeInBasket).change();
        $(`.size-select[data-product-id='${productId}'] option[value="${firstSizeInBasket}"]`).prop('selected', true);
    }
}

// Event delegation for dynamically loaded content
$(document).on('change', '.size-select', function () {
    HandleSizeSelectedChange($(this));
});
//start server functions
var removeSuccessMessagesCount = 0;
async function UpsertToBasket($btn) {
    basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const value = $btn.data('value');
    var isMinimal = $btn.hasClass('minimal');
    var productId = $btn.data('product-id');
    if (!$btn.hasClass('checked')) {
        var maxCount = $btn.data('quantity-remaining');
        var preferedCount = $('#count').val();
        if (preferedCount == undefined || preferedCount <= 0)
            preferedCount = 1;
        var currentItemInBasket = basketProducts.filter(item => item.id == value);
        var currentItemCount = 0;
        if (currentItemInBasket.length > 0)
            currentItemCount = currentItemInBasket.count;
        var totalCount = +currentItemCount + +preferedCount;
        var productId = $btn.data('product-id')
        if (+totalCount > +maxCount) {
            ShowErrorMessage('تعداد درخواستی شما از تعداد موجود در انبار بیشتر است');
            return basketProducts;
        }
        if (IsUserAuthenticated()) {
            await AddToServerBasket(value, totalCount);
        }
        else {
            ShowSuccessMessage("محصول مورد نظر به سبد خرید اضافه شد");
            $('#sidebarCart').modal('show');
        }
        basketProducts.push({
            id: value,
            count: totalCount,
            productId: productId,
            createDate: new Date().toLocaleString(),
        });
    }
    else {
        if (!isMinimal) {
            basketProducts = await RemoveSingleProductFromBasket(value);
        }
        else {
            var products = basketProducts.filter(a => a.productId == productId);
            removeSuccessMessagesCount = 0;
            products.forEach(async function (item) {
                basketProducts = await RemoveSingleProductFromBasket(item.id);
            });
            removeSuccessMessagesCount = 0;
        }
    }
    return basketProducts;
}

async function RemoveSingleProductFromBasket(value) {
    var success = true;
    if (IsUserAuthenticated()) {
        success = await RemoveFromServerBasket(value);
    }
    basketProducts = basketProducts.filter(item => item.id !== value);
    if (success) {
        if (removeSuccessMessagesCount === 0) {
            ShowSuccessMessage("محصول مورد نظر از سبد خرید حذف شد");
            removeSuccessMessagesCount++;
        }
    }
    return basketProducts;
}

async function AddToServerBasket(id, count) {
    try {
        const result = await $.ajax({
            url: `/order/add-to-basket-json/${id}/${count}`,
            method: 'GET'
        });
        ShowMessageByResponse(result);
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            $('#sidebarCart').modal('show');
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error adding product to basket:', error);
        return await false;
    }
}

async function RemoveFromServerBasket(id) {
    try {
        const result = await $.ajax({
            url: `/order/remove-product-from-basket-json/${id}`,
            method: 'GET'
        });
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error while removing product from basket:', error);
        return await false;
    }
}

async function LoadFromServerBasket() {
    try {
        const result = await $.ajax({
            url: `/order/basket-json`,
            method: 'GET'
        });
        if (result.status === "Success") {

            var newBasketProductsArray = [];
            result.data.forEach(function (product) {
                newBasketProductsArray.push({
                    id: product.id,
                    count: product.count,
                    productId: product.productId,
                    createDate: product.createDate,
                });
            });
            localStorage.setItem('basketProducts', JSON.stringify(newBasketProductsArray));

            await ShowBasket(result.data);
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error adding product to basket:', error);
        return await false;
    }
}

async function DecrementFromServerBasket(id) {
    try {
        const result = await $.ajax({
            url: `/order/decrement-basket-json/${id}`,
            method: 'GET'
        });
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error decrementing product from basket:', error);
        return await false;
    }
}

async function ChangeCountFromServerBasket(id, count) {
    try {
        const result = await $.ajax({
            url: `/order/change-basket-count-json/${id}/${count}`,
            method: 'GET'
        });
        ShowMessageByResponse(result);
        if (result.status === "Success") {
            await UpdateBasketProductsBtns();
            await UpdateBasket();
            return await true;
        }
        return await false;
    } catch (error) {
        console.error('Error changing product count from basket:', error);
        return await false;
    }
}


//end server functions
function UpdateFavoriteProductsBtns(favoriteProducts) {
    favoriteProducts.forEach(value => {
        $('.favorite-btn[data-value="' + value + '"]').addClass('checked');
    });
    $('.favorite-products-count').text(favoriteProducts.length);
    $('.favorite-btn').each(function () {
        const $btn = $(this);
        var isCustomIcon = $btn.hasClass('has-custom-icon');
        if (isCustomIcon) {
            if ($btn.hasClass('checked')) {
                $btn.html(`<svg class="svg-icon svg-icon-heavy">
                          <use xlink:href="#heart-fill"> </use>
                        </svg>`);
            } else {
                $btn.html(`<svg class="svg-icon svg-icon-heavy">
                          <use xlink:href="#heart-1"> </use>
                        </svg>`);
            }
        }
        else {
            if ($btn.hasClass('checked')) {
                $btn.html(`<i class="far fa-heart me-2"></i>حذف از مورد علاقه ها`);
            } else {
                $btn.html(`<i class="far fa-heart me-2"></i>افزودن به موردعلاقه ها`);
            }
        }
    });
}

async function UpdateBasketProductsBtns(basketProducts) {
    if (IsUserAuthenticated()) {
        try {
            const result = await $.ajax({
                url: `/order/basket-json`,
                method: 'GET'
            });
            if (result.status === "Success") {
                basketProducts = result.data;
            }
        } catch (error) {
            console.error('Error adding product to basket:', error);
        }
    }
    $('.add-to-cart-btn').removeClass('checked');
    if (!basketProducts)
        basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    basketProducts.forEach(item => {
        $('.add-to-cart-btn[data-value="' + item.id + '"]').addClass('checked');
        $('.add-to-cart-btn.minimal[data-product-id="' + item.productId + '"]').addClass('checked');
    });
    $('.add-to-cart-btn').each(function () {
        const $btn = $(this);
        const isMinimal = $(this).hasClass('minimal');
        const quantity = $(this).data('quantity-remaining');
        if (quantity <= 0) {
            $btn.text('ناموجود');
            $btn.attr('disabled', 'true');
            $btn.addClass('default-cursor');
        }
        else {
            if (isMinimal) {
                if ($btn.hasClass('checked')) {
                    $btn.removeClass('text-dark');
                    $btn.addClass('text-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.text('حذف');
                } else {
                    $btn.addClass('text-dark');
                    $btn.removeClass('text-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.text('خرید');
                }
            }
            else {
                if ($btn.hasClass('checked')) {
                    $btn.removeClass('btn-dark');
                    $btn.addClass('btn-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.html(`
                    <i class="fa fa-shopping-cart me-2"></i>
                    حذف از سبد خرید`);
                } else {
                    $btn.addClass('btn-dark');
                    $btn.removeClass('btn-danger');
                    if (!$btn.hasClass('has-icon'))
                        $btn.html(`
                    <i class="fa fa-shopping-cart me-2"></i>
                    افزودن به سبد خرید`);
                }
            }
        }
    });
}

async function UpdateBasket(basketProducts) {
    if (IsUserAuthenticated()) {
        await LoadFromServerBasket();
        return;
    }
    await ShowBasket(basketProducts);
}

async function AddLocalBasketItemsToServer(basketProducts) {
    if (!basketProducts) return;
    for (var product of basketProducts) {
        await AddToServerBasket(product.id, product.count);
    }
}

async function ShowBasket(basketProducts) {
    $('#basket-items-content-container').empty();
    $('#cart-page-items-container').empty();
    if (basketProducts.length == 0 || basketProducts == null) {
        $('.empty-basket-container').removeClass('d-none');
        $('.basket-items-content-container').addClass('d-none');
        $('.basket-buttons-container').addClass('d-none');
        $('.basket-items-count-span').html('0');
        return;
    }
    $('.empty-basket-container').addClass('d-none');
    $('.basket-items-content-container').removeClass('d-none');
    $('.basket-items-count-span').html('0');
    let totalPrice = 0;
    let totalDiscount = 0;
    let productsCount = 0;
    basketProducts.sort(function (a, b) {
        return new Date(a.createDate) - new Date(b.createDate);
    });
    for (const item of basketProducts) {
        try {
            const result = await $.ajax({
                url: `/ProductSelectedOptionValue/Detail/${item.id}`,
                method: 'GET'
            });
            var product = result.data;
            if (product != null) {
                if (product.quantityInStock > 0) {
                    totalPrice += (+product.price * item.count);
                    if (+product.discountPrice > +0) {
                        totalDiscount += (+product.discountPrice * item.count);
                    }
                    +productsCount++;
                }
                product.productImageName = $.ConvertImageNameToWebP(product.productImageName);
                $('#cart-page-items-container').append(GetBasketPageItemHtml(product, item.count));
                var itemHtml = GetBasketItemHtml(product, item.count)
                $('#basket-items-content-container').append(itemHtml);
            }
        } catch (error) {
            console.error('Error fetching product:', error);
        }
    }
    $('.basket-items-count-span').html(productsCount);
    $('#basket-total-price-container').html(`${(totalDiscount > 0 ? `
                       <h5 class="mb-4">
                            مبلغ کل:
                            <s class="me-2 text-gray-500">
                            ${totalPrice.toLocaleString()}
                            </s>
                        </h5>` : ``)}
        <h5 class="mb-4">
                        قابل پرداخت:
                           <span class="float-right">
                        ${(totalPrice - totalDiscount).toLocaleString()}
                           </span>
                        </h5>`);
    $('.basket-total-price').text(totalPrice.toLocaleString());
    $('.basket-final-price').text((totalPrice - totalDiscount).toLocaleString());
}

function GetBasketItemHtml(product, count) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    product.productTitle = `${product.productTitle} ${product.productOptionTitle} ${product.productOptionValueTitle}`;
    return `
    <div class="navbar-cart-product">
                        <div class="d-flex align-items-center">
                            <a href="${url}" title="${product.productTitle}">
                                <img class="img-fluid navbar-cart-product-image"
                                     src="${baseImagePath + product.productImageName}"
                                     alt="${product.productTitle}" title="${product.productTitle}" />
                            </a>
                            <div class="w-100">
                                <a class="navbar-cart-product-remove" 
                                   onclick="RemoveSingleProductInBasket(${product.id})">
                                    <svg class="svg-icon sidebar-cart-icon">
                                        <use xlink:href="#close-1"> </use>
                                    </svg>
                                </a>
                                ${(product.quantityInStock > 0 ?
            GetBasketItemPriceSectionHtml(product, count) :
            GetBasketItemNoQuantitySectionHtml(product))}
                            </div>
                        </div>
                    </div>`;
}

function GetBasketItemPriceSectionHtml(product, count) {
    var url = GetProductUrl(product);
    var currentPrice = product.price - (product.discountPrice > 0 ? product.discountPrice : 0);
    return `
                   <div class="ps-3">
                        <a class="navbar-cart-product-link text-dark link-animated"
                           href="${url}" title="${product.productTitle}"
                           >${product.productTitle}</a>
                        <small class="d-block text-muted">تعداد: ${count} </small>
                        ${(product.discountPrice > 0 ?
            `<s class="me-2 text-gray-500">${product.price.toLocaleString()}</s>` : ``)}
                        <strong class="d-block text-sm">${currentPrice.toLocaleString()}</strong>
                    </div>`;
}

function GetBasketItemNoQuantitySectionHtml(product) {
    var url = GetProductUrl(product);
    return `
                   <div class="ps-3">
                        <a class="navbar-cart-product-link text-dark link-animated"
                           href="${url}" title="${product.productTitle}"
                           >${product.productTitle}</a>
                        <small class="d-block text-danger">ناموجود</small>
                    </div>`;
}

function GetBasketPageInStockItemHtml(product, count) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    var finalPrice = product.price;
    if (product.discountPrice > 0)
        finalPrice -= +product.discountPrice;
    return `
    <div class="cart-item">
        <div class="row d-flex align-items-center text-start text-md-center">
            <div class="col-12 col-md-5">
                <a class="cart-remove close mt-3 d-md-none" 
                href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                <i class="fa fa-times"></i>
                </a>
                <div class="d-flex align-items-center">
                    <a href="${url}" title="${product.productTitle}">
                    <img class="cart-item-img"
                    src="${(baseImagePath + product.productImageName)}" 
                    alt="${product.productTitle}">
                    </a>
                    <div class="cart-title text-start">
                        <a class="text-dark link-animated" 
                        href="${url}" title="${product.productTitle}">
                        <strong>${product.productTitle}</strong>
                        </a>
                        <br>
                        <span class="text-muted text-sm">
                        ${product.productOptionTitle}: 
                        ${product.productOptionValueTitle}</span>
                       </div>
                </div>
            </div>
            <div class="col-12 col-md-7 mt-4 mt-md-0">
                <div class="row align-items-center">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-6 d-md-none text-muted">قیمت هر عدد</div>
                            <div class="col-6 col-md-12 text-end
                            text-md-center">${finalPrice.toLocaleString()}
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row align-items-center">
                            <div class="d-md-none col-7 
                            col-sm-9 text-muted">تعداد</div>
                            <div class="col-5 col-sm-3 col-md-12">
                                <div class="d-flex align-items-center">
                                    <button class="btn btn-items 
                                    btn-items-decrease" 
                                    onclick="DecrementSingleProductInBasket(${product.id})"
                                    >-</button>
                                    <input class="form-control text-center border-0 
                                    border-md input-items" type="text" value="${count}"
                                    onchange="ChangeSingleProductCountInBasket(this)"
                                    data-value="${product.id}"
                                    data-max-value="${product.quantityInStock}">
                                    <button class="btn btn-items btn-items-increase"
                                    onclick="IncrementSingleProductInBasket(${product.id},${+count + +1},${product.quantityInStock})"
                                    >+</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-6 d-md-none text-muted">قیمت کل</div>
                            <div class="col-6 col-md-12 text-end text-md-center">
                            ${(finalPrice * count).toLocaleString()}
                            </div>
                        </div>
                    </div>
                    <div class="col-2 d-none d-md-block text-center">
                        <a class="cart-remove text-muted" href="#"
                        onclick="RemoveSingleProductInBasket(${product.id})">
                            <svg class="svg-icon w-2rem h-2rem svg-icon-light">
                                <use xlink:href="#close-1"> </use>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>`;
}

function GetBasketPageOutOfStockItemHtml(product) {
    var url = GetProductUrl(product);
    var baseImagePath = $('#product-base-img-path').val();
    return `
    <div class="cart-item">
        <div class="row d-flex align-items-center text-start text-md-center">
            <div class="col-12 col-md-5">
                <a class="cart-remove close mt-3 d-md-none"
                href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                <i class="fa fa-times"></i>
                </a>
                <div class="d-flex align-items-center">
                    <a href="${url}" title="${product.productTitle}">
                    <img class="cart-item-img"
                    src="${(baseImagePath + product.productImageName)}" 
                    alt="${product.productTitle}">
                    </a>
                    <div class="cart-title text-start">
                        <a class="text-dark link-animated" 
                        href="${url}" title="${product.productTitle}">
                        <strong>${product.productTitle}</strong>
                        </a>
                        <br>
                        <span class="text-muted text-sm">
                        ${product.productOptionTitle}: 
                        ${product.productOptionValueTitle}</span>
                       </div>
                </div>
            </div>
            <div class="col-12 col-md-7 mt-4 mt-md-0">
                <div class="row align-items-center">
                    <div class="col-md-10 text-end
                            text-md-center text-danger">
                        ناموجود
                    </div>


                    <div class="col-2 d-none d-md-block text-center">
                        <a class="cart-remove text-muted" href="#" onclick="RemoveSingleProductInBasket(${product.id})">
                            <svg class="svg-icon w-2rem h-2rem svg-icon-light">
                                <use xlink:href="#close-1"> </use>
                            </svg>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>`;
}

function GetBasketPageItemHtml(product, count) {
    return product.quantityInStock > 0 ? GetBasketPageInStockItemHtml(product, count) :
        GetBasketPageOutOfStockItemHtml(product);
}

function GetProductUrl(product) {
    return `/products/detail/${product.productId}/${ToUrl(product.productTitle)}`;
}

function GetProductListDtoUrl(product) {
    return `/products/detail/${product.id}/${ToUrl(product.title)}`;
}

async function RemoveSingleProductInBasket(id) {
    if (IsUserAuthenticated()) {
        await RemoveFromServerBasket(id);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.id != id);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function IncrementSingleProductInBasket(id, count, quantityInStock) {
    if (+count > +quantityInStock) {
        ShowErrorMessage("تعداد درخواستی از تعداد موجود بیشتر است");
        return;
    }
    if (IsUserAuthenticated()) {
        await AddToServerBasket(id, count);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count++;
    } else {
        // Product does not exist, add it with count 1
        basketProducts.push({ id: id, count: 1 });
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function DecrementSingleProductInBasket(id) {
    if (IsUserAuthenticated()) {
        await DecrementFromServerBasket(id);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count--;
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}

async function ChangeSingleProductCountInBasket(element) {
    var id = $(element).data('value');
    var count = $(element).val();
    var maxCount = $(element).data('max-value');
    if (+count > +maxCount) {
        ShowErrorMessage("تعداد درخواستی از موجودی انبار بیشتر است");
        return;
    }
    if (IsUserAuthenticated()) {
        await ChangeCountFromServerBasket(id, count);
    }
    let basketProducts = JSON.parse(localStorage.getItem('basketProducts')) || [];
    const existingProduct = basketProducts.find(item => item.id === id);

    if (existingProduct) {
        // Product already exists, increase the count
        existingProduct.count = +count;
    }

    // Remove the product if count is 0
    basketProducts = basketProducts.filter(item => item.count > 0);

    // Save updated basketProducts to localStorage
    localStorage.setItem('basketProducts', JSON.stringify(basketProducts));

    // Update UI (you can implement your own function here)
    await UpdateBasketProductsBtns(basketProducts);
    await UpdateBasket(basketProducts);
}
function AppendStyleSheet(url) {
    var link = document.createElement('link');
    link.rel = 'stylesheet';
    link.href = url;
    document.head.appendChild(link);
  }
"use strict";

$(function () {

    $(".btn-items-decrease").on("click", function () {
        var input = $(this).siblings(".input-items");
        if (parseInt(input.val(), 10) >= 1) {
            input.val(parseInt(input.val(), 10) - 1);
        }
    });

    $(".btn-items-increase").on("click", function () {
        var input = $(this).siblings(".input-items");
        input.val(parseInt(input.val(), 10) + 1);
    });

    function setVhVar() {
        var vh = window.innerHeight * 0.01;
        // Then we set the value in the --vh custom property to the root of the document
        document.documentElement.style.setProperty("--vh", vh + "px");
    }

    setVhVar();

    window.addEventListener("resize", setVhVar);

    var navbar = $(".navbar"),
        navbarCollapse = $(".navbar-collapse");

    $(".navbar.bg-transparent .navbar-collapse").on(
        "show.bs.collapse",
        function () {
            makeNavbarWhite();
        }
    );

    $(".navbar.bg-transparent .navbar-collapse").on(
        "hidden.bs.collapse",
        function () {
            makeNavbarTransparent();
        }
    );

    function makeNavbarWhite() {
        navbar.addClass("was-transparent");
        if (navbar.hasClass("navbar-dark")) {
            navbar.addClass("was-navbar-dark");
            navbar.removeClass("navbar-dark");
        } else {
            navbar.addClass("was-navbar-light");
        }

        navbar.removeClass("bg-transparent");

        navbar.addClass("bg-white");
        navbar.addClass("navbar-light");
    }

    function makeNavbarTransparent() {
        navbar.removeClass("bg-white");
        navbar.removeClass("navbar-light");
        navbar.removeClass("was-transparent");

        navbar.addClass("bg-transparent");
        if (navbar.hasClass("was-navbar-dark")) {
            navbar.addClass("navbar-dark");
        } else {
            navbar.addClass("navbar-light");
        }
    }

    // ------------------------------------------------------- //
    //   Bootstrap tooltips
    // ------------------------------------------------------- //

    $('[data-bs-toggle="tooltip"]').tooltip();

    $(".detail-option-btn-label").on("click", function () {
        var button = $(this);

        button
            .parents(".detail-option")
            .find(".detail-option-btn-label")
            .removeClass("active");

        button.toggleClass("active");
    });
});

