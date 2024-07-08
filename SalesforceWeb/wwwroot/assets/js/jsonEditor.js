document.addEventListener("DOMContentLoaded", function () {
	var editor = CodeMirror.fromTextArea(document.getElementById("jsonEditor"), {
		mode: { name: "javascript", json: true },
		theme: "material",
		lineNumbers: true,
		autoCloseBrackets: true,
		matchBrackets: true
	});
});