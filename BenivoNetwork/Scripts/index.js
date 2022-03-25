$(() => {
    let postInput = $("#bn-post-input-wrapper > textarea");

    postInput.keydown(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            let text = postInput.val();

            if (!text) {
                e.preventDefault();
                return;
            }

            _helpers.callAJAX(_postURL, "POST", {
                Text: text
            }, function (response) {
                if (response.IsSuccessful) {
                    location.reload();
                }
                else {
                    //TODO: show in notification
                    alert(response.Message);
                }
            });
        }
    });
});