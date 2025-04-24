const cart = {};

$(document).ready(function () {
  let obj = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : {}; // Load cart from local storage")
  Object.entries(obj).forEach(([key, value]) => {
    cart[key] = value;
  })
  updateCart();
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
      Quantity: (cart[id]?.Quantity || 0) + 1

    }
    updateCart();
    updateHatList();
  });

  minusBtn.addEventListener('click', () => {
    if (cart[id].Quantity > 0) {
      cart[id].Quantity -= 1;
      updateCart();
      updateHatList();
    }
  });
});

function updateCart() {
  localStorage.setItem("cart", JSON.stringify(cart)); // Save cart to local storage
  const cartEl = $("#cart")
  cartEl.empty(); // Clear previous entries
  Object.entries(cart).forEach(([key, value]) => {
    let html = ""
    if (value.Quantity > 0) {
      html = `
      <div class="entry">
        <p>${value.Quantity} x  ${value.Name}</p>
        <p class="size"> Storlek: ${value.Size} </p>
      </div>
      `;
    }

    cartEl.append(html);
  });

  // Update Quantity texts
  document.querySelectorAll('.hatItem').forEach(hatItem => {
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');
    counterElement.textContent = cart[id].Quantity;
  })

  const priceEl = $("#total")
  const total = calculateTotal()
  priceEl.text("Total: " + total + ":-")
}

function updateHatList() {
  console.log("UpdatehatList")
  //Loop through each hat in the order
  Object.entries(cart).forEach(([k1, value]) => {
    console.log(value.Name)
    for (let i = 0; i < value.Quantity; i++){
      console.log(i + " " + value.Name)
    }
  });
}

function calculateTotal() {
  let total = 0;
  Object.entries(cart).forEach(([key, value]) => {
    total += parseInt(value.Price) * parseInt(value.Quantity);
  });
  return total;
}

$(document).on('click', '#order-hat-all', function (e) {
    e.preventDefault();
    let userId = $(this).attr("data-userId");
    $(".order-hats-input").val(userId);
});

$(document).ready(function () {
  console.log("document ready");
  
  $("#SpecialHat").on("click", function (e) {
    console.log("Submitted form");
    e.preventDefault();

    let hat = {
      Name: $("#Name").val(),
      Price: parseFloat($("#Price").val()),
      Quantity: parseInt($("#Quantity").val()),
      Size: parseInt($("#Size").val()),
      Length: parseFloat($("#Length").val()),
      Depth: parseFloat($("#Depth").val()),
      Width: parseFloat($("#Width").val()),
      Comment: $("#Comment").val()
    }

    const formData = new FormData();

    formData.append("Name", hat.Name);
    formData.append("Price", hat.Price);
    formData.append("Quantity", hat.Quantity);
    formData.append("Size", hat.Size);
    formData.append("Length", hat.Length);
    formData.append("Depth", hat.Depth);
    formData.append("Width", hat.Width);
    formData.append("Comment", hat.Comment);

    const imageInput = $("#Image")[0];
    if (imageInput && imageInput.files.length > 0) {
      formData.append("Image", imageInput.files[0]);
    }
    console.log(formData)
    $.ajax({
      url: '/Order/AddSpecialHat',
      method: 'POST',
      processData: false,
      contentType: false,
      data: formData,
      success: function (hatId) {
        console.log("Post completed hatId: " + hatId);
        const temp = {
          Name: hat.Name,
          Price: hat.Price,
          Quantity: hat.Quantity,
          Size: hat.Size,
          Length: hat.Length,
          Depth: hat.Depth,
          Width: hat.Width,
          Comment: hat.Comment
        }
        cart[hatId] = temp
        updateCartItems()
      },
      error: function (xhr, status, error) {
        console.error("POST error", status, error, xhr.responseText);
      }
    });
  });
});