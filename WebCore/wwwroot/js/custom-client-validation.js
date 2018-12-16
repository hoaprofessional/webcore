//-- CUSTOM CLIENT VALIDATION

// data-val-numberbetween
$.validator.addMethod("numberbetween",
    function (value, element, parameters) {
        var from = parseInt($(element).attr('data-val-numberbetween-from'));
        var to = parseInt($(element).attr('data-val-numberbetween-to'));
        return value >= from && value <= to;
    }
);

$.validator.unobtrusive.adapters.add("numberbetween", [], function (options) {
    options.rules.numberbetween = {};
    options.messages["numberbetween"] = options.message;
});

// data-val-moneyformat
$.validator.addMethod("moneyformat",
    function (value, element, parameters) {
        $(element).removeAttr('data-val-number');
        return parseInt(value.replace(/,/g, '')) >= 0;
    }
);

$.validator.unobtrusive.adapters.add("moneyformat", [], function (options) {
    options.rules.moneyformat = {};
    options.messages["moneyformat"] = options.message;
    $(options.element).removeAttr('data-val-number');
});

// data-val-datelessthanorequalwattr
$.validator.addMethod("datelessthanorequalwattr",

    function (value, element, parameters) {
        var comparerName = $(element).attr('data-val-datelessthanorequalwattr-property-compare');
        var comparer = $(element).closest('form').find('input[name=' + comparerName + ']');
        if (value) {
            comparer.bootstrapMaterialDatePicker('setMinDate', value);
        }
        else {
            comparer.bootstrapMaterialDatePicker('setMinDate', null);
        }

        if (!comparer.val()) {
            return true;
        }
        return moment(value, "MM-DD-YYYY") <= moment(comparer.val(), "MM-DD-YYYY");
    }
);

$.validator.unobtrusive.adapters.add("datelessthanorequalwattr", [], function (options) {
    options.rules.datelessthanorequalwattr = {};
    options.messages["datelessthanorequalwattr"] = options.message;
});

// data-val-datemorethanorequalwattr
$.validator.addMethod("datemorethanorequalwattr",
    function (value, element, parameters) {
        var comparerName = $(element).attr('data-val-datemorethanorequalwattr-property-compare');
        var comparer = $(element).closest('form').find('input[name=' + comparerName + ']');

        if (value) {
            comparer.bootstrapMaterialDatePicker('setMaxDate', value);
        }
        else {
            comparer.bootstrapMaterialDatePicker('setMaxDate', null);
        }

        if (!comparer.val()) {
            return true;
        }
        return moment(value, "MM-DD-YYYY") >= moment(comparer.val(), "MM-DD-YYYY");
    }
);

$.validator.unobtrusive.adapters.add("datemorethanorequalwattr", [], function (options) {
    options.rules.datemorethanorequalwattr = {};
    options.messages["datemorethanorequalwattr"] = options.message;
});

// data-val-lessthantoday
$.validator.addMethod("lessthantoday",
    function (value, element, parameters) {
        if (value) {
            $(element).bootstrapMaterialDatePicker('setMaxDate', moment());
        }
        else {
            $(element).bootstrapMaterialDatePicker('setMaxDate', null);
        }
        return moment(value, "DD-MM-YYYY") < moment();
    }
);

$.validator.unobtrusive.adapters.add("lessthantoday", [], function (options) {
    $(options.element).bootstrapMaterialDatePicker('setMaxDate', moment().day(-1));
    options.rules.lessthantoday = {};
    options.messages["lessthantoday"] = options.message;
});

// data-val-greaterthantoday
$.validator.addMethod("greaterthantoday",
    function (value, element, parameters) {
        return moment(value, "DD-MM-YYYY") > moment();
    }
);

$.validator.unobtrusive.adapters.add("greaterthantoday", [], function (options) {
    $(options.element).bootstrapMaterialDatePicker('setMinDate', moment().day(1));
    options.rules.lessthantoday = {};
    options.messages["greaterthantoday"] = options.message;
});