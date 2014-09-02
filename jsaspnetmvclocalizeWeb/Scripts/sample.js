'use strict';
if (!Sample)
    var Sample = Sample || {};
Sample.Global = Sample.Global || {};
Sample.Global.Resources = null;

Sample.Global.LoadResources = function () {
    var locLang = (navigator.language) ? navigator.language : navigator.userLanguage;//here you can read the language query string parameter instead if you want
    var url = "/api/Resources/" + locLang + "/MyResource/";
    $.getJSON(url, function (data) {
        Sample.Global.Resources = data;//here you can add some premise pattern if you need some data to be available right after loading (wait for it)
    });
}

Sample.Global.LoadResources();

$(function () {
    //rest of you methods and objects for your solution...
});