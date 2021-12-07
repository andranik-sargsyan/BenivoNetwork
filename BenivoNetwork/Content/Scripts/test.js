$(() => {
    let btn = $("#btn");

    btn.click(function () {
        arr.push({
            name: $("#name").val(),
            age: $("#age").val()
        });

        localStorage.setItem("people", JSON.stringify(arr));
    });
});

let peopleTable = localStorage.getItem("people");
let arr = peopleTable && JSON.parse(peopleTable) || [];
