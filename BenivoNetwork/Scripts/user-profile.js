$(() => {
    let btnSendMessage = $("#bn-btn-send-message");
    let btnEdit = $("#bn-btn-edit");
    let btnSave = $("#bn-btn-save");
    let btnCancel = $("#bn-btn-cancel");
    let displayInputs = $(".bn-display-input")
    let editorInputs = $(".bn-editor-input")
    let mainImage = $("#bn-main-pic");

    mainImage.click(function() {
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
            IsMarried: $("#IsMarried").val()
        }, function (response) {
            //TODO: handle response errors
            //TODO: show in notification

            //TODO: temporary
            let id = $("#ID").val();
            location.href = `${_userProfileURL}/${id}`;

            cancelEdit();
        }, function (response) {
            //TODO: handle response errors
            //TODO: change to notification
            alert(response.responseJSON.Message);
        });
    });

    function cancelEdit() {
        btnEdit.removeClass("d-none");
        displayInputs.removeClass("d-none");

        btnSave.addClass("d-none");
        btnCancel.addClass("d-none");
        editorInputs.addClass("d-none");
    }
});
