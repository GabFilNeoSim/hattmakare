$(document).on("click", "#show-specials", function (e) {
    const list = $("#special-list.hatList");
    const btn = $("#show-specials");

    if (list.hasClass("show")) {
        btn.html("Dölj specialhattar");
    } else {
        btn.html("Visa specialhattar");
    }
    list.toggleClass("show");
});