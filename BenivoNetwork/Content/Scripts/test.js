$(() => {
    let currentId = 8;
    let table = $("#my-table > tbody");
    let usernameInput = $("#username");
    let ageInput = $("#age");
    let editRow = undefined;

    //Add
    $("#my-btn").click(() => {
        let username = usernameInput.val().trim();
        if (!username) {
            alert("Invalid username");
            return;
        }

        let age = ageInput.val().trim();
        if (!age) {
            alert("Invalid age");
            return;
        }

        if (!editRow) {
            let row = $("<tr>");

            //1
            row.append(`<td>${currentId}</td>`);

            //2
            row.append(`<td>${username}</td>`);

            //3
            row.append(`<td>${age}</td>`);

            //4
            let actionData = $("<td>")
            let removeInput = $('<input type="button" class="btn btn-danger bn-remove" value="X" />');

            removeInput.click(function () {
                $(this).closest("tr").remove();
            });

            actionData.append(removeInput);
            row.append(actionData);

            table.append(row);

            currentId++;
        }
        else {
            let usernameData = editRow.children(".td-username");
            let ageData = editRow.children(".td-age");

            usernameData.text(usernameInput.val());
            ageData.text(ageInput.val());

            editRow.removeClass("bg-warning");
            editRow = undefined;
        }
    });

    //Remove
    $(".bn-remove").click(function () {
        $(this).closest("tr").remove();
    });

    //Edit
    $(".bn-edit").click(function () {
        editRow && editRow.removeClass("bg-warning");

        let row = $(this).closest("tr");

        let usernameData = row.children(".td-username");
        let ageData = row.children(".td-age");

        usernameInput.val(usernameData.text());
        ageInput.val(ageData.text());

        editRow = row;
        editRow.addClass("bg-warning");
    });
});
