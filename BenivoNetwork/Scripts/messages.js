$(() => {
    let divProfiles = $("#bn-profiles > div");
    let divMessages = $("#bn-messages");
    let divMessageInputWrapper = $("#bn-message-input-wrapper");
    let btnSend = $("#bn-btn-send");
    let txtMessage = $("#bn-message-text");
    let selectedUserID = 0;

    divProfiles.click(function () {
        let selectedProfile = $(this);

        selectedProfile.siblings().removeClass("bn-active");
        selectedProfile.addClass("bn-active");

        divMessageInputWrapper.removeClass("d-none");

        let id = selectedProfile.attr("data-id");
        selectedUserID = id;

        loadMessages();
    });

    btnSend.click(function () {
        sendMessage();
    });

    txtMessage.keydown(function (e) {
        if (e.keyCode == 13 && !e.shiftKey) {
            if (!txtMessage.val()) {
                e.preventDefault();
                return;
            }

            sendMessage();
        }
    });

    setInterval(() => {
        if (selectedUserID) {
            //TODO: handle new message case
            let lastMessageID = divMessages.children().last().attr("data-id");

            if (lastMessageID) {
                _helpers.callAJAX(`${_messageURL}/${selectedUserID}?lastMessageID=${lastMessageID}`, "GET", null, function (response) {
                    if (response.IsSuccessful) {
                        if (response.Data) {
                            loadMessages();
                        }
                    }
                    else {
                        //TODO: temporary
                        alert(response.Message);
                        //TODO: show in notification
                    }
                });
            }
        }
    }, 5000);

    function sendMessage() {
        //TODO: fix (with notification)
        if (selectedUserID == 0) {
            alert("Plase, select a conversation.");
            return;
        }

        let text = txtMessage.val();
        if (!text) {
            return;
        }

        _helpers.callAJAX(_messageURL, "POST", {
            ToUserID: selectedUserID,
            Text: text
        }, function (response) {
            if (response.IsSuccessful) {
                loadMessages();
                txtMessage.val("");
            }
            else {
                //TODO: show in notification
                alert(response.Message);
            }
        });
    }

    function loadMessages() {
        divMessages.load(`${_messagesPartialURL}/${selectedUserID}`, () => {
            divMessages.animate({ scrollTop: $(divMessages).prop("scrollHeight") }, 500);
        });
    }

    divProfiles.filter(".bn-active").click();
});
