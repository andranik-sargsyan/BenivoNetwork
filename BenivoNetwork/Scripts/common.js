$(() => {
    let loader = $("#loader");

    $(document).on({
        ajaxStart: function () {
            loader.removeClass("d-none");
        },
        ajaxStop: function () {
            loader.addClass("d-none");
        }
    });
});

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function callAJAX(url, type, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: type,
        data: data,
        success: successCallback,
        error: errorCallback
    });
}

function postForm(selector, url, successCallback, errorCallback) {
    $.ajax({
        url: url,
        type: "POST",
        data: $($(selector)[0]).serialize(),
        success: successCallback,
        error: errorCallback
    });
}

function postFormWithFile(selector, url, successCallback, errorCallback) {
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