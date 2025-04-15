const cart = {};

document.querySelectorAll('.hatItem').forEach(hatItem => {
    const plusBtn = hatItem.querySelector('.count-add');
    const minusBtn = hatItem.querySelector('.count-remove');
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');

    plusBtn.addEventListener('click', () => {
        cart[id] = (cart[id] || 0) + 1;
        counterElement.textContent = cart[id];
        console.log(cart);
        updateCartItems();
    });

    minusBtn.addEventListener('click', () => {
        if (cart[id] > 0) {
            cart[id] -= 1;
            counterElement.textContent = cart[id];
            console.log(cart);
        }
    });
});

function updateCartItems() {
    console.log("Updated cart")
    const cartEl = $("#cart")
    cart.forEach(function(k, v) {
        let html = `
        <p>${v} </p>
        `;
        cartEl.append(html)
    })
}

$(".editHat").on("click", function (e) {
    console.log("Clicked edit")
    console.log($(this).data("item"))
})

$(".deleteHat").on("click", function (e) {
    console.log("Clicked delete")
    console.log($(this).data("text"))
    e.preventDefault()

})