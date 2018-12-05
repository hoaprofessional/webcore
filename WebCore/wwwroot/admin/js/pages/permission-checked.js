/**
 * check quyền cha khi check vào checkbox của quyền con
 * @param {any} currentChecked: checkbox của quyền con
 */
function checkCheckedParent(currentChecked) {
    var parentCheckbox = $(currentChecked).parent().closest('ul').closest('li').find('>.checkbox.checkbox-info>input[type=checkbox]:not(#chk-rootkey)');

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

/** @returns {any}: lấy ra array các quyền được check */
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

/**
 * tạo checkbox cho quyền và nhóm quyền
 * @param {any} roleElement: element cha chứa các element nhóm quyền
 * @param {any} permissionElement: element chả chứa các element quyền
 */
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

/**
 * Xử lý khi check vào 1 nhóm quyền
 * @param {any} checkRoleElement: element nhóm quyền được check
 * @param {any} permissionElement: element cha của những elent quyền được check
 */
function checkRole(checkRoleElement, permissionElement) {
    $(permissionElement).find('input[type=checkbox]').each(function () {
        var roles = $(this).attr('data-roles').split(',');
        if (roles.indexOf($(checkRoleElement).val()) !== -1) {
            $(this).parent().addClass('role-permission');
            $(this).prop('checked', true);
        }
    });
}

/**
 * Xử lý khi uncheck vào 1 nhóm quyền
 * @param {any} checkRoleElement: element nhóm quyền được check
 * @param {any} permissionElement: element cha của những elent quyền được check
 */
function unCheckRole(checkRoleElement, permissionElement) {
    $(permissionElement).find('input[type=checkbox]').each(function () {
        var roles = $(this).attr('data-roles').split(',');
        if (roles.indexOf($(checkRoleElement).val()) !== -1) {
            $(this).parent().removeClass('role-permission');
        }
    });
}