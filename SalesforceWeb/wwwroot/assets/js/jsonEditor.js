document.addEventListener("DOMContentLoaded", function () {
    // Find all elements with class 'jsonEditor'
    var textareas = document.getElementsByClassName("jsonEditor");

    // Loop through each textarea with the class 'jsonEditor'
    Array.prototype.forEach.call(textareas, function (textarea) {
        // Initialize CodeMirror for each textarea
        var editor = CodeMirror.fromTextArea(textarea, {
            mode: { name: "javascript", json: true },
            theme: "material",
            lineNumbers: true,
            autoCloseBrackets: true,
            matchBrackets: true
        });
    });
});