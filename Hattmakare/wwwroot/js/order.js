const cart = {};

$(document).ready(function () {
  let obj = localStorage.getItem("cart") ? JSON.parse(localStorage.getItem("cart")) : {}; // Load cart from local storage")
  Object.entries(obj).forEach(([key, value]) => {
    cart[key] = value;
  })
  updateCart();
  updateHatList();
})

document.querySelectorAll('.hatItem').forEach(hatItem => {
  const plusBtn = hatItem.querySelector('.count-add');
  const minusBtn = hatItem.querySelector('.count-remove');
  const id = hatItem.getAttribute('data-id');

  plusBtn.addEventListener('click', async() => {
    console.log("cart item for id: " + id + " " + cart[id])
    if (!cart[id]){
      //Fetch hatdetails
      await addHatDetails(id);
    }
    if (cart[id]){
      cart[id].Quantity += 1;
      console.log("Increased quantity")
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

function getHatMaterials(id) {
  $.get('/materials/hatMaterials', { hatId: id }, function (data) {
    console.log(data)
});
}

async function addHatDetails(id) {
  let materials = await getHatMaterials(id);

  $.get('/hats/details', { id: id }, function (data) {
    cart[id] =
    {
      Name: data.name,
      Comment: data.comment,
      Depth: data.depth,
      Width: data.width,
      Length: data.length,
      HatTypeId: data.hatTypeId,
      ImageUrl: data.imageUrl,
      Size: data.size,
      Price: data.price,
      Quantity: 1
      
    }
    localStorage.setItem("cart", JSON.stringify(cart));
    updateCart();
});
}

function updateCart() {
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
    if (cart[id]){
      counterElement.textContent = cart[id].Quantity;
    }
  })

  const priceEl = $("#total")
  const total = calculateTotal()
  priceEl.text("Total: " + total + ":-")
  if ($("#cart").children().length === 0) {
    $("#cartContainer").hide(); // or .addClass("d-none") if using Bootstrap
  } else {
    $("#cartContainer").show(); // or .removeClass("d-none")
  }
}

function updateHatList() {
  const itemsEl = $("#orderItems")
  itemsEl.empty();
  //Loop through each hat in the order
  Object.entries(cart).forEach(([k1, value]) => {
    for (let i = 0; i < value.Quantity; i++){
      html = `
      <li class="entry" id="${k1}">
        <details>
          <summary>
            <p>${value.Name}</p>
            <p>Storlek: ${value.Size} </p>
            <p>Längd: ${value.Length} </p>
            <p>Bredd: ${value.Width} </p>
            <p>Djup: ${value.Depth} </p>
             <svg id="remove" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M135.2 17.7C140.6 6.8 151.7 0 163.8 0L284.2 0c12.1 0 23.2 6.8 28.6 17.7L320 32l96 0c17.7 0 32 14.3 32 32s-14.3 32-32 32L32 96C14.3 96 0 81.7 0 64S14.3 32 32 32l96 0 7.2-14.3zM32 128l384 0 0 320c0 35.3-28.7 64-64 64L96 512c-35.3 0-64-28.7-64-64l0-320zm96 64c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16zm96 0c-8.8 0-16 7.2-16 16l0 224c0 8.8 7.2 16 16 16s16-7.2 16-16l0-224c0-8.8-7.2-16-16-16z"/></svg>
            <p id="icon"></p>
          </summary>
          <div class="settings">

          <p> extragrejer </p>
          </div>
        </details>
      </li>
      `;
      itemsEl.append(html)
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
        updateCart()
        updateHatList()
      },
      error: function (xhr, status, error) {
        console.error("POST error", status, error, xhr.responseText);
      }
    });
  });

  $("#orderItems").on("click", "#remove", function (e) {
    const entry = $(this).closest(".entry");
    const id = entry.attr("id");

    entry.remove();
    cart[id].Quantity -= 1;
    updateCart();
  });
});