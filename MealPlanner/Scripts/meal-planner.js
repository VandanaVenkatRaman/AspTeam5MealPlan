$(function () {
    $('input:radio[name=weight]').change(function () {
        console.log("weight input element changed.");
    });

    $('input:radio[name=gender]').change(function () {
        console.log("gender input element changed.");
    });

});