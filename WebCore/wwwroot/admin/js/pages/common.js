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
    $(parent).find('.select2-with-search').select2({ width: "100%" }).on('change.select2', function (e) {
        $(e.target).focusout();
    });
    $(parent).find('.select2').select2({ width: "100%" }).on('change.select2', function (e) {
        $(e.target).focusout();
    });
}
resetSelect2($('body'));


function changeLanguage(langCode) {
    $.get('/Resource/ChangeLanguage?langCode=' + langCode).done(function () {
        location.reload();
    });
}

function runValidator(element) {
    var methods = $.validator.methods;
    $.each(methods, function (key, funct) {
        if ($(element).attr('data-val-' + key)) {
            funct.call($.data($(element).closest('form')[0], "validator"), element.value, element);
        }
    });
}

// create datepicker

function createDatepicker(formElement, dateSelector, timeSelector, fullDateSelector) {
    if (!formElement) {
        return;
    }

    // bootstrap datepicker for date
    if (dateSelector) {
        $(formElement).find(dateSelector).bootstrapMaterialDatePicker({
            time: false,
            nowButton: true,
            format: 'DD/MM/YYYY',
            lang: 'vi-VN',
            clearText: "Xóa giá trị",
            nowText: 'Hiện tại',
            cancelText: 'Hủy bỏ',
            clearButton: true
        }).on('change', function (e, date) {
            runValidator(e.target);
            $(e.target).focusout();
        });
    }

    // bootstrap datepicker for time
    if (timeSelector) {
        $(formElement).find(timeSelector).bootstrapMaterialDatePicker({
            date: false,
            nowButton: true,
            nowText: 'Hiện tại',
            format: 'HH:mm', lang: 'vi-VN', clearText: "Xóa giá trị", cancelText: 'Hủy bỏ', clearButton: true
        }).on('change', function (e, date) {
            runValidator(e.target);
        });
    }

    // bootstrap datepicker for full date
    if (timeSelector) {
        $(formElement).find(fullDateSelector).bootstrapMaterialDatePicker({
            format: 'DD/MM/YYYY HH:mm',
            nowButton: true,
            nowText: 'Hiện tại',
            lang: 'vi-VN', clearText: "Xóa giá trị", cancelText: 'Hủy bỏ', clearButton: true
        }).on('change', function (e, date) {
            runValidator(e.target);
        });
    }
}

// create texteditor

function createTextEditor(selector) {
    tinymce.init({
        selector: selector,
        setup: function (editor) {
            editor.on('change', function (e) {
                editor.save();
                $(e.originalEvent.element).focusout();
            });
        }
    });

    $.validator.setDefaults({
        ignore: []
    });
}

function destroyTextEditor(selector) {
    tinymce.EditorManager.remove(selector);
}

// create money

function createMoney(selector) {
    if ($(selector).length === 0) {
        return;
    }
    var cleave = new Cleave(selector, {
        numeral: true,
        numeralThousandsGroupStyle: 'thousand'
    });
}