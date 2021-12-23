$(() => {
    let div = $("#my-div");

    $.ajax({
        url: "https://date.nager.at/api/v2/publicholidays/2020/US",
        type: "GET",
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                div.append(`<div>${data[i].date} - ${data[i].localName}</div>`);
            }
        }
    });
});
