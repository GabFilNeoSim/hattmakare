const shoppingCart = {};

document.querySelectorAll('.hatItem').forEach(hatItem => {
    const plusBtn = hatItem.querySelector('.count-add');
    const minusBtn = hatItem.querySelector('.count-remove');
    const counterP = hatItem.querySelector('.count');
    const hatName = hatItem.getAttribute('data-name');

    plusBtn.addEventListener('click', () => {
        shoppingCart[hatName] = (shoppingCart[hatName] || 0) + 1;
        counterP.textContent = shoppingCart[hatName];
        console.log(shoppingCart);
    });

    minusBtn.addEventListener('click', () => {
        if (shoppingCart[hatName] > 0) {
            shoppingCart[hatName] -= 1;
            counterP.textContent = shoppingCart[hatName];
            console.log(shoppingCart);
        }
    });
});

$(".editHat").on("click", function (e) {
    console.log("Clicked edit")
    console.log($(this).data("item"))
})

$(".deleteHat").on("click", function (e) {
    console.log("Clicked delete")
    console.log($(this).data("text"))
    e.preventDefault()

})