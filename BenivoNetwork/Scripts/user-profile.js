$(() => {
    let btnSendMessage = $("#bn-btn-send-message");
    let btnFriendAction = $("#bn-btn-friend-action");
    let btnFriendActionCancel = $("#bn-btn-friend-action-cancel");
    let btnEdit = $("#bn-btn-edit");
    let btnSave = $("#bn-btn-save");
    let btnCancel = $("#bn-btn-cancel");
    let btnUploadImage = $("#bn-btn-upload-img");
    let btnRemoveImage = $("#bn-btn-remove-img");
    let displayInputs = $(".bn-display-input")
    let editorInputs = $(".bn-editor-input")
    let mainImage = $("#bn-main-pic");

    mainImage.click(function () {
        let url = $(this)
            .css('background-image')
            .replace('url(', '')
            .replace(')', '')
            .replace(/\"/gi, "");

        _helpers.openURL(url);
    });

    btnSendMessage.click(() => {
        let id = $("#ID").val();
        location.href = `${_messagesURL}/${id}`;
    });

    btnFriendAction.click(() => {
        let status = btnFriendAction.attr("data-status");

        switch (status) {
            case "NotFriends":
                _helpers.callAJAX(_friendshipURL, "POST", {
                    id: $("#ID").val()
                }, response => {
                    //TODO: fix
                    location.reload();
                });
                break;
            case "RequestSent":
                deleteRequest();
                break;
            case "RequestReceived":
                _helpers.callAJAX(_friendshipURL, "PUT", {
                    id: $("#ID").val()
                }, response => {
                    //TODO: fix
                    location.reload();
                });
                break;
            case "Friends":
                deleteRequest();
                break;
            default:
                break;
        }
    });

    btnFriendActionCancel.click(() => {
        deleteRequest();
    });

    btnEdit.click(() => {
        btnEdit.addClass("d-none");
        displayInputs.addClass("d-none");

        btnSave.removeClass("d-none");
        btnCancel.removeClass("d-none");
        editorInputs.removeClass("d-none");
    });

    btnCancel.click(() => {
        cancelEdit();
    });

    btnSave.click(() => {
        var dateParts = $("#DateOfBirth").val().split("/");
        var dateOfBirthISO = dateParts.length == 3 ? `${dateParts[2]}-${dateParts[1]}-${dateParts[0]}` : "";

        _helpers.callAJAX(_userURL, "PUT", {
            ID: $("#ID").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            UserName: $("#UserName").val(),
            Role: $("#Role").val(),
            Gender: $("#Gender").val(),
            DateOfBirth: dateOfBirthISO,
            IsMarried: $("#IsMarried").val(),
            ImageURL: $("#ImageURL").val()
        }, response => {
            if (response.IsSuccessful) {
                //TODO: temporary
                let id = $("#ID").val();
                location.href = `${_userProfileURL}/${id}`;
            }
            else {
                //TODO: show in notification
                alert(response.Message);
            }
        });
    });

    btnUploadImage.click(() => {
        var formData = new FormData();
        var file = $("#ImageFile")[0].files[0];

        if (!file) {
            //TODO: show in notification
            alert("No file chosen.");
            return;
        }

        formData.append("ImageFile", file, file && file.name);
        formData.append("ID", $("#ID").val());

        $.ajax({
            url: _imageURL,
            type: "POST",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: response => {
                if (response.IsSuccessful) {
                    //TODO: temporary
                    location.reload();
                }
                else {
                    //TODO: show in notification
                    alert(response.Message);
                }
            }
        });
    });

    btnRemoveImage.click(() => {
        _helpers.callAJAX(`${_imageURL}/${$("#ID").val()}`, "DELETE", null, response => {
            if (response.IsSuccessful) {
                //TODO: temporary
                location.reload();
            }
            else {
                //TODO: show in notification
                alert(response.Message);
            }
        })
    });

    function cancelEdit() {
        btnEdit.removeClass("d-none");
        displayInputs.removeClass("d-none");

        btnSave.addClass("d-none");
        btnCancel.addClass("d-none");
        editorInputs.addClass("d-none");
    }

    function deleteRequest() {
        _helpers.callAJAX(`${_friendshipURL}/${$("#ID").val()}`, "DELETE", null, response => {
            //TODO: fix
            location.reload();
        });
    }
});
