﻿$(() => {
    let divLoader = $("#bn-loader");
    let txtSearch = $("#bn-search");
    let btnLogout = $("#bn-logout");

    //AJAX loading animation
    $(document).on({
        ajaxStart: function () {
            divLoader.removeClass("d-none");
        },
        ajaxStop: function () {
            divLoader.addClass("d-none");
        }
    });

    //Search
    txtSearch.keydown(function (e) {
        var term = txtSearch.val();
        if (term.length < 2 || e.keyCode != 13) {
            return;
        }

        window.location.href = `${_searchURL}?term=${term}`;
    });

    //Logout
    btnLogout.click(function (e) {
        e.preventDefault();

        _helpers.callAJAX(_logoutURL, "POST", undefined, function (data) {
            if (data == "OK") {
                //TODO: use common response
                location.href = _welcomeURL;
            }
        });
    });
        
    //Date pickers
    $("input.bn-date-input").datepicker({
        dateFormat: "dd/mm/yy"
    });
});

let _helpers = {
    getRandomInt: (min, max) => {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    },
    callAJAX: (url, type, data, successCallback, errorCallback) => {
        $.ajax({
            url: url,
            type: type,
            data: data,
            success: successCallback,
            error: errorCallback
        });
    },
    postForm: (selector, url, successCallback, errorCallback) => {
        $.ajax({
            url: url,
            type: "POST",
            data: $($(selector)[0]).serialize(),
            success: successCallback,
            error: errorCallback
        });
    },
    postFormWithFile: (selector, url, successCallback, errorCallback) => {
        var formData = new FormData($(selector)[0]);
        $.ajax({
            url: url,
            type: "POST",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: successCallback,
            error: errorCallback
        });
    }
};