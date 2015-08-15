/// <autosync enabled="true" />
/// <reference path="modernizr-2.6.2.js" />
/// <reference path="jquery.min.js" />
/// <reference path="bootstrap.js" />
/// <reference path="respond.js" />
/// <reference path="jquery.lazyload.js" />

$(function () {
    $("img.lazy").lazyload({
        effect: "fadeIn",
        event: "scrollstop"
    });

    $(".upload-btn").change(function () {
        console.log($(this).val());
        $(".upload-text").val($(this).val());

    });

});