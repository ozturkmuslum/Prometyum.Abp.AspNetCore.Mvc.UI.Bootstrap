var prometyum = prometyum || {};

prometyum.utils = prometyum.utils || {};

prometyum.utils.readQueryString = function (name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

prometyum.utils.clearConsole = function () {
    // tslint:disable-next-line: variable-name
    const _console = console;
    // tslint:disable-next-line: no-string-literal
    let consoleAPI = console['API'];

    if (typeof _console._commandLineAPI !== 'undefined') {                // Chrome
        consoleAPI = _console._commandLineAPI;

    } else if (typeof _console._inspectorCommandLineAPI !== 'undefined') { // Safari
        consoleAPI = _console._inspectorCommandLineAPI;

    } else if (typeof _console.clear !== 'undefined') {                    // rest
        consoleAPI = _console;
    }

    consoleAPI.clear();
}