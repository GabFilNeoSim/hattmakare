const cart = {};

$(document).ready(function () {
  let obj = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : {}; // Load cart from local storage")
  Object.entries(obj).forEach(([key, value]) => {
    cart[key] = value;
  })
  updateCartItems();
})

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
    updateCartItems();
  });

  minusBtn.addEventListener('click', () => {
    if (cart[id].Amount > 0) {
      cart[id].Amount -= 1;
      updateCartItems();
    }
  });
});

function updateCartItems() {
  localStorage.setItem("cart", JSON.stringify(cart)); // Save cart to local storage
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

  // Update amount texts
  document.querySelectorAll('.hatItem').forEach(hatItem => {
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');
    counterElement.textContent = cart[id].Amount;
  })

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

//$(document).ready(function () {
//    $("#SpecialHat").on("click", function () {
//        // 1. Collect values from inputs
//        const specialHat = {
//            Name: $("#Name").val(),
//            Comment: $("#Comment").val(),
//            /*ImageUrl: $("#imageUrl").val(),*/
//            Price: parseDecimal($("#Price").val()),
//            Size: parseInt($("#Size").val()),
//            Quantity: parseInt($("#Quantity").val()),
//            Length: parseDouble($("#Length").val()),
//            Width: parseDouble($("#Width").val()),
//            Depth: parseDouble($("#Depth").val())
//        };

//        // 2. POST the hat to the controller
//        $.ajax({
//            url: '/Order/AddSpecialHat',
//            method: 'POST',
//            contentType: 'application/json',
//            data: JSON.stringify(specialHat),
//            success: function () {
//                console.log("Post completed");
//            },
//            error: function (xhr, status, error) {
//                console.error("Error posting hat:", error);
//            }
//        });
//    });
//});

