class CartHatItem {
  constructor({id, name, size, length, width, depth, price, quantity, comment, imageUrl, hatTypeId, materials = []}) {
      this.id = id;
      this.name = name;
      this.size = size;
      this.length = length;
      this.width = width;
      this.depth = depth;
      this.price = price;
      this.quantity = quantity;
      this.comment = comment;
      this.imageUrl = imageUrl,
      this.hatTypeId = hatTypeId,
      this.materials = materials.map(m => new HatMaterial(m.materialId, m.quantity, m.name, m.price, m.unit));
  }

  static async fromApi(id) {
    const [details, materials] = await Promise.all([
      $.get('/hats/details', { id }),
      $.get('/materials/hatMaterials', { hatId: id })
    ]);

    const item = new CartHatItem({
      id: id,
      name: details.name,
      size: details.size,
      length: details.length,
      width: details.width,
      depth: details.depth,
      price: details.price,
      quantity: 1,
      comment: details.comment,
      imageUrl: details.imageUrl,
      hatTypeId: details.hatTypeId
    });

    item.materials = materials.map(m => new HatMaterial(m.materialId, m.quantity, m.name, m.price, m.unit));
    return item;
  }

  addMaterial(hatMaterial){
    this.materials.push(hatMaterial)
  }

  removeMaterial(materialId){
    this.materials = this.materials.filter(x => x.materialId != materialId);
  }
} 

class HatMaterial {
  constructor(materialId, quantity, name, price, unit){
    this.materialId = materialId,
    this.quantity = quantity
    this.name = name,
    this.price = price
    this.unit = unit
  }

  changeQuantity(quantity){
    this.quantity = quantity;
  }
}

class Cart {
  constructor() {
      this.items = []
  }

  async addItem(itemId) {
    let item = this.items.find(x => x.id == itemId);
    if (item) {
      item.quantity += 1;
    } else {
      item = await CartHatItem.fromApi(itemId);
      this.items.push(item)
    }
    this.syncToStorage();
  }

  removeItem(itemId) {
    let item = this.items.find(x => x.id == itemId);
    if (item) {
      if (item.quantity > 1) item.quantity -= 1;
      else{
        this.items = this.items.filter(x => x.id !== itemId);
      }
    }
    this.syncToStorage();
  }

  getItem(id) {
    return this.items.find(x => x.id == id);
  }

  syncToStorage() {
    const plain = this.items.map(i => ({ ...i }));
    localStorage.setItem('cart', JSON.stringify(plain));
  }

  loadFromStorage() {
    const saved = JSON.parse(localStorage.getItem('cart') || '{}');
    console.log( Object.values(saved).map(data => new CartHatItem(data)))
    if (typeof saved === 'object' && saved !== null) {
      this.items = Object.values(saved).map(data => new CartHatItem(data));
    } else {
      this.items = [];
    }
  }

  total() {
    return this.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
  }
}

const cart = new Cart();
let availableMaterials = [];

async function loadAvailableMaterials() {
  try {
    const response = await fetch('/Materials/allMaterials');
    if (!response.ok) {
      throw new Error('Failed to fetch materials');
    }
    const data = await response.json();
    availableMaterials = data; // Save the array of materials here
    console.log('Loaded materials:', availableMaterials);
  } catch (error) {
    console.error('Error loading materials:', error);
  }
}

$(document).ready(async function () {
  await loadAvailableMaterials();
  cart.loadFromStorage();
  updateCart(cart.items);
  updateHatList(cart.items);
  $('#hatList').on('click', '.count-add', async function () {
    const id = $(this).closest('.hatItem').data('id');
    await cart.addItem(id);
    updateCart(cart.items);
    updateHatList(cart.items);
  });

  $('#hatList').on('click', '.count-remove', function () {
    const id = $(this).closest('.hatItem').data('id');
    cart.removeItem(id);
    updateCart(cart.items);
    updateHatList(cart.items);
  });
  
  $(document).on("input", ".material-quantity", function (e) {
    const materialId = $(this).attr('data-material-id');
    const itemId = $(this).attr('data-item-id');
    const item = cart.getItem(itemId);
    const material = item.materials.find(m => m.materialId == materialId);
    const val = parseInt($(this).val(), 10);
    // Find the item and the material inside the item
    if (material) {
      material.changeQuantity(val)
      cart.syncToStorage();
      updateCart(cart.items);
    }
  })
  $(document).on("click", ".remove-material", function (e) {
      const materialId = $(this).attr('data-material-id');
      const itemId = $(this).attr('data-item-id');
      // Find the item and the material inside the item
      const item = cart.getItem(itemId);
      if (item) {
        $(this).closest('.material-entry').remove();
        item.removeMaterial(materialId)
        cart.syncToStorage();
        updateCart(cart.items);
      }
  });
  $(document).on("click", ".add-material-btn", function (e) {
  
    const $parent = $(this).closest(".entry");
    const itemId = $(this).data('item-id');
    const $dropdown = $(this).siblings('.material-dropdown');
    const $qtyInput = $(this).siblings('.material-add-quantity');
    
    const selectedMaterialId = $dropdown.val();
    const quantity = parseInt($qtyInput.val(), 10);
    
    if (!selectedMaterialId || isNaN(quantity) || quantity <= 0) {
      alert("Välj ett material och ange en korrekt mängd.");
      return;
    }
  
    const selectedMaterial = availableMaterials.find(m => m.materialId == selectedMaterialId);
    if (!selectedMaterial) {
      alert("Material hittades inte.");
      return;
    }
  
    const item = cart.getItem(itemId);
    if (!item) {
      alert("Objektet hittades inte.");
      return;
    }
    const material = new HatMaterial(
      selectedMaterialId, 
      quantity, 
      selectedMaterial.name, 
      selectedMaterial.price, 
      selectedMaterial.unit
    )
    // Add the material to the item's material list
    item.addMaterial(material)
  
    // Save to localStorage
    cart.syncToStorage();

    //Update UI
    const materialHtml = `
      <div class="material-entry" data-material-id="${selectedMaterialId}">
        <p>Material: ${selectedMaterial.name}</p>
        <input 
          type="number" 
          value="${quantity}" 
          class="material-quantity" 
          data-material-id="${selectedMaterialId}" 
          data-item-id="${itemId}" 
          min="0"
        />
        <button class="remove-material" data-material-id="${selectedMaterialId}" data-item-id="${itemId}">Remove</button>
      </div>
    `;
    $parent.find(".settings").append(materialHtml);
  
    // Re-render cart and list
    updateCart(cart.items);
  });
})

