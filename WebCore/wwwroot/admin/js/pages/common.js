
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
});

var confimation = function (title, content, confirmButtonText, cancelButtonText, onConfirm, onCancel) {
    var confirmArea = $('#confirmModal');
    if (confirmArea.length === 0) {
        var tag = '<div id="confirmModal"></div>';
        confirmArea = $(tag);
        $('body').append(confirmArea);

    }
    var html = `<div class="modal fade" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">${title}</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <p>${content}</p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" data-dismiss="modal" class="btn btn-primary confirm">${confirmButtonText}</button>
                                    <button type="button" class="btn btn-secondary cancel" data-dismiss="modal">${cancelButtonText}</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    `;
    confirmArea.html(html);
    if (onConfirm) {
        confirmArea.find('>.modal .confirm').click(function (e) {
            onConfirm(e);
        });
    }
    if (onCancel) {
        confirmArea.find('>.modal .cancel').click(function (e) {
            onCancel(e);
        });
    }
    confirmArea.find('>.modal').modal();
};



function resetSelect2(parent) {
    $(parent).find('.select2-with-search').select2({ width: "100%" });
    $(parent).find('.select2').select2({ width: "100%" });
}
resetSelect2($('body'));


function changeLanguage(langCode) {
    $.get('/Resource/ChangeLanguage?langCode=' + langCode).done(function () {
        location.reload();
    });
}