// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var cropWithExtension = function (value, maxChars, trailingCharCount) {
    var result = value;

    if (value.length > maxChars) {
        var front = value.substr(0, value.length - (maxChars - trailingCharCount - 3));
        var mid = "...";
        var end = value.substr(-trailingCharCount);

        result = front + mid + end;
    }

    return result;
}