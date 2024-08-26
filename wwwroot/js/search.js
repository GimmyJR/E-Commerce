$(document).ready(function() {
    $('#searchInput').on('keyup', function() {
        let query = $(this).val();

        if (query.length > 0) {
            $.ajax({
                url: '/Products/Search', // Adjust URL as needed
                type: 'GET',
                data: { query: query },
                success: function(data) {
                    $('#searchSuggestions').html(data).show();
                },
                error: function() {
                    $('#searchSuggestions').html('<p class="dropdown-item">An error occurred</p>').show();
                }
            });
        } else {
            $('#searchSuggestions').hide();
        }
    });

    // Hide the dropdown when clicking outside of it
    $(document).click(function(event) { 
        if (!$(event.target).closest('#searchSuggestions').length && !$(event.target).is('#searchInput')) {
            $('#searchSuggestions').hide();
        }
    });
});
