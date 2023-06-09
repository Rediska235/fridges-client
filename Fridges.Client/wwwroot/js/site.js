﻿showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                    location.reload();
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                    $('#give-roles').html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = (action, objectType) => {
    let res = confirm('Are you sure to delete this ' + objectType + '?');
    if (res) {
        try {
            $.ajax({
                type: 'POST',
                url: action,
                success: function (html) {
                    location.reload();
                    console.log(action);
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }
}