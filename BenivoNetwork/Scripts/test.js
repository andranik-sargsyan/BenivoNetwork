$(() => {
    let txtName = $("#txt-name");
    let btnLoad = $("#btn-load");
    let btnPost = $("#btn-post");

    btnLoad.click(function () {
        _helpers.callAJAX("http://bnet.com/Home/GetUsers", "GET", { term: txtName.val() }, function (data) {
            console.log(data);
        });
    });

    btnPost.click(function (e) {
        e.preventDefault();

        _helpers.postForm("form", "http://bnet.com/Home/Test", function (data) {
            console.log(data);
        });
    });
});
