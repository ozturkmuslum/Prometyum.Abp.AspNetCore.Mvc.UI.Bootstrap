$('.pro-autocomplete').each(function (index, el) {
    var $el = $(el);

    var defaultValue = $el.data("default-value");
    var onSelectedChanged = $el.data("on-selected-changed");

    $el.autoComplete({
        resolverSettings: {
            url: $el.data("source"),
            queryKey: 'q'
        },
        minLength: parseInt($el.data("min-length")),
        //defaultValue:3,
        //defaultText:"Docker",
        //placeholder:"type to search..." 
        noResultsText: $el.data("no-results-text")
    });

    $el.on('change', function (e, item) {
        // console.log('change');
        autoCompleteValueChanged($el, onSelectedChanged, item.value, item.text);
    });

    $el.on('autocomplete.select',
        function (evt, item) {
            autoCompleteValueChanged($el, onSelectedChanged, item.value, item.text);
        });

    $el.on('autocomplete.freevalue',
        function (evt, value) {
            autoCompleteValueChanged($el, onSelectedChanged, defaultValue, value);
        });

    $el.on('focus', function (e) {
        setTimeout(function () { e.currentTarget.select(); }, 50); //select all text in any field on focus for easy re-entry. Delay sightly to allow focus to "stick" before selecting.
    });
});

function autoCompleteValueChanged($el, onSelectedChanged, value, text) {

    $($el.data("hidden-unique-class")).val(value);

    if (onSelectedChanged) {
        setTimeout(onSelectedChanged + "('" + value + "','" + text + "')", 5);
    };
}

//// user then clicks on some button and we need to change that default value
//$('.btnChangeAutoSelect').on('click',
//    function() {
//        var e = $(this);
//        $('.changeAutoSelect').autoComplete('set', { value: e.data('value'), text: e.data('text') });
//    });

//// clear current value
//$('.btnClearAutoSelect').on('click',
//    function() {
//        var e = $(this);
//        // $('.changeAutoSelect').autoComplete('set', null);
//        $('.changeAutoSelect').autoComplete('clear');

//    });