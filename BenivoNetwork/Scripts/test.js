$(() => {
    let canvas = document.getElementById("canvas");
    let ctx = canvas.getContext("2d");

    let startAngle = 0;
    let endAngle = Math.PI / 360;

    ctx.lineWidth = 1;

    setInterval(function () {
        update();
        draw();
    }, 10);

    function update() {
        startAngle += 1 / 72;
        endAngle += 1 / 36;
    }

    function draw() {
        ctx.clearRect(0, 0, canvas.width, canvas.height)

        ctx.beginPath();
        ctx.arc(200, 200, 20, startAngle, endAngle);
        ctx.stroke();
    }
});
