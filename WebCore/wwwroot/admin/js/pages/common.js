
function showSuccessNotification(message) {

    $.notify({
        icon: "check_circle",
        message: message

    }, {
            type: 'info',
            timer: 1000,
            placement: {
                from: 'bottom',
                align: 'right'
            }
        });
}

function showErrorNotification(message) {

    $.notify({
        icon: "error",
        message: message

    }, {
            type: 'danger',
            timer: 1000,
            placement: {
                from: 'bottom',
                align: 'right'
            }
        });
}

function showWarningMessage(message) {

    $.notify({
        icon: "warning",
        message: message

    }, {
            type: 'warning',
            timer: 1000,
            placement: {
                from: 'bottom',
                align: 'right'
            }
        });
}

(function ($) {
    $.fn.navigate = function (url, object, success, fail) {
        $.get(url, object).done(function (response) {
            $(this).html(response);
            if (success) {
                success(response);
            }
        }).fail(function (response) {
            if (fail) {
                fail(response);
            }
        });
    };
})(jQuery);

$('body').delegate('form[data-noreload="true"]', 'submit', function (e) {
    e.preventDefault();
    var method = $(this).attr('method');
    var action = $(this).attr('action');
    var onSubmitDone = $(this).attr('data-on-submit-done');
    var onSubmitFail = $(this).attr('data-on-submit-fail');
    if (!method) {
        method = 'get';
    }
    if (method === 'get') {
        var data = $(this).serialize();
        $.get(action, data).done(function (response) {
            if (onSubmitDone) {
                window[onSubmitDone](response);
            }
        }).fail(function (response) {
            if (onSubmitFail) {
                window[onSubmitFail](response);
            }
        });
    }
    else {
        var formData = new FormData(this);

        $.ajax({
            url: action,
            data: formData,
            processData: false,
            contentType: false,
            type: method,
            success: function (response) {
                if (onSubmitDone) {
                    window[onSubmitDone](response);
                }
            },
            error: function (response) {
                if (onSubmitFail) {
                    window[onSubmitFail](response);
                }
            }
        });
    }
})

$('.select2-with-search').select2();

function changeLanguage(langCode) {
    $.get('/Resource/ChangeLanguage?langCode=' + langCode).done(function () {
        location.reload();
    });
}