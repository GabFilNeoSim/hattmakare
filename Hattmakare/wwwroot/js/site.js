//$(".deleteHat").on("click", function (event) {
//    event.preventDefault();
//    const text = $(this).data("text");
//    currentConfirmationForm = $(this).parent().attr("class");
//    currentFormDataId = $(this).parent().data("id");

//    showConfirmModal(text);
//});

//$("#confirm-yes").on("click", function (event) {
//    event.preventDefault();
//    console.log("Confirmed")
//    console.log(currentConfirmationForm);
//    $(`.${currentConfirmationForm}[data-id="${currentFormDataId}"]`).submit();
//});

//function showConfirmModal(text) {
//    $("#confirm-background").show();
//    $("#confirm-text").html(text);
//}

//function closeConfirmModal() {
//    $("#confirm-background").hide();
//    currentConfirmationForm = "";
//}

//$("#confirm-no").on("click", function () {
//    closeConfirmModal();
//});