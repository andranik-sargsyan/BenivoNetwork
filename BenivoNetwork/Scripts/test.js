$(() => {
    let btnPost = $("#btn-post");

    btnPost.click(function () {
        //    callAJAX("http://bnet.com/Home/Test", "POST", {
        //        text: $("#Text").val(),
        //        isImported: $("#IsImported").is(":checked"),
        //        names: ["white", "blue", "red"]
        //    }, function (data) {
        //        console.log(data);
        //    });

        postForm("form", "http://bnet.com/Home/Test");
    });

    
});
