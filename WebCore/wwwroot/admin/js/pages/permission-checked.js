function checkCheckedParent(currentChecked) {
    var parentCheckbox = $(currentChecked).parent().closest('ul').closest('li').find('>.checkbox.checkbox-info>input[type=checkbox]');

    if (parentCheckbox.length === 0) {
        return;
    }

    var childsChecked = parentCheckbox.closest('li').find('>ul>li.treeview-item>.checkbox.checkbox-info>input[type=checkbox]:checked');

    // if all children is checked
    if (childsChecked.length > 0) {
        parentCheckbox.prop('checked', true);
    }

    checkCheckedParent(parentCheckbox);
}

function getAllPermissionChecked() {
    return $.map($('.checkbox.checkbox-info>input[type=checkbox]:not(#chk-rootkey):checked'), function (x) { return $(x).val().trim(); });
}

function createPermissionCheck(element) {
    $(element).find('.checkbox.checkbox-info>input[type=checkbox][data-checked=True]').each(function () {
        this.checked = true;
        checkCheckedParent(this);
    });

    $(element).find('.checkbox.checkbox-info>input[type=checkbox]').change(function () {
        var allCheckboxChildrens = $(this).closest('li').find('ul .checkbox.checkbox-info>input[type=checkbox]');
        if (this.checked) {
            checkCheckedParent(this);
            allCheckboxChildrens.prop('checked', true);
        }
        else {
            checkCheckedParent(this);
            allCheckboxChildrens.prop('checked', false);
        }
    });
}


function createRoleCheck(roleElement, permissionElement) {
    $(roleElement).find('>div>input[type=checkbox]:checked').each(function () {
        checkRole(this, permissionElement);
    });

    $(roleElement).find('>div>input[type=checkbox]').change(function () {
        if (this.checked) {
            checkRole(this, permissionElement);
        }
        else {
            unCheckRole(this, permissionElement);
            $(roleElement).find('>div>input[type=checkbox]:checked').each(function () {
                checkRole(this, permissionElement);
            });
        }
    });
}

function checkRole(checkRoleElement, permissionElement) {
    $(permissionElement).find('input[type=checkbox]').each(function () {
        var roles = $(this).attr('data-roles').split(',');
        if (roles.indexOf($(checkRoleElement).val()) !== -1) {
            $(this).parent().addClass('role-permission');
            $(this).prop('checked', true);
        }
    });
}

function unCheckRole(checkRoleElement, permissionElement) {
    $(permissionElement).find('input[type=checkbox]').each(function () {
        var roles = $(this).attr('data-roles').split(',');
        if (roles.indexOf($(checkRoleElement).val()) !== -1) {
            $(this).parent().removeClass('role-permission');
        }
    });
}