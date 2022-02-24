$(() => {
    let ddlShopCountry = $("#shop-country");
    let ddlArtists = $("select.artist");
    let otherData = $(".other-data");
    let common = $("#common");

    ddlShopCountry.change(() => {
        let value = ddlShopCountry.val();

        ddlArtists.addClass("d-none");
        ddlArtists.filter(`[data-name="${value}"]`).removeClass("d-none");

        otherData.addClass("d-none");
        otherData.filter(`[data-name="${value}"]`).removeClass("d-none");

        common.addClass("d-none");
        if (value) {
            common.removeClass("d-none");
        }
    });
});
