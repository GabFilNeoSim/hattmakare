$(".editHat").on("click", function (e) {
    console.log("Clicked edit")
    console.log($(this).data("item"))
})

$(".deleteHat").on("click", function (e) {
    console.log("Clicked delete")
    console.log($(this).data("text"))
    e.preventDefault()

})