
window.xhr = {
    callService: function (callback, controller, action, parameters) {
        $.get('/' + controller + '/' + action, parameters, callback, 'json');
    }
};