function updateCart(cartItems) {
  const cartEl = $("#cart");
  cartEl.empty();

  cartItems.forEach(item => {
    if (item.quantity > 0) {
      const html = `
        <div class="entry">
          <p>${item.quantity} x ${item.name}</p>
          <p class="size">Storlek: ${item.size}</p>
        </div>
      `;
      cartEl.append(html);
    }
  });

  document.querySelectorAll('.hatItem').forEach(hatItem => {
    const counterElement = hatItem.querySelector('.count');
    const id = hatItem.getAttribute('data-id');
    const cartItem = cart.getItem(id)
    if (cartItem) {
      counterElement.textContent = cartItem.quantity;
    }
    else{
      counterElement.textContent = 0;
    }
  });

  const total = calculateTotal(cartItems);
  $("#total").text("Total: " + total + ":-");

  if (cartItems.length === 0) {
    $("#cartContainer").hide();
  } else {
    $("#cartContainer").show();
  }
}


function updateHatList(cartItems) {
  const itemsEl = $("#orderItems");
  itemsEl.empty();

  cartItems.forEach(item => {
    const html = `
        <li class="entry" id="${item.id}">
          <details>
            <summary>
              <p>${item.name}</p>
              <p>Storlek: ${item.size}</p>
              <p>Längd: ${item.length}</p>
              <p>Bredd: ${item.width}</p>
              <p>Djup: ${item.depth}</p>
              <p> Antal: ${item.quantity} </p>
              <svg id="remove" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                <path d="..."/>
              </svg>
              <p id="icon"></p>
            </summary>

            <div class="settings">
            ${item.materials.map(material => `
              <div class="material-entry" data-material-id="${material.materialId}">
                <p>Material: ${material.name}</p>
                <input 
                  type="number" 
                  value="${material.quantity}" 
                  class="material-quantity" 
                  data-material-id="${material.materialId}" 
                  data-item-id="${item.id}" 
                  min="0"
                />
                <button class="remove-material" data-material-id="${material.materialId}" data-item-id="${item.id}">Remove</button>
              </div>
            `).join('')}
            </div>

            <div class="add-material-form">
              <select class="material-dropdown" data-item-id="${item.id}">
                <option value="">-- Välj material --</option>
                ${availableMaterials.map(mat => `
                  <option value="${mat.materialId}" data-name="${mat.name}" data-unit="${mat.unit}">
                    ${mat.name} (${mat.unit})
                  </option>
                `).join('')}
              </select>
              <input type="number" class="material-add-quantity" placeholder="Mängd" min="1" data-item-id="${item.id}" />
              <button class="add-material-btn" data-item-id="${item.id}">Lägg till material</button>
            </div>
          </details>
        </li>
      `;
      itemsEl.append(html);
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


document.querySelector('select[name="CustomerId"]').addEventListener('change', function () {
    const customerId = this.value;
 

    if (customerId) {
        fetch(`/order/get-customer/${customerId}`)
            .then(res => res.json())
            .then(data => {
               
                document.getElementById('FirstName').value = data.firstName;
                document.getElementById('LastName').value = data.lastName;
                document.getElementById('HeadMeasurements').value = data.headMeasurements;
                document.getElementById('BillingAddress').value = data.billingAddress;
                document.getElementById('DeliveryAddress').value = data.deliveryAddress;
                document.getElementById('City').value = data.city;
                document.getElementById('PostalCode').value = data.postalCode;
                document.getElementById('Country').value = data.country;
                document.getElementById('Email').value = data.email;
                document.getElementById('Phone').value = data.phone;
            });
    } else {
        
        ['FirstName', 'LastName', 'HeadMeasurements', 'BillingAddress', 'DeliveryAddress', 'City', 'PostalCode', 'Country','Email','Phone'].forEach(id => {
            document.getElementById(id).value = '';
        });
    }
});


// Kolla om en kund redan är vald vid sidladdning
window.addEventListener('DOMContentLoaded', function () {
    const customerSelect = document.querySelector('select[name="CustomerId"]');
    const customerId = customerSelect.value;

    if (customerId) {
        fetch(`/order/get-customer/${customerId}`)
            .then(res => res.json())
            .then(data => {
                document.getElementById('FirstName').value = data.firstName;
                document.getElementById('LastName').value = data.lastName;
                document.getElementById('HeadMeasurements').value = data.headMeasurements;
                document.getElementById('BillingAddress').value = data.billingAddress;
                document.getElementById('DeliveryAddress').value = data.deliveryAddress;
                document.getElementById('City').value = data.city;
                document.getElementById('PostalCode').value = data.postalCode;
                document.getElementById('Country').value = data.country;
                document.getElementById('Email').value = data.email;
                document.getElementById('Phone').value = data.phone;

            });
    }
});

