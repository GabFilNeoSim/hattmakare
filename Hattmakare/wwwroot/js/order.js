const cart = {};

$(document).ready(function () {
  let obj = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : {}; // Load cart from local storage")
  Object.entries(obj).forEach(([key, value]) => {
    cart[key] = value;
  })
  updateCartItems();

  $("#addSpecialHat").on("submit", function (e) {
    const hat = {
      Name: $("#Name").val(),
      Price: parseFloat($("#Price").val()),
      Quantity: parseInt($("#Quantity").val()),
      Size: $("#Size").val(),
      Length: parseFloat($("#Length").val()),
      Depth: parseFloat($("#Depth").val()),
      Width: parseFloat($("#Width").val()),
      Comment: $("#Comment").val(),
      ImageUrl: $("#Image").val().split("\\").pop(), // Just filename
      IsDeleted: false,
      IsSpecial: true
    };
    console.log(hat)
    cart[id]
    let savedHats = JSON.parse(localStorage.getItem("cart") || "[]");
        savedHats.push(hat);
        localStorage.setItem("cart", JSON.stringify(savedHats));
  })
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

$(document).ready(function () {
  console.log("document ready")
   $("#SpecialHat").on("click", function (e) {
    console.log("Submitted form")
    e.preventDefault();
       // 1. Collect values from inputs
      //  const specialHat = {
      //      Name: $("#Name").val(),
      //      Comment: $("#Comment").val(),
      //      /*ImageUrl: $("#imageUrl").val(),*/
      //      Price: parseFloat($("#Price").val()),
      //      Size: parseInt($("#Size").val()),
      //      Quantity: parseInt($("#Quantity").val()),
      //      Length: parseFloat($("#Length").val()),
      //      Width: parseFloat($("#Width").val()),
      //      Depth: parseFloat($("#Depth").val())
      //  };
      const formData = new FormData();

      formData.append("Name", $("#Name").val());
      formData.append("Price", $("#Price").val());
      formData.append("Quantity", $("#Quantity").val());
      formData.append("Size", $("#Size").val());
      formData.append("Length", $("#Length").val());
      formData.append("Depth", $("#Depth").val());
      formData.append("Width", $("#Width").val());
      formData.append("Comment", $("#Comment").val());
      // formData.append("Image", imageInput.files[0]);

      // console.log(imageInput)
      // console.log(imageInput.files[0])  

    // if (imageInput && imageInput.files.length > 0) {
    //   formData.append("Image", imageInput.files[0]);
    // }

       // 2. POST the hat to the controller
       $.ajax({
           url: '/Order/AddSpecialHat',
           method: 'POST',
           processType: false,
           contentType: false,
           data: formData,
           success: function () {
               console.log("Post completed");
           },
           error: function (xhr, status, error) {
            console.error("POST error", status, error, xhr.responseText);
          }
       });
   });
});


