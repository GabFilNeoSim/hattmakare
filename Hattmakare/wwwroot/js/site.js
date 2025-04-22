var confirmCallback = null;

$(document).on("click", ".confirm-btn", function (e) {
    e.preventDefault();
    const button = $(this);
    showConfirmModal(function () {
        button.closest("form").submit();
    });
});

function showConfirmModal(callback) {
    confirmCallback = callback;
    $("#confirm-background").show();
}
function closeConfirmModal() {
    $("#confirm-background").hide();
    confirmCallback = null;
}

$("#confirm-no").on("click", function () {
    closeConfirmModal();
});

$("#confirm-yes").on("click", function (e) {
    e.preventDefault();
    if (typeof confirmCallback === 'function') {
        confirmCallback();
    }
    closeConfirmModal();
});

$("#confirm-wrapper").on("click", function (e) {
    const wrapper = $("#confirm-wrapper")
    if (e.target === $("#confirm-wrapper")[0]) {
        closeConfirmModal();
    }
});