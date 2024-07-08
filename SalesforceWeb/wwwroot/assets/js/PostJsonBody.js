<script>
$(document).ready(function () {
    $('#postPractitionerForm').submit(function (event) {
        event.preventDefault(); // Prevent the form from submitting normally

        // Get JSON data from textarea
        var jsonBody = $('#jsonEditor').val();

        // Make AJAX POST request
        $.ajax({
            url: $(this).attr('action'), // Get action URL from form
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(jsonBody), // Send JSON data directly
            success: function (response) {
                // Handle success response if needed
                console.log(response);
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error('Error:', error);
            }
        });
    });
    
});

$(document).ready(function () {
$('#postOrganizationForm').submit(function (event) {
    event.preventDefault(); // Prevent the form from submitting normally

    // Get JSON data from textarea
    var jsonBody = $('#jsonEditor').val();

    // Make AJAX POST request
    $.ajax({
        url: $(this).attr('action'), // Get action URL from form
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(jsonBody), // Send JSON data directly
        success: function (response) {
            // Handle success response if needed
            console.log(response);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.error('Error:', error);
        }
    });
});

});
</script>