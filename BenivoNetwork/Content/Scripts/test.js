$(() => {
    let dogImg = $("#dog-img");
    let btn = $("#btn");
    let randomDogUrl = "https://dog.ceo/api/breeds/image/random";

    btn.click(function () {
        $.ajax({
            url: randomDogUrl,
            type: "GET",
            dataType: "json",
            success: function (result) {
                if (result.status == "success") {
                    dogImg.attr("src", result.message);
                }
                else {
                    alert("Something went wrong... :(");
                }
            }
        });
    });
});
