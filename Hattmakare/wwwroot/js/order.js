const cart = {};

document.querySelectorAll('.hatItem').forEach(hatItem => {
  const plusBtn = hatItem.querySelector('.count-add');
  const minusBtn = hatItem.querySelector('.count-remove');
  const counterElement = hatItem.querySelector('.count');
  const nameElement = hatItem.querySelector('.name');
  const priceElement = hatItem.querySelector('.price');
  const sizeElement = hatItem.querySelector('.size');
  const id = hatItem.getAttribute('data-id');

  plusBtn.addEventListener('click', () => {
    cart[id] =
    {
      Name: nameElement.textContent,
      Price: parseInt(priceElement.textContent),
      Size: parseInt(sizeElement.textContent),
      Amount: (cart[id]?.Amount || 0) + 1

    }
    // Sets the counterElement value to the amount of cart[id]
    counterElement.textContent = cart[id].Amount;
    console.log(cart);
    updateCartItems();
  });

  minusBtn.addEventListener('click', () => {
    if (cart[id].Amount > 0) {
      cart[id].Amount -= 1;
      counterElement.textContent = cart[id].Amount;
      console.log(cart);
      updateCartItems();
    }
  });
});

function updateCartItems() {
  console.log("Updated cart")
  const cartEl = $("#cart")
  cartEl.empty(); // Clear previous entries
  Object.entries(cart).forEach(([key, value]) => {
    let html = ""
    if (value.Amount > 0) {
      html = `
      <div class="entry">
        <p>${value.Amount} x  ${value.Name}</p>
        <p class="size"> Storlek: ${value.Size} </p>
      </div>
      `;
    }

    cartEl.append(html);
  });
  const priceEl = $("#total")
  const price = calculatePrice()
  priceEl.text("Total: " + price + ":-")
}

function calculatePrice() {
  let price = 0;
  Object.entries(cart).forEach(([key, value]) => {
    price += parseInt(value.Price) * parseInt(value.Amount);
  });
  return price;
}

$(document).on('click', '#order-hat-all', function (e) {
    e.preventDefault();
    let userId = $(this).attr("data-userId");
    $(".order-hats-input").val(userId);
});