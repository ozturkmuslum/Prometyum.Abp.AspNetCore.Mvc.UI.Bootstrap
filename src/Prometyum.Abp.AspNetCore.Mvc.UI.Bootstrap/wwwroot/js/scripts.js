alert("auto complate loaded");

$('.pro-autocomplete').each(function(index, el) {

    var $el = $(el);

    $el.autoComplete({
        resolverSettings: {
            url: $el.data("url"),
            queryKey: 'q'
        },
        minLength: 2,
        //defaultValue:3,
        //defaultText:"Dockker",
        //noResultsText:"Nothing to see here.",
        //placeholder:"type to search..." 
    });

    $el.on('change',
        function(e) {
            console.log('change');
        });

            
    $el.on('autocomplete.select',
        function(evt, item) {
            $($el.data("hidden-id")).val(item.value);

        });

    $el.on('autocomplete.freevalue',
        function(evt, value) {
            $($el.data("hidden-id")).val("0");
        });

});

// user then clicks on some button and we need to change that default value
$('.btnChangeAutoSelect').on('click',
    function() {
        var e = $(this);
        $('.changeAutoSelect').autoComplete('set', { value: e.data('value'), text: e.data('text') });
    });

// clear current value
$('.btnClearAutoSelect').on('click',
    function() {
        var e = $(this);
        // $('.changeAutoSelect').autoComplete('set', null);
        $('.changeAutoSelect').autoComplete('clear');

    });